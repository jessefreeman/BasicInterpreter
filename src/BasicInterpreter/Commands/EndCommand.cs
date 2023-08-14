using JesseFreeman.BasicInterpreter.Commands;

namespace JesseFreeman.BasicInterpreter;

public class EndCommand : ICommand
{
    private readonly BasicInterpreter interpreter;

    public EndCommand(BasicInterpreter interpreter)
    {
        this.interpreter = interpreter;
    }

    public void Execute()
    {
        interpreter.HasEnded = true;
    }
}