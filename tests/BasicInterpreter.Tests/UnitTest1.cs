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

        [Theory]
        [InlineData("10 PRIN =\"Hello, World!\"")] // Invalid PRINT statement
        public void TestInvalidSyntaxScript(string script)
        {
            var exception = Record.Exception(() => interpreter.Load(script));
            Assert.IsAssignableFrom<ParsingException>(exception); // Ensure a ParsingException is thrown for invalid syntax
        }

        //[Theory]
        //[InlineData("10 PRINT x")] // x is not defined
        //public void TestUndefinedVariableScript(string script)
        //{
        //    var exception = Record.Exception(() => interpreter.Load(script));
        //    Assert.IsAssignableFrom<UndefinedVariableException>(exception); // Ensure an UndefinedVariableException is thrown for undefined variables
        //}

        //[Theory]
        //[InlineData("10 PRINT \"Hello, World!\"", "Hello, World!")]
        //public void TestCorrectOutput(string script, string expectedOutput)
        //{
        //    interpreter.Load(script);
        //    interpreter.Run();
        //    Assert.Equal(expectedOutput, writer.Output); // Ensure the output is as expected
        //}



    }

}
