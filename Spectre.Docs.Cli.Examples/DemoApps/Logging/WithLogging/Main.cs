using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.Logging.WithLogging;

/// <summary>
/// Logging Tutorial - Step 2: With Logging.
/// Shows how to add Microsoft.Extensions.Logging with ILogger injection.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        var registrar = new TypeRegistrar(services);
        var app = new CommandApp<ProcessCommand>(registrar);
        return await app.RunAsync(args);
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

public class ProcessSettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    [Description("The path to process")]
    public string Path { get; init; } = string.Empty;
}

internal class ProcessCommand : Command<ProcessSettings>
{
    private readonly ILogger<ProcessCommand> _logger;

    public ProcessCommand(ILogger<ProcessCommand> logger)
    {
        _logger = logger;
    }

    protected override int Execute(CommandContext context, ProcessSettings settings, CancellationToken cancellation)
    {
        _logger.LogInformation("Starting to process: {Path}", settings.Path);

        // Simulate some work with different log levels
        for (var i = 1; i <= 3; i++)
        {
            _logger.LogDebug("Detailed step {Step} information", i);
            Thread.Sleep(100);
            _logger.LogInformation("Processing step {Step}...", i);
        }

        _logger.LogInformation("Processing complete!");
        return 0;
    }
}
