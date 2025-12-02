using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.MakingOptionsRequired;

/// <summary>
/// Demonstrates how to make options required.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "required-options";
    public string Description => "Demonstrates making options required instead of optional.";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DeployCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A deployment command with required options.
/// </summary>
internal class DeployCommand : Command<DeployCommand.Settings>
{
    public class Settings : CommandSettings
    {
        // Required option using the attribute
        [CommandOption("-e|--environment")]
        [Description("Target environment (required)")]
        public required string Environment { get; init; }

        // Another required option
        [CommandOption("-v|--version")]
        [Description("Version to deploy (required)")]
        public required string Version { get; init; }

        // Optional option for comparison
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
