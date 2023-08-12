using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Exceptions;
using System.Reflection;

namespace JesseFreeman.BasicInterpreter.Evaluators;
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
            { "^", new ExponentiationExpression() },
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
            { "expfunc", new ExponentiationExpression() },
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
                return new VariableExpression(varName, variables);
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

        if (context.LPAREN() != null)
        {
            return Visit(context.expression());
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

        throw new UnsupportedOperationException($"Unhandled func_ alternative: {context.GetText()}");
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

    private IExpression VisitBinaryExpression(ParserRuleContext context, Dictionary<string, IExpression> ops)
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
                throw new UnsupportedOperationException($"Unhandled operator: {operatorSymbol}");
            }
        }

        return left;
    }

    public override IExpression VisitAddingExpression([NotNull] BasicParser.AddingExpressionContext context)
    {
        return VisitBinaryExpression(context, operations);
    }

    public override IExpression VisitMultiplyingExpression([NotNull] BasicParser.MultiplyingExpressionContext context)
    {
        return VisitBinaryExpression(context, operations);
    }

    public override IExpression VisitExponentExpression([NotNull] BasicParser.ExponentExpressionContext context)
    {
        return VisitBinaryExpression(context, operations);
    }

    public override IExpression VisitExpression([NotNull] BasicParser.ExpressionContext context)
    {
        return VisitBinaryExpression(context, operations);
    }

    public override IExpression VisitRelationalExpression([NotNull] BasicParser.RelationalExpressionContext context)
    {
        // Check if the context contains the NOT token
        if (context.GetText().StartsWith("NOT"))
        {
            // Get the expression that follows the NOT token
            var exprContext = context.addingExpression(0);
            if (exprContext == null)
            {
                throw new Exception("Expression context is null after NOT token");
            }

            IExpression operand = Visit(exprContext);

            // Apply the NOT operation
            if (operations.TryGetValue("NOT", out IExpression notOperation))
            {
                return new LambdaExpression(notOperation, new IExpression[] { operand });
            }
            else
            {
                throw new UnsupportedOperationException("NOT operation not found in operations dictionary");
            }
        }

        // Check if the context contains a relational expression
        if (context.relop() == null)
        {
            // If not, treat it as an adding expression
            return VisitAddingExpression(context.addingExpression(0));
        }

        // Get the left and right operands
        var leftContext = context.addingExpression(0);
        var rightContext = context.addingExpression(1);
        if (leftContext == null || rightContext == null)
        {
            throw new Exception("Left or right context is null");
        }

        IExpression left = Visit(leftContext);
        IExpression right = Visit(rightContext);

        // Get the operation
        string operatorSymbol = context.relop().GetText();
        if (!operations.TryGetValue(operatorSymbol, out IExpression operation))
        {
            throw new UnsupportedOperationException($"Unhandled operator: {operatorSymbol}");
        }

        return new LambdaExpression(operation, new IExpression[] { left, right });
    }

}
