namespace JesseFreeman.BasicInterpreter.IO;

public abstract class BaseOutputWriter : IOutputWriter
{
    protected int currentLinePosition = 0;
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
            int spacesToAdd = TabSize - (currentLinePosition % TabSize);
            if (spacesToAdd == TabSize) spacesToAdd = 0; // Fix for the off-by-one issue
            Write(new string(' ', spacesToAdd));
            currentLinePosition += spacesToAdd;
        }
    }


    public abstract void NewLine();
}
