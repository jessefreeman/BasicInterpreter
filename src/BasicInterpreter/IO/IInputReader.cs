namespace JesseFreeman.BasicInterpreter.IO
{
    /// <summary>
    /// The IInputReader interface defines the contract for classes that provide input reading capabilities for the BasicInterpreter.
    /// Any class implementing this interface must provide a way to read input lines, typically used for providing user input to the interpreter.
    /// </summary>
    public interface IInputReader
    {
        /// <summary>
        /// Reads the next line of input from the input source.
        /// If there are no more lines available, the method should return null or throw an appropriate exception.
        /// </summary>
        /// <returns>The next line of input as a string, or null if there are no more lines available.</returns>
        string ReadLine();
    }
}
