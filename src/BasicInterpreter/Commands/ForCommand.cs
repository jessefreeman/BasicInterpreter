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

            interpreter.SetVariable(variableName, startValue);

            int lineNumber = interpreter.CurrentLineNumber; // Retrieve the current line number
            int position = interpreter.CurrentPosition; // Retrieve the current position within the line

            interpreter.PushLoopContext(new LoopContext(variableName, endValue, stepValue, interpreter.CurrentCommandIndex, lineNumber, position));
        }
    }
}
