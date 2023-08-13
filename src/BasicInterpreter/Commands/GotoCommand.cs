using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class GotoCommand : ICommand
    {
        public int TargetLineNumber { get; } // New property to store the target line number

        public GotoCommand(int targetLineNumber)
        {
            TargetLineNumber = targetLineNumber;
        }

        public void Execute()
        {
            // TODO 
            throw new GotoCommandException(TargetLineNumber); // Implement the GOTO logic here
        }
    }
}

