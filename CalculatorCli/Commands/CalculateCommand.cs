namespace CalculatorCli.Commands;

public class CalculateCommand(
    Calculator calculator,
    CalculationExceptionFormatter exceptionFormatter) : Command<CalculateCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(position: 0, "<infixStatement>")]
        [Description("Expressed in [link=https://en.wikipedia.org/wiki/Infix_notation]infix notation[/]")]
        public required string InfixStatement { get; init; }

        [CommandOption("--verbose")]
        [Description("Shows our working")]
        public bool IsVerbose { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            var result = calculator.Calculate(settings.InfixStatement);
            AnsiConsole.Write(new Markup($"[blue]{result}[/]"));
            return 0;
        }
        catch (Exception e)
        {
            if (e is InvalidInfixExpressionException infix)
            {
                exceptionFormatter.PrettyPrint(infix);
                return 1;
            }

            AnsiConsole.WriteException(e);
            return 1;
        }
    }
}
