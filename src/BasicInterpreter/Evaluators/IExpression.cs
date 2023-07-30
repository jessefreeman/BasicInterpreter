namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The IExpression interface defines a method for evaluating an expression.
    /// An expression in this context is a binary operation with two operands.
    /// This interface can be implemented by any class that needs to evaluate expressions,
    /// such as a class that evaluates addition expressions, subtraction expressions, etc.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <param name="left">The left operand of the expression.</param>
        /// <param name="right">The right operand of the expression.</param>
        /// <returns>The result of evaluating the expression.</returns>
        object Evaluate(object left, object right);
    }
}
