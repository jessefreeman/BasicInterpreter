using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The ExpExpression class represents an exponential operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class ExpExpression : IExpression
    {
        /// <summary>
        /// Evaluates the exponential of the operand.
        /// </summary>
        /// <param name="operands">The operands. The first operand is the value for the exponential operation.</param>
        /// <returns>The result of the exponential operation.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length != 1)
                throw new ArgumentException("ExpExpression requires exactly one operand.");

            var operand = operands[0];

            // Ensure that the operand is a valid number.
            if (operand == null || !(operand is double || operand is int))
                throw new ArgumentException("ExpExpression requires a valid numeric operand.");

            return Math.Exp(Convert.ToDouble(operand));
        }
    }
}
