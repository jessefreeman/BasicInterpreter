using System;
namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class GosubCommandException : Exception
    {
        public int TargetLineNumber { get; }

        public GosubCommandException(int targetLineNumber)
        {
            TargetLineNumber = targetLineNumber;
        }
    }
}

