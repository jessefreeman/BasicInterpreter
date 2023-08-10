using System;
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
                // If no loop context is found, ignore the NEXT statement
                return;
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
                // Continue the loop by jumping back to the stored command index
                interpreter.JumpToCommandIndex(loopContext.CommandIndex);
            }

        }
    }



}

