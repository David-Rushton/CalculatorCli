namespace CalculatorCli.Commands;

public class CalculateCommand(Calculator calculator) : Command<CalculateCommand.Settings>
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
        catch (CalculatorException e)
        {
            AnsiConsole.WriteException(e);
            return 1;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
