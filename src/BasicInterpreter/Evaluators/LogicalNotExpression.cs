using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The LogicalNotExpression class represents a logical NOT operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class LogicalNotExpression : IExpression
    {
        public object Evaluate(params object[] operands)
        {
            if (operands.Length != 1)
            {
                throw new ArgumentException("LogicalNotExpression requires exactly one operand.");
            }

            var operand = operands[0];

            if (operand is double operandDouble)
            {
                // Treat 0.0 as false and 1.0 as true
                return operandDouble == 0.0 ? 1.0 : 0.0;
            }
            else
            {
                throw new ArgumentException("Invalid operand type for LogicalNotExpression. Expected double.");
            }
        }
    }

}
