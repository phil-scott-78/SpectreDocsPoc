using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.MakingOptionsRequired;

/// <summary>
/// Demonstrates how to make options required using the C# required keyword.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DeployCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A deployment command with required options using C# required keyword.
/// </summary>
internal class DeployCommand : Command<DeployCommand.Settings>
{
    public class Settings : CommandSettings
    {
        // Use C#'s required keyword - simplest approach
        [CommandOption("-e|--environment")]
        [Description("Target environment")]
        public required string Environment { get; init; }

        [CommandOption("-v|--version")]
        [Description("Version to deploy")]
        public required string Version { get; init; }

        [CommandOption("--dry-run")]
        [Description("Preview changes without applying them")]
        public bool DryRun { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Deploying version {settings.Version} to {settings.Environment}");
        if (settings.DryRun)
        {
            System.Console.WriteLine("(Dry run - no changes applied)");
        }
        return 0;
    }
}
