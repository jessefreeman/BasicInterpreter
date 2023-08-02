using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The CosExpression class represents a cosine operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class CosExpression : IExpression
    {
        /// <summary>
        /// Evaluates the cosine of the specified angle.
        /// </summary>
        /// <param name="operands">The operands of the expression (should contain one element).</param>
        /// <returns>The cosine of the angle as a double.</returns>
        /// <exception cref="ArgumentException">Thrown when the number of operands is not exactly one.</exception>
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length != 1)
            {
                throw new ArgumentException("Exactly one operand is required for CosExpression.");
            }

            return Math.Cos(Convert.ToDouble(operands[0]));
        }
    }
}
