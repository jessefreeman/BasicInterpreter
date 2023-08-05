using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;
using System.Reflection;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    public class ExpressionEvaluator : BasicBaseVisitor<IExpression>
    {
        private readonly Dictionary<string, object> variables;
        private readonly Dictionary<string, IExpression> operations;
        private readonly Dictionary<string, IExpression> unaryOperations;
        private readonly Dictionary<(string, string), MethodInfo> methodCache = new Dictionary<(string, string), MethodInfo>();

        public ExpressionEvaluator(Dictionary<string, object> variables)
        {
            this.variables = variables;

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
            };

            unaryOperations = new Dictionary<string, IExpression>
            {
                { "absfunc", new AbsExpression() },
                { "atnfunc", new AtnExpression() },
                { "cosfunc", new CosExpression() },
                { "expfunc", new ExpExpression() },
                { "intfunc", new IntExpression() },
                { "logfunc", new LogExpression() },
                { "rndfunc", new RndExpression() },
                { "sgnfunc", new SgnExpression() },
                { "sinfunc", new SinExpression() },
                { "sqrfunc", new SqrExpression() },
                { "tanfunc", new TanExpression() },
            };
        }

        public override IExpression VisitFunc_([NotNull] BasicParser.Func_Context context)
        {
            if (context.STRINGLITERAL() != null)
            {
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

                switch (varValue)
                {
                    case double doubleValue:
                        return new NumberExpression(doubleValue);
                    case string stringValue:
                        return new StringExpression(stringValue);
                    default:
                        throw new NotImplementedException($"Unhandled variable type: {varValue.GetType()}");
                }
            }

            return HandleUnaryOperations(context);
        }

        private IExpression HandleUnaryOperations(BasicParser.Func_Context context)
        {
            foreach (var operationName in unaryOperations.Keys)
            {
                var unaryContext = InvokeMethodIfExists(context, operationName);
                if (unaryContext != null)
                {
                    var unaryExpression = InvokeMethodIfExists(unaryContext, "expression") as ParserRuleContext;
                    if (unaryExpression != null)
                    {
                        IExpression operand = Visit(unaryExpression.GetChild(0));
                        if (unaryOperations.TryGetValue(operationName, out IExpression operation))
                        {
                            return new LambdaExpression(operation, new IExpression[] { operand });
                        }
                    }
                }
            }

            throw new NotImplementedException($"Unhandled func_ alternative: {context.GetText()}");
        }

        private object InvokeMethodIfExists(object obj, string methodName)
        {
            var key = (obj.GetType().FullName, methodName);
            if (!methodCache.TryGetValue(key, out var method))
            {
                method = obj.GetType().GetMethod(methodName);
                methodCache[key] = method;
            }
            return method?.Invoke(obj, null);
        }

        public override IExpression VisitAddingExpression([NotNull] BasicParser.AddingExpressionContext context)
        {
            return VisitExpressionWithBinaryOperation(context, operations);
        }

        public override IExpression VisitMultiplyingExpression([NotNull] BasicParser.MultiplyingExpressionContext context)
        {
            return VisitExpressionWithBinaryOperation(context, operations);
        }

        public override IExpression VisitExponentExpression([NotNull] BasicParser.ExponentExpressionContext context)
        {
            return VisitExpressionWithBinaryOperation(context, operations);
        }

        public override IExpression VisitExpression([NotNull] BasicParser.ExpressionContext context)
        {
            return VisitExpressionWithBinaryOperation(context, operations);
        }

        private IExpression VisitExpressionWithBinaryOperation(ParserRuleContext context, Dictionary<string, IExpression> ops)
        {
            IExpression left = Visit(context.GetChild(0));

            for (int i = 1; i < context.ChildCount; i += 2)
            {
                string operatorSymbol = context.GetChild(i).GetText();
                if (ops.TryGetValue(operatorSymbol, out IExpression operation))
                {
                    IExpression right = Visit(context.GetChild(i + 1));
                    left = new LambdaExpression(operation, new IExpression[] { left, right });
                }
                else
                {
                    throw new NotImplementedException($"Unhandled operator: {operatorSymbol}");
                }
            }

            return left;
        }
    }
}
