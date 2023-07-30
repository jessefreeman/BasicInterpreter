using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class FailedPredicateParsingException : ParsingException
    {
        public FailedPredicateParsingException(string message, FailedPredicateException innerException)
            : base(message, innerException)
        {
        }
    }

}

