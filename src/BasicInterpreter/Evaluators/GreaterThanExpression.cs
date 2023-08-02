using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The GreaterThanExpression class represents a greater than operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class GreaterThanExpression : IExpression
    {
        /// <summary>
        /// Evaluates whether the left operand is greater than the right operand.
        /// </summary>
        /// <param name="operands">The operands. The first operand is the left value, and the second operand is the right value.</param>
        /// <returns>True if the left operand is greater than the right operand, false otherwise.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length != 2)
                throw new ArgumentException("GreaterThanExpression requires exactly two operands.");

            var left = operands[0];
            var right = operands[1];

            // Ensure that both operands are valid numbers.
            if (left == null || right == null || !(left is double || left is int) || !(right is double || right is int))
                throw new ArgumentException("GreaterThanExpression requires valid numeric operands.");

            // If both operands are integers, compare them as integers.
            if (left is int leftInt && right is int rightInt)
            {
                return leftInt > rightInt;
            }
            else
            {
                // If either operand is not an integer, convert both operands to double and compare them as doubles.
                return Convert.ToDouble(left) > Convert.ToDouble(right);
            }
        }
    }
}
