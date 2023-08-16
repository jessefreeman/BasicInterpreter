using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        var writer = new ConsoleOutputWriter()
        {
            TabSize = 4
        };
        
        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);
        var exManager = new ExceptionManager(interpreter, writer);

        var code = "10 FOR I = 1 TO 5\n20 PRINT \"Iteration: \"; I\n30 NEXT\n";

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