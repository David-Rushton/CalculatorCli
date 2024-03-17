namespace CalculatorCli.Engine.Extensions;

public static class DoubleExtensions
{
    public static double Parse(CalculationToken token) =>
        !double.TryParse(token.Value, out var number)
            ? throw new CalculatorException(token.Position, $"Expected a number.  {token.Value} is not a number.  Correct the calculation and try again.")
            : number;
}
