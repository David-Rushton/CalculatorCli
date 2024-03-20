using System.Text;
using System.Text.RegularExpressions;

namespace CalculatorCli.Engine;

public class SimpleInfixValidator
{
    private readonly Regex _validCharacters = new(@"^\s*=?[\+\-\*\/^\d\.\s()]+$");

    /// <summary>
    ///   Performs fast, but simple, high level validation on a infix statement.
    ///   Checks:
    ///   <list type="bullet">
    ///     <item>Count of opening and closing parentheses match</item>
    ///     <item>Infix statement only contains valid characters</item>
    ///   </list>
    ///   <remarks>
    ///   There are lots of cases the simple validator does not handle.
    ///   </remarks>
    /// </summary>
    public bool Validate(string infixStatement, out string reason)
    {
        var reasonBuilder = new StringBuilder();

        var invalidCharacters = GetInvalidCharacters(infixStatement);
        if (invalidCharacters.Length > 0)
            reasonBuilder.AppendLine($"- Statement contains invalid characters: {invalidCharacters}");

        var leftParentheses = infixStatement.Count(s => s == CalculatorConstants.LeftParentheses);
        var rightParentheses = infixStatement.Count(s => s == CalculatorConstants.RightParentheses);

        if (leftParentheses > rightParentheses)
            reasonBuilder.AppendLine("- Missing closing parentheses");

        if (rightParentheses > leftParentheses)
            reasonBuilder.AppendLine("- Missing opening parentheses");

        reason = reasonBuilder.Length > 0
            ? $"There are problems with the statement:\n{reasonBuilder.ToString()}"
            : string.Empty;

        return reasonBuilder.Length == 0;
    }

    private string GetInvalidCharacters(string infixStatement)
    {
        var invalidCharacters = infixStatement;

        foreach (var validCharacter in CalculatorConstants.ValidCharacters)
            invalidCharacters = invalidCharacters.Replace(validCharacter.ToString(), string.Empty);

        // humanises the output.
        // E.g Converts ABC to A,B and C.
        invalidCharacters = string.Join(", ", invalidCharacters.ToArray());
        var lastComma = invalidCharacters.LastIndexOf(",");
        if (lastComma > 0)
            invalidCharacters = $"{invalidCharacters[0..lastComma]} and{invalidCharacters[lastComma..]}";

        return invalidCharacters;
    }
}
