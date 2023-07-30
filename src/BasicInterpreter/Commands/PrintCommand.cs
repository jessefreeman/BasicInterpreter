using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class PrintCommand : ICommand
    {
        private string text;
        private IOutputWriter writer;

        public PrintCommand(string text, IOutputWriter writer)
        {
            this.text = text;
            this.writer = writer;
        }

        public void Execute()
        {
            writer.Write(text);
        }
    }

}

