namespace CalculatorCli;

public static class VerboseConsole
{
    public static bool IsEnabled = Environment.GetCommandLineArgs().Contains("--verbose");

    public static void WriteLine() =>
        WriteLine(string.Empty);

    public static void WriteLine(double value) =>
        WriteLine(value.ToString());

    public static void WriteLine(IEnumerable<string> value) =>
        WriteLine(string.Join(CalculatorConstants.Space.ToString(), value));

    public static void WriteLine(string value)
    {
        if (IsEnabled)
            AnsiConsole.Write(new Markup($"[green italic]{value.EscapeMarkup()}[/]\n"));
    }
}
