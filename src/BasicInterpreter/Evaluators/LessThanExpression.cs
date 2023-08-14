namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The LessThanExpression class represents a less than operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class LessThanExpression : IExpression
{
    /// <summary>
    ///     Evaluates whether the left operand is less than the right operand.
    /// </summary>
    /// <param name="operands">The operands of the expression.</param>
    /// <returns>True if the left operand is less than the right operand, false otherwise.</returns>
    public object Evaluate(params object[] operands)
    {
        if (operands.Length != 2) throw new ArgumentException("LessThanExpression requires exactly two operands.");

        var left = (double)operands[0];
        var right = (double)operands[1];

        return left < right;
    }
}