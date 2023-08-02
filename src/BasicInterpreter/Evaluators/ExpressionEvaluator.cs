//using System.Globalization;
//using Antlr4.Runtime;
//using JesseFreeman.BasicInterpreter.AntlrGenerated;
//using JesseFreeman.BasicInterpreter.Exceptions;

//namespace JesseFreeman.BasicInterpreter.Evaluators
//{
//    public class ExpressionEvaluator
//    {
//        private Dictionary<string, object> variables;
//        private ITokenStream tokenStream;
//        private Dictionary<string, IExpression> expressions;
//        private Dictionary<string, IUnaryExpression> unaryExpressions;

//        public ExpressionEvaluator(Dictionary<string, object> variables, ITokenStream tokenStream)
//        {
//            this.variables = variables;
//            this.tokenStream = tokenStream;

//            expressions = new Dictionary<string, IExpression>
//            {
//                // Define your expression operators here based on the grammar rules
//            };

//            unaryExpressions = new Dictionary<string, IUnaryExpression>
//            {
//                // Define your unary operators here based on the grammar rules
//            };
//        }

//        public object Evaluate(BasicParser.ExpressionContext expression)
//        {
//            // If the expression is a function
//            if (expression.func_() != null)
//            {
//                var func = expression.func_();

//                // If the function is a variable reference, return the value of the variable
//                if (func.vardecl() != null)
//                {
//                    string variableName = func.vardecl().GetText();
//                    if (variables.ContainsKey(variableName))
//                    {
//                        return variables[variableName];
//                    }
//                    else
//                    {
//                        throw new VariableNotDefinedException(variableName);
//                    }
//                }

//                // If the function is a string literal, return the string value
//                if (func.STRINGLITERAL() != null)
//                {
//                    string text = func.STRINGLITERAL().GetText();
//                    // Remove the surrounding quotes from the string literal
//                    text = text.Substring(1, text.Length - 2);
//                    return text;
//                }

//                // If the function is a number, return the numeric value
//                if (func.number() != null)
//                {
//                    string text = func.number().GetText();
//                    if (text.Contains("."))
//                    {
//                        return double.Parse(text);
//                    }
//                    else
//                    {
//                        return int.Parse(text);
//                    }
//                }
//            }

//            // If the expression is a unary expression, evaluate it
//            if (expression.unaryExpression() != null)
//            {
//                // TODO: Implement unary expression evaluation
//            }

//            // If the expression is a binary expression, evaluate it
//            if (expression.binaryExpression() != null)
//            {
//                // TODO: Implement binary expression evaluation
//            }

//            throw new NotImplementedException("Expression type not supported");
//        }





//        //public object Evaluate(BasicParser.ExpressionContext expressionContext)
//        //{
//        //    if (expressionContext.relationalExpression() != null)
//        //    {
//        //        // Evaluate each relationalExpression context and combine the results
//        //        var results = new List<object>();
//        //        foreach (var relationalExpressionContext in expressionContext.relationalExpression())
//        //        {
//        //            results.Add(EvaluateRelationalExpression(relationalExpressionContext));
//        //        }
//        //        // Combine the results based on your grammar rules
//        //        // For example, if AND and OR operators are used to combine relational expressions,
//        //        // you might need to implement appropriate logic here to combine the results
//        //        return results;
//        //    }
//        //    else
//        //    {
//        //        return EvaluateExpression(expressionContext);
//        //    }
//        //}

//        //public object EvaluateExpression(BasicParser.ExpressionContext expressionContext)
//        //{
//        //    var funcContext = expressionContext.func_();
//        //    if (funcContext.INT() != null)
//        //    {
//        //        return int.Parse(funcContext.INT().GetText());
//        //    }
//        //    else if (funcContext.FLOAT() != null)
//        //    {
//        //        return double.Parse(funcContext.FLOAT().GetText(), CultureInfo.InvariantCulture);
//        //    }
//        //    else if (funcContext.STRINGLITERAL() != null)
//        //    {
//        //        string text = funcContext.STRINGLITERAL().GetText();
//        //        return text.Substring(1, text.Length - 2);
//        //    }
//        //    else
//        //    {
//        //        return EvaluateFunc(funcContext);
//        //    }
//        //}

//        //public object EvaluateRelationalExpression(BasicParser.RelationalExpressionContext relationalExpressionContext)
//        //{
//        //    // Get all the addingExpressions from the relationalExpressionContext
//        //    var addingExpressions = relationalExpressionContext.addingExpression();

//        //    // Evaluate the first addingExpression
//        //    var leftValue = EvaluateAddingExpression(addingExpressions[0]);

//        //    // If there's more than one addingExpression, evaluate the rest and combine them using the relational operator
//        //    if (addingExpressions.Length > 1)
//        //    {
//        //        // Get the relational operator
//        //        var relationalOperator = relationalExpressionContext.relop().GetText();

//        //        for (int i = 1; i < addingExpressions.Length; i++)
//        //        {
//        //            // Evaluate the next addingExpression
//        //            var rightValue = EvaluateAddingExpression(addingExpressions[i]);

//        //            // Use the operator to evaluate the relational expression
//        //            if (expressions.ContainsKey(relationalOperator))
//        //            {
//        //                leftValue = expressions[relationalOperator].Evaluate(leftValue, rightValue);
//        //            }
//        //            else
//        //            {
//        //                throw new InvalidOperationException($"Unknown operator: {relationalOperator}");
//        //            }
//        //        }
//        //    }

//        //    return leftValue;
//        //}

//        //public object EvaluateFunc(BasicParser.Func_Context context)
//        //{
//        //    string funcName = context.GetText().ToUpper();

//        //    // Check if the function name is a known variable
//        //    if (variables.ContainsKey(funcName))
//        //    {
//        //        return variables[funcName];
//        //    }
//        //    // Check if the function name is a known unary expression
//        //    else if (unaryExpressions.ContainsKey(funcName))
//        //    {
//        //        IUnaryExpression expression = unaryExpressions[funcName];
//        //        object operand = context.expression() != null ? EvaluateExpression(context.expression()) : null; // Recursively evaluate the operand
//        //        return expression.Evaluate(operand);
//        //    }
//        //    // Check if the function name is a known binary expression
//        //    else if (expressions.ContainsKey(funcName))
//        //    {
//        //        IExpression expression = expressions[funcName];
//        //        object left = context.expression() != null ? EvaluateExpression(context.expression()) : null; // Recursively evaluate the left operand
//        //        object right = context.expression() != null ? EvaluateExpression(context.expression()) : null; // Recursively evaluate the right operand
//        //        return expression.Evaluate(left, right ?? null);
//        //    }
//        //    else
//        //    {
//        //        // If the function name is not a known function or variable, throw a VariableNotDefinedException
//        //        throw new Exception($"Function {funcName} is not defined.");
//        //    }
//        //}

//        //public object EvaluateAddingExpression(BasicParser.AddingExpressionContext addingExpressionContext)
//        //{
//        //    // Implement this method to correctly evaluate adding expressions according to your grammar rules
//        //    throw new NotImplementedException();
//        //}

//        //public object EvaluateTerm(BasicParser.ExpressionContext term)
//        //{
//        //    return Evaluate(term); // Treat the term as an expression
//        //}

//        //public object EvaluateUnary(BasicParser.ExpressionContext unaryExpression)
//        //{
//        //    return Evaluate(unaryExpression); // Treat the unary expression as an expression
//        //}
//    }
//}
