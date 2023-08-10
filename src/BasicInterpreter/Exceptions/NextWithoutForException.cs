using System;
namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class NextWithoutForException : Exception
    {
        public NextWithoutForException(string message) : base(message) { }
    }
}

