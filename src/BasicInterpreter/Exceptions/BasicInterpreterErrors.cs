namespace JesseFreeman.BasicInterpreter.Exceptions;

public enum BasicInterpreterError
{
    DuplicateLineNumber,
    FailedPredicateParsing,
    GOSUBCommand,
    GOTOCommand,
    InputMismatchParsing,
    InvalidTypeAssignment,
    InvalidTypeOperation,
    NEXTWithoutFOR,
    Parsing,
    RETURNCommand,
    UndefinedVariable,
    UnsupportedOperation,
    VariableNotDefined,
    OutOfMemory,
    DivisionByZero
}