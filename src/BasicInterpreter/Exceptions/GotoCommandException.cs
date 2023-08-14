namespace JesseFreeman.BasicInterpreter.Exceptions;

public class GotoCommandException : Exception
{
    public GotoCommandException(int targetLineNumber)
    {
        TargetLineNumber = targetLineNumber;
    }

    public int TargetLineNumber { get; }
}