using System.ComponentModel.DataAnnotations;

namespace CalculatorCli.Tests;

public class ParserTests
{
    private Parser _parser;

    [SetUp]
    public void Setup()
    {
        _parser = new Parser(new Engine.InfixValidator(), new Preprocessor(), new CalculationBuilder());
    }

    [Test]
    public void Parse_WhenWellFormed_ReturnsExpectedTokens()
    {
        var tokens = _parser.Parse("3 + 4");
        var expected = new[]
        {
            new CalculationToken(Position: 1, TokenType.Operand, "3"),
            new CalculationToken(Position: 3, TokenType.BinaryOperator, "+"),
            new CalculationToken(Position: 5, TokenType.Operand, "4")
        };

        Assert.That(tokens, Is.EqualTo(expected));
    }

    [Test]
    public void Parse_WhenContainsInvalidCharacters_ThrowsCalculatorException()
    {
        Assert.Throws<InvalidInfixCharactersException>(() => _parser.Parse("this is not a sum"));
    }
}
