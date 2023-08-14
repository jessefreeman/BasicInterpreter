using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands;

public class ReturnCommand : ICommand
{
    public void Execute()
    {
        throw new ReturnCommandException();
    }
}