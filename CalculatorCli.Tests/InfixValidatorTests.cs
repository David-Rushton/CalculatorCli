using System.Runtime.CompilerServices;

using NUnit.Framework.Internal.Commands;

namespace CalculatorCli.Tests;

public class InfixValidatorTests
{
    private InfixValidator _validator;

    [SetUp]
    public void Setup() =>
        _validator = new();

    [Test]
    [TestCase("a + b", new[] {0, 4})]
    [TestCase("1 + 2 =", new[] {6})]
    [TestCase("1 + banana", new[] {4, 5, 6, 7, 8, 9})]
    [TestCase("not an infix expression", new[] {0, 1, 2, 4, 5, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22})]
    public void ContainsInvalidCharacters_WhenExpressionContainsInvalidCharacters_ReturnsFalse(
        string infixExpression, IEnumerable<int> expectedPositions)
    {
        var result = _validator.ContainsInvalidCharacters(infixExpression, out var actualPositions);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(actualPositions, Is.EqualTo(expectedPositions));
        });
    }

    [Test]
    [TestCase("0 1 2 3 4 5 6 7 8 9")]
    [TestCase("0.0 1.1 2.2 3.14")]
    [TestCase("+ - / * ^")]
    [TestCase("()")]
    [TestCase(".")]
    [TestCase("(1.1 + 2 - 3 / 4 * 5) ^ 6")]
    public void ContainsInvalidCharacters_WhenExpressionContainsOnlyValidCharacters_ReturnsTrue(
        string infixExpression)
    {
        var result = _validator.ContainsInvalidCharacters(infixExpression, out var actualPositions);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(actualPositions, Is.EqualTo(Array.Empty<int>()));
        });
    }

    [Test]
    public void ContainsInvalidTokenSequence_Fails_WhenParenthesesLeftOpen()
    {
        var tokens = GetTokens("1 + (");
        var result  =_validator.ContainsInvalidTokenSequence(tokens, out var invalidTokens);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(invalidTokens.Count(), Is.EqualTo(1));
            Assert.That(invalidTokens.First().token.IsLeftParenthesis, Is.True);
        });
    }

    [Test]
    public void ContainsInvalidTokenSequence_Fails_WhenParenthesesUnbalanced()
    {
        // Unbalanced parentheses + missing closing parenthesis.
        var tokens = GetTokens("1 + ( ( 1 + )");
        var result  =_validator.ContainsInvalidTokenSequence(tokens, out var invalidTokens);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(invalidTokens.Count(), Is.EqualTo(2));
            Assert.That(invalidTokens.First().token.IsRightParenthesis, Is.True);
        });
    }

    // A very simple parser.
    // Elements must be separated by spaces.
    // I.e. 1+1 fails but 1 + 1 does not.
    public IEnumerable<CalculationToken> GetTokens(string infixExpression) =>
        GetTokens(infixExpression.Split(CalculatorConstants.Space));

    public IEnumerable<CalculationToken> GetTokens(IEnumerable<string> values)
    {
        foreach(var value in values)
        {
            if (double.TryParse(value, out _))
            {
                yield return new CalculationToken(Position: -1, TokenType.Number, value);
                continue;
            }

            yield return value switch
            {
                "+" => new CalculationToken(Position: -1, TokenType.Operator, value),
                "-" => new CalculationToken(Position: -1, TokenType.Operator, value),
                "/" => new CalculationToken(Position: -1, TokenType.Operator, value),
                "*" => new CalculationToken(Position: -1, TokenType.Operator, value),
                "^" => new CalculationToken(Position: -1, TokenType.Operator, value),
                "(" => new CalculationToken(Position: -1, TokenType.LeftParenthesis, value),
                ")" => new CalculationToken(Position: -1, TokenType.RightParenthesis, value),
                _ => throw new ArgumentOutOfRangeException(nameof(value), $"Expected value infix character.  Not: {value}.")
            };
        }
    }
}
