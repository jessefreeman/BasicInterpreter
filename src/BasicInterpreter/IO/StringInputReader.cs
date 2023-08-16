#region

using JesseFreeman.BasicInterpreter.IO;

#endregion

/// <summary>
///     The StringInputReader class implements the IInputReader interface and provides a way to read input lines from a
///     collection of strings.
///     It is used in the BasicInterpreter to simulate user input when executing commands that require input from the user.
/// </summary>
public class StringInputReader : IInputReader
{
    // A queue to store the lines of input to be read
    private readonly Queue<string> lines;

    /// <summary>
    ///     Initializes a new instance of the StringInputReader class.
    /// </summary>
    public StringInputReader()
    {
        // Create a new empty queue to store the lines of input
        lines = new Queue<string>();
    }

    /// <summary>
    ///     Reads the next non-empty line of input from the StringInputReader.
    ///     If there are no more non-empty lines available, returns an empty string.
    /// </summary>
    /// <returns>The next non-empty line of input as a string, or an empty string if no more non-empty lines are available.</returns>
    public string ReadLine()
    {
        // Check if there are any lines left to read
        while (lines.Count > 0)
        {
            // Dequeue the next line of input from the queue
            var line = lines.Dequeue();

            // If the line is not empty, return it
            if (!string.IsNullOrEmpty(line)) return line;
        }

        // If there are no more non-empty lines, return an empty string
        return string.Empty;
    }

    /// <summary>
    ///     Sets the input lines for the StringInputReader.
    ///     This method allows you to provide a collection of strings that represent the input lines to be read.
    /// </summary>
    /// <param name="inputLines">An IEnumerable containing the input lines as strings.</param>
    public void SetInput(IEnumerable<string> inputLines)
    {
        // Clear the current contents of the queue to start fresh
        lines.Clear();

        // Enqueue each input line into the queue, preserving the order
        foreach (var line in inputLines) lines.Enqueue(line);
    }
}