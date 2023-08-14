using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests;

public class ScriptTests
{
    private StringOutputWriter writer;
    private StringInputReader reader;
    private BasicInterpreter interpreter;
    private readonly ExceptionManager exManager;
    private const double Tolerance = 1e-10;

    // This constructor will be called before each test case
    public ScriptTests()
    {
        writer = new StringOutputWriter();
        reader = new StringInputReader(); // or any other suitable implementation for testing
        interpreter = new BasicInterpreter(writer, reader);
        exManager = new ExceptionManager(interpreter, writer);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\n")]
    [InlineData("\t")]
    public void TestEmptyScript(string script)
    {
        var exception = Record.Exception(() => interpreter.Load(script));
        Assert.Null(exception); // Ensure no exception is thrown for empty scripts
    }


    [Theory]
    [InlineData("10 PRINT \"Hello, World!\"")]
    public void TestSingleLineScript(string script)
    {
        var exception = Record.Exception(() => interpreter.Load(script));
        Assert.Null(exception); // Ensure no exception is thrown for valid scripts
    }

    [Theory]
    [InlineData("10 PRINT \"Hello, World!\"\n20 END")]
    public void TestMultiLineScript(string script)
    {
        var exception = Record.Exception(() => interpreter.Load(script));
        Assert.Null(exception); // Ensure no exception is thrown for valid scripts
    }
    
    [Theory]
    [InlineData("10 PRINT \"Hello, World!\"", "Hello, World!\n")]
    public void TestCorrectOutput(string script, string expectedOutput)
    {
        interpreter.Load(script);
        interpreter.Run();
        Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
    }
    
    


    [Theory]
    [InlineData("10 REM This is a comment\n20 PRINT \"Hello, World!\"", "Hello, World!\n")]
    [InlineData("20 PRINT \"Hello, World!\"\n30 REM THIS IS A COMMENT", "Hello, World!\n")]
    public void TestComments(string script, string expectedOutput)
    {
        interpreter.Load(script);
        interpreter.Run();
        Assert.Equal(expectedOutput, writer.Output);
    }
}