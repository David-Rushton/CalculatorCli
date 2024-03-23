using System.Runtime.CompilerServices;

using NUnit.Framework.Internal.Commands;

namespace CalculatorCli.Tests;

public class SimpleInfixValidatorTests
{
    private InfixValidator _validator;

    [SetUp]
    public void Setup() =>
        _validator = new();

    [Test()]
    public void Validate_Fails_WhenStatementContainsInvalidCharacters()
    {
        var infixStatement = "abc";

        var result = _validator.IsExpressionValid(infixStatement, out var message);
        var expectedMessage = @"There are problems with the statement:
- Statement contains invalid characters: a, b and c
";

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(message.Message, Is.EqualTo(expectedMessage));
        });
    }
}
