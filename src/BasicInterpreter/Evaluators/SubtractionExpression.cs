namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The SubtractionExpression class represents a subtraction operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class SubtractionExpression : IExpression
{
    /// <summary>
    ///     Evaluates the subtraction of the right operand from the left operand.
    /// </summary>
    /// <param name="operands">The operands of the expression (left and right).</param>
    /// <returns>The result of subtracting the right operand from the left operand.</returns>
    public object Evaluate(params object[] operands)
    {
        // Check if there are exactly two operands.
        if (operands.Length != 2) throw new ArgumentException("SubtractionExpression requires exactly two operands.");

        // Retrieve the left and right operands and convert them to doubles.
        var left = Convert.ToDouble(operands[0]);
        var right = Convert.ToDouble(operands[1]);

        // Perform the subtraction operation on the operands.
        return left - right;
    }
}