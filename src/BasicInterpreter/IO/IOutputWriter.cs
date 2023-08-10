namespace JesseFreeman.BasicInterpreter.IO
{
    /// <summary>
    /// The IOutputWriter interface defines a method for writing a line of output.
    /// This interface can be implemented by any class that needs to write output,
    /// such as a class that writes output to the console, a file, a network socket, etc.
    /// </summary>
    public interface IOutputWriter
    {
        void WriteLine(string line = "");
        void Write(string text);
        void NewLine();
        string Output { get; }
    }

}
