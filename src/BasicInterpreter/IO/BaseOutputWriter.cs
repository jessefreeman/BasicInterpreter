namespace JesseFreeman.BasicInterpreter.IO;

public abstract class BaseOutputWriter : IOutputWriter
{
    public bool AlignTabs = false;
    protected int currentLinePosition;
    public int TabSize { get; set; } = 4;

    public abstract void WriteLine(string line);
    public abstract void Write(string text);

    public void WriteSeparator(char separator)
    {
        if (separator == ';')
        {
            Write(" "); // Add a single space after the semicolon
            currentLinePosition += 1;
        }
        else if (separator == ',')
        {
            if (AlignTabs)
            {
                var spacesToAdd = TabSize - currentLinePosition % TabSize;
                Write(new string(' ', spacesToAdd));
                currentLinePosition += spacesToAdd;
            }
            else
            {
                var spacesToAdd = TabSize; // Add a fixed number of spaces for the comma separator
                Write(new string(' ', spacesToAdd));
                currentLinePosition += spacesToAdd;
            }
        }
    }

    public abstract void NewLine();
}