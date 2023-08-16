namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The LogicalOrExpression class represents a logical OR operation.
///     It implements the IExpression interface, which defines a method for evaluating an expression.
/// </summary>
public class LogicalOrExpression : IExpression
{
    public object Evaluate(params object[] operands)
    {
        if (operands.Length != 2) throw new ArgumentException("LogicalOrExpression requires exactly two operands.");

        var left = operands[0];
        var right = operands[1];

        if (left is bool leftBool && right is bool rightBool)
            return leftBool || rightBool;
        if (left is double leftDouble && right is double rightDouble)
            // Convert doubles to integers for bitwise OR operation
            // Return a boolean indicating whether the result is non-zero
            return ((int) leftDouble | (int) rightDouble) != 0;
        throw new ArgumentException(
            "Invalid operand types for LogicalOrExpression. Both operands must be either boolean or double.");
    }
}