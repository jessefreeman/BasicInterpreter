using System.Text;

namespace JesseFreeman.BasicInterpreter.IO;

/// <summary>
///     The ConsoleOutputWriter class is an implementation of the IOutputWriter interface
///     that writes output to the console. It is used when the output of the BASIC interpreter
///     needs to be displayed on the console.
/// </summary>
public class ConsoleOutputWriter : IOutputWriter
{
    // Not implemented in the console so return an empty string
    private readonly StringBuilder output = new();

    public string Output => output.ToString();

    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }

    public void Write(string text)
    {
        Console.Write(text);
    }

    public void NewLine()
    {
        Console.WriteLine();
    }
}