using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.Logging.LoggingComplete;

/// <summary>
/// Logging Tutorial - Complete: Configurable log level with interceptor.
/// Demonstrates using a base settings class and interceptor to configure
/// the log level at runtime via command-line options.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var services = new ServiceCollection();

        // Create a log level switch that can be modified at runtime
        var logLevelSwitch = new LogLevelSwitch();
        services.AddSingleton(logLevelSwitch);

        // Configure logging with a filter that checks the switch
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddFilter((category, level) => level >= logLevelSwitch.MinimumLevel);
        });

        var registrar = new TypeRegistrar(services);
        var app = new CommandApp<ProcessCommand>(registrar);

        app.Configure(config =>
        {
            // Set up the interceptor to configure logging before command execution
            config.SetInterceptor(new LogInterceptor(logLevelSwitch));
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// Holds the current minimum log level. Similar to Serilog's LoggingLevelSwitch.
/// </summary>
public class LogLevelSwitch
{
    public LogLevel MinimumLevel { get; set; } = LogLevel.Information;
}

/// <summary>
/// Base settings class that provides logging options to all commands.
/// </summary>
public class LogCommandSettings : CommandSettings
{
    [CommandOption("--logLevel")]
    [Description("Minimum level for logging (Trace, Debug, Information, Warning, Error, Critical)")]
    [DefaultValue(LogLevel.Information)]
    public LogLevel LogLevel { get; set; }
}

/// <summary>
/// Interceptor that configures the log level based on command settings.
/// Runs before command execution, allowing the log level to be set via command-line.
/// </summary>
public class LogInterceptor(LogLevelSwitch logLevelSwitch) : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is LogCommandSettings logSettings)
        {
            logLevelSwitch.MinimumLevel = logSettings.LogLevel;
        }
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

public class ProcessSettings : LogCommandSettings
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

        _logger.LogWarning("This is a warning message");
        _logger.LogInformation("Processing complete!");
        return 0;
    }
}
