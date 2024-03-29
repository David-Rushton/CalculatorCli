# Calculator CLI

[![.NET](https://github.com/David-Rushton/CalculatorCli/actions/workflows/dotnet.yml/badge.svg)](https://github.com/David-Rushton/CalculatorCli/actions/workflows/dotnet.yml)

A simple stack based CLI calculator.  With support for:

- Infix notation
- Addition, subtractor, multiplication, & division operators
- Remainder operator
- Power of operator
- Unary operators
- Parentheses
- Pretty printed errors

```bash
# Evaluates infix expressions
$ ./CalculatorCli "(1 + 1) * 5 % 3"
1
```

When things go wrong we try our best to provide useful consice error reports.

![Example Error Report]

## Build

You'll need [.Net8] to build locally.

```powershell
# From the project root.
# You'll find the binary here: ./CalculatorCli/bin/Release/
> dotnet build --configuration Release

# Or you can execute directly with the dotnet cli.
> dotnet run --project ./CalculatorCli -- "1 + 1"
```

## Pre-built Binaries

Coming soon.

[.Net8]:https://dotnet.microsoft.com/en-us/
[Example Error Report]:./docs/.media/example-error-report.png
