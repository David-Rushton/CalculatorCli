namespace CalculatorCli.Engine.Abstractions;

/// <summary>
/// Thrown when the user does not provide an infix expression.
/// </summary>
public class MissingInfixExpressionException : Exception
{
    public MissingInfixExpressionException()
        : base("What would you like to calculate?  Tip: Not sure what to do?  Add --help to the end of the command.")
    { }
}
