using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands;

public class GotoCommand : ICommand
{
    public GotoCommand(int targetLineNumber)
    {
        TargetLineNumber = targetLineNumber;
    }

    public int TargetLineNumber { get; } // New property to store the target line number

    public void Execute()
    {
        // TODO 
        throw new GotoCommandException(TargetLineNumber); // Implement the GOTO logic here
    }
}