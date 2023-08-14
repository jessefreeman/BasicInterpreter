namespace JesseFreeman.BasicInterpreter.Exceptions;

public class GosubCommandException : Exception
{
    public GosubCommandException(int targetLineNumber)
    {
        TargetLineNumber = targetLineNumber;
    }

    public int TargetLineNumber { get; }
}