using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class BasicCommandVisitor : BasicBaseVisitor<ICommand>
    {
        private Dictionary<string, object> variables;
        private IOutputWriter writer;
        private IInputReader inputReader;
        private BasicInterpreter interpreter;

        public BasicCommandVisitor(BasicInterpreter interpreter, Dictionary<string, object> variables, IOutputWriter writer, IInputReader inputReader)
        {
            this.interpreter = interpreter;
            this.variables = variables;
            this.writer = writer;
            this.inputReader = inputReader;
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


        // Add methods to visit other types of nodes in the parse tree
        // based on the new grammar rules
        public override ICommand VisitPrintstmt1(BasicParser.Printstmt1Context context)
        {
            // Check if there's an expression within the PRINT statement
            if (context.expression() != null)
            {
                var expressionContext = context.expression();

                // Check if the expression is a string literal
                if (expressionContext.func_() != null && expressionContext.func_().STRINGLITERAL() != null)
                {
                    string text = expressionContext.func_().STRINGLITERAL().GetText();
                    // Remove the surrounding quotes from the string literal
                    text = text.Substring(1, text.Length - 2);
                    return new PrintCommand(text, writer);
                }
                // Check if the expression is a numeric literal
                else if (expressionContext.func_() != null && expressionContext.func_().number() != null)
                {
                    string text = expressionContext.func_().number().GetText();
                    return new PrintCommand(text, writer);
                }
                else
                {
                    throw new NotImplementedException("Only static strings and numbers are currently supported in PRINT statements");
                }
            }
            else
            {
                throw new ParsingException("PRINT statement must contain an expression");
            }
        }


        public override ICommand VisitEndstmt(BasicParser.EndstmtContext context)
        {
            return new EndCommand(interpreter);
        }

        public override ICommand VisitGotostmt(BasicParser.GotostmtContext context)
        {
            // Parse the line number from the GOTO statement
            int targetLineNumber = int.Parse(context.linenumber().GetText());

            // Create a new GotoCommand with the target line number
            return new GotoCommand(targetLineNumber);
        }

        //public override ICommand VisitGosubstmt(BasicParser.GosubstmtContext context)
        //{
        //    int targetLineNumber = int.Parse(context.number().GetText());
        //    return new GosubCommand(targetLineNumber);
        //}

        //public override ICommand VisitReturnstmt(BasicParser.ReturnstmtContext context)
        //{
        //    return new ReturnCommand();
        //}





    }
}
