// Declare a namespace for command-related classes and interfaces within the JesseFreeman.BasicInterpreter project

namespace JesseFreeman.BasicInterpreter.Commands;

/// <summary>
///     The ICommand interface defines a contract for all commands in the BasicInterpreter.
///     Each command in the BasicInterpreter must implement this interface.
///     This ensures a consistent structure for commands and allows commands to be used interchangeably.
/// </summary>
public interface ICommand
{
    void Execute();
}