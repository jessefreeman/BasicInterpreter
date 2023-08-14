using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Evaluators;

public class VariableExpression : IExpression
{
    private readonly string _variableName;
    private readonly Dictionary<string, object> _variables;

    public VariableExpression(string variableName, Dictionary<string, object> variables)
    {
        _variableName = variableName;
        _variables = variables;
    }

    public object Evaluate(params object[] operands)
    {
        if (_variables.TryGetValue(_variableName, out var value))
            return value;
        throw new InterpreterException(BasicInterpreterError.VariableNotDefined);
    }
}