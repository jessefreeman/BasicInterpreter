using JesseFreeman.BasicInterpreter.Evaluators;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class ForCommand : ICommand
    {
        private readonly string variableName;
        private readonly IExpression startExpression;
        private readonly IExpression endExpression;
        private readonly IExpression stepExpression;
        private readonly BasicInterpreter interpreter;

        public ForCommand(string variableName, IExpression startExpression, IExpression endExpression, IExpression stepExpression, BasicInterpreter interpreter)
        {
            this.variableName = variableName;
            this.startExpression = startExpression;
            this.endExpression = endExpression;
            this.stepExpression = stepExpression;
            this.interpreter = interpreter;
        }

        public void Execute()
        {
            double startValue = (double)startExpression.Evaluate();
            double endValue = (double)endExpression.Evaluate();
            double stepValue = stepExpression != null ? (double)stepExpression.Evaluate() : 1;

            // Set the variable to its start value regardless of whether the loop will be skipped
            interpreter.SetVariable(variableName, startValue);

            // Determine if the loop should be skipped
            bool shouldSkip = (stepValue > 0 && startValue > endValue) || (stepValue < 0 && startValue < endValue);

            if (shouldSkip)
            {
                // Push a "skipped" loop context onto the stack
                interpreter.PushLoopContext(new LoopContext(variableName, endValue, stepValue, interpreter.CurrentCommandIndex, interpreter.CurrentLineNumber, interpreter.CurrentPosition, shouldSkip: true));
                return;
            }

            int lineNumber = interpreter.CurrentLineNumber;
            int position = interpreter.CurrentPosition;

            interpreter.PushLoopContext(new LoopContext(variableName, endValue, stepValue, interpreter.CurrentCommandIndex, lineNumber, position));
        }


    }

}
