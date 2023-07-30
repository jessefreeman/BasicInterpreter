namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class GotoCommandException : Exception
    {
        public int TargetLineNumber { get; }

        public GotoCommandException(int targetLineNumber)
        {
            TargetLineNumber = targetLineNumber;
        }
    }
}

