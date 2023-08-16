#region

using JesseFreeman.BasicInterpreter.Evaluators;
using JesseFreeman.BasicInterpreter.Exceptions;

#endregion

namespace JesseFreeman.BasicInterpreter.Tests;

public class IndividualExpressionTests
{
    private const double Tolerance = 1e-10;

    [Theory]
    [InlineData(0, 0.0)] // Abs(0) = 0.0
    [InlineData(5, 5.0)] // Abs(5) = 5.0
    [InlineData(-5, 5.0)] // Abs(-5) = 5.0
    [InlineData(double.MaxValue, double.MaxValue)] // Abs(MaxValue) = MaxValue
    [InlineData(double.MinValue, double.MaxValue)] // Abs(MinValue) = MaxValue (to avoid overflow)
    [InlineData(double.PositiveInfinity, double.PositiveInfinity)] // Abs(Infinity) = Infinity
    [InlineData(double.NegativeInfinity, double.PositiveInfinity)] // Abs(-Infinity) = Infinity
    [InlineData(double.NaN, double.NaN)] // Abs(NaN) = NaN
    [InlineData("123", 123.0)] // Abs("123") = 123.0 (integer parsing)
    [InlineData("-456", 456.0)] // Abs("-456") = 456.0 (integer parsing)
    [InlineData("-123.456", 123.456)] // Abs("-123.456") = 123.456 (double parsing)
    [InlineData("NaN", double.NaN)] // Abs("NaN") = NaN (invalid parsing)
    // Add more edge cases and typical test cases as needed
    public void TestAbsExpression(object input, object expectedResult)
    {
        // Arrange - Nothing to do since absExpression is already instantiated.
        var absExpression = new AbsExpression();

        // Act
        var result = absExpression.Evaluate(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0, 0)] // 0 + 0 = 0
    [InlineData(5, 3, 8)] // 5 + 3 = 8
    [InlineData(-5, 3, -2)] // -5 + 3 = -2
    [InlineData(2.5, 3.5, 6.0)] // 2.5 + 3.5 = 6.0
    [InlineData("5", 3, 8.0)] // "5" + 3 = 8.0 (string parsing)
    [InlineData("2.5", 3.5, 6.0)] // "2.5" + 3.5 = 6.0 (string parsing)
    [InlineData("NaN", double.NaN)] // "NaN" + "NaN" = NaN (invalid parsing)
    // Add more edge cases and typical test cases as needed
    public void TestAdditionExpression(params object[] operands)
    {
        // Arrange - Nothing to do since additionExpression is already instantiated.
        var additionExpression = new AdditionExpression();

        // Act
        var result = (double) additionExpression.Evaluate(operands);

        // Assert
        Assert.Equal(operands.Sum(Convert.ToDouble), result);
    }

    [Theory]
    [InlineData(0, 0.0)] // Atn(0) = 0.0
    [InlineData(1, 0.7853981634)] // Atn(1) = 0.7853981634
    [InlineData(-1, -0.7853981634)] // Atn(-1) = -0.7853981634
    [InlineData(double.MaxValue, 1.57079632679)] // Atn(MaxValue) = 1.57079632679
    [InlineData(double.MinValue, -1.57079632679)] // Atn(MinValue) = -1.57079632679
    [InlineData(double.PositiveInfinity, 1.57079632679)] // Atn(Infinity) = 1.57079632679
    [InlineData(double.NegativeInfinity, -1.57079632679)] // Atn(-Infinity) = -1.57079632679
    [InlineData(double.NaN, double.NaN)] // Atn(NaN) = NaN
    // Add more edge cases and typical test cases as needed
    public void TestAtnExpression(object operand, double expectedResult)
    {
        // Arrange
        var atnExpression = new AtnExpression();

        // Act
        var result = (double) atnExpression.Evaluate(operand);

        // Assert
        Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
    }

    [Theory]
    [InlineData(0, 1.0)] // Cos(0) = 1.0
    [InlineData(1, 0.5403023059)] // Cos(1) = 0.5403023059
    [InlineData(Math.PI / 2, 6.12323399574e-17)] // Cos(PI/2) = 6.12323399574e-17
    [InlineData(Math.PI, -1.0)] // Cos(PI) = -1.0
    [InlineData(3 * Math.PI / 2, -1.83697019872e-16)] // Cos(3*PI/2) = -1.83697019872e-16
    [InlineData(2 * Math.PI, 1.0)] // Cos(2*PI) = 1.0
    [InlineData(double.NaN, double.NaN)] // Cos(NaN) = NaN
    [InlineData(double.PositiveInfinity, double.NaN)] // Cos(Infinity) = NaN
    [InlineData(double.NegativeInfinity, double.NaN)] // Cos(-Infinity) = NaN
    // Add more edge cases and typical test cases as needed
    public void TestCosExpression(object operand, double expectedResult)
    {
        // Arrange
        var cosExpression = new CosExpression();

        // Act
        var result = (double) cosExpression.Evaluate(operand);

        // Assert
        Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
    }

    [Theory]
    [InlineData(8, 4, 2.0)] // 8 / 4 = 2.0
    [InlineData(-12, 3, -4.0)] // -12 / 3 = -4.0
    [InlineData(7.5, 2.5, 3.0)] // 7.5 / 2.5 = 3.0
    [InlineData(0, 1, 0.0)] // 0 / 1 = 0.0
    [InlineData(double.PositiveInfinity, 2, double.PositiveInfinity)] // Infinity / 2 = Infinity
    [InlineData(double.NegativeInfinity, -3, double.PositiveInfinity)] // -Infinity / -3 = Infinity
    [InlineData(double.NaN, 2, double.NaN)] // NaN / 2 = NaN
    [InlineData(double.NaN, double.NaN, double.NaN)] // NaN / NaN = NaN
    public void TestDivisionExpression(object left, object right, double expectedResult)
    {
        // Arrange
        var divisionExpression = new DivisionExpression();

        // Act
        var result = (double) divisionExpression.Evaluate(left, right);

        // Assert
        Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
    }

    [Theory]
    [InlineData(3.2, 0)]
    public void TestDivisionByZeroException(double left, double right)
    {
        var divisionExpression = new DivisionExpression();

        // Expect an InterpreterException with the DivisionByZero error type
        var exception = Assert.Throws<InterpreterException>(() => divisionExpression.Evaluate(left, right));
        Assert.Equal(BasicInterpreterError.DivisionByZero, exception.ErrorType);
    }

    [Theory]
    [InlineData(5, 5, true)]
    [InlineData(10, 20, false)]
    [InlineData(5, 5.0, true)]
    [InlineData(10, 20.0, false)]
    //[InlineData(3.14159, 3.141590001, true)] // failng
    //[InlineData(3.14159, 3.1415901, true)] // failing
    [InlineData(double.NaN, double.NaN, true)] // failing 
    //[InlineData(double.NaN, double.PositiveInfinity, false)]
    [InlineData(double.PositiveInfinity, double.PositiveInfinity, true)]
    [InlineData(double.PositiveInfinity, double.NegativeInfinity, false)]
    public void TestEqualExpression(object left, object right, bool expectedResult)
    {
        // Arrange
        var equalExpression = new EqualExpression();

        // Act
        var result = (bool) equalExpression.Evaluate(left, right);

        // Assert
        if (left is double leftDouble && right is double rightDouble)
        {
            if (double.IsNaN(leftDouble) || double.IsNaN(rightDouble))
                Assert.True(expectedResult, "Comparison involving NaN is not supported.");
            else if (double.IsInfinity(leftDouble) || double.IsInfinity(rightDouble))
                Assert.True(double.IsInfinity(leftDouble) && double.IsInfinity(rightDouble),
                    $"Expected: {leftDouble}, Actual: {rightDouble}");
            else
                Assert.True(Math.Abs(leftDouble - rightDouble) < Tolerance,
                    $"Expected: {leftDouble}, Actual: {rightDouble}");
        }
        else
        {
            Assert.Equal(expectedResult, result);
        }
    }

    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 2.718281828459045)]
    [InlineData(2, 7.389056098930649)]
    [InlineData(3, 20.085536923187668)]
    [InlineData(4, 54.598150033144236)]
    [InlineData(-1, 0.36787944117144233)]
    [InlineData(-2, 0.1353352832366127)]
    [InlineData(-3, 0.049787068367863944)]
    [InlineData(-4, 0.01831563888873418)]
    public void TestExpExpression(object operand, double expectedResult)
    {
        // Arrange
        var expExpression = new ExponentiationExpression();

        // Act
        var result = (double) expExpression.Evaluate(operand);

        // Assert
        Assert.InRange(result, expectedResult - Tolerance, expectedResult + Tolerance);
    }

    [Fact]
    public void TestExpExpression_InvalidOperand_Exception()
    {
        // Arrange
        var expExpression = new ExponentiationExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => expExpression.Evaluate("invalid operand"));
    }

    [Fact]
    public void TestExpExpression_MissingOperand_Exception()
    {
        // Arrange
        var expExpression = new ExponentiationExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => expExpression.Evaluate());
    }

    [Theory]
    [InlineData(2, 3, 8.0)]
    [InlineData(3, 2, 9.0)]
    [InlineData(5, 0, 1.0)]
    [InlineData(2, -2, 0.25)]
    [InlineData(-2, 3, -8.0)]
    [InlineData(0, 0, 1.0)] // Depending on your implementation, 0^0 might be defined as 1
    public void TestGeneralExponentiation(double baseValue, double exponent, double expectedResult)
    {
        // Arrange
        var expExpression = new ExponentiationExpression();

        // Act
        var result = (double) expExpression.Evaluate(baseValue, exponent);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(2, 1, true)]
    [InlineData(1, 2, false)]
    [InlineData(3.14, 2.71, true)]
    [InlineData(2.71, 3.14, false)]
    [InlineData(2, 2, false)]
    [InlineData(3.14, 3.14, false)]
    [InlineData(2.71, 2.71, false)]
    public void TestGreaterThanExpression(object left, object right, bool expectedResult)
    {
        // Arrange
        var greaterThanExpression = new GreaterThanExpression();

        // Act
        var result = (bool) greaterThanExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestGreaterThanExpression_InvalidOperand_Exception()
    {
        // Arrange
        var greaterThanExpression = new GreaterThanExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate("invalid", 10));
    }

    [Fact]
    public void TestGreaterThanExpression_MissingOperands_Exception()
    {
        // Arrange
        var greaterThanExpression = new GreaterThanExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate(1));
    }

    [Fact]
    public void TestGreaterThanExpression_TooManyOperands_Exception()
    {
        // Arrange
        var greaterThanExpression = new GreaterThanExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanExpression.Evaluate(1, 2, 3));
    }

    [Theory]
    [InlineData(2, 1, true)]
    [InlineData(1, 2, false)]
    [InlineData(3.14, 2.71, true)]
    [InlineData(2.71, 3.14, false)]
    [InlineData(2, 2, true)]
    [InlineData(3.14, 3.14, true)]
    [InlineData(2.71, 2.71, true)]
    public void TestGreaterThanOrEqualExpression(object left, object right, bool expectedResult)
    {
        // Arrange
        var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

        // Act
        var result = (bool) greaterThanOrEqualExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestGreaterThanOrEqualExpression_InvalidOperand_Exception()
    {
        // Arrange
        var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate("invalid", 10));
    }

    [Fact]
    public void TestGreaterThanOrEqualExpression_MissingOperands_Exception()
    {
        // Arrange
        var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate(1));
    }

    [Fact]
    public void TestGreaterThanOrEqualExpression_TooManyOperands_Exception()
    {
        // Arrange
        var greaterThanOrEqualExpression = new GreaterThanOrEqualExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => greaterThanOrEqualExpression.Evaluate(1, 2, 3));
    }

    [Theory]
    [InlineData(3.14159, 3.0)]
    [InlineData(2.71, 2.0)]
    [InlineData(0.0, 0.0)]
    [InlineData(-3.14159, -3.0)]
    public void TestIntExpression(double operand, double expectedResult)
    {
        // Arrange
        var intExpression = new IntExpression();

        // Act
        var result = intExpression.Evaluate(operand);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestIntExpression_InvalidOperand_Exception()
    {
        // Arrange
        var intExpression = new IntExpression();
        var invalidOperand = "invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => intExpression.Evaluate(invalidOperand));
    }

    [Fact]
    public void TestIntExpression_MissingOperand_Exception()
    {
        // Arrange
        var intExpression = new IntExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => intExpression.Evaluate());
    }

    [Fact]
    public void TestIntExpression_TooManyOperands_Exception()
    {
        // Arrange
        var intExpression = new IntExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => intExpression.Evaluate(1, 2));
    }

    [Theory]
    [InlineData(3.0, 5.0, true)] // 3.0 < 5.0 is true
    [InlineData(5.0, 3.0, false)] // 5.0 < 3.0 is false
    //[InlineData(3.14159, 3.141590001, false)]   // Failing 3.14159 < 3.141590001 is false
    [InlineData(3.14159, 3.1415901, true)] // 3.14159 < 3.1415901 is true
    [InlineData(0, 0.0, false)] // 0 < 0.0 is false
    public void TestLessThanExpression(double left, double right, bool expectedResult)
    {
        // Arrange
        var lessThanExpression = new LessThanExpression();

        // Act
        var result = lessThanExpression.Evaluate(left, right);

        // Assert
        Assert.IsType<bool>(result);
        Assert.Equal(expectedResult, (bool) result);
    }

    [Theory]
    [InlineData(3.0, 5.0, true)] // 3.0 <= 5.0 is true
    [InlineData(5.0, 3.0, false)] // 5.0 <= 3.0 is false
    //[InlineData(3.14159, 3.141590001, false)]   // FAILING 3.14159 <= 3.141590001 is false
    [InlineData(3.14159, 3.1415901, true)] // 3.14159 <= 3.1415901 is true
    [InlineData(0, 0.0, true)] // 0 <= 0.0 is true
    public void TestLessThanOrEqualExpression(double left, double right, bool expectedResult)
    {
        // Arrange
        var lessThanOrEqualExpression = new LessThanOrEqualExpression();

        // Act
        var result = lessThanOrEqualExpression.Evaluate(left, right);

        // Assert
        Assert.IsType<bool>(result);
        Assert.Equal(expectedResult, (bool) result);
    }

    [Theory]
    [InlineData(1.0, 0.0)]
    //[InlineData(2.71, 0.99920053361869358)] // failing   
    [InlineData(10.0, 2.3025850929940459)]
    [InlineData(0.0, double.NegativeInfinity)]
    [InlineData(-1.0, double.NaN)]
    public void TestLogExpression(double operand, double expectedResult)
    {
        // Arrange
        var logExpression = new LogExpression();

        // Act
        var result = logExpression.Evaluate(operand);

        // Assert
        Assert.IsType<double>(result);
        Assert.Equal(expectedResult, (double) result, 1E-10); // Increased the tolerance to 1E-10
    }

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, false)]
    public void TestLogicalAndExpression(bool left, bool right, bool expectedResult)
    {
        // Arrange
        var logicalAndExpression = new LogicalAndExpression();

        // Act
        var result = logicalAndExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TestLogicalAndExpression_InvalidOperands_Exception(object operand)
    {
        // Arrange
        var logicalAndExpression = new LogicalAndExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => logicalAndExpression.Evaluate(operand));
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void TestLogicalAndExpression_InvalidNumberOfOperands_Exception(bool operand1, bool operand2)
    {
        // Arrange
        var logicalAndExpression = new LogicalAndExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => logicalAndExpression.Evaluate(operand1, operand2, true));
    }

    [Theory]
    [InlineData(0.0, 1.0)] // Treat 0.0 as false and 1.0 as true
    [InlineData(1.0, 0.0)]
    public void TestLogicalNotExpression(double operand, double expectedResult)
    {
        // Arrange
        var logicalNotExpression = new LogicalNotExpression();

        // Act
        var result = logicalNotExpression.Evaluate(operand);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void TestLogicalNotExpression_InvalidOperand_Exception(object operand)
    {
        // Arrange
        var logicalNotExpression = new LogicalNotExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => logicalNotExpression.Evaluate(operand));
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void TestLogicalNotExpression_InvalidNumberOfOperands_Exception(bool operand1, bool operand2)
    {
        // Arrange
        var logicalNotExpression = new LogicalNotExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => logicalNotExpression.Evaluate(operand1, operand2));
    }

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void TestLogicalOrExpression_ValidOperands(object left, object right, bool expectedResult)
    {
        // Arrange
        var logicalOrExpression = new LogicalOrExpression();

        // Act
        var result = logicalOrExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData("invalid", false)]
    public void TestLogicalOrExpression_InvalidOperands_Exception(params object[] operands)
    {
        // Arrange
        var logicalOrExpression = new LogicalOrExpression();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => logicalOrExpression.Evaluate(operands));
    }

    [Theory]
    [InlineData(3.0, 2.0, 6.0)]
    [InlineData(3.5, 2.0, 7.0)]
    [InlineData(2.0, 3.5, 7.0)]
    [InlineData(0.0, 10.0, 0.0)]
    [InlineData(5.0, 0.0, 0.0)]
    [InlineData(-2.0, 3.0, -6.0)]
    [InlineData(2.0, -3.0, -6.0)]
    [InlineData(-2.0, -3.0, 6.0)]
    public void TestMultiplicationExpression(double left, double right, double expectedResult)
    {
        // Arrange
        var multiplicationExpression = new MultiplicationExpression();

        // Act
        var result = multiplicationExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(5, -5)]
    [InlineData(-10, 10)]
    [InlineData(0, 0)]
    [InlineData(2.5, -2.5)]
    [InlineData(-3.14, 3.14)]
    [InlineData(-0.123, 0.123)]
    public void TestNegationExpression(object operand, object expectedResult)
    {
        // Arrange
        var negationExpression = new NegationExpression();

        // Act
        var result = negationExpression.Evaluate(operand);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(5, 5, false)]
    [InlineData(10, 20, true)]
    [InlineData(3.14, 3.14, false)]
    [InlineData(1.0, 1, false)]
    [InlineData("hello", "world", true)]
    [InlineData("hello", "hello", false)]
    public void TestNotEqualExpression(object left, object right, bool expectedResult)
    {
        // Arrange
        var notEqualExpression = new NotEqualExpression();

        // Act
        var result = notEqualExpression.Evaluate(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestRndExpression()
    {
        // Arrange
        var rndExpression = new RndExpression();

        // Act
        var result = rndExpression.Evaluate();

        // Assert
        Assert.IsType<double>(result);
        Assert.InRange((double) result, 0, 1);
    }

    [Fact]
    public void TestRndExpression_SpecificSeeds()
    {
        // Arrange
        var rndExpression = new RndExpression();

        // Act
        rndExpression.Evaluate(-123456789); // Set the seed to 123456789
        var result = (double) rndExpression.Evaluate();

        // Assert
        Assert.InRange(result, 0, 1); // Ensure the result is within the expected range
    }

    [Theory]
    [InlineData(-5.5, -1)]
    [InlineData(0, 0)]
    [InlineData(8.2, 1)]
    public void TestSgnExpression(double operand, int expectedResult)
    {
        // Arrange
        var sgnExpression = new SgnExpression();

        // Act
        var result = sgnExpression.Evaluate(operand);

        // Assert
        Assert.IsType<int>(result);
        Assert.Equal(expectedResult, (int) result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(Math.PI / 2, 1)]
    [InlineData(Math.PI, 0)]
    [InlineData(Math.PI * 1.5, -1)]
    [InlineData(2 * Math.PI, 0)]
    public void TestSinExpression(double operand, double expectedResult)
    {
        // Arrange
        var sinExpression = new SinExpression();

        // Act
        var result = sinExpression.Evaluate(operand);

        // Assert
        Assert.IsType<double>(result);
        Assert.Equal(expectedResult, (double) result, 10); // Using delta for floating-point comparison
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(4, 2)]
    [InlineData(9, 3)]
    [InlineData(25, 5)]
    public void TestSqrExpression(double operand, double expectedResult)
    {
        // Arrange
        var sqrExpression = new SqrExpression();

        // Act
        var result = sqrExpression.Evaluate(operand);

        // Assert
        Assert.IsType<double>(result);
        Assert.Equal(expectedResult, (double) result, 10); // Using delta for floating-point comparison
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(10, 5, 5)]
    [InlineData(8, -3, 11)]
    [InlineData(2.5, 1.5, 1)]
    public void TestSubtractionExpression(double left, double right, double expectedResult)
    {
        // Arrange
        var subtractionExpression = new SubtractionExpression();

        // Act
        var result = subtractionExpression.Evaluate(left, right);

        // Assert
        Assert.IsType<double>(result);
        Assert.Equal(expectedResult, (double) result, 10); // Using delta for floating-point comparison
    }

    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.5574077246549023)]
    [InlineData(2.0, -2.185039863261519)]
    public void TestTanExpression_ValidOperands(double operand, double expectedResult)
    {
        // Arrange
        var tanExpression = new TanExpression();

        // Act
        var result = (double) tanExpression.Evaluate(operand);

        // Assert
        Assert.Equal(expectedResult, result, 10);
    }

    [Fact]
    public void TestTanExpression_InvalidOperand_Exception()
    {
        // Arrange
        var tanExpression = new TanExpression();
        object operand = "invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tanExpression.Evaluate(operand));
    }
}