namespace CalculatorCli.Engine;

public class Preprocessor
{
    public string Process(string infixCalculationSegments) =>
        infixCalculationSegments
            .Replace("\n", string.Empty)
            .Replace("\t", string.Empty)
            .Replace("x", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("×", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("÷", CalculatorConstants.DivideOperator.ToString())
            .Replace("%", CalculatorConstants.DivideOperator.ToString())
            .Replace("+", CalculatorConstants.AddOperator.ToString())
            .Replace("−", CalculatorConstants.SubtractOperator.ToString())
            + CalculatorConstants.Space;
}
