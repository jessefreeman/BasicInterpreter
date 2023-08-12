using JesseFreeman.BasicInterpreter.Commands;

namespace JesseFreeman.BasicInterpreter;

public interface IBasicInterpreterState
{
    int CurrentLineNumber { get; }
    string CurrentVariable { get; }
    void SetCurrentLineNumber(int lineNumber);
}

