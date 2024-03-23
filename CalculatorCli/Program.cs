var services = new ServiceCollection();
services
    .AddSingleton<InfixValidator>()
    .AddSingleton<Preprocessor>()
    .AddSingleton<Parser>()
    .AddSingleton<CalculationBuilder>()
    .AddSingleton<Calculator>()
    .AddSingleton<CalculationExceptionFormatter>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<CalculateCommand>(registrar);

return app.Run(args);
