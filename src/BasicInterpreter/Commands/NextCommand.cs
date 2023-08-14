using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands;

public class NextCommand : ICommand
{
    private readonly BasicInterpreter interpreter;
    public List<string> VariableNames { get; }

    public NextCommand(BasicInterpreter interpreter, List<string> variableNames)
    {
        this.interpreter = interpreter;
        VariableNames = variableNames; // No need for null-coalescing operator
    }

    public void Execute()
    {
        // Iterate through the variable names in reverse order
        foreach (var variableName in VariableNames.AsEnumerable().Reverse())
        {
            if (interpreter.LoopContextManager.IsEmpty())
            {
                // If the loop stack is empty, throw a "NEXT without FOR" error
                throw new InterpreterException(BasicInterpreterError.NextWithoutFor);
            }

            // Try to get the loop context for the variable
            var loopContext = interpreter.LoopContextManager.Pop();

            var currentValue = interpreter.GetVariable(loopContext.VariableName);
            currentValue += loopContext.StepValue;
            interpreter.SetVariable(loopContext.VariableName, currentValue);

            if ((loopContext.StepValue > 0 && currentValue <= loopContext.EndValue) ||
                (loopContext.StepValue < 0 && currentValue >= loopContext.EndValue))
            {
                // If the loop is not finished, push the updated loop context back onto the stack
                var updatedLoopContext = new LoopContext(loopContext.VariableName, loopContext.EndValue,
                    loopContext.StepValue, loopContext.CommandIndex, loopContext.LineNumber, loopContext.Position, loopContext.ShouldSkip);

                interpreter.LoopContextManager.Push(updatedLoopContext);
                interpreter.JumpToCommandIndex(loopContext.CommandIndex);
                break; // Break out of the loop to continue executing the inner loop
            }
        }
    }


    
}


