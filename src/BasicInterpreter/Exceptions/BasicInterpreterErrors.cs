namespace JesseFreeman.BasicInterpreter.Exceptions;

public enum BasicInterpreterError
{
    DivisionByZero,
    DuplicateLineNumber,
    FailedPredicateParsing,
    GOSUBCommand,
    GOTOCommand,
    InputMismatchParsing,
    InvalidTypeAssignment,
    InvalidTypeOperation,
    NEXTWithoutFOR,
    OutOfMemory,
    Parsing,
    RETURNCommand,
    UndefinedVariable,
    UnsupportedOperation,
    VariableNotDefined
}
