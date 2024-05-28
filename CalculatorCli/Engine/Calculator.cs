using CalculatorCli.Engine.Extensions;

namespace CalculatorCli.Engine;

public class Calculator(Parser parser)
{
    /// <summary>
    /// Calculates the product of a series of reverse Polish notation tokens.
    /// </summary>
    /// <exception cref="CalculatorException"/>
    public double Calculate(string infixStatement)
    {
        var infixTokens = parser.Parse(infixStatement);
        var rpnTokens = infixTokens.ToReversePolishNotation();
        var rpnNotation = string.Join(CalculatorConstants.Space, rpnTokens.Select(t => t.Value));
        var stack = new Stack<CalculationToken>();
        var rnpNotation = string.Join(CalculatorConstants.Space.ToString(), rpnTokens.Select(t => t.Value));

        VerboseConsole.WriteLine($"Canonical rnp notation: {rnpNotation}");

        foreach (var token in rpnTokens)
        {
            if (!token.IsOperator)
            {
                stack.Push(token);
                continue;
            }

            // TODO: It might be better to unary operation at parse time
            // https://stackoverflow.com/questions/2431863/infix-to-postfix-and-unary-binary-operators
            var y = stack.PopAndParseOrDefault();
            var x = stack.PopAndParseOrDefault();
            var result = token.Value switch
            {
                "+" => x + y,
                "-" => x - y,
                "*" => x * y,
                "/" => x / y,
                "^" => Math.Pow(x, y),
                _ => throw new CalculatorException(token.Position, "Expected operator")
            };

            VerboseConsole.WriteLine($"- {x} {token.Value} {y} = {result}");

            stack.Push(new(token.Position, TokenType.Number, result.ToString()));
        }

        VerboseConsole.WriteLine(string.Empty);
        return stack.PopAndParseOrDefault();
    }
}
