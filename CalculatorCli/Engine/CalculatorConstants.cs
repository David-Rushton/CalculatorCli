namespace CalculatorCli.Engine;

/// <summary>
/// Characters supported by this calculator.
/// </summary>
public class CalculatorConstants
{
    public const char Space = ' ';
    public const char DecimalPoint = '.';
    public const char LeftParentheses = '(';
    public const char RightParentheses = ')';
    public const char PowerOfOperator = '^';
    public const char AdditionOperator = '+';
    public const char SubtractionOperator = '-';
    public const char MultiplyOperator = '*';
    public const char DivideOperator = '/';
    public const char ModulusOperator = '%';
    public const char AddOperator = '+';
    public const char SubtractOperator = '-';

    public static readonly char[] Parentheses = [
        LeftParentheses,
        RightParentheses];

    public static readonly char[] BinaryOperators = [
        PowerOfOperator,
        AdditionOperator,
        SubtractionOperator,
        MultiplyOperator,
        DivideOperator,
        ModulusOperator];

    public static readonly char[] UnaryOperators = [
        AddOperator,
        SubtractOperator];

    public static readonly char[] Operators =
        BinaryOperators
            .Union(UnaryOperators)
            .ToArray();

    public static readonly char[] ValidCharacters = [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        PowerOfOperator, AdditionOperator, SubtractionOperator, MultiplyOperator, DivideOperator, ModulusOperator,
        AddOperator, SubtractOperator,
        LeftParentheses, RightParentheses,
        Space, DecimalPoint];
}
