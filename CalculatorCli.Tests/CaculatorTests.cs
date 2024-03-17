namespace CalculatorCli.Tests;

public class CalculatorTests
{
    private Calculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator(new Parser(new Preprocessor(), new CalculationBuilder()));
    }

    [Test]
    [TestCase("1 + 1", 2)]
    [TestCase("1+1", 2)]
    [TestCase("1+(1)", 2)]
    [TestCase("1+((1))", 2)]
    [TestCase("1 + 3", 4)]
    [TestCase("1 + 3 * 2", 7)]
    [TestCase("1 + (3 * 2)", 7)]
    [TestCase("(1 + 3) * 2", 8)]
    [TestCase("3 + 4 x 2 % ( 1 - 5 ) ^ 2 ^ 3", 11)]
    public void Calculate_WhenWellFormed_ReturnsExpectedResult(string calculation, double expected)
    {
        var actual = _calculator.Calculate([calculation]);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1 +")]
    [TestCase("1 + y")]
    [TestCase("1 + (2 +")]
    [TestCase("1 + )")]
    public void Calculate_WhenMalformed_ThrowsCalculatorException(string calculation)
    {
        Assert.Throws<CalculatorException>(() => _calculator.Calculate([calculation]));
    }
}