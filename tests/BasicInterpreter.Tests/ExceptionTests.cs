using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Tests;

public class ExceptionTests
{
    private StringOutputWriter writer;
    private StringInputReader reader;
    private BasicInterpreter interpreter;
    private readonly ExceptionManager exManager;
    private const double Tolerance = 1e-10;

    // This constructor will be called before each test case
    public ExceptionTests()
    {
        writer = new StringOutputWriter();
        reader = new StringInputReader(); // or any other suitable implementation for testing
        interpreter = new BasicInterpreter(writer, reader);
        exManager = new ExceptionManager(interpreter, writer);
    }
    
    [Theory] // Verified
    [InlineData("10 PRINT \"Hello, World!\"\n20 GOTO 10\n30 END", "Hello, World!\nHello, World!\n", BasicInterpreterError.MaxLoopsExceeded)]
    public void TestMaxIterations(string script, string expectedPrintedOutput, BasicInterpreterError expectedErrorType)
    {
        // Set the maximum number of iterations to 3
        interpreter.MaxIterations = 3;

        var exception = Record.Exception(() =>
        {
            interpreter.Load(script);
            interpreter.Run();
        });

        var interpreterException = Assert.IsType<InterpreterException>(exception);
        Assert.Equal(expectedErrorType, interpreterException.ErrorType);

        // Use the template from ErrorTemplates
        string errorMessageTemplate = ExceptionManager.ErrorTemplates[expectedErrorType];
        string expectedErrorMessage = errorMessageTemplate; // No line number replacement needed

        exManager.HandleException(interpreterException);
        
        // Concatenate the expected printed output with the expected error message
        string fullExpectedOutput = expectedPrintedOutput + expectedErrorMessage;

        Assert.Equal(fullExpectedOutput, writer.Output.Trim()); // Ensure the output is as expected
    }
    
    [Theory]
        [InlineData("10 PRINT x", 10)] // x is not defined (Should default to 0)
        [InlineData("10 PRINT A$", 10)] // Test undefined variable (Should be empty)
        [InlineData("10 LET B = 5\n20 PRINT A$", 20)] // Test undefined variable in later line (This should be empty)
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
        [InlineData("10 FOR I = 1 TO 10\n20 NEXT J\n30 NEXT I\n", BasicInterpreterError.NextWithoutFor, 30)] // Incorrect variable in NEXT
        [InlineData("10 FOR I = 1 TO 10\n30 NEXT I, J\n", BasicInterpreterError.NextWithoutFor, 30)] // Repeated variable in NEXT
        [InlineData("10 LET I = 1\n20 NEXT I\n", BasicInterpreterError.NextWithoutFor, 20)] // NEXT without corresponding FOR
        [InlineData("10 FOR I = 1 10\n20 NEXT I\n", BasicInterpreterError.ParsingError, 10)] // Missing TO keyword in FOR loop
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

        [Theory]
        [InlineData("10 FOR I = 1 TO 10\n30 NEXT J\n", BasicInterpreterError.NextWithoutFor, 30)] // Mismatched NEXT variable
        [InlineData("10 FOR I = 1 TO 10\n20 FOR I = 1 TO 5\n30 NEXT I\n40 NEXT I\n", BasicInterpreterError.NextWithoutFor, 40)] // Extra NEXT statement
        [InlineData("10 FOR I = 1 TO 10\n20 NEXT I\n30 NEXT I\n", BasicInterpreterError.NextWithoutFor, 30)] // Extra NEXT statement without corresponding FOR
        [InlineData("10 FOR I = 1 TO 10\n20 PRINT I\n30 FOR J = 1 TO 5\n40 PRINT J\n50 NEXT J\n", BasicInterpreterError.ForWithoutNext, 10)]
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
        
        [Theory]
        [InlineData("10 FOR I = 1 TO 3 STEP 0\n20 PRINT I\n30 NEXT I\n", BasicInterpreterError.StepValueZero, 10, "")]
        public void TestZeroStepValue(string script, BasicInterpreterError expectedErrorType, int expectedErrorLineNumber, string expectedPrintedOutput)
        {
            var exception = Record.Exception(() =>
            {
                interpreter.MaxIterations = 3;
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

}