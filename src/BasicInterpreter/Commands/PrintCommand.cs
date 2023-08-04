using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
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
            Console.WriteLine($"Printing result of expression: {output}");

            _writer.WriteLine(output);
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
