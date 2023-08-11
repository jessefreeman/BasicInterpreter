namespace JesseFreeman.BasicInterpreter;

public interface IBasicInterpreterState
{
    int CurrentLineNumber { get; }
    string CurrentVariable { get; }
}