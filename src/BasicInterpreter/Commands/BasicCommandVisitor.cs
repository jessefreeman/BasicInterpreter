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

        public class PrintCommand : ICommand
        {
            private BasicParser.ExpressionContext _expressionContext;
            private ExpressionEvaluator _expressionEvaluator;
            private IOutputWriter _writer;

            public PrintCommand(BasicParser.ExpressionContext expressionContext, ExpressionEvaluator expressionEvaluator, IOutputWriter writer)
            {
                _expressionContext = expressionContext;
                _expressionEvaluator = expressionEvaluator;
                _writer = writer;
            }

            public void Execute()
            {
                IExpression expression = _expressionEvaluator.Visit(_expressionContext);
                object result = expression.Evaluate();
                string output;

                if (result is double number)
                {
                    // The result is a number, so apply the formatting logic
                    output = FormatNumber(number);
                }
                else
                {
                    // The result is not a number, so print it as is
                    output = result.ToString();
                }

                _writer.WriteLine(output); // Write the final output string
            }

            private string FormatNumber(double number)
            {
                if (number == Math.Truncate(number))
                {
                    // The number is a whole number, so print it without a decimal point
                    return ((int)number).ToString();
                }
                else
                {
                    // The number is not a whole number, so print it with a decimal point
                    return number.ToString();
                }
            }
        }

        public override ICommand VisitPrintstmt1(BasicParser.Printstmt1Context context)
        {
            return new PrintCommand(context.expression(), this.expressionEvaluator, writer);
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
            var variableNames = context.vardecl()?.Select(v => v.var_().GetText()).ToList(); // Retrieve the variable names if provided
            return new NextCommand(interpreter, variableNames);
        }

    }
}
