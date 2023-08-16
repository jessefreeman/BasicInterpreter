using JesseFreeman.BasicInterpreter.Commands;

namespace JesseFreeman.BasicInterpreter;

public class LoopContextManager
{
    private readonly Dictionary<string, Stack<LoopContext>> loopContextsPerVariable = new();

    public void Push(string variableName, LoopContext context)
    {
        if (!loopContextsPerVariable.ContainsKey(variableName))
        {
            loopContextsPerVariable[variableName] = new Stack<LoopContext>();
        }

        loopContextsPerVariable[variableName].Push(context);
    }

    public LoopContext Pop(string variableName)
    {
        return loopContextsPerVariable[variableName].Pop();
    }

    public bool IsEmpty(string variableName)
    {
        return !loopContextsPerVariable.ContainsKey(variableName) || loopContextsPerVariable[variableName].Count == 0;
    }
}

