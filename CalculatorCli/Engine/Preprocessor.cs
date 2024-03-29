namespace CalculatorCli.Engine;

public class Preprocessor
{
    public string Process(string infixCalculationSegments)
    {
        var result = infixCalculationSegments
            .Replace("\t", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty)
            .Replace("x", CalculatorConstants.MultiplicationOperator.ToString())
            .Replace("×", CalculatorConstants.MultiplicationOperator.ToString())
            .Replace("÷", CalculatorConstants.DivisionOperator.ToString())
            .Replace("+", CalculatorConstants.AdditionOperator.ToString())
            .Replace("−", CalculatorConstants.SubtractionOperator.ToString())
            + CalculatorConstants.Space;

        var doubleSpace = new string(CalculatorConstants.Space, 2);
        while (result.Contains(doubleSpace))
            result = result.Replace(doubleSpace, CalculatorConstants.Space.ToString());

        return result;
    }
}
