using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests
{
    public class BasicInterpreterTests
    {
        private StringOutputWriter writer;
        private StringInputReader reader;
        private BasicInterpreter interpreter;

        // This constructor will be called before each test case
        public BasicInterpreterTests()
        {
            writer = new StringOutputWriter();
            reader = new StringInputReader(); // or any other suitable implementation for testing
            interpreter = new BasicInterpreter(writer, reader);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        public void TestEmptyScript(string script)
        {
            var exception = Record.Exception(() => interpreter.Load(script));
            Assert.IsAssignableFrom<ParsingException>(exception); // Ensure a ParsingException or any of its subclasses is thrown for empty scripts
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

        //[Theory]
        //[InlineData("10 PRINT x")] // x is not defined
        //public void TestUndefinedVariableScript(string script)
        //{
        //    var exception = Record.Exception(() => interpreter.Load(script));
        //    Assert.IsAssignableFrom<UndefinedVariableException>(exception); // Ensure an UndefinedVariableException is thrown for undefined variables
        //}

        [Theory]
        [InlineData("10 PRINT \"Hello, World!\"", "Hello, World!")]
        public void TestCorrectOutput(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        }

        [Theory]
        [InlineData("10 END", "")] // Script with only an END statement, no output
        [InlineData("10 PRINT \"Hello\"\n20 END", "Hello")] // Script with a PRINT statement before the END, should print "Hello"
        [InlineData("10 PRINT \"Hello\"\n20 END\n30 PRINT \"World\"", "Hello")] // Script with a PRINT statement after the END, should only print "Hello"
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
        [InlineData("10 PRINT \"Hello, World!\"", "Hello, World!")] // Basic string
        [InlineData("10 PRINT \"\"", "")] // Empty string
        [InlineData("10 PRINT \"12345\"", "12345")] // Numeric string
        [InlineData("10 PRINT \"Special characters: !@#$%^&*()\"", "Special characters: !@#$%^&*()")] // String with special characters
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
        [InlineData("10 PRINT 12345", "12345")] // Integer
        //[InlineData("10 PRINT 0", "0")] // Zero
        //[InlineData("10 PRINT -12345", "-12345")] // Negative integer
        //[InlineData("10 PRINT 123.45", "123.45")] // Decimal
        //[InlineData("10 PRINT -123.45", "-123.45")] // Negative decimal
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



    }

}
