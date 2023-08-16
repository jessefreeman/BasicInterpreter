namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The EqualExpression class represents an equality operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class EqualExpression : IExpression
{
    /// <summary>
    ///     Evaluates whether the left operand is equal to the right operand.
    /// </summary>
    /// <param name="operands">The operands of the expression (should contain two elements).</param>
    /// <returns>True if the left operand is equal to the right operand, false otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown when the number of operands is not exactly two.</exception>
    public object Evaluate(params object[] operands)
    {
        if (operands == null || operands.Length != 2)
            throw new ArgumentException("Exactly two operands are required for EqualExpression.");

        // If both operands are integers, compare them as integers.
        if (operands[0] is int leftInt && operands[1] is int rightInt) return leftInt == rightInt;

        // If either operand is not an integer, convert both operands to double and compare them as doubles.
        var leftOperand = Convert.ToDouble(operands[0]);
        var rightOperand = Convert.ToDouble(operands[1]);
        return Math.Abs(leftOperand - rightOperand) < double.Epsilon;
    }
}