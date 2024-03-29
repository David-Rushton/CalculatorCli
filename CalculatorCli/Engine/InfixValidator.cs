using System.Diagnostics;

using CalculatorCli.Engine.DTOs;

using Humanizer;

namespace CalculatorCli.Engine;

public class InfixValidator
{
    /// <summary>
    ///   Performs fast high level checks on an infix expression.
    ///   Checks:
    ///   <list type="bullet">
    ///     <item>Expression is not empty</item>
    ///     <item>Expression doers not contain invalid characters</item>
    ///   </list>
    ///   <remarks>
    ///   Even if validation passes the expression may be invalid.  There are many cases not handled
    ///   by this class.
    ///   </remarks>
    /// </summary>
    public bool ContainsInvalidCharacters(string infixStatement, out List<int> invalidPositions)
    {
        invalidPositions = [];

        var i = 0;
        while (i < infixStatement.Length)
        {
            if (!CalculatorConstants.ValidCharacters.Contains(infixStatement[i]))
                invalidPositions.Add(i);

            i++;
        }

        return !invalidPositions.Any();
    }

    public bool ContainsInvalidTokenSequence(
        IEnumerable<CalculationToken> tokens, out List<(CalculationToken token, string issue)> invalidTokens)
    {
        var issues = new List<(CalculationToken token, string issue)>();
        var parenthesisLevel = 0;

        TestFirstTokenValid();
        TestRemainingTokensValid();

        invalidTokens = issues;

        return !invalidTokens.Any();

        void TestFirstTokenValid()
        {
            var first = tokens.First();

            // Is expression of "<number>" valid?  Would conversion or calculation blow up?
            // TODO: Add test case and/or consider.
            if (tokens.Count() == 1)
            {
                if (first.Type is not TokenType.Number)
                    issues.Add((first, "Expected number."));
            }
            else
            {
                if (first.Type is not (TokenType.Number or TokenType.LeftParenthesis))
                    issues.Add((first, "Expected number or opens parenthesis."));

                if (first.Type is TokenType.LeftParenthesis)
                    parenthesisLevel++;
            }
        }

        void TestRemainingTokensValid()
        {
            var expected = new Dictionary<TokenType, HashSet<TokenType>>
            {
                { TokenType.Number, [TokenType.Operator, TokenType.LeftParenthesis, TokenType.RightParenthesis] },
                { TokenType.Operator, [TokenType.Number, TokenType.LeftParenthesis] },
                { TokenType.LeftParenthesis, [TokenType.Number, TokenType.LeftParenthesis] },
                { TokenType.RightParenthesis, [TokenType.Operator, TokenType.RightParenthesis] },
            };

            var last = tokens.First();
            foreach (var current in tokens.Skip(1))
            {
                // Unary operations allow for consecutive +/- operators.
                // These are treated as a special case here.
                if (!expected[last.Type].Contains(current.Type))
                    if (!MaybeUnaryOperation(current, last))
                        issues.Add((current, $"Expected {GetExpected(expected[last.Type])}."));

                if (current.IsLeftParenthesis)
                    parenthesisLevel++;

                if (current.IsRightParenthesis)
                {
                    parenthesisLevel--;
                    if (parenthesisLevel < 0)
                        issues.Add((current, "Missing opening parenthesis."));
                }

                last = current;
            }

            if (parenthesisLevel > 0)
                issues.Add((last, "Missing closing parenthesis."));

            bool MaybeUnaryOperation(CalculationToken current, CalculationToken last) =>
                last.IsOperator
                && last.Value[0] is CalculatorConstants.AddOperator or CalculatorConstants.SubtractOperator
                && current.IsOperator
                && current.Value[0] is CalculatorConstants.AddOperator or CalculatorConstants.SubtractOperator;

            static string GetExpected(HashSet<TokenType> tokenTypes) =>
                tokenTypes.Humanize(tt => tt.Humanize(LetterCasing.LowerCase), "or");
        }
    }
}
