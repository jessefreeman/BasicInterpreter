using Antlr4.Runtime;
using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.Commands;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter
{
    public class BasicInterpreter
    {
        private readonly BasicCommandVisitor visitor;
        private List<(int lineNumber, ICommand command)> commands;
        private Dictionary<string, object> variables;
        private bool hasEnded = false;
        private int currentCommandIndex = 0;
        private int currentPosition = 0;
        private Stack<LoopContext> loopStack = new Stack<LoopContext>();

        public int CurrentLineNumber => commands[currentCommandIndex].lineNumber;
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
            
            var inputStream = new AntlrInputStream(script);
            var lexer = new BasicLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new BasicParser(commonTokenStream);

            // Set the custom error strategy
            parser.ErrorHandler = new ThrowingErrorStrategy();

            try
            {
                var tree = parser.prog();

                foreach (BasicParser.LineContext line in tree.line())
                {
                    int lineNumber = int.Parse(line.linenumber().GetText());
                    ICommand command = visitor.Visit(line);
                    if (command != null) // Check if the command is not null
                    {
                        if (commands.Any(cmd => cmd.lineNumber == lineNumber))
                        {
                            throw new DuplicateLineNumberException(lineNumber);
                        }
                        commands.Add((lineNumber, command));
                    }
                }
                commands.Sort((cmd1, cmd2) => cmd1.lineNumber.CompareTo(cmd2.lineNumber));
            }
            catch (RecognitionException ex)
            {
                throw new ParsingException("Syntax error in script", ex);
            }
            
        }

        public void Run()
        {
            currentCommandIndex = 0;
            hasEnded = false;
            int iterationCount = 0;

            while (currentCommandIndex < commands.Count)
            {
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
            variables[name] = value;
        }

        public double GetVariable(string name)
        {
            return Convert.ToDouble(variables[name]);
        }

        public int CurrentCommandIndex => currentCommandIndex;

        public void JumpToCommandIndex(int index)
        {
            currentCommandIndex = index;
        }

        public void JumpToLineAndPosition(int lineNumber, int position)
        {
            var index = commands.FindIndex(cmd => cmd.lineNumber == lineNumber);
            if (index == -1)
            {
                throw new InvalidOperationException($"No such line: {lineNumber}");
            }
            currentCommandIndex = index + position; // Adjusting the index based on the position within the line
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

        public void JumpToNextStatement(string variableName)
        {
            // Find the corresponding NEXT command for the given variable
            for (int i = currentCommandIndex + 1; i < commands.Count; i++)
            {
                if (commands[i].command is NextCommand nextCommand && nextCommand.VariableName == variableName)
                {
                    currentCommandIndex = i; // Set the current command index to the NEXT command
                    return;
                }
            }
            throw new InvalidOperationException($"No NEXT statement found for variable: {variableName}");
        }

        public bool HasNextStatement(string variableName, int startIndex)
        {
            for (int i = startIndex + 1; i < commands.Count; i++)
            {
                if (commands[i].command is NextCommand nextCommand && nextCommand.VariableName == variableName)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
