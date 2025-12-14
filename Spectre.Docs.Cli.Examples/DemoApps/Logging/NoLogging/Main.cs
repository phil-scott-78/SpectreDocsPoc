using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.Logging.NoLogging;

/// <summary>
/// Logging Tutorial - Step 1: No Logging.
/// Shows the starting point with a simple file processing command.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<ProcessCommand>();
        return await app.RunAsync(args);
    }
}

public class ProcessSettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    [Description("The path to process")]
    public string Path { get; init; } = string.Empty;
}

internal class ProcessCommand : Command<ProcessSettings>
{
    protected override int Execute(CommandContext context, ProcessSettings settings, CancellationToken cancellation)
    {
        AnsiConsole.WriteLine($"Starting to process: {settings.Path}");

        // Simulate some work
        for (var i = 1; i <= 3; i++)
        {
            Thread.Sleep(100);
            AnsiConsole.WriteLine($"Processing step {i}...");
        }

        AnsiConsole.WriteLine("Processing complete!");
        return 0;
    }
}
