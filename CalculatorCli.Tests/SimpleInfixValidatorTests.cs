using System.Runtime.CompilerServices;

namespace CalculatorCli.Tests;

public class SimpleInfixValidatorTests
{
    private SimpleInfixValidator _validator;

    [SetUp]
    public void Setup() =>
        _validator = new();

    [Test]
    public void Validate_Fails_WhenStatementContainsInvalidCharacters()
    {
        var infixStatement = "abc";

        var result = _validator.Validate(infixStatement, out var message);
        var expectedMessage = @"There are problems with the statement:
- Statement contains invalid characters: a, b and c
";

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(message, Is.EqualTo(expectedMessage));
        });
    }
}
