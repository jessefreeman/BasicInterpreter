using Antlr4.Runtime;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter;

public class BasicInterpreter : IBasicInterpreterState
{
    private readonly BasicCommandVisitor visitor;
    private List<(int lineNumber, ICommand command)> commands;
    private Dictionary<string, object> variables;
    private bool hasEnded = false;
    private int currentCommandIndex = 0;
    private int currentPosition = 0;
    private Stack<LoopContext> loopStack = new Stack<LoopContext>();
    public int CurrentLineNumber => currentLineNumber;

    public IReadOnlyList<(int lineNumber, ICommand command)> Commands => commands.AsReadOnly();

    public void SetCurrentLineNumber(int lineNumber)
    {
        currentLineNumber = lineNumber;
    }

    private int currentLineNumber = 0;
    
    // Field to store the last accessed variable name
    private string currentVariable;
    public Dictionary<int, int> PhysicalToBasicLineNumbers;

    public string CurrentVariable => currentVariable;

    public int CurrentPosition => currentPosition;

    public IInputReader InputReader { get; }

    public bool HasEnded
    {
        get { return hasEnded; }
        set { hasEnded = value; }
    }

    public int MaxIterations { get; set; } = 10000;

    public BasicInterpreter(IOutputWriter writer, IInputReader inputReader)
    {
        variables = new Dictionary<string, object>();
        visitor = new BasicCommandVisitor(this, variables, writer, inputReader);
        commands = new List<(int lineNumber, ICommand command)>();
        InputReader = inputReader;
    }

    public void Load(string script)
    {
        // If the script is empty or contains only whitespace, return without doing anything
        if (string.IsNullOrWhiteSpace(script))
        {
            return;
        }
        
        // Preprocess the input to build a mapping from physical line numbers to BASIC line numbers
        // Preprocess the input to build a mapping from physical line numbers to BASIC line numbers
        PhysicalToBasicLineNumbers = new Dictionary<int, int>();
        using (var reader = new StringReader(script))
        {
            int physicalLineNumber = 1; // Start at 1 for ANTLR's 1-based line numbering
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int basicLineNumber = ExtractBasicLineNumber(line);
                PhysicalToBasicLineNumbers[physicalLineNumber] = basicLineNumber;
                physicalLineNumber++;
            }
        }
        
        var inputStream = new AntlrInputStream(script);
        var lexer = new BasicLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(lexer);
        var parser = new BasicParser(commonTokenStream);

        // Set the custom error strategy
        parser.ErrorHandler = new ThrowingErrorStrategy(this);

        try
        {
            var tree = parser.prog();

            foreach (BasicParser.LineContext line in tree.line())
            {
                currentLineNumber = int.Parse(line.linenumber().GetText());
                ICommand command = visitor.Visit(line);
                if (command != null) // Check if the command is not null
                {
                    if (commands.Any(cmd => cmd.lineNumber == currentLineNumber))
                    {
                        throw new InterpreterException(BasicInterpreterError.DuplicateLineNumber);
                    }
                    commands.Add((currentLineNumber, command));
                }
            }
            commands.Sort((cmd1, cmd2) => cmd1.lineNumber.CompareTo(cmd2.lineNumber));
        }
        catch (RecognitionException ex)
        {
            throw new InterpreterException(BasicInterpreterError.ParsingError);
        }
        
    }

    private int ExtractBasicLineNumber(string line)
    {
        // Trim leading whitespace
        line = line.TrimStart();

        // Find the first space or non-numeric character
        int endIndex = 0;
        while (endIndex < line.Length && char.IsDigit(line[endIndex]))
        {
            endIndex++;
        }

        // Extract the substring containing the line number
        string lineNumberString = line.Substring(0, endIndex);

        // Parse the line number as an integer
        if (int.TryParse(lineNumberString, out int lineNumber))
        {
            return lineNumber;
        }
        else
        {
            // Handle the case where the line number is missing or malformed
            // You could throw an exception, return a sentinel value, or handle this case in some other way
            throw new FormatException($"Failed to extract BASIC line number from line: {line}");
        }
    }

    public void Run()
    {
        currentCommandIndex = 0;
        currentLineNumber = 0; // Reset the current line number
        hasEnded = false;
        int iterationCount = 0;

        while (currentCommandIndex < commands.Count)
        {
            currentLineNumber = commands[currentCommandIndex].lineNumber; // Update the current line number

            iterationCount++;

            if (iterationCount > MaxIterations)
            {
                throw new InvalidOperationException("Maximum number of iterations exceeded");
            }

            var (_, command) = commands[currentCommandIndex];
            currentPosition = currentCommandIndex; // Update the current position

            try
            {
                command.Execute();
            }
            catch (GotoCommandException gotoException)
            {
                if (iterationCount >= MaxIterations)
                {
                    throw new InvalidOperationException("Maximum number of iterations exceeded");
                }
                JumpToLine(gotoException.TargetLineNumber);
                continue;
            }
            catch (SkipNextCommandException)
            {
                currentCommandIndex += 2;
                continue;
            }

            if (hasEnded)
            {
                break;
            }

            currentCommandIndex++;
        }
    }

    public void JumpToLine(int lineNumber)
    {
        var index = commands.FindIndex(cmd => cmd.lineNumber == lineNumber);
        if (index == -1)
        {
            throw new InvalidOperationException($"No such line: {lineNumber}");
        }
        currentCommandIndex = index;
    }

    public void PushLoopContext(LoopContext context)
    {
        loopStack.Push(context);
    }

    public LoopContext PopLoopContext()
    {
        return loopStack.Pop();
    }

    public void SetVariable(string name, double value)
    {
        currentVariable = name; // Store the variable name
        variables[name] = value;
    }

    public double GetVariable(string name)
    {
        currentVariable = name; // Store the variable name
        return Convert.ToDouble(variables[name]);
    }

    public int CurrentCommandIndex => currentCommandIndex;

    public void JumpToCommandIndex(int index)
    {
        currentCommandIndex = index;
    }

    public LoopContext GetLoopContext(string variableName = null)
    {
        if (string.IsNullOrEmpty(variableName))
        {
            // Return the most recent loop context if no variable name is provided
            return loopStack.Peek();
        }
        else
        {
            // Search the loop stack for the context associated with the specified variable name
            foreach (var context in loopStack)
            {
                if (context.VariableName == variableName)
                {
                    return context;
                }
            }

            throw new InvalidOperationException($"No loop context found for variable: {variableName}");
        }
    }

}

