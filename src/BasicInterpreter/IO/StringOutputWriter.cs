#region

using System.Text;

#endregion

namespace JesseFreeman.BasicInterpreter.IO;

public class StringOutputWriter : BaseOutputWriter
{
    private readonly StringBuilder output = new();

    public string Output => output.ToString();

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

    public void Clear()
    {
        output.Clear();
        currentLinePosition = 0;
    }
}