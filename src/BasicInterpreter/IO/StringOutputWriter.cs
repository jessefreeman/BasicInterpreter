using System.Text;

namespace JesseFreeman.BasicInterpreter.IO;

public class StringOutputWriter : BaseOutputWriter
{
    private readonly StringBuilder output = new();

    public override void WriteLine(string line)
    {
        output.AppendLine(line);
        currentLinePosition = 0;
    }

    public override void Write(string text)
    {
        output.Append(text);
        currentLinePosition += text.Length;
    }

    public override void NewLine()
    {
        output.AppendLine();
        currentLinePosition = 0;
    }

    public string Output => output.ToString();

    public void Clear()
    {
        output.Clear();
        currentLinePosition = 0;
    }
}


