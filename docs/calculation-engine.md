# Calculation Engine

The calculation engine describes the steps taken to cover user input into either:

- A result
- An error report

## Steps

The following steps are wrapped in a try/catch statement.  Allowing us to capture the result or any
exceptions thrown.  Where possible exceptions will inherit from a common base that provides the properties
required to pretty print an error report.

![overview diagram](./.media/calculation-engine.png)

### Preprocessor

The preprocessor makes minimal, simple, updates to the user input.  It is faster and exits only to 
simplify last steps by:

- Replaces tabs and new lines with a spaces 
- Replace multiple spaces with a single space
- Replace common operator variants, like `x` & `×`, with the officially supported variant (e.g. `*`) 
- Appending a trailing space to the expression

As a consequence:

- Parsing is simplified
- Error reports are clearer

The preprocessor assumes most expressions can be clearly printed on a single line.  Which feels 
reasonable for a CLI based calculator.  

### Validator

Scans the expression for common mistakes, including:

- Uneven parentheses count
- Invalid characters within the expression
- Consecutive operators
- Consecutive operands

The validation cannot account for all inaccuracies.  Testing parentheses are properly balanced, for 
example, requires parsing.

Like the preprocessor; the validator is design to be fast and simplify later steps.

### Parser

Converts the infix expression into a series of tokens.

### Postfix Converter

Takes a series of infix tokens and converts into a series of postfix tokens.

### Calculator

Convert a series of postfix tokens into a result.
