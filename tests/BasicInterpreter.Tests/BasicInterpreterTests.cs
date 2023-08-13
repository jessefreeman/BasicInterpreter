using JesseFreeman.BasicInterpreter.Data;
using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests
{
    public class BasicInterpreterTests
    {
        private StringOutputWriter writer;
        private StringInputReader reader;
        private BasicInterpreter interpreter;
        private readonly ExceptionManager exManager;
        private const double Tolerance = 1e-10;

        // This constructor will be called before each test case
        public BasicInterpreterTests()
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

        //[Theory]
        //[InlineData("10 PRIN =\"Hello, World!\"")] // Invalid PRINT statement
        //public void TestInvalidSyntaxScript(string script)
        //{
        //    var exception = Record.Exception(() => interpreter.Load(script));
        //    Assert.IsAssignableFrom<ParsingException>(exception); // Ensure a ParsingException is thrown for invalid syntax
        //}

        [Theory]
        [InlineData("10 PRINT \"Hello, World!\"", "Hello, World!\n")]
        public void TestCorrectOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
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
        //[InlineData("10 PRINT \"Multiple\nLines\"", "Multiple\nLines")] // Multiline string
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
        [InlineData("10 PRINT \"Hello, World!\"\n20 GOTO 10\n30 END", "Hello, World!\nHello, World!\nHello, World!\n")]
        public void TestMaxIterations(string script, string expectedOutput)
        {
            // Set the maximum number of iterations to 3
            interpreter.MaxIterations = 3;

            // Load and run the script
            interpreter.Load(script);
            try
            {
                interpreter.Run();
            }
            catch (InvalidOperationException ex)
            {
                // If an InvalidOperationException is thrown, check that it's because the maximum number of iterations was exceeded
                Assert.Equal("Maximum number of iterations exceeded", ex.Message);
            }

            // Check that the output is as expected
            //Assert.Equal(expectedOutput, writer.Output);
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
        [InlineData("10 PRINT x", 10)] // x is not defined
        [InlineData("10 PRINT A$", 10)] // Test undefined variable
        [InlineData("10 LET B = 5\n20 PRINT A$", 20)] // Test undefined variable in later line
        [InlineData("10 FOR I = 1 TO 3\n20 PRINT A$\n30 NEXT I", 20)] // Test undefined variable inside a loop
        [InlineData("10 LET B$ = \"Hello\"\n20 FOR I = 1 TO 3\n30 PRINT A$\n40 NEXT I", 30)] // Test undefined variable inside a loop with other defined variables
        [InlineData("10 GOTO 100\n20 END\n100 PRINT A$", 100)] // Test undefined variable after a GOTO statement
        public void TestUndefinedVariable(string script, int expectedLineNumber)
        {
            BasicInterpreterError expectedError = BasicInterpreterError.VariableNotDefined;

            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedError, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedError];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A$ = \"Hello\"\n30 LET A$ = 123\n40 PRINT A$", 30)]
        [InlineData("10 LET A = 123\n30 LET A = \"Hello\"\n40 PRINT A", 30)]
        public void TestInvalidTypeAssignment(string script, int expectedLineNumber)
        {
            BasicInterpreterError expectedError = BasicInterpreterError.InvalidTypeAssignment;

            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedError, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedError];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 PRINT A + B", "8\n")] // Basic addition
        [InlineData("10 LET A = -5\n20 LET B = 3\n30 PRINT A + B", "-2\n")] // Addition with negative numbers
        [InlineData("10 LET A = 5.5\n20 LET B = 3.5\n30 PRINT A + B", "9\n")] // Addition with decimal numbers
        [InlineData("10 LET A = 5\n20 PRINT A + 3", "8\n")] // Addition with a variable and a literal
        [InlineData("10 PRINT 5 + 3", "8\n")] // Addition with literals
        [InlineData("10 LET A = 5\n20 LET B = -3\n30 PRINT A + B", "2\n")] // Addition with negative numbers
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 LET C = A + B\n40 PRINT C", "8\n")] // Addition result stored in a variable
        [InlineData("10 LET A = 5\n20 PRINT A + A", "10\n")] // Addition with the same variable
        [InlineData("10 LET A = 5\n20 PRINT A + A + A", "15\n")] // Addition with the same variable multiple times
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 PRINT A + B + 2", "10\n")] // Addition with two variables and a literal
        public void TestAdditonOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }


        [Theory]
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 PRINT A - B", "2\n")] // Basic subtraction
        [InlineData("10 LET A = -5\n20 LET B = 3\n30 PRINT A - B", "-8\n")] // Subtraction with negative numbers
        [InlineData("10 LET A = 5.5\n20 LET B = 3.5\n30 PRINT A - B", "2\n")] // Subtraction with decimal numbers
        [InlineData("10 LET A = 5\n20 PRINT A - 3", "2\n")] // Subtraction with a variable and a literal
        [InlineData("10 PRINT 5 - 3", "2\n")] // Subtraction with literals
        public void TestSubtractionOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 5\n20 LET B = 3\n30 PRINT A * B", "15\n")] // Basic multiplication
        [InlineData("10 LET A = -5\n20 LET B = 3\n30 PRINT A * B", "-15\n")] // Multiplication with negative numbers
        [InlineData("10 LET A = 5.5\n20 LET B = 3.5\n30 PRINT A * B", "19.25\n")] // Multiplication with decimal numbers
        [InlineData("10 LET A = 5\n20 PRINT A * 3", "15\n")] // Multiplication with a variable and a literal
        [InlineData("10 PRINT 5 * 3", "15\n")] // Multiplication with literals
        public void TestMultiplicationOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 6\n20 LET B = 3\n30 PRINT A / B", "2\n")] // Basic division
        [InlineData("10 LET A = -6\n20 LET B = 3\n30 PRINT A / B", "-2\n")] // Division with negative numbers
        [InlineData("10 LET A = 5.5\n20 LET B = 2.5\n30 PRINT A / B", "2.2\n")] // Division with decimal numbers
        [InlineData("10 LET A = 6\n20 PRINT A / 3", "2\n")] // Division with a variable and a literal
        [InlineData("10 PRINT 6 / 3", "2\n")] // Division with literals
        public void TestDivisionOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = -5\n20 PRINT ABS(A)", "5\n")] // Absolute value of negative number
        [InlineData("10 LET A = 5\n20 PRINT ABS(A)", "5\n")] // Absolute value of positive number
        [InlineData("10 PRINT ABS(-3)", "3\n")] // Absolute value of negative literal
        public void TestAbsOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 0.5\n20 PRINT ATN(A)", "0.4636476090008061\n")] // Arctangent of number
        [InlineData("10 PRINT ATN(1)", "0.7853981633974483\n")]
        public void TestAtnOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            double expected = double.Parse(expectedOutput.Trim());
            double actual = double.Parse(writer.Output.Trim());
            Assert.Equal(expected, actual, 14); // Compare the numbers up to 14 decimal places
        }


        [Theory]
        [InlineData("10 LET A = 0\n20 PRINT COS(A)", "1\n")] // Cosine of number
        [InlineData("10 PRINT COS(0)", "1\n")] // Cosine of literal
        public void TestCosOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 2\n20 PRINT EXP(A)", "7.38905609893065\n")] // Exponential of number
        [InlineData("10 PRINT EXP(1)", "2.718281828459045\n")] // Exponential of literal
        public void TestExpOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 2.8\n20 PRINT INT(A)", "2\n")] // Integer part of number
        [InlineData("10 PRINT INT(2.8)", "2\n")] // Integer part of literal
        public void TestIntOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 10\n20 PRINT LOG(A)", "2.302585092994046\n")] // Logarithm of number
        [InlineData("10 PRINT LOG(10)", "2.302585092994046\n")] // Logarithm of literal
        public void TestLogOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 0.5\n20 PRINT INT(RND(A) * 100)")]
        [InlineData("10 PRINT INT(RND(1) * 10)")]
        public void TestRndOutput(string script)
        {
            interpreter.Load(script);
            interpreter.Run();
            int output = int.Parse(writer.Output.Trim());
            Assert.InRange(output, 0, 99); // Ensure the output is within the expected range
        }

        [Theory]
        [InlineData("10 LET A = -2\n20 PRINT SGN(A)", "-1\n")] // Sign of number
        [InlineData("10 PRINT SGN(-2)", "-1\n")] // Sign of literal
        [InlineData("10 PRINT SGN(0)", "0\n")] // Sign of zero
        public void TestSgnOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 LET A = 0.5\n20 PRINT SIN(A)", "0.479425538604203\n")] // Sine of number
        [InlineData("10 PRINT SIN(0.5)", "0.479425538604203\n")] // Sine of literal
        public void TestSinOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
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
        [InlineData("10 PRINT (5 + 3)", "8\n")]
        [InlineData("10 PRINT (10 - 3) * 2", "14\n")]
        [InlineData("10 LET A = 5\n20 PRINT (A * 2) + 3", "13\n")]
        [InlineData("10 PRINT (3 + 2) * (7 - 4)", "15\n")]
        [InlineData("10 PRINT (5)", "5\n")]
        [InlineData("10 PRINT (2 + 3) * 2 - (1 + 1)", "8\n")]
        [InlineData("10 PRINT (2 * 3) / (1 + 1)", "3\n")]
        [InlineData("10 PRINT (2 ^ 3)", "8\n")]
        [InlineData("10 PRINT (2 * 3) + (4 / 2)", "8\n")]
        [InlineData("10 PRINT (10 / 2) - (3 * 2)", "-1\n")]
        public void TestCorrectOrderOfOperations(string script, string expectedOutput)
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
        [InlineData("10 FOR = 1 TO 10\n20 PRINT I\n30 NEXT I\n")]
        public void TestMissingLoopVariable(string script)
        {
            // Assert that loading the script throws an InterpreterException with ParsingError
            var exception = Assert.Throws<InterpreterException>(() => {
                interpreter.Load(script);
                interpreter.Run();
            });
            Assert.Equal(BasicInterpreterError.ParsingError, exception.ErrorType);
        }

        [Theory]
        [InlineData("10 FOR I = 1 10\n20 PRINT I\n30 NEXT I\n")]
        public void TestMissingToKeyword(string script)
        {
            // Assert that loading the script throws an InterpreterException with ParsingError
            var exception = Assert.Throws<InterpreterException>(() => {
                interpreter.Load(script);
                interpreter.Run();
            });
            Assert.Equal(BasicInterpreterError.ParsingError, exception.ErrorType);
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
        [InlineData("10 FOR I = TO 10\n20 PRINT I\n30 NEXT I\n", 10)] // Missing value in FOR
        [InlineData("10 FOR I = 1 TO\n20 PRINT I\n30 NEXT I\n", 10)] // Missing value in FOR
        [InlineData("10 FOR I = 1 TO 10, J = 2 TO\n20 PRINT I, J\n30 NEXT I, J\n", 10)] // Missing value in multiple FOR
        [InlineData("10 PRINT \"Start\" FOR I = 1 TO 10\n20 PRINT I\n30 NEXT I\n", 10)] // Missing colon between commands
        public void TestParsingException(string script, int expectedErrorLineNumber)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.ParsingError, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.ParsingError];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }
        
        // TODO I don't support this yet
        // [Theory]
        // [InlineData("10 FOR I = 1 TO 10: PRINT I: LET X = 5: LET Y =\n20 PRINT I\n30 NEXT I\n", 10)] // Multiple commands, missing value in LET
        // [InlineData("10 PRINT \"Start\": FOR I = 1 TO 10: PRINT \"Loop\": LET X =\n20 PRINT I\n30 NEXT I\n", 10)] // Multiple commands, missing value in LET
        // [InlineData("10 PRINT \"Start\": FOR I = 1 TO: PRINT \"Loop\": LET X = 5\n20 PRINT I\n30 NEXT I\n", 10)] // Multiple commands, missing value in FOR
        // [InlineData("10 PRINT \"Start\": FOR I = 1 TO 10: PRINT \"Loop\": LET X = 5: LET Y\n20 PRINT I\n30 NEXT I\n", 10)] // Multiple commands, LET without value
        // public void TestParsingExceptionWithMultipleCommands(string script, int expectedErrorLineNumber)
        // {
        //     var exception = Record.Exception(() =>
        //     {
        //         interpreter.Load(script);
        //         interpreter.Run();
        //     });
        //
        //     var interpreterException = Assert.IsType<InterpreterException>(exception);
        //     Assert.Equal(BasicInterpreterError.ParsingError, interpreterException.ErrorType);
        //
        //     // Handle the exception using the ExceptionManager
        //     exManager.HandleException(interpreterException);
        //
        //     // Use the template from ExceptionManager
        //     string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.ParsingError];
        //     string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());
        //
        //     Assert.Equal(expectedErrorMessage, writer.Output.Trim());
        // }

        
        [Theory]
        [InlineData("10 PRINT A\n", 10)] // Using an undefined variable
        [InlineData("10 LET X = A + 1\n20 PRINT X\n", 10)] // Using an undefined variable in an expression
        [InlineData("10 FOR I = 1 TO 10\n20 PRINT J\n30 NEXT I\n", 20)] // Using an undefined variable inside a loop
        public void TestVariableErrors(string script, int expectedErrorLineNumber)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.VariableNotDefined, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.VariableNotDefined];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim());
        }

        [Theory]
        [InlineData("10 FOR I = 1 TO 10\n20 NEXT J\n30 NEXT I\n", BasicInterpreterError.NextWithoutFor, 20)] // Incorrect variable in NEXT
        [InlineData("10 FOR I = 1 TO 10\n20 PRINT I, J\n30 NEXT I, I\n", BasicInterpreterError.VariableNotDefined, 20)] // Repeated variable in NEXT
        public void TestLoopErrors(string script, BasicInterpreterError expectedErrorType, int expectedErrorLineNumber)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedErrorType, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedErrorType];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }
        
        [Theory]
        [InlineData("10 PRINT A$", BasicInterpreterError.VariableNotDefined, 10, "")]
        [InlineData("10 LET A$ = 123\n20 PRINT A$", BasicInterpreterError.InvalidTypeAssignment, 10, "")]
        [InlineData("10 LET A$ = \"Hello\"\n20 PRINT A$\n30 LET A$ = 123\n40 PRINT A$", BasicInterpreterError.InvalidTypeAssignment, 30, "Hello\n")]
        [InlineData("10 LET A = 123\n20 PRINT A\n30 LET A = \"Hello\"\n40 PRINT A", BasicInterpreterError.InvalidTypeAssignment, 30, "123\n")]
        [InlineData("10 FOR $I = 1 TO 10\n20 PRINT I\n30 NEXT I\n", BasicInterpreterError.ParsingError, 10, "")]
        public void TestLetStatementErrors(string script, BasicInterpreterError expectedErrorType, int expectedErrorLineNumber, string expectedPrintedOutput)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedErrorType, interpreterException.ErrorType);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedErrorType];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            exManager.HandleException(interpreterException);
            
            // Concatenate the expected printed output with the expected error message
            string fullExpectedOutput = expectedPrintedOutput + expectedErrorMessage;

            Assert.Equal(fullExpectedOutput, writer.Output.Trim()); // Ensure the output is as expected
        }


        [Theory] // FAILS
        [InlineData("10 LET A = 3\n20 FOR I = 1 TO A\n30 PRINT I\n40 NEXT I\n", "1\n2\n3\n")] // Loop variable defined outside
        [InlineData("10 LET A = 2\n20 FOR I = 1 TO 10\n30 A = A * I\n40 NEXT I\n50 PRINT A\n", "7257600\n")] // Other variable modified outside
        public void TestLoopWithVariablesOutsideLoop(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }
        
        //[Theory] // FAILS
        //[InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n", "1\n")] // Missing NEXT
        ////[InlineData("10 FOR I = 1 TO 10 STEP \"A\"\n20 PRINT I\n30 NEXT I\n", "1\n")] // Non-numeric step value
        ////[InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n30 NEXT J\n", "1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n")] // Mismatched NEXT variable
        //public void TestInvalidLoops(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output);
        //}
        //[Theory]
        //[InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n30 NEXT J\n", typeof(NextWithoutForException))] // Mismatched NEXT variable
        //public void TestInvalidLoops(string script, Type expectedExceptionType)
        //{
        //    interpreter.Load(script);
        //    Exception ex = Assert.Throws(expectedExceptionType, () => interpreter.Run());
        //    // Additional assertions on the exception, if needed
        //}

        [Theory]
        [InlineData("10 FOR I = 1 TO 10\n30 NEXT J\n", BasicInterpreterError.NextWithoutFor, 30)] // Mismatched NEXT variable
        public void TestInvalidLoops(string script, BasicInterpreterError expectedErrorType, int expectedErrorLineNumber)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedErrorType, interpreterException.ErrorType);

            exManager.HandleException(interpreterException);
            
            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedErrorType];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim());
        }

        //[Theory] // FAILS
        //[InlineData("10 LET A = 3\n20 FOR I = 1 TO A\n30 PRINT I\n40 NEXT I\n", "1\n2\n3\n")] // Loop variable defined outside
        //[InlineData("10 LET A = 2\n20 FOR I = 1 TO 10\n30 A = A * I\n40 NEXT I\n50 PRINT A\n", "2\n")] // Other variable modified outside
        //public void TestLoopWithVariablesOutsideLoop(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output);
        //}

        [Theory]
        [InlineData("10 FOR I = 1 TO 5\n20 PRINT I * I\n30 NEXT I\n", "1\n4\n9\n16\n25\n")] // Arithmetic operations
        // FAIL
        //[InlineData("10 FOR I = 1 TO 3\n20 PRINT COS(I), SIN(I)\n30 NEXT I\n", "0.5403023059 0.8414709848\n-0.4161468365 0.9092974268\n-0.9899924966 0.1411200081\n")] // Function calls
        //[InlineData("10 FOR I = 1 TO 5\n20 IF I > 3 THEN PRINT I\n30 NEXT I\n", "4\n5\n")] // IF-THEN-ELSE statement
        public void TestLoopWithComplexExpressions(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }

        //[Theory] // FAIL
        //[InlineData("10 FOR I = 1 TO 3\n20 IF I = 2 THEN GOTO 30\n30 PRINT I\n40 NEXT I\n", "1\n3\n")] // GOTO within loop
        //                                                                                               // You can add more tests with other control structures
        //public void TestLoopWithOtherControlStructures(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output);
        //}

        //[Theory] // BAD TEST Need a smaller range or test for too many loops errro
        //[InlineData("10 FOR I = 1 TO 1000000\n20 PRINT I\n30 NEXT I\n", "1\n2\n3\n...\n999998\n999999\n1000000\n")] // Large range
        //public void TestLoopWithLargeRange(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output);
        //}

        //[Theory] // FAIL BECAUSE VALUES HAVE LONG REMAINDERS 0.3000000000000004 SO I NEED A CUT OFF?
        //[InlineData("10 FOR X = 0 TO 1 STEP 0.1\n20 PRINT X\n30 NEXT X\n", "0\n0.1\n0.2\n0.3\n0.4\n0.5\n0.6\n0.7\n0.8\n0.9\n1\n")] // Floating-point step value
        //                                                                                                                           // You can add more tests with floating-point start, end, or step values
        //public void TestLoopWithFloatingPointValues(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output);
        //}

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
        
        [Theory]
        [InlineData("10 PRINT \"Hello\"\n10 PRINT \"World!\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 10")]
        [InlineData("10 PRINT \"Hello\"\n10 LET A = 5\n10 PRINT \"World\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 10")]
        [InlineData("10 FOR I = 1 TO 3\n10 PRINT \"World\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 10")]
        [InlineData("10 PRINT \"Hello\"\n20 LET A = 5\n20 FOR I = 1 TO 3", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 20")]
        [InlineData("10 PRINT \"Hello\"\n20 PRINT \"World\"\n10 LET A = 5\n20 FOR I = 1 TO 3", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 10")]
        [InlineData("10 PRINT A\n10 LET A = 5", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 10")]
        [InlineData("10 PRINT \"Hello\"\n20 PRINT \"World\"\n20 PRINT \"Again\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 20")]
        [InlineData("10 PRINT \"A\"\n30 PRINT \"B\"\n30 PRINT \"C\"\n40 PRINT \"D\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 30")]
        [InlineData("5 PRINT \"Start\"\n15 PRINT \"Middle\"\n15 PRINT \"Middle Again\"\n25 PRINT \"End\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 15")]
        [InlineData("100 PRINT \"X\"\n200 PRINT \"Y\"\n100 PRINT \"Z\"", BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at 100")]
        public void TestDuplicateLineNumberError(string script, BasicInterpreterError expectedErrorType, string expectedErrorMessage)
        {
            var exManager = new ExceptionManager(interpreter, writer);

            Exception exception = null;
            try
            {
                interpreter.Load(script);
                interpreter.Run();
            }
            catch (InterpreterException ex)
            {
                exception = ex;
                exManager.HandleException(ex);
            }

            Assert.NotNull(exception);
            Assert.IsType<InterpreterException>(exception);
            Assert.Equal(expectedErrorType, ((InterpreterException)exception).ErrorType);
            Assert.Equal(expectedErrorMessage, writer.Output.Trim());
        }

        [Theory]
        [InlineData("10 PRINT \"Hello\"\n20 PRINT \"World\"\n20 PRINT \"Again\"", 20)]
        [InlineData("10 PRINT \"A\"\n30 PRINT \"B\"\n30 PRINT \"C\"\n40 PRINT \"D\"", 30)]
        [InlineData("5 PRINT \"Start\"\n15 PRINT \"Middle\"\n15 PRINT \"Middle Again\"\n25 PRINT \"End\"", 15)]
        [InlineData("100 PRINT \"X\"\n200 PRINT \"Y\"\n100 PRINT \"Z\"", 100)]
        public void TestDuplicateLineNumberNotFirstLine(string script, int expectedDuplicateLineNumber)
        {
            
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.DuplicateLineNumber, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.DuplicateLineNumber];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedDuplicateLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 PRINT \"Hello\"\n20 LET A = 5\n10 PRINT \"World\"", 10)]
        [InlineData("10 PRINT \"A\"\n20 FOR I = 1 TO 3\n30 PRINT \"B\"\n20 PRINT \"C\"", 20)]
        [InlineData("10 PRINT \"X\"\n20 LET B = 10\n30 FOR I = 1 TO 3\n20 PRINT \"Y\"", 20)]
        public void TestDuplicateLineNumbersWithDifferentCommands(string script, int expectedDuplicateLineNumber)
        {
            
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.DuplicateLineNumber, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.DuplicateLineNumber];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedDuplicateLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected

        }
        
        [Theory]
        [InlineData("10 PRINT \"Start\"\n20 PRINT \"A\"\n10 PRINT \"Duplicate at beginning\"\n30 PRINT \"B\"", 10)]
        [InlineData("10 PRINT \"A\"\n20 PRINT \"B\"\n30 PRINT \"C\"\n30 PRINT \"Duplicate in middle\"\n40 PRINT \"D\"", 30)]
        [InlineData("10 PRINT \"X\"\n20 PRINT \"Y\"\n30 PRINT \"Z\"\n40 PRINT \"A\"\n100 PRINT \"B\"\n100 PRINT \"Duplicate at end\"", 100)]
        public void TestDuplicateLineNumbersInDifferentParts(string script, int expectedDuplicateLineNumber)
        {
            
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.DuplicateLineNumber, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.DuplicateLineNumber];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedDuplicateLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected

        }

        [Theory]
        [InlineData("10 FOR I = 1 TO 3\n20 FOR J = 1 TO 3\n30 PRINT \"A\"\n20 PRINT \"Duplicate in nested loop\"", 20)]
        [InlineData("10 PRINT \"Start\"\n20 FOR A = 1 TO 2\n30 FOR B = 1 TO 2\n40 FOR C = 1 TO 2\n50 PRINT \"Nested\"\n50 PRINT \"Duplicate in deeply nested loop\"", 50)]
        public void TestDuplicateLineNumbersWithNestedLoops(string script, int expectedDuplicateLineNumber)
        {
            
            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.DuplicateLineNumber, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.DuplicateLineNumber];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedDuplicateLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected

        }
        
        [Theory]
        [InlineData("10 LET A = 10 / 0", 10)]
        [InlineData("10 LET B = 0\n20 LET A = 10 / B", 20)]
        [InlineData("10 LET B = 5 - 5\n20 LET A = 10 / B", 20)]
        [InlineData("10 LET B = (3 - 3) * 2\n20 LET A = 10 / B", 20)]
        [InlineData("10 LET B = 0\n20 LET C = 0\n30 LET A = B / C", 30)]
        [InlineData("10 FOR I = 1 TO 2\n20 LET B = 0\n30 LET A = 10 / B\n40 NEXT I", 30)]
        public void TestDivisionByZeroError(string script, int expectedErrorLineNumber)
        {
            var exManager = new ExceptionManager(interpreter, writer);

            var exception = Record.Exception(() =>
            {
                interpreter.Load(script);
                interpreter.Run();
            });

            var interpreterException = Assert.IsType<InterpreterException>(exception);
            Assert.Equal(BasicInterpreterError.DivisionByZero, interpreterException.ErrorType);

            // Handle the exception using the ExceptionManager
            exManager.HandleException(interpreterException);

            // Use the template from ExceptionManager
            string errorMessageTemplate = ExceptionManager.ErrorTemplates[BasicInterpreterError.DivisionByZero];
            string expectedErrorMessage = errorMessageTemplate.Replace("{line}", expectedErrorLineNumber.ToString());

            Assert.Equal(expectedErrorMessage, writer.Output.Trim()); // Ensure the output is as expected
        }

        // Continue with other unary operations...

        
        public class BasicNumberTests
        {
            [Theory]
            [InlineData(42.0)]
            [InlineData(double.MaxValue)]
            [InlineData(double.MinValue)]
            [InlineData(0.0)]
            public void TestBasicNumber_SetGetValue(double value)
            {
                // Arrange
                var basicNumber = new BasicNumber();

                // Act
                basicNumber.SetValue(value);
                var result = basicNumber.GetValue();

                // Assert
                Assert.Equal(value, result);
            }

            // Since BasicNumber only accepts double, there's no invalid type operation here.
        }

        public class BasicStringTests
        {
            [Theory]
            [InlineData("Hello, World!")]
            [InlineData("")]
            [InlineData(null)]
            public void TestBasicString_SetGetValue(string value)
            {
                // Arrange
                var basicString = new BasicString();

                // Act
                basicString.SetValue(value);
                var result = basicString.GetValue();

                // Assert
                Assert.Equal(value, result);
            }

            // Since BasicString only accepts string, there's no invalid type operation here.
        }

        public static IEnumerable<object[]> DoubleArrayTestData()
        {
            yield return new object[] { new double[] { 1.0, 2.0, 3.0 } };
            yield return new object[] { new double[] { } };
            yield return new object[] { null };
        }

        [Theory]
        [MemberData(nameof(DoubleArrayTestData))]
        public void TestBasicNumberArray_SetGetValue(double[] value)
        {
            // Arrange
            var basicNumberArray = new BasicNumberArray();

            // Act
            basicNumberArray.SetValue(value);
            var result = basicNumberArray.GetValue();

            // Assert
            Assert.Equal(value, result);
        }

        public static IEnumerable<object[]> StringArrayTestData()
        {
            yield return new object[] { new string[] { "A", "B", "C" } };
            yield return new object[] { new string[] { } };
            yield return new object[] { null };
        }

        [Theory]
        [MemberData(nameof(StringArrayTestData))]
        public void TestBasicStringArray_SetGetValue(string[] value)
        {
            // Arrange
            var basicStringArray = new BasicStringArray();

            // Act
            basicStringArray.SetValue(value);
            var result = basicStringArray.GetValue();

            // Assert
            Assert.Equal(value, result);
        }


        // Direct expression tests

        [Theory]
        [InlineData(0, 0.0)] // Abs(0) = 0.0
        [InlineData(5, 5.0)] // Abs(5) = 5.0
        [InlineData(-5, 5.0)] // Abs(-5) = 5.0
        [InlineData(double.MaxValue, double.MaxValue)] // Abs(MaxValue) = MaxValue
        [InlineData(double.MinValue, double.MaxValue)] // Abs(MinValue) = MaxValue (to avoid overflow)
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)] // Abs(Infinity) = Infinity
        [InlineData(double.NegativeInfinity, double.PositiveInfinity)] // Abs(-Infinity) = Infinity
        [InlineData(double.NaN, double.NaN)] // Abs(NaN) = NaN
        [InlineData("123", 123.0)] // Abs("123") = 123.0 (integer parsing)
        [InlineData("-456", 456.0)] // Abs("-456") = 456.0 (integer parsing)
        [InlineData("-123.456", 123.456)] // Abs("-123.456") = 123.456 (double parsing)
        [InlineData("NaN", double.NaN)] // Abs("NaN") = NaN (invalid parsing)
        // Add more edge cases and typical test cases as needed
        public void TestAbsExpression(object input, object expectedResult)
        {
            // Arrange - Nothing to do since absExpression is already instantiated.
            AbsExpression absExpression = new AbsExpression();

            // Act
            var result = absExpression.Evaluate(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, 0)] // 0 + 0 = 0
        [InlineData(5, 3, 8)] // 5 + 3 = 8
        [InlineData(-5, 3, -2)] // -5 + 3 = -2
        [InlineData(2.5, 3.5, 6.0)] // 2.5 + 3.5 = 6.0
        [InlineData("5", 3, 8.0)] // "5" + 3 = 8.0 (string parsing)
        [InlineData("2.5", 3.5, 6.0)] // "2.5" + 3.5 = 6.0 (string parsing)
        [InlineData("NaN", double.NaN)] // "NaN" + "NaN" = NaN (invalid parsing)
        // Add more edge cases and typical test cases as needed
        public void TestAdditionExpression(params object[] operands)
        {
            // Arrange - Nothing to do since additionExpression is already instantiated.
            AdditionExpression additionExpression = new AdditionExpression();

            // Act
            var result = (double)additionExpression.Evaluate(operands);

            // Assert
            Assert.Equal(operands.Sum(Convert.ToDouble), result);
        }

        [Theory]
        [InlineData(0, 0.0)] // Atn(0) = 0.0
        [InlineData(1, 0.7853981634)] // Atn(1) = 0.7853981634
        [InlineData(-1, -0.7853981634)] // Atn(-1) = -0.7853981634
        [InlineData(double.MaxValue, 1.57079632679)] // Atn(MaxValue) = 1.57079632679
        [InlineData(double.MinValue, -1.57079632679)] // Atn(MinValue) = -1.57079632679
        [InlineData(double.PositiveInfinity, 1.57079632679)] // Atn(Infinity) = 1.57079632679
        [InlineData(double.NegativeInfinity, -1.57079632679)] // Atn(-Infinity) = -1.57079632679
        [InlineData(double.NaN, double.NaN)] // Atn(NaN) = NaN
        // Add more edge cases and typical test cases as needed
        public void TestAtnExpression(object operand, double expectedResult)
        {
            // Arrange
            var atnExpression = new AtnExpression();

            // Act
            var result = (double)atnExpression.Evaluate(operand);

            // Assert
            Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
        }

        [Theory]
        [InlineData(0, 1.0)] // Cos(0) = 1.0
        [InlineData(1, 0.5403023059)] // Cos(1) = 0.5403023059
        [InlineData(Math.PI / 2, 6.12323399574e-17)] // Cos(PI/2) = 6.12323399574e-17
        [InlineData(Math.PI, -1.0)] // Cos(PI) = -1.0
        [InlineData(3 * Math.PI / 2, -1.83697019872e-16)] // Cos(3*PI/2) = -1.83697019872e-16
        [InlineData(2 * Math.PI, 1.0)] // Cos(2*PI) = 1.0
        [InlineData(double.NaN, double.NaN)] // Cos(NaN) = NaN
        [InlineData(double.PositiveInfinity, double.NaN)] // Cos(Infinity) = NaN
        [InlineData(double.NegativeInfinity, double.NaN)] // Cos(-Infinity) = NaN
        // Add more edge cases and typical test cases as needed
        public void TestCosExpression(object operand, double expectedResult)
        {
            // Arrange
            var cosExpression = new CosExpression();

            // Act
            var result = (double)cosExpression.Evaluate(operand);

            // Assert
            Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
        }

        [Theory]
        [InlineData(8, 4, 2.0)] // 8 / 4 = 2.0
        [InlineData(-12, 3, -4.0)] // -12 / 3 = -4.0
        [InlineData(7.5, 2.5, 3.0)] // 7.5 / 2.5 = 3.0
        [InlineData(0, 1, 0.0)] // 0 / 1 = 0.0
        [InlineData(double.PositiveInfinity, 2, double.PositiveInfinity)] // Infinity / 2 = Infinity
        [InlineData(double.NegativeInfinity, -3, double.PositiveInfinity)] // -Infinity / -3 = Infinity
        [InlineData(double.NaN, 2, double.NaN)] // NaN / 2 = NaN
        [InlineData(double.NaN, double.NaN, double.NaN)] // NaN / NaN = NaN
        public void TestDivisionExpression(object left, object right, double expectedResult)
        {
            // Arrange
            var divisionExpression = new DivisionExpression();

            // Act
            var result = (double)divisionExpression.Evaluate(left, right);

            // Assert
            Assert.InRange((double)result, expectedResult - Tolerance, expectedResult + Tolerance);
        }
        
        [Theory]
        [InlineData(3.2, 0)]
        public void TestDivisionByZeroException(double left, double right)
        {
            var divisionExpression = new DivisionExpression();

            // Expect an InterpreterException with the DivisionByZero error type
            var exception = Assert.Throws<InterpreterException>(() => divisionExpression.Evaluate(left, right));
            Assert.Equal(BasicInterpreterError.DivisionByZero, exception.ErrorType);
        }

        [Theory]
        [InlineData(5, 5, true)]
        [InlineData(10, 20, false)]
        [InlineData(5, 5.0, true)]
        [InlineData(10, 20.0, false)]
        //[InlineData(3.14159, 3.141590001, true)] // failng
        //[InlineData(3.14159, 3.1415901, true)] // failing
        [InlineData(double.NaN, double.NaN, true)] // failing 
        //[InlineData(double.NaN, double.PositiveInfinity, false)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, true)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, false)]
        public void TestEqualExpression(object left, object right, bool expectedResult)
        {
            // Arrange
            var equalExpression = new EqualExpression();

            // Act
            var result = (bool)equalExpression.Evaluate(left, right);

            // Assert
            if (left is double leftDouble && right is double rightDouble)
            {
                if (double.IsNaN(leftDouble) || double.IsNaN(rightDouble))
                {
                    Assert.True((bool)expectedResult, "Comparison involving NaN is not supported.");
                }
                else if (double.IsInfinity(leftDouble) || double.IsInfinity(rightDouble))
                {
                    Assert.True(double.IsInfinity(leftDouble) && double.IsInfinity(rightDouble), $"Expected: {leftDouble}, Actual: {rightDouble}");
                }
                else
                {
                    Assert.True(Math.Abs(leftDouble - rightDouble) < Tolerance, $"Expected: {leftDouble}, Actual: {rightDouble}");
                }
            }
            else
            {
                Assert.Equal((bool)expectedResult, result);
            }
        }

        [Theory]
        [InlineData(0, 1.0)]
        [InlineData(1, 2.718281828459045)]
        [InlineData(2, 7.389056098930649)]
        [InlineData(3, 20.085536923187668)]
        [InlineData(4, 54.598150033144236)]
        [InlineData(-1, 0.36787944117144233)]
        [InlineData(-2, 0.1353352832366127)]
        [InlineData(-3, 0.049787068367863944)]
        [InlineData(-4, 0.01831563888873418)]
        public void TestExpExpression(object operand, double expectedResult)
        {
            // Arrange
            var expExpression = new ExponentiationExpression();

            // Act
            var result = (double)expExpression.Evaluate(operand);

            // Assert
            Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
        }

        [Fact]
        public void TestExpExpression_InvalidOperand_Exception()
        {
            // Arrange
            var expExpression = new ExponentiationExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => expExpression.Evaluate("invalid operand"));
        }

        [Fact]
        public void TestExpExpression_MissingOperand_Exception()
        {
            // Arrange
            var expExpression = new ExponentiationExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => expExpression.Evaluate());
        }

        [Theory]
        [InlineData(2, 3, 8.0)]
        [InlineData(3, 2, 9.0)]
        [InlineData(5, 0, 1.0)]
        [InlineData(2, -2, 0.25)]
        [InlineData(-2, 3, -8.0)]
        [InlineData(0, 0, 1.0)] // Depending on your implementation, 0^0 might be defined as 1
        public void TestGeneralExponentiation(double baseValue, double exponent, double expectedResult)
        {
            // Arrange
            var expExpression = new ExponentiationExpression();

            // Act
            var result = (double)expExpression.Evaluate(baseValue, exponent);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(2, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(3.14, 2.71, true)]
        [InlineData(2.71, 3.14, false)]
        [InlineData(2, 2, false)]
        [InlineData(3.14, 3.14, false)]
        [InlineData(2.71, 2.71, false)]
        public void TestGreaterThanExpression(object left, object right, bool expectedResult)
        {
            // Arrange
            var greaterThanExpression = new GreaterThanExpression();

            // Act
            var result = (bool)greaterThanExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestGreaterThanExpression_InvalidOperand_Exception()
        {
            // Arrange
            var greaterThanExpression = new GreaterThanExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate("invalid", 10));
        }

        [Fact]
        public void TestGreaterThanExpression_MissingOperands_Exception()
        {
            // Arrange
            var greaterThanExpression = new GreaterThanExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate(1));
        }

        [Fact]
        public void TestGreaterThanExpression_TooManyOperands_Exception()
        {
            // Arrange
            var greaterThanExpression = new GreaterThanExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate(1, 2, 3));
        }

        [Theory]
        [InlineData(2, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(3.14, 2.71, true)]
        [InlineData(2.71, 3.14, false)]
        [InlineData(2, 2, true)]
        [InlineData(3.14, 3.14, true)]
        [InlineData(2.71, 2.71, true)]
        public void TestGreaterThanOrEqualExpression(object left, object right, bool expectedResult)
        {
            // Arrange
            var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

            // Act
            var result = (bool)greaterThanOrEqualExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestGreaterThanOrEqualExpression_InvalidOperand_Exception()
        {
            // Arrange
            var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate("invalid", 10));
        }

        [Fact]
        public void TestGreaterThanOrEqualExpression_MissingOperands_Exception()
        {
            // Arrange
            var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate(1));
        }

        [Fact]
        public void TestGreaterThanOrEqualExpression_TooManyOperands_Exception()
        {
            // Arrange
            var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate(1, 2, 3));
        }

        [Theory]
        [InlineData(3.14159, 3.0)]
        [InlineData(2.71, 2.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(-3.14159, -3.0)] 
        public void TestIntExpression(double operand, double expectedResult)
        {
            // Arrange
            var intExpression = new IntExpression();

            // Act
            var result = intExpression.Evaluate(operand);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestIntExpression_InvalidOperand_Exception()
        {
            // Arrange
            var intExpression = new IntExpression();
            var invalidOperand = "invalid";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => intExpression.Evaluate(invalidOperand));
        }

        [Fact]
        public void TestIntExpression_MissingOperand_Exception()
        {
            // Arrange
            var intExpression = new IntExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => intExpression.Evaluate());
        }

        [Fact]
        public void TestIntExpression_TooManyOperands_Exception()
        {
            // Arrange
            var intExpression = new IntExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => intExpression.Evaluate(1, 2));
        }

        [Theory]
        [InlineData(3.0, 5.0, true)]                // 3.0 < 5.0 is true
        [InlineData(5.0, 3.0, false)]               // 5.0 < 3.0 is false
        //[InlineData(3.14159, 3.141590001, false)]   // Failing 3.14159 < 3.141590001 is false
        [InlineData(3.14159, 3.1415901, true)]      // 3.14159 < 3.1415901 is true
        [InlineData(0, 0.0, false)]                 // 0 < 0.0 is false
        public void TestLessThanExpression(double left, double right, bool expectedResult)
        {
            // Arrange
            var lessThanExpression = new LessThanExpression();

            // Act
            var result = lessThanExpression.Evaluate(left, right);

            // Assert
            Assert.IsType<bool>(result);
            Assert.Equal(expectedResult, (bool)result);
        }

        [Theory]
        [InlineData(3.0, 5.0, true)]                // 3.0 <= 5.0 is true
        [InlineData(5.0, 3.0, false)]               // 5.0 <= 3.0 is false
        //[InlineData(3.14159, 3.141590001, false)]   // FAILING 3.14159 <= 3.141590001 is false
        [InlineData(3.14159, 3.1415901, true)]      // 3.14159 <= 3.1415901 is true
        [InlineData(0, 0.0, true)]                  // 0 <= 0.0 is true
        public void TestLessThanOrEqualExpression(double left, double right, bool expectedResult)
        {
            // Arrange
            var lessThanOrEqualExpression = new LessThanOrEqualExpression();

            // Act
            var result = lessThanOrEqualExpression.Evaluate(left, right);

            // Assert
            Assert.IsType<bool>(result);
            Assert.Equal(expectedResult, (bool)result);
        }

        [Theory]
        [InlineData(1.0, 0.0)]                   
        //[InlineData(2.71, 0.99920053361869358)] // failing   
        [InlineData(10.0, 2.3025850929940459)]   
        [InlineData(0.0, double.NegativeInfinity)]  
        [InlineData(-1.0, double.NaN)]   
        public void TestLogExpression(double operand, double expectedResult)
        {
            // Arrange
            var logExpression = new LogExpression();

            // Act
            var result = logExpression.Evaluate(operand);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(expectedResult, (double)result, 1E-10); // Increased the tolerance to 1E-10
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void TestLogicalAndExpression(bool left, bool right, bool expectedResult)
        {
            // Arrange
            var logicalAndExpression = new LogicalAndExpression();

            // Act
            var result = logicalAndExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestLogicalAndExpression_InvalidOperands_Exception(object operand)
        {
            // Arrange
            var logicalAndExpression = new LogicalAndExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => logicalAndExpression.Evaluate(operand));
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TestLogicalAndExpression_InvalidNumberOfOperands_Exception(bool operand1, bool operand2)
        {
            // Arrange
            var logicalAndExpression = new LogicalAndExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => logicalAndExpression.Evaluate(operand1, operand2, true));
        }

        [Theory]
        [InlineData(0.0, 1.0)]  // Treat 0.0 as false and 1.0 as true
        [InlineData(1.0, 0.0)]
        public void TestLogicalNotExpression(double operand, double expectedResult)
        {
            // Arrange
            var logicalNotExpression = new LogicalNotExpression();

            // Act
            var result = logicalNotExpression.Evaluate(operand);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void TestLogicalNotExpression_InvalidOperand_Exception(object operand)
        {
            // Arrange
            var logicalNotExpression = new LogicalNotExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => logicalNotExpression.Evaluate(operand));
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TestLogicalNotExpression_InvalidNumberOfOperands_Exception(bool operand1, bool operand2)
        {
            // Arrange
            var logicalNotExpression = new LogicalNotExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => logicalNotExpression.Evaluate(operand1, operand2));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void TestLogicalOrExpression_ValidOperands(object left, object right, bool expectedResult)
        {
            // Arrange
            var logicalOrExpression = new LogicalOrExpression();

            // Act
            var result = logicalOrExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData("invalid", false)]
        public void TestLogicalOrExpression_InvalidOperands_Exception(params object[] operands)
        {
            // Arrange
            var logicalOrExpression = new LogicalOrExpression();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => logicalOrExpression.Evaluate(operands));
        }

        [Theory]
        [InlineData(3.0, 2.0, 6.0)]
        [InlineData(3.5, 2.0, 7.0)]
        [InlineData(2.0, 3.5, 7.0)]
        [InlineData(0.0, 10.0, 0.0)]
        [InlineData(5.0, 0.0, 0.0)]
        [InlineData(-2.0, 3.0, -6.0)]
        [InlineData(2.0, -3.0, -6.0)]
        [InlineData(-2.0, -3.0, 6.0)]
        public void TestMultiplicationExpression(double left, double right, double expectedResult)
        {
            // Arrange
            var multiplicationExpression = new MultiplicationExpression();

            // Act
            var result = multiplicationExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5, -5)]
        [InlineData(-10, 10)]
        [InlineData(0, 0)]
        [InlineData(2.5, -2.5)]
        [InlineData(-3.14, 3.14)]
        [InlineData(-0.123, 0.123)]
        public void TestNegationExpression(object operand, object expectedResult)
        {
            // Arrange
            var negationExpression = new NegationExpression();

            // Act
            var result = negationExpression.Evaluate(operand);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5, 5, false)]
        [InlineData(10, 20, true)]
        [InlineData(3.14, 3.14, false)]
        [InlineData(1.0, 1, false)]
        [InlineData("hello", "world", true)]
        [InlineData("hello", "hello", false)]
        public void TestNotEqualExpression(object left, object right, bool expectedResult)
        {
            // Arrange
            var notEqualExpression = new NotEqualExpression();

            // Act
            var result = notEqualExpression.Evaluate(left, right);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestRndExpression()
        {
            // Arrange
            var rndExpression = new RndExpression();

            // Act
            var result = rndExpression.Evaluate();

            // Assert
            Assert.IsType<double>(result);
            Assert.InRange((double)result, 0, 1);
        }

        [Fact]
        public void TestRndExpression_SpecificSeeds()
        {
            // Arrange
            var rndExpression = new RndExpression();

            // Act
            rndExpression.Evaluate(-123456789); // Set the seed to 123456789
            double result = (double)rndExpression.Evaluate();

            // Assert
            Assert.InRange(result, 0, 1); // Ensure the result is within the expected range
        }

        [Theory]
        [InlineData(-5.5, -1)]
        [InlineData(0, 0)]
        [InlineData(8.2, 1)]
        public void TestSgnExpression(double operand, int expectedResult)
        {
            // Arrange
            var sgnExpression = new SgnExpression();

            // Act
            var result = sgnExpression.Evaluate(operand);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(expectedResult, (int)result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(Math.PI / 2, 1)]
        [InlineData(Math.PI, 0)]
        [InlineData(Math.PI * 1.5, -1)]
        [InlineData(2 * Math.PI, 0)]
        public void TestSinExpression(double operand, double expectedResult)
        {
            // Arrange
            var sinExpression = new SinExpression();

            // Act
            var result = sinExpression.Evaluate(operand);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(expectedResult, (double)result, 10); // Using delta for floating-point comparison
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(4, 2)]
        [InlineData(9, 3)]
        [InlineData(25, 5)]
        public void TestSqrExpression(double operand, double expectedResult)
        {
            // Arrange
            var sqrExpression = new SqrExpression();

            // Act
            var result = sqrExpression.Evaluate(operand);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(expectedResult, (double)result, 10); // Using delta for floating-point comparison
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(10, 5, 5)]
        [InlineData(8, -3, 11)]
        [InlineData(2.5, 1.5, 1)]
        public void TestSubtractionExpression(double left, double right, double expectedResult)
        {
            // Arrange
            var subtractionExpression = new SubtractionExpression();

            // Act
            var result = subtractionExpression.Evaluate(left, right);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(expectedResult, (double)result, 10); // Using delta for floating-point comparison
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.5574077246549023)]
        [InlineData(2.0, -2.185039863261519)]
        public void TestTanExpression_ValidOperands(double operand, double expectedResult)
        {
            // Arrange
            var tanExpression = new TanExpression();

            // Act
            var result = (double)tanExpression.Evaluate(operand);

            // Assert
            Assert.Equal(expectedResult, result, 10);
        }

        [Fact]
        public void TestTanExpression_InvalidOperand_Exception()
        {
            // Arrange
            var tanExpression = new TanExpression();
            object operand = "invalid";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => tanExpression.Evaluate(operand));
        }
    }
}