using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The SinExpression class represents a sine function operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class SinExpression : IExpression
    {
        /// <summary>
        /// Evaluates the sine function of the operand.
        /// </summary>
        /// <param name="operands">The operand of the expression.</param>
        /// <returns>The sine function result of the operand as a double.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there is exactly one operand.
            if (operands.Length != 1)
            {
                throw new ArgumentException("SinExpression requires exactly one operand.");
            }

            // Retrieve the operand and convert it to double.
            var operand = operands[0];
            double value = Convert.ToDouble(operand);

            // Perform the sine function on the operand.
            return Math.Sin(value);
        }
    }
}
