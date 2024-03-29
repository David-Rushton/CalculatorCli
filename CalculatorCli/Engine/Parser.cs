namespace CalculatorCli.Engine;

public class Parser(InfixValidator validator, Preprocessor preprocessor, CalculationBuilder calculationBuilder)
{
    /// <summary>
    /// Converts an infix expression into a series of tokens.
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="preprocessor"></param>
    /// <param name="calculationBuilder"></param>
    /// <exception cref="InvalidInfixCharactersException"/>
    /// <exception cref="InvalidInfixExpressionException"/>
    /// <exception cref="MissingInfixExpressionException"/>
    public IEnumerable<CalculationToken> Parse(string infixStatement)
    {
        var infixExpression = preprocessor.Process(infixStatement);

        if (infixExpression.Trim().Length == 0)
            throw new MissingInfixExpressionException();

        if (!validator.ContainsInvalidCharacters(infixExpression, out var invalidPositions))
            throw new InvalidInfixCharactersException(infixExpression, invalidPositions);

        var tokens = GetTokens(infixExpression).ToList();

        if (!validator.ContainsInvalidTokenSequence(tokens, out var invalidTokens))
            throw new InvalidInfixExpressionException(tokens, invalidTokens);

        return tokens;
    }

    private IEnumerable<CalculationToken> GetTokens(string infixExpression)
    {
        var numberBuffer = new StringBuilder();
        var numberBufferStart = -1;
        var lastIsOperator = false;

        VerboseConsole.WriteLine($"canonical infix notation: {infixExpression}");

        foreach (var i in Enumerable.Range(0, infixExpression.Length))
        {
            var character = infixExpression[i];
            var position = i + 1;

            if (ShouldYieldBuffer(character))
            {
                if (numberBuffer.Length > 0)
                {
                    calculationBuilder.AddNumber(numberBufferStart, numberBuffer.ToString().Trim());
                    numberBufferStart = -1;
                    numberBuffer.Clear();
                }

                if (CalculatorConstants.Operators.Contains(character))
                {
                    if (lastIsOperator && CalculatorConstants.UnaryOperators.Contains(character))
                    {
                        calculationBuilder.AddUnaryOperator(position, character);
                        continue;
                    }

                    calculationBuilder.AddBinaryOperator(position, character);
                    lastIsOperator = true;
                }

                if (CalculatorConstants.Parentheses.Contains(character))
                {
                    calculationBuilder.AddParenthesis(position, character);
                    lastIsOperator = false;
                }

                // Whitespace is swallowed.
                continue;
            }

            lastIsOperator = false;

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
