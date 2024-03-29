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
public class InvalidInfixExpressionException(
    IEnumerable<CalculationToken> tokens,
    List<(CalculationToken token, string issue)> tokenIssues): Exception(message: "Is the expression valid?")
{
    public readonly IEnumerable<CalculationToken> Tokens = tokens;
    public readonly List<(CalculationToken token, string issue)> TokenIssues = tokenIssues;
}
