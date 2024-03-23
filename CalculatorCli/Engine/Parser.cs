using CalculatorCli.Engine.Abstractions;
using System.Text;

namespace CalculatorCli.Engine;

public class Parser(InfixValidator validator, Preprocessor preprocessor, CalculationBuilder calculationBuilder)
{
    public IEnumerable<CalculationToken> Parse(string infixStatement)
    {
        var infixExpression = preprocessor.Process(infixStatement);

        if (!validator.IsExpressionValid(infixExpression, out var issue))
            throw new InvalidInfixExpressionException(issue.Message, infixExpression.Trim(), issue.Positions);

        var tokens = GetTokens(infixExpression).ToList();

        if (!validator.HasValidInfixTokenSequence(tokens, out var issues))
            throw new InvalidInfixTokensException(tokens, issues);

        return tokens;
    }

    private IEnumerable<CalculationToken> GetTokens(string infixExpression)
    {
        var numberBuffer = new StringBuilder();
        var numberBufferStart = -1;

        VerboseConsole.WriteLine($"canonical infix notation: {infixExpression}");

        foreach (var i in Enumerable.Range(0, infixExpression.Length))
        {
            var character = infixExpression[i];
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
