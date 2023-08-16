namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The LogicalAndExpression class represents a logical AND operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class LogicalAndExpression : IExpression
{
    public object Evaluate(params object[] operands)
    {
        if (operands.Length != 2) throw new ArgumentException("LogicalAndExpression requires exactly two operands.");

        var left = operands[0];
        var right = operands[1];

        if (left is bool leftBool && right is bool rightBool) return leftBool && rightBool;

        if (left is double leftDouble && right is double rightDouble)
        {
            // Convert doubles to integers for bitwise AND operation
            var leftInt = (int) leftDouble;
            var rightInt = (int) rightDouble;
            // Return a boolean indicating whether the result is non-zero
            return (leftInt & rightInt) != 0;
        }

        throw new ArgumentException(
            "Operands must be either both booleans or both doubles for the logical AND operation.");
    }
}