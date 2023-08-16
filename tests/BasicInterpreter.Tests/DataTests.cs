#region

using JesseFreeman.BasicInterpreter.Data;
using JesseFreeman.BasicInterpreter.Exceptions;
using JesseFreeman.BasicInterpreter.IO;

#endregion

namespace JesseFreeman.BasicInterpreter.Tests;

public class DataTests
{
    public static IEnumerable<object[]> DoubleArrayTestData()
    {
        yield return new object[] {new[] {1.0, 2.0, 3.0}};
        yield return new object[] {new double[] { }};
        yield return new object[] {null};
    }

    [Theory]
    [MemberData(nameof(DoubleArrayTestData))]
    public void TestBasicNumberArray_SetGetValue(double[] value)
    {
        // Arrange
        var basicNumberArray = new BasicNumberArray();

        // Act
        basicNumberArray.SetValue(value);
        var result = basicNumberArray.GetValue();

        // Assert
        Assert.Equal(value, result);
    }

    public static IEnumerable<object[]> StringArrayTestData()
    {
        yield return new object[] {new[] {"A", "B", "C"}};
        yield return new object[] {new string[] { }};
        yield return new object[] {null};
    }

    [Theory]
    [MemberData(nameof(StringArrayTestData))]
    public void TestBasicStringArray_SetGetValue(string[] value)
    {
        // Arrange
        var basicStringArray = new BasicStringArray();

        // Act
        basicStringArray.SetValue(value);
        var result = basicStringArray.GetValue();

        // Assert
        Assert.Equal(value, result);
    }

    public class BasicNumberTests
    {
        private const double Tolerance = 1e-10;
        private readonly ExceptionManager exManager;
        private readonly BasicInterpreter interpreter;
        private readonly StringInputReader reader;

        private readonly StringOutputWriter writer;

        // This constructor will be called before each test case
        public BasicNumberTests()
        {
            writer = new StringOutputWriter();
            reader = new StringInputReader(); // or any other suitable implementation for testing
            interpreter = new BasicInterpreter(writer, reader);
            exManager = new ExceptionManager(interpreter, writer);
        }


        [Theory]
        [InlineData(42.0)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(0.0)]
        public void TestBasicNumber_SetGetValue(double value)
        {
            // Arrange
            var basicNumber = new BasicNumber();

            // Act
            basicNumber.SetValue(value);
            var result = basicNumber.GetValue();

            // Assert
            Assert.Equal(value, result);
        }

        // Since BasicNumber only accepts double, there's no invalid type operation here.
    }

    public class BasicStringTests
    {
        [Theory]
        [InlineData("Hello, World!")]
        [InlineData("")]
        [InlineData(null)]
        public void TestBasicString_SetGetValue(string value)
        {
            // Arrange
            var basicString = new BasicString();

            // Act
            basicString.SetValue(value);
            var result = basicString.GetValue();

            // Assert
            Assert.Equal(value, result);
        }

        // Since BasicString only accepts string, there's no invalid type operation here.
    }
}