namespace JesseFreeman.BasicInterpreter.Exceptions;

public class SkipNextCommandException : Exception
{
    public SkipNextCommandException() : base("Skip the next command due to a false IF condition.")
    {
    }
}