namespace CalculatorCli.Model;

public enum Associativity
{
    Left,
    Right
}

public enum TokenType
{
    Operand,
    BinaryOperator,
    UnaryOperator,
    LeftParenthesis,
    RightParenthesis
}

/// <summary>
///   <para>
///     Calculations are broken down in a sequence of tokens.  Where each token describes a step within
///     the calculation.
///   </para>
///   <para>
///     There are 3 tokens in the calculation 3 + 4:
///     <list type="bullet">
///         <item>Token = { Type: TokenType.Number,   Value: 3 }</item>
///         <item>Token = { Type: TokenType.Operator, Value: + }</item>
///         <item>Token = { Type: TokenType.Number,   Value: 4 }</item>
///     </list>
///   </para>
///   <remarks>
///     Supports infix and reverse Polish notation.
///   </remarks>
/// </summary>
//TODO: Is this the best name I can think of?
public readonly record struct CalculationToken(
    int Position,
    TokenType Type,
    string Value)
{
    public bool IsNumber =>
        double.TryParse(Value, out _);

    public bool IsOperator =>
        Type is TokenType.BinaryOperator or TokenType.UnaryOperator;

    public bool IsBinaryOperator =>
        Type is TokenType.BinaryOperator;

    public bool IsUnaryOperator =>
        Type is TokenType.UnaryOperator;

    public bool IsParenthesis =>
        Type is TokenType.LeftParenthesis or TokenType.LeftParenthesis;

    public bool IsLeftParenthesis =>
        Type is TokenType.LeftParenthesis;

    public bool IsRightParenthesis =>
        Type is TokenType.RightParenthesis;

    public bool HasLeftAssociativity =>
        Associativity == Associativity.Left;

    // https://en.wikipedia.org/wiki/Order_of_operations
    // https://en.wikipedia.org/wiki/Shunting_yard_algorithm#Detailed_examples
    public int Precedence =>
        IsUnaryOperator
            ? 5
            : Value[0] switch
              {
                  CalculatorConstants.PowerOfOperator => 4,
                  CalculatorConstants.MultiplyOperator => 3,
                  CalculatorConstants.DivideOperator => 3,
                  CalculatorConstants.ModulusOperator => 3,
                  CalculatorConstants.AdditionOperator => 2,
                  CalculatorConstants.SubtractionOperator => 2,
                  _ => 1
              };

    // https://en.wikipedia.org/wiki/Operator_associativity
    public Associativity Associativity =>
        Value == "^" || IsUnaryOperator
            ? Associativity.Right
            : Associativity.Left;
}
