namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The MultiplicationExpression class represents a multiplication operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class MultiplicationExpression : IExpression
    {
        /// <summary>
        /// Evaluates the multiplication of the operands.
        /// </summary>
        /// <param name="operands">The operands of the expression.</param>
        /// <returns>The result of multiplying the operands.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if there are exactly two operands.
            if (operands.Length != 2)
            {
                throw new ArgumentException("MultiplicationExpression requires exactly two operands.");
            }

            // Retrieve the operands.
            var left = Convert.ToDouble(operands[0]);
            var right = Convert.ToDouble(operands[1]);

            // Multiply the operands as doubles.
            return left * right;
        }
    }
}
