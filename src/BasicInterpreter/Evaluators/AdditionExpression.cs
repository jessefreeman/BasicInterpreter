namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The AdditionExpression class represents an addition operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class AdditionExpression : IExpression
{
    public object Evaluate(params object[] operands)
    {
        if (operands == null || operands.Length == 0)
            throw new ArgumentException("At least one operand is required for addition.");

        // Convert all operands to double and perform the addition.
        var result = operands.Select(o => Convert.ToDouble(o)).Sum();
        return result;
    }
}