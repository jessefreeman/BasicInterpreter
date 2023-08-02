using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The SqrExpression class represents a square root operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class SqrExpression : IExpression
    {
        /// <summary>
        /// Evaluates the square root of the operand.
        /// </summary>
        /// <param name="operands">The operand of the expression.</param>
        /// <returns>The square root of the operand as a double.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there is exactly one operand.
            if (operands.Length != 1)
            {
                throw new ArgumentException("SqrExpression requires exactly one operand.");
            }

            // Retrieve the operand and convert it to double.
            var operand = operands[0];
            double value = Convert.ToDouble(operand);

            // Perform the square root operation on the operand.
            return Math.Sqrt(value);
        }
    }
}
