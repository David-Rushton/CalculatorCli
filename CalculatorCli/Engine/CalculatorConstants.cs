namespace CalculatorCli.Engine;

public class CalculatorConstants
{
    public const char Space = ' ';
    public const char DecimalPoint = '.';
    public const char LeftParentheses = '(';
    public const char RightParentheses = ')';
    public const char PowerOfOperator = '^';
    public const char AdditionOperator = '+';
    public const char SubtractionOperator = '-';
    public const char MultiplicationOperator = '*';
    public const char DivisionOperator = '/';
    public const char RemainderOperator = '%';
    public const char PlusOperator = '+';
    public const char MinusOperator = '-';

    public static readonly char[] Parentheses = [
        LeftParentheses,
        RightParentheses];

    public static readonly char[] BinaryOperators = [
        PowerOfOperator,
        AdditionOperator,
        SubtractionOperator,
        MultiplicationOperator,
        DivisionOperator];

    public static readonly char[] UnaryOperators = [
        PlusOperator,
        MinusOperator];

    public static readonly char[] Operators =
        BinaryOperators
            .Union(UnaryOperators)
            .ToArray();

    public static readonly char[] ValidCharacters = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        PowerOfOperator, AdditionOperator, SubtractionOperator, MultiplicationOperator, DivisionOperator,
        PlusOperator, MinusOperator,
        LeftParentheses, RightParentheses,
        Space, DecimalPoint];
}
