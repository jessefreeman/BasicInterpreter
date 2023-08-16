#region

using Antlr4.Runtime;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

#endregion

namespace JesseFreeman.BasicInterpreter;

public class BasicInterpreter : IBasicInterpreterState
{
    private const int DefaultMaxIterations = 10000;
    private readonly List<(int lineNum, ICommand command)> commands;

    private readonly Dictionary<string, ForCommand> forCommands = new();
    private readonly HashSet<int> lineNums = new();
    public readonly LoopContextManager LoopContextManager = new();
    private readonly Dictionary<string, object> vars;
    private readonly BasicCommandVisitor visitor;
    public Dictionary<int, int> PhysicalToBasicLineNumbers;

    public BasicInterpreter(IOutputWriter writer, IInputReader inputReader)
    {
        vars = new Dictionary<string, object>();
        visitor = new BasicCommandVisitor(this, vars, writer, inputReader);
        commands = new List<(int lineNum, ICommand command)>();
        InputReader = inputReader;
        MaxIterations = DefaultMaxIterations;
    }

    public IReadOnlyList<(int lineNum, ICommand command)> Commands => commands.AsReadOnly();
    public string CurrentVariable { get; private set; }
    public int CurrentPosition { get; private set; }
    public IInputReader InputReader { get; }
    public bool HasEnded { get; set; }
    public int MaxIterations { get; set; } = DefaultMaxIterations;
    public int CurrentCommandIndex { get; private set; }
    public int CurrentLineNumber { get; private set; }

    public void SetCurrentLineNumber(int lineNum)
    {
        CurrentLineNumber = lineNum;
    }

    public void Load(string script)
    {
        if (string.IsNullOrWhiteSpace(script)) return;
        PreprocessInput(script);
        ParseScript(script);
    }

    private void PreprocessInput(string script)
    {
        PhysicalToBasicLineNumbers = new Dictionary<int, int>();
        using var reader = new StringReader(script);
        var physicalLineNum = 1;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var basicLineNum = ExtractBasicLineNumber(line);
            PhysicalToBasicLineNumbers[physicalLineNum] = basicLineNum;
            physicalLineNum++;
        }
    }

    private void ParseScript(string script)
    {
        var inputStream = new AntlrInputStream(script);
        var lexer = new BasicLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new BasicParser(tokenStream);
        parser.ErrorHandler = new ThrowingErrorStrategy(this);
        var tree = parser.prog();

        // Print the parse tree as a string
        Console.WriteLine(tree.ToStringTree(parser));

        foreach (var line in tree.line())
        {
            CurrentLineNumber = int.Parse(line.linenumber().GetText());
            var command = visitor.Visit(line);
            if (command != null)
            {
                if (lineNums.Contains(CurrentLineNumber))
                    throw new InterpreterException(BasicInterpreterError.DuplicateLineNumber);
                commands.Add((CurrentLineNumber, command));
                lineNums.Add(CurrentLineNumber);
            }
        }

        commands.Sort((cmd1, cmd2) => cmd1.lineNum.CompareTo(cmd2.lineNum));

        // Check for unmatched FOR and NEXT commands
        var forStack = new Stack<(int lineNum, string variableName)>();
        foreach (var command in commands)
            if (command.command is CompositeCommand compositeCommand)
                foreach (var innerCommand in compositeCommand.Commands)
                    if (innerCommand is ForCommand forCommand)
                    {
                        forStack.Push((command.lineNum, forCommand.variableName));
                    }
                    else if (innerCommand is NextCommand nextCommand)
                    {
                        // Check that the NextCommand has the same number of variables as the corresponding FOR commands
                        if (nextCommand.VariableNames.Count > forStack.Count)
                            throw new InterpreterException(BasicInterpreterError.NextWithoutFor);

                        // Pop the stack once for each variable in the NextCommand
                        for (var i = 0; i < nextCommand.VariableNames.Count; i++)
                        {
                            if (forStack.Count == 0)
                                throw new InterpreterException(BasicInterpreterError.NextWithoutFor);
                            forStack.Pop();
                        }
                    }

        // Check for unmatched FOR commands
        if (forStack.Count > 0)
        {
            // Get the line number of the unmatched FOR command
            var unmatchedForLineNumber = forStack.Pop().lineNum;
            throw new InterpreterException(BasicInterpreterError.ForWithoutNext)
            {
                Data = {{"line", unmatchedForLineNumber}}
            };
        }
    }


    private int ExtractBasicLineNumber(string line)
    {
        line = line.TrimStart();
        var endIndex = 0;
        while (endIndex < line.Length && char.IsDigit(line[endIndex])) endIndex++;
        var lineNumString = line.Substring(0, endIndex);
        if (int.TryParse(lineNumString, out var lineNum))
            return lineNum;
        throw new InterpreterException(BasicInterpreterError.ParsingError);
    }

    public void Run()
    {
        CurrentCommandIndex = 0;
        CurrentLineNumber = 0;
        HasEnded = false;
        var iterationCount = 0;

        while (CurrentCommandIndex < commands.Count)
        {
            CurrentLineNumber = commands[CurrentCommandIndex].lineNum;
            iterationCount++;
            if (iterationCount > MaxIterations) throw new InterpreterException(BasicInterpreterError.MaxLoopsExceeded);

            var (_, command) = commands[CurrentCommandIndex];
            CurrentPosition = CurrentCommandIndex;

            try
            {
                command.Execute();
            }
            catch (GotoCommandException gotoEx)
            {
                if (iterationCount >= MaxIterations)
                    throw new InterpreterException(BasicInterpreterError.MaxLoopsExceeded);
                JumpToLine(gotoEx.TargetLineNumber);
                continue;
            }
            catch (SkipNextCommandException)
            {
                CurrentCommandIndex += 2;
                continue;
            }

            if (HasEnded) break;
            CurrentCommandIndex++;
        }
    }


    public void JumpToLine(int lineNum)
    {
        var index = commands.FindIndex(cmd => cmd.lineNum == lineNum);
        if (index == -1) throw new InterpreterException(BasicInterpreterError.GoTo);
        CurrentCommandIndex = index;
    }

    public void SetVariable(string name, double value)
    {
        CurrentVariable = name;
        vars[name] = value;
    }

    public double GetVariable(string name)
    {
        CurrentVariable = name;
        return Convert.ToDouble(vars[name]);
    }

    public void JumpToCommandIndex(int index)
    {
        CurrentCommandIndex = index;
    }

    public void RegisterForCommand(string variableName, ForCommand forCommand)
    {
        forCommands[variableName] = forCommand;
    }

    public ForCommand GetCorrespondingForCommand(string variableName)
    {
        forCommands.TryGetValue(variableName, out var forCommand);
        return forCommand;
    }
}