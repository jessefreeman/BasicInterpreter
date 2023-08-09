using System;
namespace JesseFreeman.BasicInterpreter.Commands
{
    public class NextCommand : ICommand
    {
        private readonly BasicInterpreter interpreter;
        private readonly List<string> variableNames;

        public NextCommand(BasicInterpreter interpreter, List<string> variableNames = null)
        {
            this.interpreter = interpreter;
            this.variableNames = variableNames ?? new List<string>();
        }

        public void Execute()
        {
            foreach (var variableName in variableNames)
            {
                var loopContext = interpreter.GetLoopContext(variableName);

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
                    // Continue the loop by jumping back to the stored line number and position
                    interpreter.JumpToLineAndPosition(loopContext.LineNumber, loopContext.Position);
                }
            }
        }
    }


}

