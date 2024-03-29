# Reading List

Want to learn more about software calculators?  

- https://en.wikipedia.org/wiki/Calculator_input_methods
- https://en.wikipedia.org/wiki/Comparison_of_software_calculators
- https://en.wikipedia.org/wiki/Infix_notation
- https://en.wikipedia.org/wiki/Reverse_Polish_notation
- https://en.wikipedia.org/wiki/Polish_notation
- https://en.wikipedia.org/wiki/Shunting_yard_algorithm
- https://en.wikipedia.org/wiki/Associative_property
- https://en.wikipedia.org/wiki/Operator-precedence_parser
- https://en.wikipedia.org/wiki/Operator_(computer_programming)
- https://datatrained.com/post/infix-expression/
- https://en.wikipedia.org/wiki/Tree_traversal
- https://en.wikipedia.org/wiki/Unary_operation
- https://stackoverflow.com/a/5240912/2572928
- https://stackoverflow.com/questions/5239715/problems-with-a-shunting-yard-algorithm/5240781#5240781


> To detect if the operator is unary I simply had to check if the token before the operator was an operator or an opening bracket.

Last = operator or opening paren or null (i.e. first)

> -Make them right-associative.
> -Make them higher precedence than any of the infix operators.
> -Handle them separately in EvaluateExpression (make a separate PerformUnaryExpression function > -which only takes one operand).
> -Distinguish between unary and binary minus in InfixToPostfix by keeping track of some kind of > -state. See how '-' is turned into '-u' in this Python example.
