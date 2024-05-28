namespace CalculatorCli.Engine.Abstractions;

/// <summary>
/// Thrown when the calculator cannot complete a calculation.
/// </summary>
public class CalculationException(string message) : Exception(message)
{ }
