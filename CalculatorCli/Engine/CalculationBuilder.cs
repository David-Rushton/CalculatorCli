namespace CalculatorCli.Engine;

public class CalculationBuilder()
{
    private readonly List<CalculationToken> _tokens = new();

    public CalculationBuilder AddNumber(int position, string value)
    {
        if (!double.TryParse(value, out _))
            throw new CalculatorException(position,$"Expected number.  Not {value}.  Check calculation and try again.");

        _tokens.Add(new(position, TokenType.Operand, value));
        return this;
    }

    public CalculationBuilder AddBinaryOperator(int position, char value)
    {
        if (!CalculatorConstants.BinaryOperators.Contains(value))
            throw new CalculatorException(position, $"Expected binary operator.  Not {value}.  Check calculation and try again.");

        _tokens.Add(new(position, TokenType.BinaryOperator, value.ToString()));
        return this;
    }

    public CalculationBuilder AddUnaryOperator(int position, char value)
    {
        if (!CalculatorConstants.UnaryOperators.Contains(value))
            throw new CalculatorException(position, $"Expected unary operator.  Not {value}.  Check calculation and try again.");

        _tokens.Add(new(position, TokenType.UnaryOperator, value.ToString()));
        return this;
    }

    public CalculationBuilder AddParenthesis(int position, char value)
    {
        if (value == CalculatorConstants.LeftParentheses)
        {
            AddLeftParenthesis(position, value);
            return this;
        }

        if (value == CalculatorConstants.RightParentheses)
        {
            AddRightParenthesis(position, value);
            return this;
        }

        throw new CalculatorException(position, $"Expected parentheses.  Not {value}.  Check calculation and try again.");
    }

    public CalculationBuilder AddLeftParenthesis(int position, char value)
    {
        if (value != CalculatorConstants.LeftParentheses)
            throw new CalculatorException(position, $"Expected opening parentheses.  Not {value}.  Check calculation and try again.");

        _tokens.Add(new(position, TokenType.LeftParenthesis, value.ToString()));
        return this;
    }

    public CalculationBuilder AddRightParenthesis(int position, char value)
    {
        if (value != CalculatorConstants.RightParentheses)
            throw new CalculatorException(position, $"Expected closing parentheses.  Not {value}.  Check calculation and try again.");

        _tokens.Add(new(position, TokenType.RightParenthesis, value.ToString()));
        return this;
    }

    public IEnumerable<CalculationToken> Build() =>
        _tokens;
}
