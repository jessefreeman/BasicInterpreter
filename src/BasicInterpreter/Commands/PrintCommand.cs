using System.Text;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class PrintCommand : ICommand
    {
        private List<BasicParser.ExpressionContext> _expressionContexts;
        private ExpressionEvaluator _expressionEvaluator;
        private IOutputWriter _writer;

        public PrintCommand(List<BasicParser.ExpressionContext> expressionContexts, ExpressionEvaluator expressionEvaluator, IOutputWriter writer)
        {
            _expressionContexts = expressionContexts;
            _expressionEvaluator = expressionEvaluator;
            _writer = writer;
        }

        public void Execute()
        {
            StringBuilder output = new StringBuilder();

            foreach (var expressionContext in _expressionContexts)
            {
                IExpression expression = _expressionEvaluator.Visit(expressionContext);
                object result = expression.Evaluate();
                string value;

                if (result is double number)
                {
                    value = FormatNumber(number);
                }
                else
                {
                    value = result.ToString();
                }

                output.Append(value);
                output.Append(" "); // Separate values with spaces
            }

            _writer.WriteLine(output.ToString().TrimEnd(' ')); // Write the final output string, removing trailing space
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


}
