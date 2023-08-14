namespace JesseFreeman.BasicInterpreter.Exceptions;

public class InterpreterException : Exception
{
    public InterpreterException(BasicInterpreterError errorType)
    {
        ErrorType = errorType;
    }

    public BasicInterpreterError ErrorType { get; }
}