using JesseFreeman.BasicInterpreter.IO;

namespace JesseFreeman.BasicInterpreter.Exceptions;

public class ExceptionManager
{
    public static Dictionary<BasicInterpreterError, string> ErrorTemplates = new Dictionary<BasicInterpreterError, string>
    {
        { BasicInterpreterError.DivisionByZero, "Division by zero at {line}" },
        { BasicInterpreterError.DuplicateLineNumber, "Duplicate line number at {line}" },
        // { BasicInterpreterError.FailedPredicateParsing, "Failed predicate parsing at {line}" },
        { BasicInterpreterError.GoSub, "Undefined line number in GOSUB at {line}" },
        { BasicInterpreterError.GoTo, "Undefined line number in GOTO at {line}" },
        // { BasicInterpreterError.InputMismatch, "Type mismatch error at {line}" },
        { BasicInterpreterError.InvalidTypeAssignment, "Invalid type assignment at {line}" },
        { BasicInterpreterError.InvalidTypeOperation, "Invalid type operation at {line}" },
        { BasicInterpreterError.NextWithoutFor, "NEXT without FOR at {line}" },
        { BasicInterpreterError.OutOfMemory, "Out of memory at {line}" },
        { BasicInterpreterError.ParsingError, "Syntax error at {line}" },
        { BasicInterpreterError.Return, "RETURN without GOSUB at {line}" },
        { BasicInterpreterError.UndefinedVariable, "Undefined variable at {line}" },
        { BasicInterpreterError.UnsupportedOperation, "Unsupported operation at {line}" },
        { BasicInterpreterError.VariableNotDefined, "Variable not defined at {line}" }
    };
    
    private readonly IBasicInterpreterState _basicInterpreterState;
    private readonly IOutputWriter _writer;

    public ExceptionManager(IBasicInterpreterState basicInterpreterState, IOutputWriter writer)
    {
        _basicInterpreterState = basicInterpreterState;
        _writer = writer;
    }

    public void HandleException(InterpreterException ex)
    {
        // Get the error message template based on the error type
        string errorMessageTemplate = ErrorTemplates[ex.ErrorType];

        // Replace the token in the error message template with the line number
        string errorMessage = errorMessageTemplate.Replace("{line}", _basicInterpreterState.CurrentLineNumber.ToString());

        // Write the error message to the console using the provided writer
        _writer.WriteLine(errorMessage);
    }

}