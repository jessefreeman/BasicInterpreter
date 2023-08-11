namespace JesseFreeman.BasicInterpreter.Exceptions;
public class InterpreterException : Exception
{
    public BasicInterpreterError ErrorType { get; }

    public InterpreterException(BasicInterpreterError errorType)
    {
        ErrorType = errorType;
    }
}