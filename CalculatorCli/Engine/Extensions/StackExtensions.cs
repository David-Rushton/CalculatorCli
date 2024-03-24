namespace CalculatorCli.Engine.Extensions;

public static class StackExtensions
{
    public static double PopAndParseOrDefault(this Stack<CalculationToken> tokens)
    {
        if (tokens.TryPop(out var current))
            return DoubleExtensions.Parse(current);

        return 0;
    }
}
