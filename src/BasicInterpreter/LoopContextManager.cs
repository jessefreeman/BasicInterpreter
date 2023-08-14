using JesseFreeman.BasicInterpreter.Commands;

namespace JesseFreeman.BasicInterpreter;

public class LoopContextManager
{
    private readonly Stack<LoopContext> loopStack = new();

    public void Push(LoopContext context)
    {
        loopStack.Push(context);
    }

    public LoopContext Pop()
    {
        return loopStack.Pop();
    }

    public LoopContext Peek() // Added Peek method
    {
        if (loopStack.Count == 0)
            throw new InvalidOperationException("Stack is empty");
        return loopStack.Peek();
    }

    public bool IsEmpty()
    {
        return loopStack.Count == 0;
    }
}

