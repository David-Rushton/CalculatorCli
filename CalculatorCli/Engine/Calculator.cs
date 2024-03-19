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
        var rnpNotation = string.Join(CalculatorConstants.Space.ToString(), rpnTokens.Select(t => t.Value));

        VerboseConsole.WriteLine($"Canonical rnp notation: {rnpNotation}");

        foreach (var token in rpnTokens)
        {
            if (!token.IsOperator)
            {
                stack.Push(token);
                continue;
            }

            var y = stack.PopAndParse();
            var x = stack.PopAndParse();
            var result = token.Value switch
            {
                "+" => x + y,
                "-" => x - y,
                "*" => x * y,
                "/" => x / y,
                "^" => Math.Pow(x, y),
                _ => throw new CalculatorException(token.Position, "Expected operator")
            };

            VerboseConsole.WriteLine($" - {x} {token.Value} {y} = {result}");
            stack.Push(new(token.Position, TokenType.Number, result.ToString()));
        }

        VerboseConsole.WriteLine(string.Empty);
        return stack.PopAndParse();
    }
}
