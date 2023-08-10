namespace JesseFreeman.BasicInterpreter.Commands
{
    public class LoopContext
    {
        public string VariableName { get; }
        public double EndValue { get; }
        public double StepValue { get; }
        public int CommandIndex { get; }
        public int LineNumber { get; }
        public int Position { get; }
        public bool ShouldSkip { get; } // Added flag to determine if the loop should be skipped

        public LoopContext(string variableName, double endValue, double stepValue, int commandIndex, int lineNumber, int position, bool shouldSkip = false)
        {
            VariableName = variableName;
            EndValue = endValue;
            StepValue = stepValue;
            CommandIndex = commandIndex;
            LineNumber = lineNumber;
            Position = position;
            ShouldSkip = shouldSkip; // Initialize the flag
        }
    }


}

