#region

using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Commands;

public class IfCommand : ICommand
{
    private readonly IExpression _condition;
    private readonly ICommand _thenCommand;
    private readonly int _thenLineNumber;

    public IfCommand(IExpression condition, int thenLineNumber, ICommand thenCommand)
    {
        _condition = condition;
        _thenLineNumber = thenLineNumber;
        _thenCommand = thenCommand;
    }

    public void Execute()
    {
        // Evaluate the condition
        var conditionResult = _condition.Evaluate();
        bool conditionIsTrue;

        if (conditionResult is bool conditionBool)
            conditionIsTrue = conditionBool;
        else if (conditionResult is double conditionDouble)
            conditionIsTrue = conditionDouble != 0.0;
        else
            throw new ArgumentException("Invalid condition result type for IfCommand. Expected boolean or double.");

        if (conditionIsTrue)
        {
            // If the condition is true, check if we have a command to execute
            if (_thenCommand != null)
                _thenCommand.Execute();
            else if (_thenLineNumber != -1)
                // If we don't have a command, but we have a line number, throw a GotoCommandException
                throw new GotoCommandException(_thenLineNumber);
        }
        else
        {
            // If the condition is false, throw a SkipNextCommandException to skip the next command
            throw new SkipNextCommandException();
        }
    }
}