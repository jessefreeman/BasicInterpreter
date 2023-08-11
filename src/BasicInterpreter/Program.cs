using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.IO;

class Program
{
    static void Main(string[] args)
    {
        var writer = new ConsoleOutputWriter();
        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);
        //interpreter.MaxIterations = 3;

        var code = "10 LET A = 3\n20 FOR I = 1 TO A\n30 PRINT I\n40 NEXT I\n";

        interpreter.Load(code);

        try
        {
            interpreter.Run();
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message == "Maximum number of iterations exceeded")
            {
                Console.WriteLine("Error: The program was terminated because it exceeded the maximum number of iterations. This may be due to an infinite loop.");
            }
            else
            {
                // If the exception is not about exceeding max iterations, rethrow it
                throw;
            }
        }

    }
}

