#region

using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

#endregion

namespace JesseFreeman.BasicInterpreter.Tests;

public class ForNextTests
{
    private const double Tolerance = 1e-10;
    private readonly ExceptionManager exManager;
    private readonly BasicInterpreter interpreter;
    private readonly StringInputReader reader;
    private readonly StringOutputWriter writer;

    // This constructor will be called before each test case
    public ForNextTests()
    {
        writer = new StringOutputWriter
        {
            TabSize = 4,
            AlignTabs = false
        };
        reader = new StringInputReader(); // or any other suitable implementation for testing
        interpreter = new BasicInterpreter(writer, reader);
        exManager = new ExceptionManager(interpreter, writer);
    }

    /// <summary>
    ///     Tests the functionality of the FOR NEXT loop in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute a simple FOR NEXT loop,
    ///     iterating from 1 to 5 and printing the iteration number.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 PRINT \"Iteration: \"; I\n30 NEXT I\n",
        "Iteration:  1\nIteration:  2\nIteration:  3\nIteration:  4\nIteration:  5\n")]
    public void TestForNextLoop(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of the FOR NEXT loop with a negative step in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute a FOR NEXT loop,
    ///     iterating from 5 to 1 with a step of -1, printing the countdown.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 5 TO 1 STEP -1\n20 PRINT \"Countdown: \"; I\n30 NEXT I\n",
        "Countdown:  5\nCountdown:  4\nCountdown:  3\nCountdown:  2\nCountdown:  1\n")]
    public void TestForNextLoopWithNegativeStep(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of the FOR NEXT loop with user input for the loop limit in C64 BASIC.
    ///     This test will fail initially since the INPUT command is not yet wired up.
    ///     Once implemented, this test will verify that the interpreter can execute a FOR NEXT loop,
    ///     iterating from 1 to a user-defined limit.
    /// </summary>
    [Theory]
    [InlineData("10 INPUT \"Enter loop limit: \"; Limit\n20 FOR I = 1 TO Limit\n30 PRINT I\n40 NEXT I\n",
        "1\n2\n3\n4\n")] // Expected output when user enters 4
    public void TestForNextLoopWithUserInput(string script, string expectedOutput)
    {
        // TODO: Wire up the INPUT command and provide the necessary input for testing

        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result (this assertion will fail until the INPUT command is implemented)
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of nested FOR NEXT loops in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute two nested FOR NEXT loops,
    ///     iterating the outer loop from 1 to 5, and the inner loop from 1 to 3, printing the combination of I and J.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 FOR J = 1 TO 3\n30 PRINT I; \",\"; J\n40 NEXT J\n50 NEXT I\n",
        "1 , 1\n1 , 2\n1 , 3\n2 , 1\n2 , 2\n2 , 3\n3 , 1\n3 , 2\n3 , 3\n4 , 1\n4 , 2\n4 , 3\n5 , 1\n5 , 2\n5 , 3\n")]
    public void TestNestedForNextLoops(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of a FOR NEXT loop in C64 BASIC where the loop variable is altered inside the loop.
    ///     This test verifies that the interpreter can correctly execute the FOR NEXT loop,
    ///     iterating from 1 to 5, but with the loop variable incremented inside the loop, resulting in the odd numbers being
    ///     printed.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 PRINT I\n30 I = I + 1\n40 NEXT I\n", "1\n3\n5\n")]
    public void TestForNextLoopWithVariableAlteration(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of nested FOR NEXT loops using two variables (X and Y) in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute two nested FOR NEXT loops,
    ///     iterating the outer loop from 1 to 3, and the inner loop from 1 to 4, printing the combination of X and Y.
    /// </summary>
    [Theory]
    [InlineData("10 FOR X = 1 TO 3\n20 FOR Y = 1 TO 4\n30 PRINT \"X:\"; X; \" Y:\"; Y\n40 NEXT Y\n50 NEXT X\n",
        "X: 1  Y: 1\nX: 1  Y: 2\nX: 1  Y: 3\nX: 1  Y: 4\nX: 2  Y: 1\nX: 2  Y: 2\nX: 2  Y: 3\nX: 2  Y: 4\nX: 3  Y: 1\nX: 3  Y: 2\nX: 3  Y: 3\nX: 3  Y: 4\n")]
    public void TestNestedForNextLoopsWithTwoVariables(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of nested FOR NEXT loops with both loop variables terminated in the same NEXT statement in
    ///     C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute two nested FOR NEXT loops,
    ///     iterating the outer loop from 1 to 5, and the inner loop from 1 to 3, printing the combination of I and J.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 FOR J = 1 TO 3\n30 PRINT I, J\n40 NEXT J, I\n",
        "1   1\n1   2\n1   3\n2   1\n2   2\n2   3\n3   1\n3   2\n3   3\n4   1\n4   2\n4   3\n5   1\n5   2\n5   3\n")]
    [InlineData("10 FOR I = 1 TO 5\n20 FOR J = 1 TO 3\n30 PRINT \"I:\"; I; \" J:\"; J, \"|\"\n40 NEXT J, I\n",
        "I: 1  J: 1   |\nI: 1  J: 2   |\nI: 1  J: 3   |\nI: 2  J: 1   |\nI: 2  J: 2   |\nI: 2  J: 3   |\nI: 3  J: 1   |\nI: 3  J: 2   |\nI: 3  J: 3   |\nI: 4  J: 1   |\nI: 4  J: 2   |\nI: 4  J: 3   |\nI: 5  J: 1   |\nI: 5  J: 2   |\nI: 5  J: 3   |\n")]
    public void TestNestedForNextLoopsWithSharedNextStatement(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests exception handling for a missing FOR statement in nested FOR NEXT loops in C64 BASIC.
    ///     This test verifies that the interpreter correctly throws an exception when a NEXT statement is encountered without
    ///     a corresponding FOR statement,
    ///     and that the exception type and error message match the expected ones.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 FOR J = 1 TO 3\n30 PRINT I,J\n40 NEXT I, J\n",
        "1  1\n1  1\n2  1\n3  1\n4  1\n5  1\n")]
    public void TestMissingForStatementInNestedForNextLoops(string script, string expectedOutputBeforeException)
    {
        // Arrange: Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Act: Run the interpreter and capture the exception
        var exception = Record.Exception(() => interpreter.Run());

        // Assert: Check that the output before the exception matches the expected output
        Assert.Equal(expectedOutputBeforeException, writer.Output.Trim());

        // Check that the exception is thrown, and its type and message match the expected ones
        Assert.NotNull(exception);
        Assert.IsType<InterpreterException>(exception);

        var interpreterException = (InterpreterException) exception;
        Assert.Equal(BasicInterpreterError.NextWithoutFor, interpreterException.ErrorType);

        var errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.NextWithoutFor];
        var expectedErrorMessage = errorMessageTemplate.Replace("{line}", 40.ToString());

        exManager.HandleException(interpreterException);

        var fullExpectedOutput = expectedErrorMessage;

        Assert.Equal(fullExpectedOutput, writer.Output.Trim()); // Ensure the output is as expected
    }

    /// <summary>
    ///     Tests the functionality of a FOR NEXT loop with conditional GOTO in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute a FOR NEXT loop,
    ///     iterating from 1 to 10, printing the iteration number, and performing a conditional GOTO to exit the loop.
    /// </summary>
    [Theory]
    [InlineData(
        "10 FOR I = 1 TO 10\n20 IF I = 5 THEN GOTO 50\n30 PRINT \"Iteration: \"; I\n40 NEXT I\n50 PRINT \"Loop exited\"\n",
        "Iteration: 1\nIteration: 2\nIteration: 3\nIteration: 4\nLoop exited\n")]
    public void TestForNextLoopWithConditionalGoto(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests the functionality of a FOR NEXT loop with conditional GOTO in C64 BASIC.
    ///     This test verifies that the interpreter can correctly execute a FOR NEXT loop,
    ///     iterating from 1 to 10, printing the iteration number, and performing a conditional GOTO to exit the loop.
    /// </summary>
    [Theory]
    [InlineData(
        "10 FOR I = 1 TO 10\n20 IF I = 5 THEN GOTO 40\n30 PRINT \"Iteration: \"; I\n40 NEXT I\n50 PRINT \"Loop exited\"\n",
        "Iteration:  1\nIteration:  2\nIteration:  3\nIteration:  4\nIteration:  5\nIteration:  6\nIteration:  7\nIteration:  8\nIteration:  9\nIteration:  10\nLoop exited")]
    public void TestForNextLoopWithConditionalGotoNext(string script, string expectedOutput)
    {
        // Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Run the interpreter
        interpreter.Run();

        // Check that the output matches the expected result
        Assert.Equal(expectedOutput, writer.Output);
    }

    /// <summary>
    ///     Tests exception handling for a missing FOR statement in a FOR NEXT loop in C64 BASIC.
    ///     This test verifies that the interpreter correctly throws an exception when a NEXT statement is encountered without
    ///     a corresponding FOR statement,
    ///     and that the exception type and error message match the expected ones.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 PRINT \"Iteration: \"; I\n30 NEXT I\n40 NEXT I REM Error: NEXT without FOR\n",
        "Iteration: 1\nIteration: 2\nIteration: 3\nIteration: 4\n")]
    public void TestMissingForStatementInForNextLoop(string script, string expectedOutputBeforeException)
    {
        // Arrange: Load the BASIC script into the interpreter
        interpreter.Load(script);

        // Act: Run the interpreter and capture the exception
        var exception = Record.Exception(() => interpreter.Run());

        // Assert: Check that the output before the exception matches the expected output
        Assert.Equal(expectedOutputBeforeException, writer.Output.Trim());

        // Check that the exception is thrown, and its type and message match the expected ones
        Assert.NotNull(exception);
        Assert.IsType<InterpreterException>(exception);

        var interpreterException = (InterpreterException) exception;
        Assert.Equal(BasicInterpreterError.NextWithoutFor, interpreterException.ErrorType);

        var errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.NextWithoutFor];
        var expectedErrorMessage = errorMessageTemplate.Replace("{line}", 40.ToString());

        exManager.HandleException(interpreterException);

        var fullExpectedOutput = expectedErrorMessage;

        Assert.Equal(fullExpectedOutput, writer.Output.Trim()); // Ensure the output is as expected
    }

    /// <summary>
    ///     Tests exception handling for a syntax error in a FOR NEXT loop in C64 BASIC.
    ///     This test verifies that the interpreter correctly throws a syntax error exception when a NEXT statement is
    ///     encountered without a loop variable,
    ///     and that the exception type and error message match the expected ones.
    /// </summary>
    [Theory]
    [InlineData("10 FOR I = 1 TO 5\n20 PRINT \"Iteration: \"; I\n30 NEXT REM Syntax Error\n",
        BasicInterpreterError.ParsingError, 30, "Iteration: 1\n")]
    public void TestSyntaxErrorInForNextLoop(string script, BasicInterpreterError expectedErrorType,
        int expectedErrorLineNumber, string expectedOutputBeforeError)
    {
        var exception = Record.Exception(() =>
        {
            interpreter.Load(script);
            interpreter.Run();
        });

        var interpreterException = Assert.IsType<InterpreterException>(exception);
        Assert.Equal(expectedErrorType, interpreterException.ErrorType);

        var errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedErrorType];
        var expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

        exManager.HandleException(interpreterException);

        var fullExpectedOutput = expectedOutputBeforeError + expectedErrorMessage;

        Assert.Equal(fullExpectedOutput, writer.Output.Trim()); // Ensure the output is as expected
    }
}