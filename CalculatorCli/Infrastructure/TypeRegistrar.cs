namespace CalculatorCli.Infrastructure;

public sealed class TypeRegistrar(IServiceCollection services) : ITypeRegistrar
{
    public ITypeResolver Build() => 
        new TypeResolver(services.BuildServiceProvider());

    public void Register(Type service, Type implementation) => 
        services.AddSingleton(service, implementation);

    public void RegisterInstance(Type service, object implementation) => 
        services.AddSingleton(service, implementation);

    public void RegisterLazy(Type service, Func<object>? func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        services.AddSingleton(service, (provider) => func());
    }
}
