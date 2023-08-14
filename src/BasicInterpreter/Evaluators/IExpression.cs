namespace JesseFreeman.BasicInterpreter.Evaluators;

/// <summary>
///     The IExpression interface defines a method for evaluating an expression.
///     An expression in this context is a binary operation with two operands.
///     This interface can be implemented by any class that needs to evaluate expressions,
///     such as a class that evaluates addition expressions, subtraction expressions, etc.
/// </summary>
public interface IExpression
{
    /// <summary>
    ///     Evaluates the expression with the given operands.
    /// </summary>
    /// <param name="operands">The operands of the expression.</param>
    /// <returns>The result of evaluating the expression.</returns>
    object Evaluate(params object[] operands);
}