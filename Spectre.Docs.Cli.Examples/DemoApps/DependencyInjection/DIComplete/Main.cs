using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete;

/// <summary>
/// Dependency Injection Tutorial - Complete: Full DI example with multiple greeting styles.
/// Demonstrates the flexibility that DI provides for swapping implementations.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "di-tutorial";
    public string Description => "DI tutorial: complete greeting CLI with multiple styles.";

    public async Task<int> RunAsync(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IGreetingService, GreetingService>();

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
    string GetGreeting(string name, GreetingStyle style);
}

/// <summary>
/// Default implementation of the greeting service with multiple styles.
/// </summary>
public class GreetingService : IGreetingService
{
    public string GetGreeting(string name, GreetingStyle style)
    {
        return style switch
        {
            GreetingStyle.Formal => $"Good day, {name}.",
            GreetingStyle.Enthusiastic => $"Hey there, {name}! Great to see you!",
            _ => $"Hello, {name}!"
        };
    }
}

/// <summary>
/// Bridges Microsoft.Extensions.DependencyInjection with Spectre.Console.Cli.
/// </summary>
public sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _services;

    public TypeRegistrar(IServiceCollection services)
    {
        _services = services;
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(_services.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        _services.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _services.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        _services.AddSingleton(service, _ => factory());
    }
}

/// <summary>
/// Resolves services from the built service provider.
/// </summary>
public sealed class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider;
    }

    public object? Resolve(Type? type)
    {
        return type == null ? null : _provider.GetService(type);
    }
}

internal class GreetCommand : Command<GreetCommand.Settings>
{
    private readonly IGreetingService _greetingService;

    public GreetCommand(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;

        [CommandOption("-f|--formal")]
        [Description("Use formal greeting style")]
        [DefaultValue(false)]
        public bool Formal { get; init; }

        [CommandOption("-e|--enthusiastic")]
        [Description("Use enthusiastic greeting style")]
        [DefaultValue(false)]
        public bool Enthusiastic { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var style = settings.Formal ? GreetingStyle.Formal
            : settings.Enthusiastic ? GreetingStyle.Enthusiastic
            : GreetingStyle.Casual;

        var greeting = _greetingService.GetGreeting(settings.Name, style);
        System.Console.WriteLine(greeting);
        return 0;
    }
}
