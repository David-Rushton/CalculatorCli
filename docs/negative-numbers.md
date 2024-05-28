# Negative Numbers

It is common for infix expressions to contain negative numbers.  Example: `1 + -1`.  The same symbol
is used for the subtraction operator and negation prefix.  The same is true for the addition operator
and the positive prefix.  Example: `1 - +1`.  This complicates parsing and validation.

The parser converts the plus and minus prefixes into unary operators.  These are supported throughout
the calculation engine.
