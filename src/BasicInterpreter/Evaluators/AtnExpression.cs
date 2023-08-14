namespace JesseFreeman.BasicInterpreter.Evaluators;

public class AtnExpression : IExpression
{
    public object Evaluate(params object[] operands)
    {
        if (operands == null || operands.Length != 1)
            throw new ArgumentException("A single operand is required for AtnExpression.");

        return Math.Atan(Convert.ToDouble(operands[0]));
    }
}