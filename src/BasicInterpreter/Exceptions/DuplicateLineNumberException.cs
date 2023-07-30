namespace JesseFreeman.BasicInterpreter.Exceptions
{
    /// <summary>
    /// The DuplicateLineNumberException class represents an exception that is thrown when a duplicate line number is encountered.
    /// It extends the base Exception class.
    /// </summary>
    public class DuplicateLineNumberException : Exception
    {
        /// <summary>
        /// Constructs a new DuplicateLineNumberException.
        /// </summary>
        /// <param name="lineNumber">The duplicate line number.</param>
        public DuplicateLineNumberException(int lineNumber)
            // Call the base constructor with a message that includes the duplicate line number.
            : base($"Duplicate line number: {lineNumber}")
        {
        }
    }
}
