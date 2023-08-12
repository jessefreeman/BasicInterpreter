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

### Core Functionality

This table provides an overview of the core structural elements in the BASIC programming language, as defined in the grammar file you shared. These elements, while not commands or functions in the traditional sense, are essential for structuring a BASIC program and controlling its flow:

| Functionality                     | Description                                                  | Example                                          | Importance       |
| --------------------------------- | ------------------------------------------------------------ | ------------------------------------------------ | ---------------- |
| ~~Line Number~~                   | ~~Represents a line number at the start of each line of code (`linenumber`).~~ | ~~`10 PRINT "Hello, world!"`~~                   | ~~Critical~~     |
| ~~Variable Declaration~~          | ~~Represents a variable declaration (`vardecl`).~~           | ~~`LET A = 10`~~                                 | ~~Critical~~     |
| ~~Variable Assignment~~           | ~~Represents an assignment of a value to a variable (`variableassignment`).~~ | ~~`A = 10`~~                                     | ~~Critical~~     |
| ~~Relational Operators~~          | ~~Represents relational operators, such as `=`, `<`, `>`, `<=`, `>=`, and `<>` (not equal) (`relop`, `neq`).~~ | ~~`IF A > B THEN PRINT "A is greater"`~~         | ~~Critical~~     |
| ~~Variable~~                      | ~~Represents a variable (`var_`).~~                          | ~~`A`, `B`, `COUNTER`, etc.~~                    | ~~Critical~~     |
| ~~Variable Name~~                 | ~~Represents the name of a variable (`varname`).~~           | ~~`LET COUNTER = 10`~~                           | ~~Critical~~     |
| Variable List                     | Represents a list of variables (`varlist`).                  | `INPUT A, B, C`                                  | Critical         |
| Expression List                   | Represents a list of expressions (`exprlist`).               | `PRINT A+B, C-D, E*F`                            | Critical         |
| ~~Sign Expression~~               | ~~Represents an expression that may optionally start with a plus (+) or minus (-) sign (`signExpression`).~~ | ~~`-A`, `+B`~~                                   | ~~Critical~~     |
| ~~Multiplying Expression~~        | ~~Represents an expression with multiplication or division (`multiplyingExpression`).~~ | ~~`A * B`, `C / D`~~                             | ~~Critical~~     |
| ~~Adding Expression~~             | ~~Represents an expression with addition or subtraction (`addingExpression`).~~ | ~~`A + B`, `C - D`~~                             | ~~Critical~~     |
| ~~Expression~~                    | ~~Represents a general expression, which can be a combination of various operators and operands (`expression`).~~ | ~~`A + B * C`, `D / E - F`~~                     | ~~Critical~~     |
| ~~Order of operations~~           | ~~Defines the precedence of arithmetic and logical operations, following the standard mathematical rules (e.g., parentheses, exponents, multiplication and division, addition and subtraction).~~ | ~~`(2 * 3) + (4 / 2)` evaluates to `8`~~         | ~~Critical~~     |
| Statement with Optional Ampersand | Represents a statement that may optionally start with an ampersand (`amprstmt`). | `&PRINT "Hello, world!"`                         | Nice to have     |
| Ampersand Operator                | Represents the ampersand (&) operator (`amperoper`).         | `&PRINT`                                         | Nice to have     |
| ~~Variable Suffix~~               | ~~Represents the suffix of a variable, which can be `$` for string variables (`varsuffix`).~~ | ~~`A$`~~                                         | ~~Nice to have~~ |
| ~~Exponent Expression~~           | ~~Represents an expression with an exponent (`exponentExpression`).~~ | ~~`2^3`~~                                        | ~~Nice to have~~ |
| ~~Not Equal Operator~~            | ~~Represents the not equal (`<>`) operator (`neq`).~~        | ~~`IF A <> B THEN PRINT "A is not equal to B"`~~ | ~~Nice to have~~ |
| Data                              | Represents a piece of data, which can be a number or a string literal (`datum`). | `DATA 10, "Hello"`                               | Nice to have     |
| ~~Comment~~                       | ~~Represents a comment in the code. Anything following `REM` on a line is ignored by the interpreter (`COMMENT`).~~ | ~~`REM This is a comment`~~                      | ~~Nice to have~~ |

### Statements

In the BASIC programming language, statements are the building blocks of the program. They represent instructions for the computer to follow. Here's a brief summary of some key statements in BASIC:

| Statement            | Description                              | Example                              | Importance   |
| -------------------- | ---------------------------------------- | ------------------------------------ | ------------ |
| ~~Let Statement~~    | ~~Assigns a value to a variable.~~       | ~~`LET A = 10`~~                     | ~~Critical~~ |
| ~~If Statement~~     | ~~Conditional execution of statements.~~ | ~~`IF A > 10 THEN PRINT "Greater"`~~ | ~~Critical~~ |
| ~~For Statement~~    | ~~Loop with a counter.~~                 | ~~`FOR I = 1 TO 10`~~                | ~~Critical~~ |
| ~~Next Statement~~   | ~~Specifies the end of the For loop.~~   | ~~`NEXT I`~~                         | ~~Critical~~ |
| Input Statement      | Reads user input.                        | `INPUT A`                            | Critical     |
| ~~Goto Statement~~   | ~~Jumps to a line in the program.~~      | ~~`GOTO 100`~~                       | ~~Critical~~ |
| Gosub Statement      | Calls a subroutine at a specific line.   | `GOSUB 200`                          | Critical     |
| ~~Return Statement~~ | ~~Returns from a subroutine.~~           | ~~`RETURN`~~                         | ~~Critical~~ |
| ~~Print Statement~~  | ~~Outputs a value.~~                     | ~~`PRINT A`~~                        | ~~Critical~~ |
| E~~nd Statement~~    | ~~Ends the program.~~                    | ~~`END`~~                            | ~~Critical~~ |
| Read Statement       | Reads data from a DATA statement.        | `READ A`                             | Nice to have |
| Data Statement       | Holds constant data for READ.            | `DATA 10, 20, 30`                    | Nice to have |
| Restore Statement    | Resets the data pointer for READ.        | `RESTORE`                            | Nice to have |
| Dim Statement        | Declares an array.                       | `DIM A(10)`                          | Nice to have |
| Poke Statement       | Changes a memory location's value.       | `POKE 1024, 65`                      | Nice to have |
| On Goto Statement    | Jumps to one of several lines.           | `ON A GOTO 100, 200, 300`            | Nice to have |
| On Gosub Statement   | Calls one of several subroutines.        | `ON A GOSUB 100, 200, 300`           | Nice to have |
| Wait Statement       | Pauses until a condition is met.         | `WAIT 1000`                          | Nice to have |
| Def Statement        | Defines a user function.                 | `DEF FN A(X) = X * X`                | Nice to have |
| Stop Statement       | Halts execution.                         | `STOP`                               | Nice to have |
| List Statement       | Lists the program in memory.             | `LIST`                               | Not needed   |
| Run Statement        | Starts execution from the beginning.     | `RUN`                                | Not needed   |
| Clear Statement      | Clears all variables.                    | `CLEAR`                              | Not needed   |
| Include Statement    | Includes another BASIC program.          | `INCLUDE "OTHER.BAS"`                | Not needed   |
| Load Statement       | Loads a program from disk.               | `LOAD "PROGRAM.BAS"`                 | Not needed   |
| Save Statement       | Saves the program to disk.               | `SAVE "PROGRAM.BAS"`                 | Not needed   |
| Cls Statement        | Clears the screen.                       | `CLS`                                | Not needed   |
| Pop Statement        | Removes the last GOSUB return address.   | `POP`                                | Not needed   |
| Store Statement      | Stores the current program state.        | `STORE`                              | Not needed   |
| Recall Statement     | Restores the program state.              | `RECALL`                             | Not needed   |
| OnErr Statement      | Jumps to a line when an error occurs.    | `ONERR GOTO 1000`                    | Not needed   |
| Ampersand Statement  | Calls a machine language subroutine.     | `&A9,00`                             | Not needed   |

### Comma Separated Statements

In BASIC, commas and other separators played a significant role in various statements. This functionality allowed for more concise code and facilitated the handling of multiple variables and expressions within a single line.

| Functionality              | Description                                                  | Example                                 | Importance   |
| -------------------------- | ------------------------------------------------------------ | --------------------------------------- | ------------ |
| PRINT Statement            | Prints multiple expressions separated by commas.             | `PRINT A, B, "HELLO"`                   | Critical     |
| INPUT Statement            | Accepts multiple variables, separated by commas.             | `INPUT A, B, C`                         | Critical     |
| READ Statement             | Reads multiple values from a DATA statement, separated by commas. | `READ A, B, C`                          | Critical     |
| DATA Statement             | Embeds literal values in the program, separated by commas.   | `DATA 10, 20, 30`                       | Critical     |
| DIM Statement              | Declares the size of an array, dimensions separated by commas. | `DIM A(10, 20)`                         | Critical     |
| POKE Statement             | Writes a value to a specific memory address, separated by a comma. | `POKE 53280, 0`                         | Critical     |
| ON...GOTO/GOSUB Statements | Followed by a list of line numbers separated by commas.      | `ON A GOTO 100, 200, 300`               | Nice to Have |
| DEF FN Statement           | Defines a function with parameters separated by commas.      | `DEF FN A(X, Y) = X + Y`                | Nice to Have |
| Graphics Statements        | Takes multiple parameters separated by commas.               | `PLOT 10, 20` or `HLIN 10, 20 AT 5`     | Nice to Have |
| RESTORE Statement          | Resets the READ pointer to a specific DATA line with comma-separated values. | `RESTORE 100` (with `100 DATA 1, 2, 3`) | Not Needed   |

### Functions

Functions can take inputs (known as arguments or parameters), perform operations, and return a result. They are a fundamental building block in many programming languages, including BASIC, and are crucial for creating modular, maintainable code. In the context of the BASIC programming language, functions are used to perform various tasks such as mathematical calculations, string manipulation, and input/output operations. 

Here's a brief summary of some key functions in BASIC:

| Function                 | Description                                  | Example                                              | Importance              |
| ------------------------ | -------------------------------------------- | ---------------------------------------------------- | ----------------------- |
| ~~Number Function~~      | ~~Represents a numeric value.~~              | ~~`LET A = 10`~~                                     | ~~Critical~~            |
| ~~String Function~~      | ~~Represents a string value.~~               | ~~`LET A$ = "Hello"`~~                               | ~~Critical~~            |
| ~~Relational Operators~~ | ~~Compare two values.~~                      | ~~`IF A > B THEN PRINT "A is greater"`~~             | ~~Critical~~            |
| ~~Arithmetic Operators~~ | ~~Perform arithmetic operations.~~           | ~~`LET C = A + B`~~                                  | ~~Critical~~            |
| ~~Logical Operators~~    | ~~Perform logical operations.~~              | ~~`IF A > 10 AND B > 10 THEN PRINT "Both greater"`~~ | ~~Critical~~            |
| ~~Sqr Function~~         | ~~Calculates the square root of a number.~~  | ~~`PRINT SQR(A)`~~                                   | ~~Nice to have~~        |
| Chr Function             | Converts a number to a character.            | `PRINT CHR$(65)`                                     | Nice to have            |
| Len Function             | Returns the length of a string.              | `PRINT LEN(A$)`                                      | Nice to have            |
| Asc Function             | Returns the ASCII value of a character.      | `PRINT ASC("A")`                                     | Nice to have            |
| Mid Function             | Returns a substring.                         | `PRINT MID$(A$, 2, 3)`                               | Nice to have            |
| Peek Function            | Returns the value of a memory location.      | `PRINT PEEK(1024)`                                   | Nice to have            |
| Int Function             | Rounds a number down to the nearest integer. | `PRINT INT(3.14)`                                    | Nice to have            |
| Spc Function             | Inserts a number of spaces.                  | `PRINT "Hello"; SPC(5); "World"`                     | Nice to have            |
| Fre Function             | Returns the amount of free memory.           | `PRINT FRE(0)`                                       | Nice to have            |
| Pos Function             | Returns the current cursor column.           | `PRINT POS(0)`                                       | Nice to have            |
| Tab Function             | Moves the cursor to a specific column.       | `PRINT TAB(10); "Hello"`                             | Nice to have            |
| ~~Sin Function~~         | ~~Calculates the sine of an angle.~~         | ~~`PRINT SIN(45)`~~                                  | ~~Nice to have~~        |
| ~~Cos Function~~         | ~~Calculates the cosine of an angle.~~       | ~~`PRINT COS(45)`~~                                  | ~~Nice to have~~        |
| ~~Tan Function~~         | ~~Calculates the tangent of an angle.~~      | ~~`PRINT TAN(45)`~~                                  | ~~Nice to have~~        |
| ~~Atn Function~~         | ~~Calculates the arctangent of a number.~~   | ~~`PRINT ATN(1)`~~                                   | ~~Nice to have~~        |
| ~~Rnd Function~~         | ~~Returns a random number.~~                 | ~~`PRINT RND(1)`~~                                   | ~~Nice to have~~        |
| <u>~~Fn Function~~</u>   | <u>~~Calls a user-defined function.~~</u>    | <u>~~`PRINT FN A(5)`~~</u>                           | <u>~~Nice to have~~</u> |
| Pdl Function             | Reads the position of the paddle.            | `PRINT PDL(0)`                                       | Not needed              |
| Scrn Function            | Reads the color of a pixel.                  | `PRINT SCRN(100, 100)`                               | Not needed              |
| Usr Function             | Calls a machine language subroutine.         | `PRINT USR(768)`                                     | Not needed              |
| Ampersand Function       | Calls a machine language subroutine.         | `PRINT &A9,00`                                       | Not needed              |

### Exceptions

Errors in a BASIC interpreter are essential feedback mechanisms that inform the programmer of mistakes or issues within the code. They are crucial for debugging and ensuring that the code adheres to the syntax and semantics of the language. In the context of BASIC programming, especially in environments like C64 or Apple BASIC, errors can occur due to various reasons such as syntax mistakes, type mismatches, undefined variables, and unsupported operations.

Here's a brief summary of some key errors in BASIC:

Certainly! Here's the complete table including the "Not Needed" errors:

| Error Name                         | Error Message Template                                       | Example Code to Trigger Error                | Importance   |
| ---------------------------------- | ------------------------------------------------------------ | -------------------------------------------- | ------------ |
| ~~Duplicate Line Number~~          | ~~"Duplicate line number at {line}"~~                        | ~~`10 PRINT "HELLO"`<br>`10 PRINT "WORLD"`~~ | ~~Critical~~ |
| Failed Predicate Parsing           | "Failed predicate parsing at {line}"                         | `IF A THEN 10`                               | Critical     |
| GOSUB Command                      | "Undefined line number in GOSUB at {line}"                   | `GOSUB 100`                                  | Critical     |
| GOTO Command                       | "Undefined line number in GOTO at {line}"                    | `GOTO 200`                                   | Critical     |
| Input Mismatch Parsing             | "Type mismatch error at {line}"                              | `LET A = "STRING"`                           | Critical     |
| Invalid Type Assignment            | "Invalid type assignment at {line}"                          | `LET A$ = 10`                                | Critical     |
| Invalid Type Operation             | "Invalid type operation at {line}"                           | `LET A = "HELLO" + 10`                       | Critical     |
| NEXT Without FOR                   | "NEXT without FOR at {line}"                                 | `NEXT I`                                     | Critical     |
| Parsing                            | "Syntax error at {line}"                                     | `PRINT "HELLO`                               | Critical     |
| RETURN Command                     | "RETURN without GOSUB at {line}"                             | `RETURN`                                     | Critical     |
| Undefined Variable                 | "Undefined variable at {line}"                               | `PRINT A`                                    | Critical     |
| Unsupported Operation              | "Unsupported operation at {line}"                            | `LET A = B ^ C`                              | Critical     |
| Variable Not Defined               | "Variable not defined at {line}"                             | `PRINT B`                                    | Critical     |
| Out of Memory                      | "Out of memory at {line}"                                    | (Exceeding available memory)                 | Critical     |
| Division by Zero                   | "Division by zero at {line}"                                 | `LET A = 10 / 0`                             | Critical     |
| Skip Next Command (Specific)       | "Skip next command error at {line}" (Specific to your interpreter) | (Example needed from your code)              | Nice To Have |
| Throwing Error Listener (Specific) | "Throwing error listener at {line}" (Specific to your interpreter) | (Example needed from your code)              | Nice To Have |
| Throwing Error Strategy (Specific) | "Throwing error strategy at {line}" (Specific to your interpreter) | (Example needed from your code)              | Nice To Have |
| File Exceptions                    | "File error at {line}"                                       | `LOAD "FILE"`                                | Not Needed   |
| Device Exceptions                  | "Device error at {line}"                                     | `OPEN 4,4`                                   | Not Needed   |
| Input Output File                  | "Input/output error at {line}"                               | `PRINT#1, "TEXT"`                            | Not Needed   |
| Filename Exceptions                | "Filename error at {line}"                                   | `SAVE ""`                                    | Not Needed   |
| Break Error                        | "Break at {line}"                                            | (User presses the BREAK key)                 | Not Needed   |
| Extra Ignored                      | "Extra ignored at {line}"                                    | `PRINT "HELLO" EXTRA`                        | Not Needed   |
| Redo from Start                    | "Redo from start at {line}"                                  | `INPUT A$` (without entering anything)       | Not Needed   |
| How Error                          | "How?"                                                       | `PRINT 5+"HELLO"`                            | Not Needed   |
| Load Error                         | "Load error at {line}"                                       | `LOAD "NONEXISTENT"`                         | Not Needed   |
| Verify Error                       | "Verify error at {line}"                                     | `VERIFY "FILE"`                              | Not Needed   |
| Direct Mode Error                  | "Direct mode error at {line}"                                | `RUN` (in direct mode)                       | Not Needed   |
| Illegal Direct Error               | "Illegal direct error at {line}"                             | `FOR I=1 TO 10` (in direct mode)             | Not Needed   |
| Not Available Error                | "Not available at {line}"                                    | (Specific to unavailable features)           | Not Needed   |

This table includes all the errors, sorted by importance, and includes the "Not Needed" errors as well.

### Family BASIC Commands

Family BASIC is a version of the BASIC programming language developed by Nintendo and Hudson Soft for the Nintendo Family Computer (Famicom or NES). It allowed users to create their own games and applications, including the ability to manipulate graphics and sound.

Here's a brief summary of some key Family BASIC commands:

| Command                | Description                                                  | Example                                 | Importance   |
| ---------------------- | ------------------------------------------------------------ | --------------------------------------- | ------------ |
| SPRITE                 | Defines and displays sprites.                                | `SPRITE 1, 10, 20, 3`                   | Critical     |
| MOVE                   | Moves sprites.                                               | `MOVE 1, 30, 40`                        | Critical     |
| PLAY                   | Plays music.                                                 | `PLAY "CDEFGAB"`                        | Nice to have |
| PALET                  | Changes the palettes of the sprites or the background/text.  | `PALET 1, 2`                            | Nice to have |
| STRIG                  | Takes input from the gamepad action buttons.                 | `IF STRIG(0) THEN SPRITE 1, 10, 20, 3`  | Critical     |
| STICK                  | Takes input from the gamepad directionals.                   | `IF STICK(0) = 8 THEN MOVE 1, 30, 40`   | Critical     |
| LOADS                  | Loads background graphics data from tape (Family BASIC v3 only). | `LOADS "BG1"`                           | Not needed   |
| SAVES                  | Saves background graphics data to tape (Family BASIC v3 only). | `SAVES "BG1"`                           | Not needed   |
| BEEP                   | Plays a beep sound.                                          | `BEEP`                                  | Nice to have |
| SCROLL                 | Scrolls the screen in a specified direction.                 | `SCROLL "UP"`                           | Nice to have |
| COLOR                  | Changes the color of the text.                               | `COLOR 2`                               | Nice to have |
| PRINT                  | Prints text or variables to the screen.                      | `PRINT "HELLO"`                         | Critical     |
| INPUT                  | Takes user input.                                            | `INPUT A$`                              | Critical     |
| IF...THEN              | Conditional statement.                                       | `IF A$="HELLO" THEN PRINT "WORLD"`      | Critical     |
| FOR...TO...STEP...NEXT | Loop statement.                                              | `FOR I=1 TO 10 STEP 2: PRINT I: NEXT I` | Critical     |
| GOTO                   | Jumps to a specified line number.                            | `GOTO 100`                              | Critical     |
| GOSUB...RETURN         | Calls a subroutine at a specified line number.               | `GOSUB 100: RETURN`                     | Critical     |
| END                    | Ends the program.                                            | `END`                                   | Critical     |

**Apple Specific and Graphics/Sound Functionality**

Apple-specific and graphics/sound functionality in AppleSoft BASIC, a variant of BASIC for the Apple II series of computers, provided users with the ability to create graphical and audio elements in their programs. I am leaving this in here since it's defined in the grammar file I'm using but I will most likely not implement any of these. 

Here's a brief summary:

| Functionality       | Description                                                  | Example                  | Importance |
| ------------------- | ------------------------------------------------------------ | ------------------------ | ---------- |
| Hgr Statement       | Switches to high resolution graphics mode.                   | `HGR`                    | Not needed |
| Hgr2 Statement      | Switches to high resolution graphics mode with double page buffering. | `HGR2`                   | Not needed |
| Home Statement      | Clears the text screen and moves the cursor to the top-left. | `HOME`                   | Not needed |
| Plot Statement      | Plots a point in graphics mode.                              | `PLOT 100, 100`          | Not needed |
| Hlin Statement      | Draws a horizontal line in graphics mode.                    | `HLIN 0, 100 AT 50`      | Not needed |
| Vlin Statement      | Draws a vertical line in graphics mode.                      | `VLIN 0, 100 AT 50`      | Not needed |
| Hplot Statement     | Draws a line in graphics mode.                               | `HPLOT 0, 0 TO 100, 100` | Not needed |
| Vplot Statement     | Draws a line in graphics mode.                               | `VPLOT 0, 0 TO 100, 100` | Not needed |
| Hcolor Statement    | Sets the color for graphics mode.                            | `HCOLOR=3`               | Not needed |
| Text Statement      | Switches to text mode.                                       | `TEXT`                   | Not needed |
| Flash Statement     | Makes the text flash.                                        | `FLASH`                  | Not needed |
| Inverse Statement   | Makes the text inverse.                                      | `INVERSE`                | Not needed |
| Normal Statement    | Makes the text normal.                                       | `NORMAL`                 | Not needed |
| PrNumber Statement  | Sets the output device number.                               | `PR#1`                   | Not needed |
| InNumber Statement  | Sets the input device number.                                | `IN#1`                   | Not needed |
| Ampersand Statement | Calls a machine language subroutine.                         | `&A9,00`                 | Not needed |
| Shload Statement    | Loads a shape table from disk.                               | `SHLOAD`                 | Not needed |
| Store Statement     | Stores the current program state.                            | `STORE`                  | Not needed |
| Recall Statement    | Restores the program state.                                  | `RECALL`                 | Not needed |
| OnErr Statement     | Jumps to a line when an error occurs.                        | `ONERR GOTO 1000`        | Not needed |
| Trace Statement     | Turns on trace mode.                                         | `TRACE`                  | Not needed |
| Notrace Statement   | Turns off trace mode.                                        | `NOTRACE`                | Not needed |
| Wait Statement      | Waits for a specified condition.                             | `WAIT 1000`              | Not needed |
| Load Statement      | Loads a program from disk.                                   | `LOAD "PROGRAM.BAS"`     | Not needed |
| Save Statement      | Saves the program to disk.                                   | `SAVE "PROGRAM.BAS"`     | Not needed |
| Cls Statement       | Clears the screen.                                           | `CLS`                    | Not needed |
| Include Statement   | Includes another BASIC program.                              | `INCLUDE "OTHER.BAS"`    | Not needed |