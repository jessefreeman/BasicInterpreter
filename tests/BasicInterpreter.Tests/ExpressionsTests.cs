using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests;

public class ExpressionsTests
{
    private StringOutputWriter writer;
    private StringInputReader reader;
    private BasicInterpreter interpreter;
    private readonly ExceptionManager exManager;
    private const double Tolerance = 1e-10;

    // This constructor will be called before each test case
    public ExpressionsTests()
    {
        writer = new StringOutputWriter();
        reader = new StringInputReader(); // or any other suitable implementation for testing
        interpreter = new BasicInterpreter(writer, reader);
        exManager = new ExceptionManager(interpreter, writer);
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
}