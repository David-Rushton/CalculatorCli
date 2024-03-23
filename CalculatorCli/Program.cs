


// Console.WriteLine($"\x1b[4:3m\x1b[58:2::172:0:0mA small mistake here\x1b[0m");
// Console.WriteLine($"\x1b[4:3m\x1b[58:5:31mA small mistake here\x1b[0m");
// Console.WriteLine("\x1b[36mTEST\x1b[0m");
// Console.WriteLine("\x1b[38;2;172;0;0mTEST\x1b[0m");
// Console.WriteLine();
// Console.WriteLine();
// Console.WriteLine();







using CalculatorCli.Formatters;

var services = new ServiceCollection();
services
    .AddSingleton<InfixValidator>()
    .AddSingleton<Preprocessor>()
    .AddSingleton<Parser>()
    .AddSingleton<CalculationBuilder>()
    .AddSingleton<Calculator>()
    .AddSingleton<ExceptionFormatter>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<CalculateCommand>(registrar);

return app.Run(args);
