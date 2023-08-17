#region

using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

#endregion

internal class Program
{
    private static void Main(string[] args)
    {
        var writer = new ConsoleOutputWriter
        {
            TabSize = 4,
            AlignTabs = false
        };

        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);
        var exManager = new ExceptionManager(interpreter, writer);

        // Use this for debugging
        Logger.OutputWriter = writer;
        
        // var code = "10 FOR I = 1 TO 10\n20 IF I = 5 THEN GOTO 50\n30 PRINT \"Iteration: \"; I\n40 NEXT I\n50 PRINT \"Loop exited\"\n";

        var code = "10 FOR I = 1 TO 5\n20 FOR J = 1 TO 3\n30 PRINT I, J\n40 NEXT J, I\n";
        
        // interpreter.MaxIterations = 4;

        try
        {
            interpreter.Load(code);
            interpreter.Run();
        }
        catch (InterpreterException ex)
        {
            // Pass the exception to the error manager for handling
            exManager.HandleException(ex);

            // Rethrow the exception to allow it to propagate up the call stack
            // throw;
        }
    }
}