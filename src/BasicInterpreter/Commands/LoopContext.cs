using System;
namespace JesseFreeman.BasicInterpreter.Commands
{
    public class LoopContext
    {
        public string VariableName { get; }
        public double EndValue { get; }
        public double StepValue { get; }
        public int StartCommandIndex { get; }
        public int LineNumber { get; }
        public int Position { get; }

        public LoopContext(string variableName, double endValue, double stepValue, int startCommandIndex, int lineNumber, int position)
        {
            VariableName = variableName;
            EndValue = endValue;
            StepValue = stepValue;
            StartCommandIndex = startCommandIndex;
            LineNumber = lineNumber;
            Position = position;
        }
    }


}

