using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using CalculatorCli.Engine.DTOs;

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
        var colours = new[]
        {
            "255:100:100",
            "100:255:100",
            "255:0:255",
            "255:255:100",
            "172:172:172"
        };
        var issueColour = new Dictionary<string, string>();

        var distinctIssues = e.InvalidTokens.Select(it => it.Key).Distinct();
        foreach (var issue in distinctIssues)
            issueColour[issue] = colours[issueColour.Count % 5];

        var invalidTokens = e.InvalidTokens.Values.Select(v => v);

        var buffer = new StringBuilder();

        foreach (var token in e.Tokens)
        {
            if (invalidTokens.Contains(token))
            {
                // TODO: Refactor this mess.
                // May require a 2 pass solution, where we wrap tokens.
                var issue = " ?????? ";
                var underlineRgb = CurlyUnderline.Replace("<RGB>", issueColour[issue]);
                buffer.Append($"{underlineRgb}{token.Value}{AnsiReset}");
            }
            else
            {
                buffer.Append(token.Value);
            }

            buffer.Append(CalculatorConstants.Space);
        }

        AnsiConsole.WriteLine(buffer.ToString());

        foreach (var issue in distinctIssues)
        {
            var issueRgb = issueColour[issue].Replace(":", ",");
            AnsiConsole.MarkupLine($"[red]Error[/] [rgb({issueRgb})]{issue}[/]");
        }
    }

    public void PrettyPrint(MissingInfixExpressionException e) =>
        AnsiConsole.MarkupLineInterpolated($"[red]Error[/] {e.Message}");
}
