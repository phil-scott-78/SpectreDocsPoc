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
        // FlagValue<int> - can be used as --port (uses default) or --port 8080 (uses specified)
        [CommandOption("--port [PORT]")]
        [Description("The port to listen on (default: 3000 if flag present)")]
        public required FlagValue<int> Port { get; init; }

        // FlagValue<int?> - nullable inner type for truly optional values
        [CommandOption("--timeout [SECONDS]")]
        [Description("Connection timeout in seconds")]
        public required FlagValue<int?> Timeout { get; init; }

        // Regular option for comparison
        [CommandOption("-h|--host")]
        [Description("The host to bind to")]
        [DefaultValue("localhost")]
        public required string Host { get; init; } = "localhost";
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Host: {settings.Host}");

        // Check if --port flag was provided
        if (settings.Port?.IsSet == true)
        {
            // Value is the parsed port number (or default if none specified)
            var port = settings.Port.Value;
            System.Console.WriteLine($"Port: {port}");
        }
        else
        {
            System.Console.WriteLine("Port: not specified (will use system default)");
        }

        // Check if --timeout flag was provided
        if (settings.Timeout?.IsSet == true)
        {
            if (settings.Timeout.Value.HasValue)
            {
                System.Console.WriteLine($"Timeout: {settings.Timeout.Value} seconds");
            }
            else
            {
                System.Console.WriteLine("Timeout: enabled (no specific value)");
            }
        }
        else
        {
            System.Console.WriteLine("Timeout: not specified");
        }

        return 0;
    }
}
