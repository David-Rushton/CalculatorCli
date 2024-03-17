namespace CalculatorCli.Engine.Extensions;

public static class StackExtensions
{
    public static double PopAndParse(this Stack<CalculationToken> tokens)
    {
        if (tokens.TryPop(out var current))
            return DoubleExtensions.Parse(current);

        throw new CalculatorException(position: -1, $"Unexpected end of calculation.  Check calculation and try again.");
    }
}
