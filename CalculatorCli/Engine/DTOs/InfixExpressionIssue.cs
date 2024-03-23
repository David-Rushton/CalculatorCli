namespace CalculatorCli.Engine.DTOs;

/// <summary>
/// Describes a validation failure.
/// </summary>
/// <param name="Message">Reason the expression is invalid.  Empty when valid.</param>
/// <param name="Positions">Location of characters that failed validation.</param>
public readonly record struct InfixExpressionIssue(
    string Message,
    IEnumerable<int> Positions);
