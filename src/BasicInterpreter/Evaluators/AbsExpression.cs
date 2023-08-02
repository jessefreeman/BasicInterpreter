namespace JesseFreeman.BasicInterpreter.Evaluators
{
    public class AbsExpression : IExpression
    {
        public object Evaluate(params object[] operands)
        {
            if (operands == null || operands.Length == 0)
            {
                throw new ArgumentException("At least one operand is required for absolute value.");
            }

            if (operands.Length > 1)
            {
                throw new ArgumentException("The absolute value expression only supports one operand.");
            }

            return Math.Abs(Convert.ToDouble(operands[0]));
        }
    }
}
