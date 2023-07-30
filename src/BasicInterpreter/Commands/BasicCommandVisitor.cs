using JesseFreeman.BasicInterpreter.AntlrGenerated;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Commands
{
    public class BasicCommandVisitor : BasicBaseVisitor<ICommand>
    {
        private Dictionary<string, object> variables;
        private IOutputWriter writer;
        private IInputReader inputReader;
        private BasicInterpreter interpreter;

        public BasicCommandVisitor(BasicInterpreter interpreter, Dictionary<string, object> variables, IOutputWriter writer, IInputReader inputReader)
        {
            this.interpreter = interpreter;
            this.variables = variables;
            this.writer = writer;
            this.inputReader = inputReader;
        }

        public override ICommand VisitProg(BasicParser.ProgContext context)
        {
            // Process each line in the program
            var commands = new List<ICommand>();
            foreach (var line in context.line())
            {
                var command = Visit(line);
                commands.Add(command);
            }

            // Return a CompositeCommand that holds all commands for this program
            return new CompositeCommand(commands);
        }

        public override ICommand VisitLine(BasicParser.LineContext context)
        {
            // Process each statement on the line
            var commands = new List<ICommand>();
            foreach (var amprstmt in context.amprstmt())
            {
                var statement = amprstmt.statement();
                if (statement != null)
                {
                    var command = Visit(statement);
                    commands.Add(command);
                }
            }

            // Return a CompositeCommand that holds all commands for this line
            return new CompositeCommand(commands);
        }

        // Add methods to visit other types of nodes in the parse tree
        // based on the new grammar rules
    }
}
