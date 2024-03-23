namespace CalculatorCli.Engine.DTOs;

/// <summary>
/// Describes a validation failure.
/// </summary>
/// <param name="Message">Reason the expression is invalid.  Empty when valid.</param>
/// <param name="Positions">Tokens that failed validation.</param>
public readonly record struct InfixTokenIssue(
    string Message,
    CalculationToken Tokens);
