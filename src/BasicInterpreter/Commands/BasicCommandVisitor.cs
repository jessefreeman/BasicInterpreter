using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
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
                var command = Visit(line);
                commands.Add(command);
            }

            // Return a CompositeCommand that holds all commands for this program
            return new CompositeCommand(commands);
        }

        public override ICommand VisitLine(BasicParser.LineContext context)
        {
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
                var statement = amprstmt.statement();
                if (statement != null)
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
            if (context.expression() != null)
            {
                var expressionContext = context.expression();
                return new PrintCommand(expressionContext, this.expressionEvaluator, writer);
            }
            else
            {
                throw new ParsingException("PRINT statement must contain an expression");
            }
        }

        public override ICommand VisitLetstmt(BasicParser.LetstmtContext context)
        {
            string variableName = context.variableassignment().vardecl().var_().GetText();
            var expressionContext = context.variableassignment().exprlist().expression(0);
            IExpression expression = this.expressionEvaluator.Visit(expressionContext);
            return new LetCommand(variableName, expression, variables);
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

    }
}
