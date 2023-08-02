using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The NotEqualExpression class represents a not equal operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class NotEqualExpression : IExpression
    {
        /// <summary>
        /// Evaluates whether the left operand is not equal to the right operand.
        /// </summary>
        /// <param name="operands">The operands of the expression.</param>
        /// <returns>True if the left operand is not equal to the right operand, false otherwise.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands.Length != 2)
            {
                throw new ArgumentException("NotEqualExpression requires exactly two operands.");
            }

            var left = operands[0];
            var right = operands[1];

            if (left is string leftString && right is string rightString)
            {
                return !string.Equals(leftString, rightString);
            }
            else
            {
                return !Convert.ToDouble(left).Equals(Convert.ToDouble(right));
            }
        }
    }
}
