#region

using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.IO;

#endregion

namespace JesseFreeman.BasicInterpreter.Commands;

public class PrintCommand : ICommand
{
    private readonly List<BasicParser.ExpressionContext> _expressionContexts;
    private readonly ExpressionEvaluator _expressionEvaluator;
    private readonly List<char> _separators;
    private readonly IOutputWriter _writer;

    public PrintCommand(List<BasicParser.ExpressionContext> expressionContexts, List<char> separators,
        ExpressionEvaluator expressionEvaluator, IOutputWriter writer)
    {
        _expressionContexts = expressionContexts;
        _separators = separators;
        _expressionEvaluator = expressionEvaluator;
        _writer = writer;
    }

    public void Execute()
    {
        for (var i = 0; i < _expressionContexts.Count; i++)
        {
            var expression = _expressionEvaluator.Visit(_expressionContexts[i]);
            var result = expression.Evaluate();
            string value;

            if (result is double number)
                value = FormatNumber(number);
            else
                value = result.ToString();

            _writer.Write(value);

            if (i < _separators.Count)
            {
                var separator = _separators[i];
                _writer.WriteSeparator(separator); // Use the writer's method to handle separators
            }
        }

        _writer.NewLine(); // Write a new line after printing
    }

    private string FormatNumber(double number)
    {
        if (number == Math.Truncate(number))
            // The number is a whole number, so print it without a decimal point
            return ((int) number).ToString();
        return number.ToString();
    }
}