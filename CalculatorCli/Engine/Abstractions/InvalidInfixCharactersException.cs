using CalculatorCli.Engine.DTOs;

namespace CalculatorCli.Engine.Abstractions;

/// <summary>
/// Throw when an infix statement contains invalid characeters.
/// </summary>
public class InvalidInfixCharactersException(
    string infixExpression,
    IEnumerable<int> invalidCharacterPositions) : Exception("Expression contains invalid characers.")
{
    public string InfixExpression = infixExpression;
    public HashSet<int> InvalidCharacterPositions = new(invalidCharacterPositions);
}
