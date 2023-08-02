namespace JesseFreeman.BasicInterpreter.Evaluators
{
    public class NumberExpression : IExpression
    {
        private readonly object _value;

        public NumberExpression(object value)
        {
            _value = value;
        }

        public object Evaluate(params object[] operands)
        {
            return _value;
        }
    }
}

