using System;
namespace JesseFreeman.BasicInterpreter.Evaluators
{
    public class LambdaExpression : IExpression
    {
        private IExpression[] operands;
        private IExpression operation;

        public LambdaExpression(IExpression operation, IExpression[] operands)
        {
            this.operation = operation;
            this.operands = operands;
        }

        public object Evaluate(params object[] operands)
        {
            // Evaluate all the operands immediately
            object[] evaluatedOperands = this.operands.Select(o => o.Evaluate()).ToArray();

            // Perform the operation with the evaluated operands
            return operation.Evaluate(evaluatedOperands);
        }
    }


}

