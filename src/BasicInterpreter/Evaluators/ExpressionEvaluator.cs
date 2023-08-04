
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;

namespace JesseFreeman.BasicInterpreter.Evaluators
{

    public class ExpressionEvaluator : BasicBaseVisitor<IExpression>
    {
        private Dictionary<string, object> variables;
        private Dictionary<string, IExpression> operations;
        private Dictionary<string, IExpression> unaryOperations;

        public ExpressionEvaluator(Dictionary<string, object> variables)
        {
            this.variables = variables;

            // Initialize the operations dictionary
            operations = new Dictionary<string, IExpression>
            {
                { "+", new AdditionExpression() },
                { "-", new SubtractionExpression() },
                { "*", new MultiplicationExpression() },
                { "/", new DivisionExpression() },
                { "^", new ExpExpression() },
                { "=", new EqualExpression() },
                { "<", new LessThanExpression() },
                { "<=", new LessThanOrEqualExpression() },
                { ">", new GreaterThanExpression() },
                { ">=", new GreaterThanOrEqualExpression() },
                { "<>", new NotEqualExpression() },
                { "AND", new LogicalAndExpression() },
                { "OR", new LogicalOrExpression() },
                { "NOT", new LogicalNotExpression() },
                // Add more operations as needed...
            };

            unaryOperations = new Dictionary<string, IExpression>
            {
                { "ABS", new AbsExpression() },
                { "ATN", new AtnExpression() },
                { "COS", new CosExpression() },
                { "EXP", new ExpExpression() },
                { "INT", new IntExpression() },
                { "LOG", new LogExpression() },
                { "RND", new RndExpression() },
                { "SGN", new SgnExpression() },
                { "SIN", new SinExpression() },
                { "SQR", new SqrExpression() },
                { "TAN", new TanExpression() },
                // Add more operations as needed
            };

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

            // Handle unary operations
            if (context.absfunc() != null)
            {
                return VisitAbsfunc(context.absfunc());
            }
            else if (context.atnfunc() != null)
            {
                return VisitAtnfunc(context.atnfunc());
            }
            else if (context.cosfunc() != null)
            {
                return VisitCosfunc(context.cosfunc());
            }
            else if (context.expfunc() != null)
            {
                return VisitExpfunc(context.expfunc());
            }
            else if (context.intfunc() != null)
            {
                return VisitIntfunc(context.intfunc());
            }
            else if (context.logfunc() != null)
            {
                return VisitLogfunc(context.logfunc());
            }
            else if (context.rndfunc() != null)
            {
                return VisitRndfunc(context.rndfunc());
            }
            else if (context.sgnfunc() != null)
            {
                return VisitSgnfunc(context.sgnfunc());
            }
            else if (context.sinfunc() != null)
            {
                return VisitSinfunc(context.sinfunc());
            }
            else if (context.sqrfunc() != null)
            {
                return VisitSqrfunc(context.sqrfunc());
            }
            else if (context.tanfunc() != null)
            {
                return VisitTanfunc(context.tanfunc());
            }

            // Handle other func_ alternatives as needed.
            // E.g., if number token is found, parse it into NumberExpression, etc.
            // If the token is neither a string nor a number, you could throw an exception,
            // return null or a specific default instance, depending on your design.

            throw new NotImplementedException($"Unhandled func_ alternative: {context.GetText()}");
        }

        public override IExpression VisitAddingExpression([NotNull] BasicParser.AddingExpressionContext context)
        {
            // Visit the first child node
            IExpression left = Visit(context.GetChild(0));

            // Iterate over the rest of the child nodes
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                // Get the operator between this child node and the previous one
                string operatorSymbol = context.GetChild(i).GetText();

                // Try to get the operation from the dictionary
                if (operations.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    // Visit the next child node
                    IExpression right = Visit(context.GetChild(i + 1));

                    // Create a new LambdaExpression with the operation and the current left and right as operands
                    left = new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            return left;
        }

        public override IExpression VisitMultiplyingExpression([NotNull] BasicParser.MultiplyingExpressionContext context)
        {
            // Visit the first exponentExpression
            IExpression left = VisitExponentExpression(context.exponentExpression(0));

            // Iterate over the rest of the exponentExpressions
            for (int i = 1; i < context.exponentExpression().Length; i++)
            {
                // Get the operator between this exponentExpression and the previous one
                string operatorSymbol = context.GetChild(i * 2 - 1).GetText();

                // Try to get the operation from the dictionary
                if (operations.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    // Visit the next exponentExpression
                    IExpression right = VisitExponentExpression(context.exponentExpression(i));

                    // Create a new LambdaExpression with the operation and the current left and right as operands
                    left = new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            return left;
        }

        public override IExpression VisitExponentExpression([NotNull] BasicParser.ExponentExpressionContext context)
        {
            // Visit the first signExpression
            IExpression left = VisitSignExpression(context.signExpression(0));

            // Iterate over the rest of the signExpressions
            for (int i = 1; i < context.signExpression().Length; i++)
            {
                // Get the operator between this signExpression and the previous one
                string operatorSymbol = context.GetChild(i * 2 - 1).GetText();

                // Try to get the operation from the dictionary
                if (operations.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    // Visit the next signExpression
                    IExpression right = VisitSignExpression(context.signExpression(i));

                    // Create a new LambdaExpression with the operation and the current left and right as operands
                    left = new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            return left;
        }

        public override IExpression VisitSignExpression([NotNull] BasicParser.SignExpressionContext context)
        {
            // Visit the first func_
            IExpression operand = VisitFunc_(context.func_());

            // If there is a NOT or a MINUS before the func_, create a new LambdaExpression with the corresponding operation and the operand
            if (context.NOT() != null)
            {
                operand = new LambdaExpression(new NotExpression(), new IExpression[] { operand });
            }
            else if (context.MINUS() != null)
            {
                operand = new LambdaExpression(new NegationExpression(), new IExpression[] { operand });
            }

            return operand;
        }

        public override IExpression VisitRelationalExpression([NotNull] BasicParser.RelationalExpressionContext context)
        {
            // Visit the left expression
            IExpression left = Visit(context.addingExpression(0));

            // Check if there is a relational operator
            if (context.relop() != null)
            {
                // Get the operator symbol
                string operatorSymbol = context.relop().GetText();

                // Try to get the operation from the dictionary
                if (operations.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    // Visit the right expression
                    IExpression right = Visit(context.addingExpression(1));

                    // Create a new LambdaExpression with the operation and the left and right as operands
                    return new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            // If there is no relational operator, just return the left expression
            return left;
        }

        public override IExpression VisitExpression([NotNull] BasicParser.ExpressionContext context)
        {
            // Visit the first child node
            IExpression left = Visit(context.GetChild(0));

            // Iterate over the rest of the child nodes
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                // Get the operator between this child node and the previous one
                string operatorSymbol = context.GetChild(i).GetText();

                // Try to get the operation from the dictionary
                if (operations.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    // Visit the next child node
                    IExpression right = Visit(context.GetChild(i + 1));

                    // Create a new LambdaExpression with the operation and the current left and right as operands
                    left = new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            return left;
        }

        private IExpression VisitUnaryOperation(string operationName, ParserRuleContext context)
        {
            // Visit the operand
            IExpression operand = Visit(context.GetChild(0));

            // Try to get the operation from the dictionary
            if (unaryOperations.TryGetValue(operationName, out IExpression operation))
            {
                // Create a new LambdaExpression with the operation and the operand
                return new LambdaExpression(operation, new IExpression[] { operand });
            }
            else
            {
                throw new NotImplementedException($"Unhandled operation: {operationName}");
            }
        }

        public override IExpression VisitAbsfunc([NotNull] BasicParser.AbsfuncContext context)
    => VisitUnaryOperation("ABS", context.expression());

        public override IExpression VisitAtnfunc([NotNull] BasicParser.AtnfuncContext context) => VisitUnaryOperation("ATN", context.expression());

        public override IExpression VisitCosfunc([NotNull] BasicParser.CosfuncContext context)
    => VisitUnaryOperation("COS", context.expression());

        public override IExpression VisitExpfunc([NotNull] BasicParser.ExpfuncContext context)
            => VisitUnaryOperation("EXP", context.expression());

        public override IExpression VisitIntfunc([NotNull] BasicParser.IntfuncContext context)
            => VisitUnaryOperation("INT", context.expression());

        public override IExpression VisitLogfunc([NotNull] BasicParser.LogfuncContext context)
            => VisitUnaryOperation("LOG", context.expression());

        public override IExpression VisitRndfunc([NotNull] BasicParser.RndfuncContext context)
            => VisitUnaryOperation("RND", context.expression());

        public override IExpression VisitSgnfunc([NotNull] BasicParser.SgnfuncContext context)
            => VisitUnaryOperation("SGN", context.expression());

        public override IExpression VisitSinfunc([NotNull] BasicParser.SinfuncContext context)
            => VisitUnaryOperation("SIN", context.expression());

        public override IExpression VisitSqrfunc([NotNull] BasicParser.SqrfuncContext context)
            => VisitUnaryOperation("SQR", context.expression());

        public override IExpression VisitTanfunc([NotNull] BasicParser.TanfuncContext context)
            => VisitUnaryOperation("TAN", context.expression());

    }


}
