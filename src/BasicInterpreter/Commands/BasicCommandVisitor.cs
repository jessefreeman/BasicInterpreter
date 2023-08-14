using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using Jessefreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands;

public class BasicCommandVisitor : BasicBaseVisitor<ICommand>
{
    private readonly ExpressionEvaluator expressionEvaluator;
    private readonly IInputReader inputReader;
    private readonly BasicInterpreter interpreter;
    private readonly Dictionary<string, object> variables;
    private readonly IOutputWriter writer;

    public BasicCommandVisitor(BasicInterpreter interpreter, Dictionary<string, object> variables, IOutputWriter writer,
        IInputReader inputReader)
    {
        this.interpreter = interpreter;
        this.variables = variables;
        this.writer = writer;
        this.inputReader = inputReader;
        expressionEvaluator = new ExpressionEvaluator(variables);
    }

    public override ICommand VisitProg(BasicParser.ProgContext context)
    {
        // Process each line in the program
        var commands = new List<ICommand>();
        foreach (var line in context.line())
        {
            var currentLineNumber = int.Parse(line.linenumber().GetText());
            interpreter.SetCurrentLineNumber(currentLineNumber); // Set the current line number on the interpreter state
            var command = Visit(line);
            commands.Add(command);
        }

        // Return a CompositeCommand that holds all commands for this program
        return new CompositeCommand(commands);
    }

    public override ICommand VisitLine(BasicParser.LineContext context)
    {
        var currentLineNumber = int.Parse(context.linenumber().GetText());
        interpreter.SetCurrentLineNumber(currentLineNumber); // Set the current line number on the interpreter state

        // Process each statement on the line
        var commands = new List<ICommand>();

        // Check if context corresponds to a comment
        if (context.COMMENT() != null || context.REM() != null)
            // This line is a comment, so we can return a 'no operation' command or null
            // Here's an example of returning null
            return null;

        foreach (var amprstmt in context.amprstmt())
        foreach (var statement in amprstmt.statement())
        {
            var command = Visit(statement);
            if (command == null) throw new InvalidOperationException("Visit method returned null for a statement");
            commands.Add(command);
        }

        // Return a CompositeCommand that holds all commands for this line
        return new CompositeCommand(commands);
    }

    public override ICommand VisitPrintstmt1(BasicParser.Printstmt1Context context)
    {
        var expressionContexts = context.exprlist()?.expression().ToList() ?? new List<BasicParser.ExpressionContext>();
        return new PrintCommand(expressionContexts, expressionEvaluator, writer);
    }


    public override ICommand VisitLetstmt(BasicParser.LetstmtContext context)
    {
        var variableAssignment = context.variableassignment();
        string variableName;
        var expressionContext = variableAssignment.exprlist().expression(0);

        if (variableAssignment.vardecl() != null)
            variableName = variableAssignment.vardecl().var_().GetText();
        else if (variableAssignment.stringVarDecl() != null)
            variableName = variableAssignment.stringVarDecl().var_().GetText() + "$";
        else
            // "Neither vardecl nor stringVarDecl is matched"
            throw new InterpreterException(BasicInterpreterError.ParsingError);

        return new LetCommand(variableName, expressionContext, expressionEvaluator, variables);
    }

    public override ICommand VisitEndstmt([NotNull] BasicParser.EndstmtContext context)
    {
        return new EndCommand(interpreter);
    }

    public override ICommand VisitGotostmt([NotNull] BasicParser.GotostmtContext context)
    {
        var targetLineNumber =
            int.Parse(context.linenumber().GetText()); // parse the target line number from the context
        return new GotoCommand(targetLineNumber);
    }

    public override ICommand VisitIfstmt([NotNull] BasicParser.IfstmtContext context)
    {
        // Visit the expression context to create the IExpression
        var condition = expressionEvaluator.Visit(context.expression());

        var thenLineNumber = -1;
        ICommand thenCommand = null;

        // Check if the THEN branch is a statement or a line number
        if (context.statement() != null)
            // If it's a statement, visit the statement context to create the ICommand
            thenCommand = Visit(context.statement());
        else if (context.linenumber() != null)
            // If it's a line number, parse it to set thenLineNumber
            thenLineNumber = int.Parse(context.linenumber().GetText());

        // Create and return the IfCommand with the condition, thenLineNumber, and thenCommand
        return new IfCommand(condition, thenLineNumber, thenCommand);
    }

    public override ICommand VisitForstmt1([NotNull] BasicParser.Forstmt1Context context)
    {
        var variableName = context.vardecl()[0].var_().varname().GetText();
        var startExpression = expressionEvaluator.Visit(context.expression(0));
        var endExpression = expressionEvaluator.Visit(context.expression(1));
        var stepExpression = context.expression(2) != null ? expressionEvaluator.Visit(context.expression(2)) : null;

        return new ForCommand(variableName, startExpression, endExpression, stepExpression, interpreter);
    }

    public override ICommand VisitNextstmt([NotNull] BasicParser.NextstmtContext context)
    {
        // Get the variable names from the context
        var variableNames = context.vardecl()?.Select(v => v.var_().GetText()).ToList() ?? new List<string>();

        // Create a NextCommand object with the list of variable names
        return new NextCommand(interpreter, variableNames);
    }

    public override ICommand VisitInputstmt([NotNull] BasicParser.InputstmtContext context)
    {
        var variableNames = context.varlist().vardecl().Select(v => v.var_().GetText()).ToList();
        return new InputCommand(variableNames, inputReader);
    }
}