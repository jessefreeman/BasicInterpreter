namespace JesseFreeman.BasicInterpreter.Exceptions
{
    /// <summary>
    /// The InvalidTypeAssignmentException class represents an exception that is thrown when an invalid type assignment is attempted.
    /// It extends the base Exception class.
    /// </summary>
    public class InvalidTypeAssignmentException : Exception
    {
        /// <summary>
        /// Constructs a new InvalidTypeAssignmentException.
        /// </summary>
        /// <param name="variableName">The name of the variable where the invalid assignment was attempted.</param>
        /// <param name="expectedType">The expected type of the variable.</param>
        /// <param name="actualType">The actual type of the value that was attempted to be assigned to the variable.</param>
        public InvalidTypeAssignmentException(string variableName, string expectedType, string actualType)
            // Call the base constructor with a message that includes the variable name, the expected type, and the actual type.
            : base($"Invalid type assignment: variable '{variableName}' expected a value of type '{expectedType}', but got a value of type '{actualType}'")
        {
        }
    }
}
