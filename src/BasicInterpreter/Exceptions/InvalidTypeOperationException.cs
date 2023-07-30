namespace JesseFreeman.BasicInterpreter.Exceptions
{
    /// <summary>
    /// The InvalidTypeOperationException class represents an exception that is thrown when an invalid operation is performed on a type.
    /// It extends the base Exception class.
    /// </summary>
    public class InvalidTypeOperationException : Exception
    {
        /// <summary>
        /// Constructs a new InvalidTypeOperationException with no message or inner exception.
        /// </summary>
        public InvalidTypeOperationException()
        {
        }

        /// <summary>
        /// Constructs a new InvalidTypeOperationException with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidTypeOperationException(string message)
            // Call the base constructor with the error message.
            : base(message)
        {
        }

        /// <summary>
        /// Constructs a new InvalidTypeOperationException with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidTypeOperationException(string message, Exception inner)
            // Call the base constructor with the error message and the inner exception.
            : base(message, inner)
        {
        }
    }
}
