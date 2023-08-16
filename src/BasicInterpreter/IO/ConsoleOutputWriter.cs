using System.Text;

namespace JesseFreeman.BasicInterpreter.IO;

/// <summary>
///     The ConsoleOutputWriter class is an implementation of the IOutputWriter interface
///     that writes output to the console. It is used when the output of the BASIC interpreter
///     needs to be displayed on the console.
/// </summary>
public class ConsoleOutputWriter : BaseOutputWriter
{
    public override void WriteLine(string line)
    {
        Console.WriteLine(line);
        currentLinePosition = 0;
    }

    public override void Write(string text)
    {
        Console.Write(text);
        currentLinePosition += text.Length;
    }

    public override void NewLine()
    {
        Console.WriteLine();
        currentLinePosition = 0;
    }
}
