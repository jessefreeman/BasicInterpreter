using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LogicalOrExpression class represents a logical OR operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LogicalOrExpression : IExpression
    {
        /// <summary>
        /// Evaluates the logical OR of the left operand and the right operand.
        /// </summary>
        /// <param name="operands">The operands of the expression.</param>
        /// <returns>True if either the left operand or the right operand is true, false otherwise.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there are exactly two operands.
            if (operands.Length != 2)
            {
                throw new ArgumentException("LogicalOrExpression requires exactly two operands.");
            }

            // Retrieve the operands.
            var left = operands[0];
            var right = operands[1];

            // Convert both operands to boolean and perform a logical OR operation.
            if (bool.TryParse(left?.ToString(), out bool leftBool) && bool.TryParse(right?.ToString(), out bool rightBool))
            {
                return leftBool || rightBool;
            }

            // If the conversion fails, throw an ArgumentException.
            throw new ArgumentException("Invalid operand types for LogicalOrExpression. Both operands must be convertible to boolean.");
        }
    }
}
