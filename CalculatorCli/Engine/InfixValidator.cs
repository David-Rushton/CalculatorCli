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
    public bool IsExpressionValid(string infixStatement, out InfixExpressionIssue failure)
    {
        var reason = string.Empty;

        var invalidCharacters = new List<int>();
        var i = 0;
        while (i < infixStatement.Length)
        {
            if (!CalculatorConstants.ValidCharacters.Contains(infixStatement[i]))
                invalidCharacters.Add(i);

            i++;
        }

        if (i == 0)
            reason = "Please provide an infix expression.  See --help for instructions.";

        if (invalidCharacters.Count == 1)
            reason = "Unexpected character in expression.";
        if (invalidCharacters.Count > 1)
            reason = "Unexpected characters in expression.";

        failure = new (reason, invalidCharacters);

        return reason == string.Empty;
    }

    public bool HasValidInfixTokenSequence(
        IEnumerable<CalculationToken> tokens, out IEnumerable<InfixTokenIssue> infixTokenIssues)
    {
        // rules:
        //  - if one token, must be a number
        //  - else:
            //  - must start lp or operand
            //  - num -> op lp rp
            //  - op -> lp num
            //  - lp -> lp num
            //  - rp -> rp op

        Debug.Assert(tokens.Any());


        var invalidTokens = new List<(CalculationToken token, string message)>();



        // TODO: Is this valid?  Would it blow up elsewhere.  Add test case and consider.
        var first = tokens.First();
        if (tokens.Count() == 1 && first.Type is not TokenType.Number)
            invalidTokens.Add((tokens.First(), "Expected number."));

        if (tokens.Count() > 1 && first.Type is not TokenType.Number or TokenType.LeftParenthesis)
            invalidTokens.Add((tokens.First(), "Expected number or opening parenthesis."));



        var expectedTypes = new Dictionary<TokenType, HashSet<TokenType>>
        {
            { TokenType.Number, [TokenType.Operator, TokenType.LeftParenthesis, TokenType.RightParenthesis] },
            { TokenType.Operator, [TokenType.Number, TokenType.LeftParenthesis] },
            { TokenType.LeftParenthesis, [TokenType.Number, TokenType.LeftParenthesis] },
            { TokenType.RightParenthesis, [TokenType.Operator, TokenType.RightParenthesis] },
        };

        var parenthesisLevel = 0;


        var last = first;
        foreach (var current in tokens.Skip(1))
        {
            if (!expectedTypes[last.Type].Contains(current.Type))
                invalidTokens.Add((current, $"Expected {GetExpected(expectedTypes[last.Type])}."));

            if (current.IsLeftParenthesis)
                parenthesisLevel++;

            if (current.IsRightParenthesis)
            {
                parenthesisLevel--;
                if (parenthesisLevel < 0)
                    invalidTokens.Add((current, "Missing opening parenthesis."));

            }

            last = current;
        }


        if (parenthesisLevel > 0)
            invalidTokens.Add((last, "Missing closing parenthesis."));


        infixTokenIssues = invalidTokens.Select(it => new InfixTokenIssue(it.message, it.token));

        return !invalidTokens.Any();


        static string GetExpected(HashSet<TokenType> tokenTypes) =>
            tokenTypes.Humanize(tt => tt.Humanize(LetterCasing.LowerCase), "or");
    }
}
