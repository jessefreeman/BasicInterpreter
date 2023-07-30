using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.IO;

class Program
{
    static void Main(string[] args)
    {
        var writer = new ConsoleOutputWriter();
        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);

        var code = "10 PRIN =\"Hello, World!\"";

        interpreter.Load(code);
        interpreter.Run();
    }
}

