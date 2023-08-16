namespace JesseFreeman.BasicInterpreter.IO;

/// <summary>
///     The ConsoleInputReader class implements the IInputReader interface and provides a way to read input lines from the
///     console.
///     It allows the BasicInterpreter to read user input from the console when executing commands that require input from
///     the user.
/// </summary>
public class ConsoleInputReader : IInputReader
{
    /// <summary>
    ///     Reads the next line of input from the console.
    /// </summary>
    /// <returns>The next line of input as a string, or null if there are no more lines available.</returns>
    public string ReadLine()
    {
        // Use the Console.ReadLine() method to read a line of input from the console
        // The method will wait for the user to enter a line of text and press Enter
        // The entered line will be returned as a string
        return Console.ReadLine();
    }
}