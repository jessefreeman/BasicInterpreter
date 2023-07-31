using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class LetCommand : ICommand
    {
        private string variableName;
        private BasicParser.ExpressionContext expression;
        private Dictionary<string, object> variables;
        private IOutputWriter writer;

        public LetCommand(string variableName, BasicParser.ExpressionContext expression, Dictionary<string, object> variables, IOutputWriter writer)
        {
            this.variableName = variableName;
            this.expression = expression;
            this.variables = variables;
            this.writer = writer;
        }

        public void Execute()
        {
            object value = null;

            // Check if the expression is a string literal
            if (expression.func_() != null && expression.func_().STRINGLITERAL() != null)
            {
                string text = expression.func_().STRINGLITERAL().GetText();
                // Remove the surrounding quotes from the string literal
                text = text.Substring(1, text.Length - 2);
                value = text;
            }
            // Check if the expression is a numeric literal
            else if (expression.func_() != null && expression.func_().number() != null)
            {
                string text = expression.func_().number().GetText();
                if (text.Contains("."))
                {
                    value = double.Parse(text);
                }
                else
                {
                    value = int.Parse(text);
                }
            }
            else
            {
                throw new NotImplementedException("Only static strings and numbers are currently supported in LET statements");
            }

            if ((variableName.EndsWith("$") && value is string) || (!variableName.EndsWith("$") && (value is int || value is double)))
            {
                variables[variableName] = value;
            }
            else
            {
                string expectedType = variableName.EndsWith("$") ? "string" : "number";
                string actualType = value is string ? "string" : (value is int ? "integer" : "floating-point number");
                throw new InvalidTypeAssignmentException(variableName, expectedType, actualType);
            }
        }
    }
}
