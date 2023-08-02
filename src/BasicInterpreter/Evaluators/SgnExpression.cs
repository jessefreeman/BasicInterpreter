using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The SgnExpression class represents a sign function operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class SgnExpression : IExpression
    {
        /// <summary>
        /// Evaluates the sign function of the operand.
        /// </summary>
        /// <param name="operands">The operand of the expression.</param>
        /// <returns>The sign function result of the operand as an integer (-1, 0, or 1).</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there is exactly one operand.
            if (operands.Length != 1)
            {
                throw new ArgumentException("SgnExpression requires exactly one operand.");
            }

            // Retrieve the operand and convert it to double.
            var operand = operands[0];
            double value = Convert.ToDouble(operand);

            // Perform the sign function on the operand.
            return Math.Sign(value);
        }
    }
}
