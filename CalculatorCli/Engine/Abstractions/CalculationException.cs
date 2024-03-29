namespace CalculatorCli.Engine.Abstractions;

public class CalculationException(string message) : Exception(message)
{ }
