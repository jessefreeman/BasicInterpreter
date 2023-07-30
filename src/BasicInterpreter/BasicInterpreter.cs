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
        private int CurrentLineNumber => commands[currentCommandIndex].lineNumber;

        public bool HasEnded
        {
            get { return hasEnded; }
            set { hasEnded = value; }
        }

        public BasicInterpreter(IOutputWriter writer, IInputReader inputReader)
        {
            variables = new Dictionary<string, object>();
            visitor = new BasicCommandVisitor(this, variables, writer, inputReader);
            commands = new List<(int lineNumber, ICommand command)>();
            InputReader = inputReader;
        }

        public IInputReader InputReader { get; }

        public void Load(string script)
        {
            var inputStream = new AntlrInputStream(script);
            var lexer = new BasicLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new BasicParser(commonTokenStream);

            // Set the custom error strategy
            parser.ErrorHandler = new ThrowingErrorStrategy();

            try
            {
                var tree = parser.prog(); // Adjusted line

            foreach (BasicParser.LineContext line in tree.line())
            {
                foreach (var amprstmt in line.amprstmt())
                {
                    var statement = amprstmt.statement();
                    if (statement != null && statement.letstmt() != null)
                    {
                        string variableName = statement.letstmt().variableassignment().vardecl().GetText();
                        variables[variableName] = null;
                    }
                }
            }

            foreach (BasicParser.LineContext line in tree.line())
            {
                int lineNumber = int.Parse(line.linenumber().GetText()); // Adjusted line
                ICommand command = visitor.Visit(line);
                if (commands.Any(cmd => cmd.lineNumber == lineNumber))
                {
                    throw new DuplicateLineNumberException(lineNumber);
                }
                commands.Add((lineNumber, command));
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
            while (currentCommandIndex < commands.Count)
            {
                var (_, command) = commands[currentCommandIndex];
                try
                {
                    command.Execute();
                }
                catch (GotoCommandException gotoException)
                {
                    JumpToLine(gotoException.TargetLineNumber);
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


    }
}
