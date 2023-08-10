using System;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class NextCommand : ICommand
    {
        private readonly BasicInterpreter interpreter;
        private readonly string variableName;

        // Add this property to expose the variableName field
        public string VariableName => variableName;

        public NextCommand(BasicInterpreter interpreter, string variableName)
        {
            this.interpreter = interpreter;
            this.variableName = variableName;
        }

        public void Execute()
        {
            // Try to get the loop context for the variable
            LoopContext loopContext;
            try
            {
                loopContext = interpreter.GetLoopContext(variableName);
            }
            catch (InvalidOperationException)
            {
                // If no loop context is found, throw a specific exception for "NEXT without FOR"
                throw new NextWithoutForException($"NEXT without FOR at line {interpreter.CurrentLineNumber}");
            }

            // Check if the loop should be skipped
            if (loopContext.ShouldSkip)
            {
                interpreter.PopLoopContext();
                return;
            }

            // Increment the loop variable by the step value
            double currentValue = interpreter.GetVariable(loopContext.VariableName);
            currentValue += loopContext.StepValue;
            interpreter.SetVariable(loopContext.VariableName, currentValue);

            // Check if the loop should terminate
            if ((loopContext.StepValue > 0 && currentValue > loopContext.EndValue) ||
                (loopContext.StepValue < 0 && currentValue < loopContext.EndValue))
            {
                // Terminate the loop by popping the loop context
                interpreter.PopLoopContext();
            }
            else
            {
                // Update the loop context with the new current value
                LoopContext updatedLoopContext = new LoopContext(loopContext.VariableName, loopContext.EndValue, loopContext.StepValue, loopContext.CommandIndex, loopContext.LineNumber, loopContext.Position);
                interpreter.PopLoopContext(); // Pop the old loop context
                interpreter.PushLoopContext(updatedLoopContext); // Push the updated loop context

                // Continue the loop by jumping back to the stored command index
                interpreter.JumpToCommandIndex(loopContext.CommandIndex);
            }
        }

    }

}

