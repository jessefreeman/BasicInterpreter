﻿using JesseFreeman.BasicInterpreter;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

class Program
{
    static void Main(string[] args)
    {
        
        var writer = new ConsoleOutputWriter();
        var reader = new ConsoleInputReader();
        var interpreter = new BasicInterpreter(writer, reader);
        var exManager = new ExceptionManager(interpreter, writer);

        var code = "10 PRINT \"Hello\"\n20 PRINT \"World\"\n20 PRINT \"Again\"";

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

