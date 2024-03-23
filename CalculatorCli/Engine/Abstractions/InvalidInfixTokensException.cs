using CalculatorCli.Engine.DTOs;

namespace CalculatorCli.Engine.Abstractions;

/// <summary>
///   Thrown when the sequence of infix tokens cannot be parsed.
/// <example>
///   This normally occurs when there is a invalid sequence.
///   <code>
///     "1 + not-a-number"
///   </code>
/// </example>
/// </summary>
public class InvalidInfixTokensException(
    IEnumerable<CalculationToken> tokens,
    IEnumerable<InfixTokenIssue> issues): Exception(message: "Is the expression valid?"), ICalculatorException
{
    public readonly IEnumerable<CalculationToken> Tokens = tokens;
    public readonly IEnumerable<InfixTokenIssue> Issues = issues;
}
