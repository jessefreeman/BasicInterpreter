namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LogExpression class represents a logarithm operation.
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LogExpression : IExpression
    {
        /// <summary>
        /// Evaluates the logarithm of the operand.
        /// </summary>
        /// <param name="operands">The operand to evaluate.</param>
        /// <returns>The logarithm of the operand as a double.</returns>
        public object Evaluate(params object[] operands)
        {
            // Check if the number of operands is exactly one.
            if (operands.Length != 1)
            {
                throw new ArgumentException("LogExpression requires exactly one operand.");
            }

            // Retrieve the operand.
            var operand = operands[0];

            // If the operand is a double, calculate the logarithm and return it.
            if (operand is double doubleValue)
            {
                return Math.Log(doubleValue);
            }
            else
            {
                // Throw an exception if the operand is not of type double.
                throw new ArgumentException("Invalid operand type for LogExpression. Expected double.");
            }
        }
    }
}
