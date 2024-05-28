namespace CalculatorCli.Engine.Extensions;

public static class StackExtensions
{
    public static double PopAndParse(this Stack<CalculationToken> tokens)
    {
        if (tokens.TryPop(out var current))
            return DoubleExtensions.Parse(current);

        throw new CalculationException(
            "Calculation failed.  Unexpected end of numbers.  Have you entered a valid expression?");
    }
}
