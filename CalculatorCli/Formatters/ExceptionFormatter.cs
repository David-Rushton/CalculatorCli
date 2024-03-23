using System.Text;

using CalculatorCli.Engine.Abstractions;

namespace CalculatorCli.Formatters;

public class ExceptionFormatter
{
    private const string RedCurlyUnderline = "\x1b[58:2::172:0:0m\x1b[4:3m";
    private const string AnsiReset = "\x1b[0m";

    public void PrettyPrint(InvalidInfixExpressionException e)
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

    public void PrettyPrint(InvalidInfixTokensException e)
    {

    }
}
