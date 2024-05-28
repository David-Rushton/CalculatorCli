namespace CalculatorCli.Engine.Abstractions;

public class CalculatorException(int position, string message) : Exception(message)
{
    public int Position { get; } = position;
}
