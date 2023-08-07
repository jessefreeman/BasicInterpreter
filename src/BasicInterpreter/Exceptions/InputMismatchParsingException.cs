using System;
using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class InputMismatchParsingException : ParsingException
    {
        public InputMismatchParsingException(string errorContext, InputMismatchException innerException)
            : base($"Input mismatch occurred in context: '{errorContext}'.", innerException)
        {
        }
    }

}

