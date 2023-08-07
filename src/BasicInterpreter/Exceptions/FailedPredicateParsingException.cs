using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class FailedPredicateParsingException : ParsingException
    {
        public FailedPredicateParsingException(string errorContext, FailedPredicateException innerException)
            : base($"Failed predicate occurred in context: '{errorContext}'.", innerException)
        {
        }
    }

}

