using System.Text;

namespace JesseFreeman.BasicInterpreter.IO
{
    public class StringOutputWriter : IOutputWriter
    {
        private StringBuilder output = new StringBuilder();

        public void WriteLine(string line)
        {
            output.AppendLine(line);
        }

        public void Write(string text)
        {
            output.Append(text);
        }

        public void NewLine()
        {
            output.AppendLine();
        }

        public string Output => output.ToString();

        // Add a method to clear the output
        public void Clear()
        {
            output.Clear();
        }
    }
}

