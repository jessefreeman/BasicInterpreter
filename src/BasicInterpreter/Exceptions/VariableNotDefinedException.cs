using System;

namespace JesseFreeman.BasicInterpreter.Exceptions
{
    public class VariableNotDefinedException : Exception
    {
        public VariableNotDefinedException(string variableName)
            : base($"Variable '{variableName}' has not been defined.")
        {
        }
    }
}
