using System;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Evaluators;
public class VariableExpression : IExpression
{
    private string _variableName;
    private Dictionary<string, object> _variables;

    public VariableExpression(string variableName, Dictionary<string, object> variables)
    {
        _variableName = variableName;
        _variables = variables;
    }

    public object Evaluate(params object[] operands)
    {
        if (_variables.TryGetValue(_variableName, out var value))
        {
            return value;
        }
        else
        {
            throw new InterpreterException(BasicInterpreterError.VariableNotDefined);
        }
    }
}



