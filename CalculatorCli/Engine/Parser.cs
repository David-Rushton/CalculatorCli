using System.Text;

namespace CalculatorCli.Engine;

public class Parser(Preprocessor preprocessor, CalculationBuilder calculationBuilder)
{
    public IEnumerable<CalculationToken> Parse(IEnumerable<string> infixCalculationSegments)
    {
        var numberBuffer = new StringBuilder();
        var numberBufferStart = -1;
        var infixCalculation = preprocessor.Process(infixCalculationSegments);

        foreach (var i in Enumerable.Range(0, infixCalculation.Length))
        {
            var character = infixCalculation[i];
            var position = i + 1;

            if (!CalculatorConstants.ValidCharacters.Contains(character))
                throw new CalculatorException(position, $"Invalid character. {character} is not valid calculation character.  Please check calculation and try again.");

            if (ShouldYieldBuffer(character))
            {
                if (numberBuffer.Length > 0)
                {
                    calculationBuilder.AddNumber(numberBufferStart, numberBuffer.ToString().Trim());
                    numberBufferStart = -1;
                    numberBuffer.Clear();
                }

                if (CalculatorConstants.Operators.Contains(character))
                    calculationBuilder.AddOperator(position, character);

                if (CalculatorConstants.Parentheses.Contains(character))
                    calculationBuilder.AddParenthesis(position, character);

                // Whitespace is swallowed.
                continue;
            }

            if (numberBufferStart == -1)
                numberBufferStart = position;

            numberBuffer.Append(character);
        }

        return calculationBuilder.Build();

        static bool ShouldYieldBuffer(char character) =>
            CalculatorConstants.Parentheses.Contains(character)
            || CalculatorConstants.Operators.Contains(character)
            || character is CalculatorConstants.Space;
    }
}
