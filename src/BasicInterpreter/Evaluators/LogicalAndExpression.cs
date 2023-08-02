using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LogicalAndExpression class represents a logical AND operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LogicalAndExpression : IExpression
    {
        /// <summary>
        /// Evaluates the logical AND of the left operand and the right operand.
        /// </summary>
        /// <param name="operands">The operands of the expression.</param>
        /// <returns>The result of the logical AND operation on the left and right operands.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there are exactly two operands.
            if (operands.Length != 2)
            {
                throw new ArgumentException("LogicalAndExpression requires exactly two operands.");
            }

            // Retrieve the operands.
            var left = operands[0];
            var right = operands[1];

            // If both operands are booleans, perform a logical AND operation.
            if (left is bool leftBool && right is bool rightBool)
            {
                return leftBool && rightBool;
            }
            else
            {
                throw new ArgumentException("Both operands must be booleans for the logical AND operation.");
            }
        }
    }
}
