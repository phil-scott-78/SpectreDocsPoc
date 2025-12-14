using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete;

/// <summary>
/// Dependency Injection Tutorial - Complete: Keyed services with factory pattern.
/// Demonstrates how to use .NET 8+ keyed services to register multiple implementations
/// of the same interface and resolve them at runtime using a factory.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var services = new ServiceCollection();

        // Register keyed greeting services - each style gets its own implementation
        services.AddKeyedSingleton<IGreetingService, CasualGreetingService>(GreetingStyle.Casual);
        services.AddKeyedSingleton<IGreetingService, FormalGreetingService>(GreetingStyle.Formal);
        services.AddKeyedSingleton<IGreetingService, EnthusiasticGreetingService>(GreetingStyle.Enthusiastic);

        // Register the factory that resolves the appropriate service based on settings
        services.AddScoped<IGreetingFactory, GreetingFactory>();

        var registrar = new TypeRegistrar(services);
        var app = new CommandApp<GreetCommand>(registrar);
        return await app.RunAsync(args);
    }
}

/// <summary>
/// Defines the style of greeting to use.
/// </summary>
public enum GreetingStyle
{
    Casual,
    Formal,
    Enthusiastic
}

/// <summary>
/// Service interface for generating greetings.
/// </summary>
public interface IGreetingService
{
    string GetGreeting(string name);
}

/// <summary>
/// Casual greeting service - friendly and simple.
/// </summary>
public class CasualGreetingService : IGreetingService
{
    public string GetGreeting(string name) => $"Hello, {name}!";
}

/// <summary>
/// Formal greeting service - professional and polite.
/// </summary>
public class FormalGreetingService : IGreetingService
{
    public string GetGreeting(string name) => $"Good day, {name}.";
}

/// <summary>
/// Enthusiastic greeting service - energetic and welcoming.
/// </summary>
public class EnthusiasticGreetingService : IGreetingService
{
    public string GetGreeting(string name) => $"Hey there, {name}! Great to see you!";
}

/// <summary>
/// Factory interface for creating greeting services.
/// </summary>
public interface IGreetingFactory
{
    IGreetingService Create();
}

/// <summary>
/// Factory that resolves the appropriate keyed greeting service based on command settings.
/// Receives the parsed Settings via DI - Spectre.Console.Cli registers them automatically.
/// </summary>
public class GreetingFactory(IServiceProvider serviceProvider, GreetCommand.Settings settings)
    : IGreetingFactory
{
    public IGreetingService Create()
    {
        return serviceProvider.GetRequiredKeyedService<IGreetingService>(settings.Style);
    }
}

/// <summary>
/// Bridges Microsoft.Extensions.DependencyInjection with Spectre.Console.Cli.
/// </summary>
public sealed class TypeRegistrar(IServiceCollection services) : ITypeRegistrar
{
    public ITypeResolver Build() => new TypeResolver(services.BuildServiceProvider());

    public void Register(Type service, Type implementation) => services.AddSingleton(service, implementation);

    public void RegisterInstance(Type service, object implementation) => services.AddSingleton(service, implementation);

    public void RegisterLazy(Type service, Func<object> factory) => services.AddSingleton(service, _ => factory());
}

/// <summary>
/// Resolves services from the built service provider.
/// </summary>
public sealed class TypeResolver(IServiceProvider provider) : ITypeResolver
{
    public object? Resolve(Type? type) => type == null ? null : provider.GetService(type);
}

public class GreetCommand : Command<GreetCommand.Settings>
{
    private readonly IGreetingFactory _greetingFactory;
    private readonly IAnsiConsole _console;

    public GreetCommand(IGreetingFactory greetingFactory, IAnsiConsole console)
    {
        _greetingFactory = greetingFactory;
        _console = console;
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;

        [CommandOption("-s|--style")]
        [Description("The greeting style to use (Casual, Formal, or Enthusiastic)")]
        [DefaultValue(GreetingStyle.Casual)]
        public GreetingStyle Style { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var service = _greetingFactory.Create();
        var greeting = service.GetGreeting(settings.Name);
        _console.WriteLine(greeting);
        return 0;
    }
}
