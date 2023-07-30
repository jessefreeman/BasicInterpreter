using System;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class GosubCommand : ICommand
    {
        public int TargetLineNumber { get; }

        public GosubCommand(int targetLineNumber)
        {
            TargetLineNumber = targetLineNumber;
        }

        public void Execute()
        {
            throw new GosubCommandException(TargetLineNumber);
        }
    }

}

