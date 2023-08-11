namespace JesseFreeman.BasicInterpreter.Exceptions;

/// <summary>
/// Represents an exception that occurs during the interpretation of a BASIC program.
/// This exception wraps the original exception that caused the error, and includes the line number where the error occurred.
/// 
/// Example usage:
/// <code>
/// try
/// {
///     // Code that may throw an exception
/// }
/// catch (InterpreterException ex)
/// {
///     Console.WriteLine(ex.Message); // Prints the error message with the line number
///     Console.WriteLine(ex.OriginalException.Message); // Prints the original exception's message
///     Console.WriteLine(ex.OriginalExceptionType); // Prints the original exception's type
/// }
/// </code>
/// </summary>
public class InterpreterException : Exception
{
    /// <summary>
    /// Gets the original exception that caused the error.
    /// </summary>
    public Exception OriginalException { get; }

    /// <summary>
    /// Gets the line number where the error occurred.
    /// </summary>
    public int LineNumber { get; }

    /// <summary>
    /// Gets the type of the original exception.
    /// </summary>
    public Type OriginalExceptionType => OriginalException.GetType();

    /// <summary>
    /// Initializes a new instance of the <see cref="InterpreterException"/> class with the specified original exception and line number.
    /// </summary>
    /// <param name="originalException">The original exception that caused the error.</param>
    /// <param name="lineNumber">The line number where the error occurred.</param>
    public InterpreterException(Exception originalException, int lineNumber)
        : base(originalException.Message, originalException)
    {
        OriginalException = originalException;
        LineNumber = lineNumber;
    }

    /// <summary>
    /// Gets a message that describes the current exception.
    /// The message includes the line number and the original exception's message, with the line number placeholder replaced if present.
    /// </summary>
    public override string Message
    {
        get
        {
            // Replace the placeholder with the actual line number in the original exception's message
            string newMessage = OriginalException.Message.Replace("{line}", LineNumber.ToString());

            // Return the new message with the line number included
            return $"Error at line {LineNumber}: {newMessage}";
        }
    }

    
}
