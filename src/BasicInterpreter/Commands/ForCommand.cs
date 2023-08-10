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

            // Check if the loop should be skipped
            if ((stepValue > 0 && startValue > endValue) || (stepValue < 0 && startValue < endValue))
            {
                // Set the variable to 0 and skip the loop
                interpreter.SetVariable(variableName, 0);
                return;
            }

            // Set the variable to its start value if the loop is not skipped
            interpreter.SetVariable(variableName, startValue);

            int lineNumber = interpreter.CurrentLineNumber;
            int position = interpreter.CurrentPosition;

            interpreter.PushLoopContext(new LoopContext(variableName, endValue, stepValue, interpreter.CurrentCommandIndex, lineNumber, position));
        }

    }

}
