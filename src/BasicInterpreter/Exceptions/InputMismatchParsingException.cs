using System;
using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class InputMismatchParsingException : ParsingException
    {
        public InputMismatchParsingException(string message, InputMismatchException innerException)
            : base(message, innerException)
        {
        }
    }

}

