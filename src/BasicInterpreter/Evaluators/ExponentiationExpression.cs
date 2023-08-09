using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The ExpExpression class represents an exponential operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class ExponentiationExpression : IExpression
    {
        public object Evaluate(params object[] operands)
        {
            if (operands == null || (operands.Length != 1 && operands.Length != 2))
                throw new ArgumentException("ExponentiationExpression requires exactly one or two operands.");

            if (operands.Length == 1)
            {
                var operand = operands[0];
                if (operand == null || !(operand is double || operand is int))
                    throw new ArgumentException("ExponentiationExpression requires a valid numeric operand.");

                return Math.Exp(Convert.ToDouble(operand));
            }
            else
            {
                var left = Convert.ToDouble(operands[0]);
                var right = Convert.ToDouble(operands[1]);
                return Math.Pow(left, right);
            }
        }
    }


}
