using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands;

public class NextCommand : ICommand
{
    private readonly BasicInterpreter interpreter;
    public List<string> VariableNames { get; }

    public NextCommand(BasicInterpreter interpreter, List<string> variableNames)
    {
        this.interpreter = interpreter;
        VariableNames = variableNames ?? new List<string>(); // Handle null variableNames
    }

    public void Execute()
    {
        // Iterate through the variable names in the order they are provided
        foreach (var variableName in VariableNames)
        {
            if (interpreter.LoopContextManager.IsEmpty(variableName))
            {
                // If the loop stack is empty for the given variable, throw a "NEXT without FOR" error
                throw new InterpreterException(BasicInterpreterError.NextWithoutFor);
            }

            // Try to get the loop context for the variable
            var loopContext = interpreter.LoopContextManager.Pop(variableName);

            var currentValue = interpreter.GetVariable(loopContext.VariableName);
            currentValue += loopContext.StepValue;
            interpreter.SetVariable(loopContext.VariableName, currentValue);

            if ((loopContext.StepValue > 0 && currentValue <= loopContext.EndValue) ||
                (loopContext.StepValue < 0 && currentValue >= loopContext.EndValue))
            {
                // If the loop is not finished, push the updated loop context back onto the stack
                var updatedLoopContext = new LoopContext(loopContext.VariableName, loopContext.EndValue,
                    loopContext.StepValue, loopContext.CommandIndex, loopContext.LineNumber, loopContext.Position, loopContext.ShouldSkip);

                interpreter.LoopContextManager.Push(variableName, updatedLoopContext);
                interpreter.JumpToCommandIndex(loopContext.CommandIndex);
                return; // Return after processing the innermost loop
            }
        }
    }

}




