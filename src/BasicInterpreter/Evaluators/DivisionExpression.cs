using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The DivisionExpression class represents a division operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class DivisionExpression : IExpression
    {
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length != 2)
            {
                throw new ArgumentException("Exactly two operands are required for DivisionExpression.");
            }

            var leftOperand = Convert.ToDouble(operands[0]);
            var rightOperand = Convert.ToDouble(operands[1]);

            // If the right operand is zero, throw a DivideByZeroException.
            if (Math.Abs(rightOperand) < double.Epsilon)
            {
                throw new DivideByZeroException("Division by zero is not allowed");
            }

            // If both operands are integers, divide them as integers but return the result as a double.
            if (operands[0] is int leftInt && operands[1] is int rightInt)
            {
                return (double)leftInt / rightInt;
            }
            else
            {
                // If either operand is not an integer, convert both operands to double and divide them as doubles.
                return leftOperand / rightOperand;
            }
        }
    }
}
