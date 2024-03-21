# Validation

## Thoughts on input validation

In an ideal world we would reject invalid statements early, with detailed error information.

```powershell
# Mock up 
> calc-cli "1 + not-a-number" 
Cannot calculate 
1 + not-a-number
    ^^^^^^^^^^^^
Expected a number but found `not-a-number`
```

Where a combination of ANSI escape codes and/or Spectre Console pretty prints error information using 
a combination of colours, squiggly lines and whatever else helps the reader to quickly grok their 
mistake.

Some errors can be found without the need to parse the infix expression.

```powershell
# Whoops we forget to to close the final parenthesis
# We can find cases like this using functions like count
> calc-cli "(1 + 2) * (3 + 4
(1 + 2) * (3 + 4
          ^^^^^^
Missing closing parenthesis
```

There are cases where spotting an error is easier than identifying where things went wrong.

```bash
# Do we highlight from A, B or C?
# What did the user intend 🤷‍♀️.
$ calc-cli "(1 + 2) * (3 + 4 * (5 + 6) * (7 + 8)
calc-cli "(1 + 2) * (3 + 4 * (5 + 6) * (7 + 8)
A         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
B                   ^^^^^^^^^^^^^^^^^^^^^^^^^^
C                                            ^
Missing closing parenthesis
```

Error report **B** requires us to walk the input.  We can do that by emitting tokens that allow us to 
rebuild the input in our error report.  But that means later steps would need to discard superfluous 
whitespace etc.  Which adds to the complexity of converting to RNP, and possibly calculating the result.  

An alternative is to walk the input in a separate process.  But that adds an extra step.  It is also
likely there are cases that are hard to catch until later in the process.  Which could complicate 
providing a consistent error report.

## Proposed approach

Validation will occur as a separate step.  Where we will attempt to cover all common input mistakes.
It is likely there are some errors that cannot be uncovered until we have parsed and/or converted the
input to RNP.  This requires further investigation, and may lead to a different recommended approach.
