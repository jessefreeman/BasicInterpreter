using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Jessefreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands;

public class BasicCommandVisitor : BasicBaseVisitor<ICommand>
{
    private Dictionary<string, object> variables;
    private IOutputWriter writer;
    private IInputReader inputReader;
    private ExpressionEvaluator expressionEvaluator;
    private BasicInterpreter interpreter;

    public BasicCommandVisitor(BasicInterpreter interpreter, Dictionary<string, object> variables, IOutputWriter writer, IInputReader inputReader)
    {
        this.interpreter = interpreter;
        this.variables = variables;
        this.writer = writer;
        this.inputReader = inputReader;
        this.expressionEvaluator = new ExpressionEvaluator(variables);
    }
    
    public override ICommand VisitProg(BasicParser.ProgContext context)
    {
        // Process each line in the program
        var commands = new List<ICommand>();
        foreach (var line in context.line())
        {
            int currentLineNumber = int.Parse(line.linenumber().GetText());
            interpreter.SetCurrentLineNumber(currentLineNumber); // Set the current line number on the interpreter state
            var command = Visit(line);
            commands.Add(command);
        }

        // Return a CompositeCommand that holds all commands for this program
        return new CompositeCommand(commands);
    }

    public override ICommand VisitLine(BasicParser.LineContext context)
    {
        int currentLineNumber = int.Parse(context.linenumber().GetText());
        interpreter.SetCurrentLineNumber(currentLineNumber); // Set the current line number on the interpreter state

        // Process each statement on the line
        var commands = new List<ICommand>();

        // Check if context corresponds to a comment
        if (context.COMMENT() != null || context.REM() != null)
        {
            // This line is a comment, so we can return a 'no operation' command or null
            // Here's an example of returning null
            return null;
        }

        foreach (var amprstmt in context.amprstmt())
        {
            foreach (var statement in amprstmt.statement())
            {
                var command = Visit(statement);
                if (command == null)
                {
                    throw new InvalidOperationException("Visit method returned null for a statement");
                }
                commands.Add(command);
            }
        }

        // Return a CompositeCommand that holds all commands for this line
        return new CompositeCommand(commands);
    }

    public override ICommand VisitPrintstmt1(BasicParser.Printstmt1Context context)
    {
        var expressionContexts = context.exprlist()?.expression().ToList() ?? new List<BasicParser.ExpressionContext>();
        return new PrintCommand(expressionContexts, this.expressionEvaluator, writer);
    }


    public override ICommand VisitLetstmt(BasicParser.LetstmtContext context)
    {
        string variableName = context.variableassignment().vardecl().var_().GetText();
        var expressionContext = context.variableassignment().exprlist().expression(0);
        return new LetCommand(variableName, expressionContext, this.expressionEvaluator, this.variables);
    }

    public override ICommand VisitEndstmt([NotNull] BasicParser.EndstmtContext context)
    {
        return new EndCommand(interpreter);
    }

    public override ICommand VisitGotostmt([NotNull] BasicParser.GotostmtContext context)
    {
        int targetLineNumber = int.Parse(context.linenumber().GetText());  // parse the target line number from the context
        return new GotoCommand(targetLineNumber);
    }

    public override ICommand VisitIfstmt([NotNull] BasicParser.IfstmtContext context)
    {
        // Visit the expression context to create the IExpression
        IExpression condition = expressionEvaluator.Visit(context.expression());

        int thenLineNumber = -1;
        ICommand thenCommand = null;

        // Check if the THEN branch is a statement or a line number
        if (context.statement() != null)
        {
            // If it's a statement, visit the statement context to create the ICommand
            thenCommand = Visit(context.statement());
        }
        else if (context.linenumber() != null)
        {
            // If it's a line number, parse it to set thenLineNumber
            thenLineNumber = int.Parse(context.linenumber().GetText());
        }

        // Create and return the IfCommand with the condition, thenLineNumber, and thenCommand
        return new IfCommand(condition, thenLineNumber, thenCommand);
    }

    public override ICommand VisitForstmt1([NotNull] BasicParser.Forstmt1Context context)
    {
        string variableName = context.vardecl()[0].var_().varname().GetText(); // Extracting the variable name
        IExpression startExpression = expressionEvaluator.Visit(context.expression(0));
        IExpression endExpression = expressionEvaluator.Visit(context.expression(1));
        IExpression stepExpression = context.expression(2) != null ? expressionEvaluator.Visit(context.expression(2)) : null;

        return new ForCommand(variableName, startExpression, endExpression, stepExpression, interpreter);
    }

    public override ICommand VisitNextstmt([NotNull] BasicParser.NextstmtContext context)
    {
        var variableNames = context.vardecl()?.Select(v => v.var_().GetText()).ToList() ?? new List<string>();
        var commands = variableNames.Select(v => new NextCommand(interpreter, v)).ToList<ICommand>();
        return new CompositeCommand(commands);
    }

    public override ICommand VisitInputstmt([NotNull] BasicParser.InputstmtContext context)
    {
        var variableNames = context.varlist().vardecl().Select(v => v.var_().GetText()).ToList();
        return new InputCommand(variableNames, inputReader);
    }

}

