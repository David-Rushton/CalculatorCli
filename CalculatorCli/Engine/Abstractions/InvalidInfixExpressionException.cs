using CalculatorCli.Engine.DTOs;

namespace CalculatorCli.Engine.Abstractions;

/// <summary>
/// Thrown when an invalid infix expression is encountered.
/// </summary>
public class InvalidInfixExpressionException(
    string message,
    string infixExpression,
    IEnumerable<int> invalidCharacterPositions) : Exception(message), ICalculatorException
{
    public string InfixExpression = infixExpression;
    public HashSet<int> InvalidCharacterPositions = new(invalidCharacterPositions);
}
