﻿#region

using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The DivisionExpression class represents a division operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class DivisionExpression : IExpression
{
    public object Evaluate(params object[] operands)
    {
        if (operands == null || operands.Length != 2)
            throw new ArgumentException("Exactly two operands are required for DivisionExpression.");

        var leftOperand = Convert.ToDouble(operands[0]);
        var rightOperand = Convert.ToDouble(operands[1]);

        // If the right operand is zero, throw an InterpreterException with the DivisionByZero error type.
        if (Math.Abs(rightOperand) < double.Epsilon)
            throw new InterpreterException(BasicInterpreterError.DivisionByZero);

        // If both operands are integers, divide them as integers but return the result as a double.
        if (operands[0] is int leftInt && operands[1] is int rightInt)
            return (double) leftInt / rightInt;
        return leftOperand / rightOperand;
    }
}