var services = new ServiceCollection();
services
    .AddSingleton<SimpleInfixValidator>()
    .AddSingleton<Preprocessor>()
    .AddSingleton<Parser>()
    .AddSingleton<CalculationBuilder>()
    .AddSingleton<Calculator>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<CalculateCommand>(registrar);

return app.Run(args);
