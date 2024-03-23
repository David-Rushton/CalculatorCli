namespace CalculatorCli.Engine;

public class Preprocessor
{
    public string Process(string infixCalculationSegments)
    {
        var result = infixCalculationSegments
            .Replace("\t", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty)
            .Replace("x", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("×", CalculatorConstants.MultiplyOperator.ToString())
            .Replace("÷", CalculatorConstants.DivideOperator.ToString())
            .Replace("%", CalculatorConstants.DivideOperator.ToString())
            .Replace("+", CalculatorConstants.AddOperator.ToString())
            .Replace("−", CalculatorConstants.SubtractOperator.ToString())
            + CalculatorConstants.Space;

        var doubleSpace = new string(CalculatorConstants.Space, 2);
        while (result.Contains(doubleSpace))
            result = result.Replace(doubleSpace, CalculatorConstants.Space.ToString());

        return result;
    }
}
