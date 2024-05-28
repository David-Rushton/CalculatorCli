using System.Diagnostics;

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

            var y = stack.PopAndParse();
            var x = token.IsBinaryOperator
                ? stack.PopAndParse()
                : 0;

            Debug.Assert(token.Value.Length == 1);
            var result = token.Value[0] switch
            {
                CalculatorConstants.AdditionOperator     => x + y,
                CalculatorConstants.SubtractionOperator  => x - y,
                CalculatorConstants.MultiplyOperator     => x * y,
                CalculatorConstants.DivideOperator       => x / y,
                CalculatorConstants.ModulusOperator      => x % y,
                CalculatorConstants.PowerOfOperator      => Math.Pow(x, y),
                _ => throw new CalculatorException(token.Position, "Expected operator")
            };

            VerboseConsole.WriteLine($"- {x} {token.Value} {y} = {result}");

            stack.Push(new(token.Position, TokenType.Operand, result.ToString()));
        }

        VerboseConsole.WriteLine(string.Empty);
        return stack.PopAndParse();
    }
}
