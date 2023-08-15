namespace JesseFreeman.BasicInterpreter.Exceptions;

public enum BasicInterpreterError
{
    DivisionByZero,
    DuplicateLineNumber,
    GoSub,
    GoTo,
    ForWithoutNext,
    InvalidTypeAssignment,
    InvalidTypeOperation,
    MaxLoopsExceeded,
    NextWithoutFor,
    OutOfMemory,
    ParsingError,
    StepValueZero,
    Return,
    UndefinedVariable,
    UnsupportedOperation,
    VariableNotDefined,
    
}