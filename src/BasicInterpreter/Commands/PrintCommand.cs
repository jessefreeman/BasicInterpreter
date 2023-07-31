using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;
using System;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class PrintCommand : ICommand
    {
        private string variableName;
        private Dictionary<string, object> variables;
        private IOutputWriter writer;
        private string literalValue;

        public PrintCommand(string variableName, Dictionary<string, object> variables, IOutputWriter writer, string literalValue = null)
        {
            this.variableName = variableName;
            this.variables = variables;
            this.writer = writer;
            this.literalValue = literalValue;
        }

        public void Execute()
        {
            if (literalValue != null)
            {
                writer.WriteLine(literalValue);
            }
            else if (variables.ContainsKey(variableName))
            {
                writer.WriteLine(variables[variableName].ToString());
            }
            else
            {
                throw new VariableNotDefinedException(variableName);
            }
        }
    }
}
