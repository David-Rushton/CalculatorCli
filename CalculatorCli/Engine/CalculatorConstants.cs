namespace CalculatorCli.Engine;

public class CalculatorConstants
{
    public const char Space = ' ';
    public const char DecimalPoint = '.';
    public const char LeftParentheses = '(';
    public const char RightParentheses = ')';
    public const char PowerOfOperator = '^';
    public const char AddOperator = '+';
    public const char SubtractOperator = '-';
    public const char MultiplyOperator = '*';
    public const char DivideOperator = '/';
    public static readonly char[] Parentheses = [
        LeftParentheses,
        RightParentheses];
    public static readonly char[] Operators = [
        PowerOfOperator,
        AddOperator,
        SubtractOperator,
        MultiplyOperator,
        DivideOperator];
    public static readonly char[] ValidCharacters = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        PowerOfOperator, AddOperator, SubtractOperator, MultiplyOperator, DivideOperator,
        LeftParentheses, RightParentheses,
        Space, DecimalPoint
    ];
}
