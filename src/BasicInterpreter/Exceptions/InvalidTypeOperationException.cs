namespace JesseFreeman.BasicInterpreter.Exceptions
{
    /// <summary>
    /// The InvalidTypeOperationException class represents an exception that is thrown when an invalid operation is performed on a type.
    /// It extends the base Exception class.
    /// </summary>
    public class InvalidTypeOperationException : Exception
    {
        public InvalidTypeOperationException(string operation)
            : base($"Invalid type operation: '{operation}'.")
        {
        }

        public InvalidTypeOperationException(string operation, Exception inner)
            : base($"Invalid type operation: '{operation}'.", inner)
        {
        }
    }
}
