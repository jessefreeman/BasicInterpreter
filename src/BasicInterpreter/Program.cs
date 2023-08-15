using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        var writer = new ConsoleOutputWriter();
        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);
        var exManager = new ExceptionManager(interpreter, writer);

        var code = "10 FOR I = 1 TO 10\n20 PRINT I\n30 FOR J = 1 TO 5\n40 PRINT J\n50 NEXT J\n";

        // var code = "10 FOR I = 1 TO 10\n20 FOR I = 1 TO 5\n30 NEXT I\n40 NEXT I\n";
        interpreter.MaxIterations = 4;
        
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