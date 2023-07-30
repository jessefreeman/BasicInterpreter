namespace JesseFreeman.BasicInterpreter.Exceptions
{
    /// <summary>
    /// The UndefinedVariableException class represents an exception that is thrown when an undefined variable is encountered.
    /// It extends the base Exception class.
    /// </summary>
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs a new UndefinedVariableException.
        /// </summary>
        /// <param name="variableName">The name of the undefined variable.</param>
        public UndefinedVariableException(string variableName)
            // Call the base constructor with a message that includes the variable name.
            : base($"Undefined variable: {variableName}")
        {
        }
    }
}
