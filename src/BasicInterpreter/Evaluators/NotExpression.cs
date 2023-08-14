namespace JesseFreeman.BasicInterpreter.Evaluators;

public class NotExpression : IExpression
{
    /// <summary>
    ///     Evaluates the logical NOT of the operand.
    /// </summary>
    /// <param name="operands">The operand of the expression.</param>
    /// <returns>The result of applying the logical NOT to the operand.</returns>
    public object Evaluate(params object[] operands)
    {
        // Check if there is exactly one operand.
        if (operands.Length != 1) throw new ArgumentException("NotExpression requires exactly one operand.");

        // Retrieve the operand and convert it to a boolean.
        var operand = Convert.ToBoolean(operands[0]);

        // Perform the logical NOT operation on the operand.
        return !operand;
    }
}