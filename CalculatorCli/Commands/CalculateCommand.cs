namespace CalculatorCli.Commands;

public class CalculateCommand(
    Calculator calculator,
    CalculationExceptionFormatter exceptionFormatter) : Command<CalculateCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(position: 0, "<infixExpression>")]
        [Description("Expressed in [link=https://en.wikipedia.org/wiki/Infix_notation]infix notation[/]")]
        public required string InfixExpression { get; init; }

        [CommandOption("--verbose")]
        [Description("Shows our working")]
        public bool IsVerbose { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            var result = calculator.Calculate(settings.InfixExpression);
            AnsiConsole.Write(new Markup($"[blue]{result}[/]"));
            return 0;
        }
        catch (Exception e)
        {
            exceptionFormatter.PrettyPrint(e, settings.IsVerbose);
            return 1;
        }
    }
}
