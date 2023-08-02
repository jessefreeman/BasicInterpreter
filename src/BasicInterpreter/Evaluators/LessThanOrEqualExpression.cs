namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LessThanOrEqualExpression class represents a less than or equal operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LessThanOrEqualExpression : IExpression
    {
        /// <summary>
        /// Evaluates whether the left operand is less than or equal to the right operand.
        /// </summary>
        /// <param name="operands">The operands of the expression.</param>
        /// <returns>True if the left operand is less than or equal to the right operand, false otherwise.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands.Length != 2)
            {
                throw new ArgumentException("LessThanOrEqualExpression requires exactly two operands.");
            }

            var left = operands[0];
            var right = operands[1];

            // If both operands are integers, compare them as integers.
            if (left is int leftInt && right is int rightInt)
            {
                return leftInt <= rightInt;
            }
            else
            {
                // If either operand is not an integer, convert both operands to double and compare them as doubles.
                return Convert.ToDouble(left) <= Convert.ToDouble(right);
            }
        }
    }
}
