using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.FlagArguments;

/// <summary>
/// Demonstrates how to use FlagValue for optional flag arguments.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<ServerCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A server command demonstrating FlagValue patterns.
/// </summary>
internal class ServerCommand : Command<ServerCommand.Settings>
{
    /// <summary>
    /// Settings demonstrating FlagValue with optional values.
    /// </summary>
    public class Settings : CommandSettings
    {
        [CommandOption("--port [PORT]")]
        [Description("The port to listen on (default: 3000 if flag present)")]
        [DefaultValue(3000)]
        public required FlagValue<int> Port { get; init; }

        [CommandOption("--timeout [SECONDS]")]
        [Description("Connection timeout in seconds")]
        public required FlagValue<int?> Timeout { get; init; }

        [CommandOption("-h|--host")]
        [Description("The host to bind to")]
        [DefaultValue("localhost")]
        public required string Host { get; init; } = "localhost";
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Host: {settings.Host}");

        // Check if --port flag was provided
        if (settings.Port.IsSet)
        {
            System.Console.WriteLine($"Port: {settings.Port.Value}");
        }
        else
        {
            System.Console.WriteLine("Port: not specified (will use system default)");
        }

        // Check if --timeout flag was provided
        if (settings.Timeout is { IsSet: true, Value: not null })
        {
            System.Console.WriteLine($"Timeout: {settings.Timeout.Value} seconds");
        }
        else if (settings.Timeout.IsSet)
        {
            System.Console.WriteLine($"Timeout: system default seconds");
        }
        else
        {
            System.Console.WriteLine("Timeout: disabled");
        }

        return 0;
    }
}
