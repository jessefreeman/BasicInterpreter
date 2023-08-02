
using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Evaluators
{

    public class ExpressionEvaluator : BasicBaseVisitor<IExpression>
    {
        private Dictionary<string, object> variables;

        public ExpressionEvaluator(Dictionary<string, object> variables)
        {
            this.variables = variables;
        }

        public override IExpression VisitFunc_([NotNull] BasicParser.Func_Context context)
        {
            
            if (context.STRINGLITERAL() != null)
            {
                // Remove the starting and ending quotation marks from the string literal
                string value = context.STRINGLITERAL().GetText().Trim('"');
                return new StringExpression(value);
            }

            if (context.number() != null)
            {
                double value = double.Parse(context.number().GetText());
                return new NumberExpression(value);
            }

            if (context.vardecl() != null)
            {
                string varName = context.vardecl().var_().GetText();

                if (!variables.TryGetValue(varName, out object varValue))
                {
                    throw new VariableNotDefinedException($"Variable {varName} not found");
                }

                // Now, regardless of whether the variable was previously defined or not,
                // varValue contains the current value of the variable.

                // Check the type of varValue and return corresponding IExpression
                if (varValue is double)
                {
                    return new NumberExpression((double)varValue);
                }
                else if (varValue is string)
                {
                    return new StringExpression((string)varValue);
                }
                else
                {
                    throw new NotImplementedException($"Unhandled variable type: {varValue.GetType()}");
                }
            }

            // Handle other func_ alternatives as needed.
            // E.g., if number token is found, parse it into NumberExpression, etc.
            // If the token is neither a string nor a number, you could throw an exception,
            // return null or a specific default instance, depending on your design.

            throw new NotImplementedException($"Unhandled func_ alternative: {context.GetText()}");
        }


        //public override IExpression VisitNumber([NotNull] BasicParser.NumberContext context)
        //{
        //    // Extract the number from the context and return a NumberExpression instance
        //    double value = double.Parse(context.GetText());
        //    return new NumberExpression(value);
        //}

        //public override IExpression VisitStrfunc([NotNull] BasicParser.StrfuncContext context)
        //{
        //    return base.VisitStrfunc(context);
        //}

        //public override IExpression VisitString([NotNull] BasicParser.StringContext context)
        //{
        //    // Extract the string from the context and return a StringExpression instance
        //    string value = context.GetText();
        //    return new StringExpression(value);
        //}

        // Override other expression methods as needed.
    }


}
