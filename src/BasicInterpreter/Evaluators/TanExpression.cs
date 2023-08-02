using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The TanExpression class represents a tangent operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class TanExpression : IExpression
    {
        /// <summary>
        /// Evaluates the tangent of the operand.
        /// </summary>
        /// <param name="operands">The operand to evaluate.</param>
        /// <returns>The tangent of the operand as a double.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands.Length != 1)
            {
                throw new ArgumentException("TanExpression requires exactly one operand.");
            }

            var operand = operands[0];

            if (operand is double doubleValue)
            {
                return Math.Tan(doubleValue);
            }
            else
            {
                throw new ArgumentException("Invalid operand type for TanExpression. Expected double.");
            }
        }
    }
}
