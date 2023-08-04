using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class LetCommand : ICommand
    {
        private string _variableName;
        private BasicParser.ExpressionContext _expressionContext;
        private ExpressionEvaluator _expressionEvaluator;
        private Dictionary<string, object> _variables;

        public LetCommand(string variableName, BasicParser.ExpressionContext expressionContext, ExpressionEvaluator expressionEvaluator, Dictionary<string, object> variables)
        {
            _variableName = variableName;
            _expressionContext = expressionContext;
            _expressionEvaluator = expressionEvaluator;
            _variables = variables;
        }

        public void Execute()
        {
            IExpression expression = _expressionEvaluator.Visit(_expressionContext);
            object value = expression.Evaluate();
            _variables[_variableName] = value;

            // Log the execution of the command
            Console.WriteLine($"Executed LET command: {_variableName} = {value}");
        }
    }

}
