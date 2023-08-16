#region

using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Commands;

public class GosubCommand : ICommand
{
    public GosubCommand(int targetLineNumber)
    {
        TargetLineNumber = targetLineNumber;
    }

    public int TargetLineNumber { get; }

    public void Execute()
    {
        throw new GosubCommandException(TargetLineNumber);
    }
}