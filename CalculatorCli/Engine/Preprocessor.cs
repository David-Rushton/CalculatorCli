namespace CalculatorCli.Engine;

public class Preprocessor
{
    public string Process(IEnumerable<string> infixCalculationSegments) =>
        string.Join(CalculatorConstants.Space.ToString(), infixCalculationSegments)
            .Replace("x", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("×", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("÷", CalculatorConstants.DivideOperator.ToString())
            .Replace("%", CalculatorConstants.DivideOperator.ToString())
            .Replace("+", CalculatorConstants.AddOperator.ToString())
            .Replace("−", CalculatorConstants.SubstractOperator.ToString())
            .Replace("\n", string.Empty)
            .Replace("\t", string.Empty)
            + CalculatorConstants.Space;
}
