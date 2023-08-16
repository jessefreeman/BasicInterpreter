#region

using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Commands;

public class ReturnCommand : ICommand
{
    public void Execute()
    {
        throw new ReturnCommandException();
    }
}