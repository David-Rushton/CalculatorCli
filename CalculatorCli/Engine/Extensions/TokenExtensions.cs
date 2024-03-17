namespace CalculatorCli;

public static class TokenExtensions
{
    /// <summary>
    /// Converts a series of infix notation tokens to reverse Polish notation tokens.
    /// </summary>
    public static IEnumerable<CalculationToken> ToReversePolishNotation(this IEnumerable<CalculationToken> elements)
    {
        // https://en.wikipedia.org/wiki/Shunting_yard_algorithm
        var input = new Queue<CalculationToken>(elements);
        var operators = new Stack<CalculationToken>();
        var output = new Queue<CalculationToken>();

        while (input.TryDequeue(out var token))
        {
            if (token.Type is TokenType.Number)
                output.Enqueue(token);

            if (token.IsOperator)
            {
                while (ShouldMoveToOutput(token))
                    output.Enqueue(operators.Pop());

                operators.Push(token);
            }

            if (token .IsLeftParenthesis)
                operators.Push(token);

            if (token.IsRightParenthesis)
            {
                while (operators.Peek() is not { Type: TokenType.LeftParenthesis })
                {
                    if (!operators.Any())
                        throw new CalculatorException(token.Position, "Unbalanced parenthesis.  Check calculation and try again.");

                    output.Enqueue(operators.Pop());
                }

                if (!operators.TryPeek(out var current) || current.Type != TokenType.LeftParenthesis)
                    throw new CalculatorException(token.Position, "Expected left parenthesis.  Check calculation and try again.");

                _ = operators.Pop();
            }
        }

        while (operators.TryPop(out var token))
        {
            if (token.IsParenthesis)
                throw new CalculatorException(token.Position, "Mismatching patenthese found.  Check calculation and try again.");

            output.Enqueue(token);
        }

        return output;

        bool ShouldMoveToOutput(CalculationToken token) =>
            operators.TryPeek(out var current)
                && !current.IsLeftParenthesis
                && (current.Precedence > token.Precedence
                || (current.Precedence == token.Precedence && token.HasLeftAssociativity));
    }
}
