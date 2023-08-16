#region

using Antlr4.Runtime;

#endregion

namespace JesseFreeman.BasicInterpreter.Exceptions;

public class ThrowingErrorStrategy : DefaultErrorStrategy
{
    private readonly BasicInterpreter _interpreter;

    // private readonly Dictionary<int, int> physicalToBasicLineNumbers;

    public ThrowingErrorStrategy(BasicInterpreter interpreter)
    {
        _interpreter = interpreter;
    }

    public override void Recover(Parser recognizer, RecognitionException e)
    {
        var physicalLineNumber = e.OffendingToken.Line; // No subtraction needed
        _interpreter.SetCurrentLineNumber(_interpreter.PhysicalToBasicLineNumbers[physicalLineNumber]);
        throw new InterpreterException(BasicInterpreterError.ParsingError);
    }

    protected override void ReportInputMismatch(Parser recognizer, InputMismatchException e)
    {
        var physicalLineNumber = e.OffendingToken.Line; // No subtraction needed
        _interpreter.SetCurrentLineNumber(_interpreter.PhysicalToBasicLineNumbers[physicalLineNumber]);
        throw new InterpreterException(BasicInterpreterError.ParsingError);
    }

    protected override void ReportFailedPredicate(Parser recognizer, FailedPredicateException e)
    {
        var physicalLineNumber = e.OffendingToken.Line; // No subtraction needed
        _interpreter.SetCurrentLineNumber(_interpreter.PhysicalToBasicLineNumbers[physicalLineNumber]);
        throw new InterpreterException(BasicInterpreterError.ParsingError);
    }

    // Override any other methods as needed...
}