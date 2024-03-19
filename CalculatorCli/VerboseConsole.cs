namespace CalculatorCli;

public class VerboseConsole
{
    private const string Space = " ";

    public static bool IsEnabled = Environment.GetCommandLineArgs().Contains("--verbose");

    public static void WriteLine(IEnumerable<string> value)
        => WriteLine(string.Join(Space, value));

    public static void WriteLine(string value)
    {
        if (IsEnabled)
            AnsiConsole.Write(new Markup($"[green italic]{value}[/]\n"));
    }
}
