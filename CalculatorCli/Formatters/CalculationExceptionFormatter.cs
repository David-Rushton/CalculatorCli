namespace CalculatorCli.Formatters;

/// <summary>
/// Pretty prints custom exceptions.
/// </summary>
public class CalculationExceptionFormatter
{
    private const string RedCurlyUnderline = "\x1b[58:2::255:100:100m\x1b[4:3m";
    private const string CurlyUnderline = "\x1b[58:2::<RGB>m\x1b[4:3m";
    private const string AnsiReset = "\x1b[0m";

    public void PrettyPrint(Exception e, bool includeStackTrace)
    {
        switch (e)
        {
            //TODO: Add support for CalculatorException
            // Or fold exception into InvalidInfixCharactersException.
            case InvalidInfixCharactersException invalidCharactersException:
                PrettyPrint(invalidCharactersException);
                break;
            case InvalidInfixExpressionException expressionException:
                PrettyPrint(expressionException);
                break;
            case MissingInfixExpressionException missingExpressionException:
                PrettyPrint(missingExpressionException);
                break;
            default:
                AnsiConsole.WriteException(e, ExceptionFormats.ShortenEverything);
                break;
        }

        if (includeStackTrace && e.StackTrace != null)
            AnsiConsole.WriteLine(e.StackTrace);
    }

    public void PrettyPrint(InvalidInfixCharactersException e)
    {
        var buffer = new StringBuilder();
        var isUnderlineActive = false;
        var i = 0;

        while (i < e.InfixExpression.Length)
        {
            if (e.InvalidCharacterPositions.Contains(i))
            {
                if (!isUnderlineActive)
                {
                    buffer.Append(RedCurlyUnderline);
                    isUnderlineActive = true;
                }
            }
            else
            {
                if (isUnderlineActive)
                {
                    buffer.Append(AnsiReset);
                    isUnderlineActive = false;
                }
            }

            buffer.Append(e.InfixExpression[i]);

            i++;
        }

        buffer.Append(AnsiReset);

        AnsiConsole.WriteLine(buffer.ToString());
        AnsiConsole.MarkupLineInterpolated($"[red]Error[/] {e.Message}");
    }

    public void PrettyPrint(InvalidInfixExpressionException e)
    {
        // Each issue is underlined with a distinct colour.
        // We cannot underline a single token with two colours.
        // We _could_ merge issues (i.e. red = x, blue = y, green = x & y).
        // We _could_ use other styles (i.e bold = x, blue = y, bold blue = x & y).
        // But I suspect this will make the input harder to read.
        // For now we pick the first issue reported for any token and discard the rest.

        var tokenIssues = e
            .TokenIssues
            .GroupBy(ti => ti.token)
            .ToDictionary(k => k.Key, v => v.First().issue);
        var invalidTokens = tokenIssues.Select(ti => ti.Key).ToList();
        var issues = tokenIssues.Select(ti => ti.Value).ToList();
        var issueColours = GetIssueColours(issues);
        var buffer = new StringBuilder();

        // build output.
        foreach (var token in e.Tokens)
        {
            if (invalidTokens.Contains(token))
            {
                var issue = tokenIssues[token];
                var underlineRgb = CurlyUnderline.Replace("<RGB>", issueColours[issue]);
                buffer.Append($"{underlineRgb}{token.Value}{AnsiReset}");
            }
            else
            {
                buffer.Append(token.Value);
            }

            buffer.Append(CalculatorConstants.Space);
        }

        // write.
        AnsiConsole.WriteLine(buffer.ToString());

        foreach (var issue in issues)
        {
            var issueRgb = issueColours[issue].Replace(":", ",");
            AnsiConsole.MarkupLine($"[rgb({issueRgb})]Error[/] {issue}[/]");
        }

        // Assigns a colour to each issue.
        static Dictionary<string, string> GetIssueColours(IEnumerable<string> issues)
        {
            string[] colours = [
                "255:100:100",
                "100:255:100",
                "255:0:255",
                "255:255:100",
                "172:172:172"];

            var issueColour = new Dictionary<string, string>();

            foreach (var issue in issues)
                issueColour[issue] = colours[issueColour.Count % 5];

            return issueColour;
        }
    }

    public void PrettyPrint(MissingInfixExpressionException e) =>
        AnsiConsole.MarkupLineInterpolated($"[red]Error[/] {e.Message}");
}
