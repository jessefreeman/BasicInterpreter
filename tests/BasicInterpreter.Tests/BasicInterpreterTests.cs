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
        [InlineData("10 PRINT \"Hello, World!\"\n20 GOTO 40\n30 PRINT \"This will not be printed\"\n40 END", "Hello, World!\n")]
        [InlineData("10 GOTO 30\n20 PRINT \"This will not be printed\"\n30 PRINT \"Hello, World!\"\n40 END", "Hello, World!\n")]
        //[InlineData("10 PRINT 123\n20 GOTO 10\n30 END", "123\n123\n123\n123\n123\n...")] // This will result in an infinite loop
        public void TestGotoCommand(string script, string expectedOutput)
        {
            interpreter.Load(script);
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
        [InlineData("10 PRINT \"Hello, World!\" REM This is a comment", "Hello, World!\n")]
        public void TestComments(string script, string expectedOutput)
        {
            interpreter.Load(script);
            interpreter.Run();
            Assert.Equal(expectedOutput, writer.Output);
        }




    }

}
