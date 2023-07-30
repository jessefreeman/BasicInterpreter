# Basic Interpreter - A Collaboration with ChatGPT

This is a Basic Interpreter written in C# and uses [ANTLR](https://www.antlr.org/) for parsing the BASIC language syntax. This project is a collaboration with ChatGPT, an AI language model from OpenAI, to explore how complicated of a code project can be built using an AI as your coding buddy.

The interpreter supports line numbers, PRINT statements, and variable assignment and usage.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 7 SDK
- Visual Studio

### Installing

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution.

### Usage

To use the Basic interpreter, instantiate the `BasicInterpreter` class, load a script using the `Load` method, and then execute the script with the `Run` method.

```csharp
var interpreter = new BasicInterpreter();
interpreter.Load("10 PRINT \"Hello, World!\"");
interpreter.Run(Console.Out);
```

This will execute the Basic script and print the output to the console.

## Running the tests

Tests for this project can be run using xUnit. The tests are located in the `BasicInterpreterTests` project.

To run the tests, use the "Run All Tests" command in Visual Studio, or run the following command in the terminal from the root of the repository:

```shell
dotnet test
```

## Contributing

Please read CONTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the LICENSE.md file for details

## Work in Progress

Please note that this project is a work in progress. Contributions, bug reports, and feature suggestions are welcome as we continue to improve and expand the Basic Interpreter.

Feel free to reach out if you have any questions or ideas for the project. Here's a list of things I still need to implement:

1. **New Statements**: These are new commands that your interpreter will be able to handle. They will require new classes to be created for their execution logic.

   - `CLS`, `LOAD`, `SAVE`, `TRACE`, `NOTRACE`, `FLASH`, `INVERSE`, `GR`, `NORMAL`, `SHLOAD`, `CLEAR`, `RUN`, `STOP`, `TEXT`, `HOME`, `HGR`, `HGR2`, and many more. These are all commands that were supported by the original BASIC language and can add a lot of functionality to your interpreter.

     Below is a table with each of the commands you mentioned, their functions, and their significance in a BASIC interpreter:

     | Command   | Description                                                  | Critical for Interpreter |
     | --------- | ------------------------------------------------------------ | ------------------------ |
     | `CLS`     | Clears the screen.                                           | Critical                 |
     | `LOAD`    | Loads a program from a storage device into memory.           | Critical                 |
     | `SAVE`    | Saves a program from memory to a storage device.             | Critical                 |
     | `TRACE`   | Enables tracing of program execution.                        | Nice to Have             |
     | `NOTRACE` | Disables tracing of program execution.                       | Nice to Have             |
     | `FLASH`   | Enables flashing text mode.                                  | Nice to Have             |
     | `INVERSE` | Enables inverse text mode.                                   | Nice to Have             |
     | `GR`      | Switches the screen mode to graphics mode.                   | Nice to Have             |
     | `NORMAL`  | Switches the screen mode to text mode.                       | Nice to Have             |
     | `SHLOAD`  | Loads a machine language program from cassette tape.         | Nice to Have             |
     | `CLEAR`   | Clears the memory and variables.                             | Nice to Have             |
     | `RUN`     | Executes a program.                                          | Nice to Have             |
     | `STOP`    | Stops program execution.                                     | Nice to Have             |
     | `TEXT`    | Switches the screen mode to text mode.                       | Nice to Have             |
     | `HOME`    | Moves the cursor to the top-left corner of the screen.       | Nice to Have             |
     | `HGR`     | Switches the screen mode to high-resolution graphics mode.   | Nice to Have             |
     | `HGR2`    | Switches the screen mode to high-resolution graphics mode 2. | Nice to Have             |

     Please note that the "Critical for Interpreter" column indicates the commands that are essential for performing basic operations and functionality in a BasicInterpreter. The "Nice to Have" commands can enhance the interpreter's capabilities but are not required for its core operation.

2. **New Functions**: These are new functions that your interpreter will be able to evaluate. They will require new classes to be created for their evaluation logic.

   - `SQR`, `CHR`, `LEN`, `ASC`, `MID`, `PDL`, `PEEK`, `INTF`, `SPC`, `FRE`, `POS`, `USR`, `LEFT`, `RIGHT`, `STR`, `FN`, `VAL`, `SCRN`, `SIN`, `COS`, `TAN`, `ATN`, `RND`, `SGN`, `EXP`, `LOG`, `ABS`, `TAB`. These are all functions that were supported by the original BASIC language and can add a lot of functionality to your interpreter.

     | Function | Description                                                  | Critical for Interpreter |
     | -------- | ------------------------------------------------------------ | ------------------------ |
     | `SQR`    | Calculates the square root of a number.                      | Critical                 |
     | `CHR`    | Returns the character represented by an ASCII code.          | Critical                 |
     | `LEN`    | Returns the length of a string.                              | Critical                 |
     | `ASC`    | Returns the ASCII code of the first character in a string.   | Critical                 |
     | `MID`    | Extracts a substring from a given string.                    | Critical                 |
     | `INTF`   | Returns the integer part of a floating-point number.         | Critical                 |
     | `USR`    | Calls a machine language routine at a specified address.     | Not Needed               |
     | `PDL`    | Reads the state of a paddle control on the C64.              | Not Needed               |
     | `PEEK`   | Reads the byte of memory at a specified address.             | Nice to Have             |
     | `SPC`    | Moves the cursor to a specified column on the screen.        | Not Needed               |
     | `FRE`    | Returns the amount of free memory in bytes.                  | Nice to Have             |
     | `POS`    | Returns the current cursor position on the screen.           | Not Needed               |
     | `LEFT`   | Returns a specified number of characters from the beginning of a string. | Nice to Have             |
     | `RIGHT`  | Returns a specified number of characters from the end of a string. | Nice to Have             |
     | `STR`    | Converts a number to a string.                               | Nice to Have             |
     | `FN`     | Indicates the beginning of a user-defined function.          | Nice to Have             |
     | `VAL`    | Converts a string to a number.                               | Nice to Have             |
     | `SCRN`   | Switches the screen mode between text and graphics.          | Not Needed               |
     | `SIN`    | Calculates the sine of an angle.                             | Nice to Have             |
     | `COS`    | Calculates the cosine of an angle.                           | Nice to Have             |
     | `TAN`    | Calculates the tangent of an angle.                          | Nice to Have             |
     | `ATN`    | Calculates the arctangent of a number.                       | Nice to Have             |
     | `RND`    | Generates a random number between 0 and 1.                   | Nice to Have             |
     | `SGN`    | Returns the sign of a number (-1, 0, or 1).                  | Nice to Have             |
     | `EXP`    | Calculates the value of e raised to the power of a number.   | Nice to Have             |
     | `LOG`    | Calculates the natural logarithm of a number.                | Nice to Have             |
     | `ABS`    | Returns the absolute value of a number.                      | Nice to Have             |
     | `TAB`    | Moves the cursor to a specified column on the screen.        | Nice to Have             |

     Please note that the "Critical for Interpreter" column indicates the functions that are essential for performing basic operations and functionality in a BasicInterpreter. The "Nice to Have" functions can enhance the interpreter's capabilities but are not required for its core operation.

3. **New Variables and Variable Types**: These are new types of variables that your interpreter will be able to handle.

   - `varname` and `varsuffix`: These were added to support variable names that include letters and numbers, and variable types that are indicated by a suffix (`$` for string variables, `%` for integer variables).

4. **New Error Handling**: This is a new way for your interpreter to handle errors.

   - `ONERR`: This command was added to support error handling in your interpreter.

5. **New Data Types**: These are new types of data that your interpreter will be able to handle.

   - `FLOAT`: This was added to support floating-point numbers in your interpreter.

6. **New Comments**: This is a new way for your interpreter to handle comments.

   - `REM` and `COMMENT`: These were added to support comments in your interpreter.