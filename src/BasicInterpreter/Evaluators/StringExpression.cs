using System;
namespace JesseFreeman.BasicInterpreter.Evaluators
{
    public class StringExpression : IExpression
    {
        private readonly object _value;

        public StringExpression(object value)
        {
            _value = value;
        }

        public object Evaluate(params object[] operands)
        {
            return _value;
        }
    }
}

