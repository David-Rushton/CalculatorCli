using CalculatorCli.Engine.Extensions;

namespace CalculatorCli.Engine;

public class Calculator(Parser parser)
{
    /// <summary>
    /// Calculates the product from a sequence of reverse Polish notation tokens.
    /// </summary>
    /// <exception cref="CalculatorException"/>
    public double Calculate(IEnumerable<string> infixCalculationSegments)
    {
        var infixTokens = parser.Parse(infixCalculationSegments);
        var rpnTokens = infixTokens.ToReversePolishNotation();
        var stack = new Stack<CalculationToken>(rpnTokens);

        foreach (var token in rpnTokens)
        {
            if (!token.IsOperator)
            {
                stack.Push(token);
                continue;
            }

            var left = stack.PopAndParse();
            var right = stack.PopAndParse();
            var result = token.Value switch
            {
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => left / right,
                "^" => Math.Pow(left, right),
                _ => throw new CalculatorException(token.Position, "Expected operator")
            };

            stack.Push(new(token.Position, TokenType.Number, result.ToString()));
        }

        return stack.PopAndParse();
    }
}
