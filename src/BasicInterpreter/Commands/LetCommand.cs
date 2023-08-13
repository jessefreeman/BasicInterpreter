﻿using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands;
public class LetCommand : ICommand
{
    private string _variableName;
    private BasicParser.ExpressionContext _expressionContext;
    private ExpressionEvaluator _expressionEvaluator;
    private Dictionary<string, object> _variables;

    public LetCommand(string variableName, BasicParser.ExpressionContext expressionContext, ExpressionEvaluator expressionEvaluator, Dictionary<string, object> variables)
    {
        _variableName = variableName;
        _expressionContext = expressionContext ?? throw new InterpreterException(BasicInterpreterError.ParsingError);
        _expressionEvaluator = expressionEvaluator;
        _variables = variables;
    }

    public void Execute()
    {
        IExpression expression = _expressionEvaluator.Visit(_expressionContext);
        object value = expression.Evaluate();

        // Check if the variable is already defined
        if (_variables.TryGetValue(_variableName, out var existingValue))
        {
            // Check for type mismatch
            if (existingValue.GetType() != value.GetType())
            {
                throw new InterpreterException(BasicInterpreterError.InvalidTypeAssignment);
            }
        }

        // Check if the variable name ends with $, indicating a string variable
        if (_variableName.EndsWith("$") && !(value is string))
        {
            throw new InterpreterException(BasicInterpreterError.InvalidTypeAssignment);
        }

        _variables[_variableName] = value;
    }
}


