namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The IntExpression class represents an integer operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class IntExpression : IExpression
{
    /// <summary>
    ///     Evaluates the integer part of the operand.
    /// </summary>
    /// <param name="operands">The operand to evaluate.</param>
    /// <returns>The integer part of the operand as a double.</returns>
    public object Evaluate(params object[] operands)
    {
        // Check if there is exactly one operand provided.
        if (operands.Length != 1) throw new ArgumentException("IntExpression requires exactly one operand.");

        // Get the operand from the array.
        var operand = operands[0];

        // Check if the operand is of type double.
        if (operand is double doubleValue)
            // Convert the double value to an integer and then back to a double to get the integer part.
            return (double)(int)doubleValue;
        throw new ArgumentException("Invalid operand type for IntExpression. Expected double.");
    }
}