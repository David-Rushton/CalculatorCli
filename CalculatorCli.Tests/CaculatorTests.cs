namespace CalculatorCli.Tests;

public class CalculatorTests
{
    private Calculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator(new Parser(new InfixValidator(), new Preprocessor(), new CalculationBuilder()));
    }

    [Test]
    [TestCase("1 + 1", 2)]
    [TestCase("1 + -1", 0)]
    [TestCase("1+1", 2)]
    [TestCase("1/2", .5)]
    [TestCase("1+(1)", 2)]
    [TestCase("1+((1))", 2)]
    [TestCase("10 / 2", 5)]
    [TestCase("2 / 10", .2)]
    [TestCase("1 + 3", 4)]
    [TestCase("1 + 3 * 2", 7)]
    [TestCase("1 + (3 * 2)", 7)]
    [TestCase("(1 + 3) * 2", 8)]
    [TestCase("2 ^ 5", 32)]
    [TestCase("1 - 5", -4)]
    [TestCase("5 - 1", 4)]
    [TestCase("3 + 4 * 2 / (1 - 5)", 1)]
    [TestCase("3 + 4 * 2 / (1 - 5) ^ 2 ^ 3", 3.0001220703125)]
    public void Calculate_WhenWellFormed_ReturnsExpectedResult(string infixStatement, double expected)
    {
        var actual = _calculator.Calculate(infixStatement);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("12 -(2 *3)", 6)]
    [TestCase("12 + + + -(2 *3)", 6)]
    [TestCase("12 + - + + -(
        2 *3)", 18)]
    public void Calculate_WhenWellFormedWithUnary_ReturnsExpectedResult(string infixStatement, double expected)
    {
        var actual = _calculator.Calculate(infixStatement);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    // TODO: Should this throw?
    [TestCase("1 +")]
    [TestCase("1 + y")]
    public void Calculate_WhenMalformed_ThrowsInvalidInfixCharactersException(string infixStatement)
    {
        Assert.Throws<InvalidInfixCharactersException>(() => _calculator.Calculate(infixStatement));
    }

    [Test]
    [TestCase("1 + )")]
    [TestCase("(()) 1 + 1")]
    public void Calculate_WhenMalformed_InvalidInfixCharactersException(string infixStatement)
    {

        Assert.Throws<InvalidInfixExpressionException>(() => _calculator.Calculate(infixStatement));
    }
}
