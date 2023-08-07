
namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string errorContext)
            : base($"Parsing error occurred in context: '{errorContext}'.")
        {
        }

        public ParsingException(string errorContext, Exception innerException)
            : base($"Parsing error occurred in context: '{errorContext}'.", innerException)
        {
        }
    }

}

