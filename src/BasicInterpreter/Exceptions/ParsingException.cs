using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string message, RecognitionException innerException)
            : base(message, innerException)
        {
        }
    }

}

