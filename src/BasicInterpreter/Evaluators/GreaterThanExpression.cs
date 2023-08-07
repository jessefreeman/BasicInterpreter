using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The GreaterThanExpression class represents a greater than operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class GreaterThanExpression : IExpression
    {
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length != 2)
                throw new ArgumentException("GreaterThanExpression requires exactly two operands.");

            var left = operands[0];
            var right = operands[1];

            if (left == null || right == null || !(left is double || left is int) || !(right is double || right is int))
                throw new ArgumentException("GreaterThanExpression requires valid numeric operands.");

            // Convert both operands to double and compare them as doubles.
            return Convert.ToDouble(left) > Convert.ToDouble(right);
        }
    }

}
