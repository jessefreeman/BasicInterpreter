using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests;

public class CommandTests
{
    private StringOutputWriter writer;
    private StringInputReader reader;
    private BasicInterpreter interpreter;
    private readonly ExceptionManager exManager;
    private const double Tolerance = 1e-10;

    // This constructor will be called before each test case
    public CommandTests()
    {
        writer = new StringOutputWriter();
        reader = new StringInputReader(); // or any other suitable implementation for testing
        interpreter = new BasicInterpreter(writer, reader);
        exManager = new ExceptionManager(interpreter, writer);
    }
    
    [Theory]
        [InlineData("10 END", "")]
        [InlineData("10 PRINT \"Hello\"\n20 END", "Hello\n")]
        [InlineData("10 PRINT \"Hello\"\n20 END\n30 PRINT \"World\"", "Hello\n")]
        public void TestEndCommand(string script, string expectedOutput)
        {
            // No exception should be thrown when loading and running the script
            var loadException = Record.Exception(() => interpreter.Load(script));
            Assert.Null(loadException);

            var runException = Record.Exception(() => interpreter.Run());
            Assert.Null(runException);

            // After running the script, the interpreter should have ended
            Assert.True(interpreter.HasEnded);

            // The output should be as expected
            Assert.Equal(expectedOutput, writer.Output);
        }

        [Theory]
        [InlineData("10 PRINT \"Hello, World!\"", "Hello, World!\n")]
        [InlineData("10 PRINT \"\"", "\n")]
        [InlineData("10 PRINT \"12345\"", "12345\n")]
        [InlineData("10 PRINT \"Special characters: !@#$%^&*()\"", "Special characters: !@#$%^&*()\n")]
        [InlineData("10 PRINT \"Multiple\"\n20 PRINT \"Lines\"", "Multiple\nLines\n")] // Multiline string
        public void TestPrintCommand(string script, string expectedOutput)
        {
            // No exception should be thrown when loading and running the script
            var loadException = Record.Exception(() => interpreter.Load(script));
            Assert.Null(loadException);

            var runException = Record.Exception(() => interpreter.Run());
            Assert.Null(runException);

            // The output should be as expected
            Assert.Equal(expectedOutput, writer.Output);
        }

        [Theory]
        [InlineData("10 PRINT 12345", "12345\n")]
        [InlineData("10 PRINT 0", "0\n")]
        [InlineData("10 PRINT -12345", "-12345\n")]
        [InlineData("10 PRINT 123.45", "123.45\n")]
        [InlineData("10 PRINT -123.45", "-123.45\n")]
        public void TestPrintCommandWithNumbers(string script, string expectedOutput)
        {
            // No exception should be thrown when loading and running the script
            var loadException = Record.Exception(() => interpreter.Load(script));
            Assert.Null(loadException);

            var runException = Record.Exception(() => interpreter.Run());
            Assert.Null(runException);

            // The output should be as expected
            Assert.Equal(expectedOutput, writer.Output);
        }

        [Theory]
        [InlineData("10 PRINT 123", "123\n")] // Test with an integer
        [InlineData("10 PRINT 123.0", "123\n")] // Test with a floating point number that is a whole number
        [InlineData("10 PRINT 123.45", "123.45\n")] // Test with a floating point number
        [InlineData("10 LET A = 123\n20 PRINT A", "123\n")] // Test with an integer
        [InlineData("10 LET A = 123.0\n20 PRINT A", "123\n")] // Test with a floating point number that is a whole number
        [InlineData("10 LET A = 123.45\n20 PRINT A", "123.45\n")] // Test with a floating point number
        public void TestPrintCommandNumberFormating(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 PRINT \"Hello, World!\"\n20 GOTO 40\n30 PRINT \"This will not be printed\"\n40 END", "Hello, World!\n")]
        [InlineData("10 GOTO 30\n20 PRINT \"This will not be printed\"\n30 PRINT \"Hello, World!\"\n40 END", "Hello, World!\n")]
        public void TestGotoCommand(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.MaxIterations = 3;
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory]
        [InlineData("10 LET A$ = \"Hello, World!\"\n20 PRINT A$", "Hello, World!\n")]
        [InlineData("10 LET A = 123\n20 PRINT A", "123\n")]
        [InlineData("10 LET A$ = \"Hello, World!\"\n20 PRINT A$\n30 LET A$ = \"Goodbye, World!\"\n40 PRINT A$", "Hello, World!\nGoodbye, World!\n")]
        [InlineData("10 LET A = 123\n20 PRINT A\n30 LET A = 456\n40 PRINT A", "123\n456\n")]
        public void TestLetCommand(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory]
        [InlineData("10 IF 5 > 3 THEN PRINT \"Correct\"", "Correct\n")]
        [InlineData("10 IF 5 < 3 THEN PRINT \"Incorrect\"", "")]
        [InlineData("10 IF 5 = 5 THEN PRINT \"Equal\"", "Equal\n")]
        [InlineData("10 IF 5 <> 5 THEN PRINT \"Not Equal\"", "")]
        [InlineData("10 IF 5 >= 3 THEN PRINT \"Greater or Equal\"", "Greater or Equal\n")]
        [InlineData("10 IF 5 <= 3 THEN PRINT \"Less or Equal\"", "")]
        [InlineData("10 IF 5 AND 3 THEN PRINT \"And Operation\"", "And Operation\n")]
        [InlineData("10 IF 0 AND 3 THEN PRINT \"And Operation\"", "")]
        [InlineData("10 IF 5 OR 0 THEN PRINT \"Or Operation\"", "Or Operation\n")]
        [InlineData("10 IF 0 OR 0 THEN PRINT \"Or Operation\"", "")]
        [InlineData("10 IF NOT 0 THEN PRINT \"Not Operation\"", "Not Operation\n")]
        [InlineData("10 IF NOT 5 THEN PRINT \"Not Operation\"", "")]
        [InlineData("10 IF 5 > 3 THEN 20\n20 PRINT \"Correct\"", "Correct\n")]
        [InlineData("10 IF 5 < 3 THEN 20\n20 PRINT \"Incorrect\"", "")]
        [InlineData("10 IF 5 > 3 THEN 30\n20 PRINT \"Incorrect\"\n30 PRINT \"Correct\"", "Correct\n")]
        [InlineData("10 IF 5 < 3 THEN 30\n20 PRINT \"Correct\"\n30 PRINT \"Incorrect\"", "Incorrect\n")]
        [InlineData("10 IF 5 > 3 THEN PRINT \"First True\"\n20 IF 4 < 6 THEN PRINT \"Second True\"", "First True\nSecond True\n")]
        [InlineData("10 IF 5 < 3 THEN PRINT \"First False\"\n20 IF 4 > 6 THEN PRINT \"Second False\"", "")]
        [InlineData("10 LET A = 5\n20 IF A = 5 THEN PRINT \"Variable Check\"", "Variable Check\n")]
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 IF A > B THEN PRINT \"A is Greater\"", "A is Greater\n")]
        [InlineData("10 IF (5 + 3) * 2 = 16 THEN PRINT \"Math Check\"", "Math Check\n")]
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 IF A + B = 8 THEN PRINT \"Sum Check\"", "Sum Check\n")]
        [InlineData("10 IF 5 < 3 THEN 40\n20 PRINT \"First\"\n30 PRINT \"Second\"\n40 PRINT \"Jumped\"", "Second\nJumped\n")]
        [InlineData("10 IF 5 > 3 THEN 40\n20 PRINT \"Incorrect\"\n30 PRINT \"Still Incorrect\"\n40 PRINT \"Jumped\"", "Jumped\n")]
        public void TestIfThen(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }
        
        [Theory]
        [InlineData("10 FOR A = 1 TO 5\n20 PRINT A\n30 NEXT A", "1\n2\n3\n4\n5\n")] // Basic FOR loop
        [InlineData("10 FOR A = 1 TO 5 STEP 2\n20 PRINT A\n30 NEXT A", "1\n3\n5\n")] // FOR loop with step
        [InlineData("10 FOR A = 5 TO 1 STEP -1\n20 PRINT A\n30 NEXT A", "5\n4\n3\n2\n1\n")] // FOR loop with negative step
        [InlineData("10 FOR A = 1 TO 3\n20 FOR B = 1 TO 2\n30 PRINT A, B\n40 NEXT B\n50 NEXT A", "1 1\n1 2\n2 1\n2 2\n3 1\n3 2\n")] // Nested FOR loop TODO needs to be added back in when comma seporated statements is supported
        [InlineData("10 FOR A = 1 TO 3\n20 PRINT A\n30 NEXT A", "1\n2\n3\n")] // Simple loop from 1 to 3
        [InlineData("10 FOR A = 3 TO 1 STEP -1\n20 PRINT A\n30 NEXT A", "3\n2\n1\n")] // Loop with negative step value
        [InlineData("10 FOR A = 5 TO 5\n20 PRINT A\n30 NEXT A", "5\n")] // Loop with start and end value the same
        [InlineData("10 FOR A = 3 TO 1\n20 PRINT A\n30 NEXT A", "3\n")] // Loop with end value less than start value and no step value
        public void TestForNextOutput(string script, string expectedOutput)
        {
            interpreter.MaxIterations = 100;
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n30 NEXT I\n", "1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n")]
        [InlineData("10 FOR I = 1 TO 3\n20 FOR J = 1 TO 3\n30 PRINT I, J\n40 NEXT J, I\n", "1 1\n1 2\n1 3\n2 1\n2 2\n2 3\n3 1\n3 2\n3 3\n")]
        [InlineData("10 FOR I = 1 TO 3\n20 NEXT I\n", "")]
        public void TestValidLoop(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory] // Expected to FAIL if a NEXT statement is required
        [InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n", "1\n")]
        public void TestMissingNextStatement(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory]
        [InlineData("10 FOR I = 1 TO 10 STEP -1\n20 PRINT I\n30 NEXT I\n", "1\n")] // Updated expected output
        public void TestNegativeStepWithoutProperBounds(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory]
        [InlineData("10 FOR I = 1 TO 5\n20 PRINT I * I\n30 NEXT I\n", "1\n4\n9\n16\n25\n")] // Arithmetic operations
        //[InlineData("10 FOR I = 1 TO 3\n20 PRINT COS(I), SIN(I)\n30 NEXT I\n", "0.5403023059 0.8414709848\n-0.4161468365 0.9092974268\n-0.9899924966 0.1411200081\n")] // Function calls
        //[InlineData("10 FOR I = 1 TO 5\n20 IF I > 3 THEN PRINT I\n30 NEXT I\n", "4\n5\n")] // IF-THEN-ELSE statement
        public void TestLoopWithComplexExpressions(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        [Theory]
        [InlineData("10 LET A = 1\n20 LET B = 2\n30 PRINT A, B", "1 2\n")] // Printing variables
        [InlineData("10 PRINT 1, 2, 3", "1 2 3\n")] // Printing literals
        [InlineData("10 PRINT \"Hello\", \"World!\"", "Hello World!\n")] // Printing strings
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 PRINT A + B, A - B", "8 2\n")] // Printing expressions
        [InlineData("10 PRINT COS(0), SIN(0)", "1 0\n")] // Printing function values
        public void TestPrintWithCommas(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
}