namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The NegationExpression class represents a negation operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class NegationExpression : IExpression
    {
        /// <summary>
        /// Evaluates the negation of the operand.
        /// </summary>
        /// <param name="operands">The operand of the expression.</param>
        /// <returns>The negation of the operand.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there is exactly one operand.
            if (operands.Length != 1)
            {
                throw new ArgumentException("NegationExpression requires exactly one operand.");
            }

            // Retrieve the operand.
            var operand = operands[0];

            // If the operand is an integer, negate it as an integer.
            if (operand is int)
            {
                return -(int)operand;
            }
            else
            {
                // If the operand is not an integer, convert it to double and negate it.
                return -Convert.ToDouble(operand);
            }
        }
    }
}
