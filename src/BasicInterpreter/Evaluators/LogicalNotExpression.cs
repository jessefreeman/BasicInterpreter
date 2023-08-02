using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LogicalNotExpression class represents a logical NOT operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LogicalNotExpression : IExpression
    {
        /// <summary>
        /// Evaluates the logical NOT of the operand.
        /// </summary>
        /// <param name="operands">The operand of the expression.</param>
        /// <returns>The logical NOT of the operand.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there is exactly one operand.
            if (operands.Length != 1)
            {
                throw new ArgumentException("LogicalNotExpression requires exactly one operand.");
            }

            // Retrieve the operand.
            var operand = operands[0];

            // Convert the operand to boolean and perform a logical NOT operation.
            if (bool.TryParse(operand?.ToString(), out bool boolValue))
            {
                return !boolValue;
            }

            // If the conversion fails, throw an ArgumentException.
            throw new ArgumentException("Invalid operand type for LogicalNotExpression. Expected boolean.");
        }
    }
}
