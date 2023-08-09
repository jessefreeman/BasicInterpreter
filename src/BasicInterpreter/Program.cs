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

        var code = "10 FOR A = 1 TO 5\n20 PRINT A\n30 NEXT A";

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

