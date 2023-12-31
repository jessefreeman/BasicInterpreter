﻿#region

using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Commands;

public class ForCommand : ICommand
{
    private readonly IExpression endExpression;
    private readonly BasicInterpreter interpreter;
    private readonly IExpression startExpression;
    private readonly IExpression stepExpression;
    public readonly string variableName;

    public ForCommand(string variableName, IExpression startExpression, IExpression endExpression,
        IExpression stepExpression, BasicInterpreter interpreter)
    {
        this.variableName = variableName;
        this.startExpression = startExpression;
        this.endExpression = endExpression;
        this.stepExpression = stepExpression;
        this.interpreter = interpreter;
    }

    public NextCommand CorrespondingNextCommand { get; set; }

    public void Execute()
    {
        var startValue = (double) startExpression.Evaluate();
        var endValue = (double) endExpression.Evaluate();
        var stepValue = stepExpression != null ? (double) stepExpression.Evaluate() : 1;

        // Log the values here
        Console.WriteLine($"FOR Command: StartValue={startValue}, EndValue={endValue}, StepValue={stepValue}, CommandIndex={interpreter.CurrentCommandIndex + 1}");

        if (stepValue == 0) throw new InterpreterException(BasicInterpreterError.StepValueZero);

        interpreter.SetVariable(variableName, startValue);

        var shouldSkip = stepValue > 0 && startValue > endValue || stepValue < 0 && startValue < endValue;

        var loopContext = new LoopContext(variableName, endValue, stepValue,
            interpreter.CurrentCommandIndex, // Store the current index, not +1
            interpreter.CurrentLineNumber, interpreter.CurrentPosition, shouldSkip);

        interpreter.LoopContextManager.Push(variableName, loopContext); // Push with variable name

        // If the loop should be skipped, jump to the next command after the loop
        if (shouldSkip)
        {
            interpreter.JumpToCommandIndex(interpreter.CurrentCommandIndex + 1);
        }
    }

}