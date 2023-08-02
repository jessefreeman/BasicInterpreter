using JesseFreeman.BasicInterpreter.Evaluators;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class LetCommand : ICommand
    {
        private string _variableName;
        private IExpression _expression;
        private Dictionary<string, object> _variables;

        public LetCommand(string variableName, IExpression expression, Dictionary<string, object> variables)
        {
            _variableName = variableName;
            _expression = expression;
            _variables = variables;
        }

        public void Execute()
        {
            // Check if the variable is not yet defined and assign a default value
            if (!_variables.ContainsKey(_variableName))
            {
                // The variable is not yet defined, so we initialize it with a default value
                if (_variableName.EndsWith("$"))
                {
                    // Variable names that end with '$' are strings
                    _variables[_variableName] = "";
                }
                else
                {
                    // Other variables are numbers
                    _variables[_variableName] = 0.0;
                }
            }

            object value = _expression.Evaluate();
            _variables[_variableName] = value;
        }

    }
}
