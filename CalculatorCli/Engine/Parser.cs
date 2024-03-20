using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Transactions;

namespace CalculatorCli.Engine;

public class Parser(SimpleInfixValidator validator, Preprocessor preprocessor, CalculationBuilder calculationBuilder)
{
    public IEnumerable<CalculationToken> Parse(string infixStatement)
    {


        var infixCalculation = preprocessor.Process(infixStatement);

        if (!ParenthesesBalanced(infixCalculation))
            throw new CalculatorException(position: 1, $"Unbalanced parentheses.  Check calculation and try again.");


        // @"^\s*=?[\+\-\*\/^\d\.\s()]+$"
        // valid notation characters

        // 1 + 2 + ( 2 * ( 6 * 8 ) )
        // 1 + ((2 * 3) + (5 +  3))
        // 1 + ( 2 * 3 ) / 4

        // num  > op lp rp
        // op   > num lp
        // lp   > lp num op
        // rp   > op rp


        // convert to postfix
        // read
        // loop
        //  operand push
        //  number pop two
        //  insert two with op inside parentheses
        //  push back to stack


        return GetTokens(infixCalculation);

        static bool ParenthesesBalanced(string infixCalculation) =>
            infixCalculation.Count(c => c == CalculatorConstants.LeftParentheses)
                == infixCalculation.Count(c => c == CalculatorConstants.RightParentheses);
    }

    private IEnumerable<CalculationToken> GetTokens(string infixCalculation)
    {
        var numberBuffer = new StringBuilder();
        var numberBufferStart = -1;

        VerboseConsole.WriteLine($"canonical infix notation: {infixCalculation}");

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
