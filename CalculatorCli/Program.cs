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
app.Configure(config =>
{
    config
        .CaseSensitivity(CaseSensitivity.None)
        .SetApplicationName("calc-cli");
});

return app.Run(args);
