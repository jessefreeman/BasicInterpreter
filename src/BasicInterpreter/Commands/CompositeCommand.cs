using System;
namespace JesseFreeman.BasicInterpreter.Commands
{
    public class CompositeCommand : ICommand
    {
        private readonly List<ICommand> commands;

        // Add a public property to expose the list of commands
        public IReadOnlyList<ICommand> Commands => commands;

        public CompositeCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }


}

