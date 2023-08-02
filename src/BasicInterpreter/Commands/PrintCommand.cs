using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

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
                // Try to parse the literal value as a double
                if (double.TryParse(literalValue, out double number))
                {
                    // The literal value is a number, so apply the formatting logic
                    if (number % 1 == 0)
                    {
                        // The number is a whole number, so print it without a decimal point
                        writer.WriteLine(((int)number).ToString());
                    }
                    else
                    {
                        // The number is not a whole number, so print it with a decimal point
                        writer.WriteLine(number.ToString());
                    }
                }
                else
                {
                    // The literal value is not a number, so print it as is
                    writer.WriteLine(literalValue);
                }
            }
            else if (variables.ContainsKey(variableName))
            {
                object variableValue = variables[variableName];
                if (variableValue is double)
                {
                    double number = (double)variableValue;
                    if (number == Math.Truncate(number))
                    {
                        // The number is a whole number, so print it without a decimal point
                        writer.WriteLine(((int)number).ToString());
                    }
                    else
                    {
                        // The number is not a whole number, so print it with a decimal point
                        writer.WriteLine(number.ToString());
                    }
                }
                else
                {
                    // The variable is not a number, so print it as is
                    writer.WriteLine(variableValue.ToString());
                }
            }
            else
            {
                throw new VariableNotDefinedException(variableName);
            }
        }



    }
}
