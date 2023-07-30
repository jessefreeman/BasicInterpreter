using Antlr4.Runtime;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class ThrowingErrorStrategy : DefaultErrorStrategy
    {
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            throw new ParsingException("Failed to recover from a parsing error", e);
        }

        protected override void ReportInputMismatch(Parser recognizer, InputMismatchException e)
        {
            throw new InputMismatchParsingException("Input mismatch error", e);
        }

        protected override void ReportFailedPredicate(Parser recognizer, FailedPredicateException e)
        {
            throw new FailedPredicateParsingException("Failed predicate error", e);
        }

        // Override any other methods as needed...
    }

}

