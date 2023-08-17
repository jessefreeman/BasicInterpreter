using System.Text;

namespace JesseFreeman.BasicInterpreter.Commands;

public class CompositeCommand : ICommand
{
    private readonly List<ICommand> commands;

    public CompositeCommand(List<ICommand> commands)
    {
        this.commands = commands;
    }

    // Add a public property to expose the list of commands
    public IReadOnlyList<ICommand> Commands => commands;

    public void Execute()
    {
        foreach (var command in commands) command.Execute();
    }

    public override string ToString()
    {
        StringBuilder innerCommandsDetails = new StringBuilder();
        foreach (var innerCommand in Commands)
        {
            innerCommandsDetails.Append($"Inner Command Type: {innerCommand.GetType().Name}, ");
        }

        // Remove the trailing comma and space
        if (innerCommandsDetails.Length > 2)
        {
            innerCommandsDetails.Length -= 2;
        }

        return $"CompositeCommand with Inner Commands: {innerCommandsDetails.ToString()}";
    }

}