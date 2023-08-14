using JesseFreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.IO;

namespace Jessefreeman.BasicInterpreter.Commands;

public class InputCommand : ICommand
{
    private readonly IInputReader _inputReader;
    private readonly List<string> _variableNames;

    public InputCommand(List<string> variableNames, IInputReader inputReader)
    {
        _variableNames = variableNames;
        _inputReader = inputReader;
    }

    public void Execute()
    {
        //foreach (var variableName in _variableNames)
        //{
        //    string input = _inputReader.ReadLine();
        //    double value;
        //    if (double.TryParse(input, out value))
        //    {
        //        // Store the input value in the variable
        //        interpreter.SetVariable(variableName, value);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException($"Invalid input for variable {variableName}");
        //    }
        //}
    }
}