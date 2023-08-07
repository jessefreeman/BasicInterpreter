using System;
namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string operation)
            : base($"Operation '{operation}' is not supported.")
        {
        }

        public UnsupportedOperationException(string operation, Exception inner)
            : base($"Operation '{operation}' is not supported.", inner)
        {
        }
    }

}

