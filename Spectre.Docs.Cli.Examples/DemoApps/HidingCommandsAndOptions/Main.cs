using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.HidingCommandsAndOptions;

/// <summary>
/// Demonstrates hiding commands and options from help output.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.AddCommand<DeployCommand>("deploy")
                .WithDescription("Deploy the application");

            config.AddCommand<StatusCommand>("status")
                .WithDescription("Show deployment status");

            // Hidden command - works but won't appear in help
            config.AddCommand<DiagnosticsCommand>("diagnostics")
                .WithDescription("Internal diagnostics")
                .IsHidden();
        });

        return await app.RunAsync(args);
    }
}

internal class DeployCommand : Command<DeployCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<environment>")]
        [Description("Target environment")]
        public string Environment { get; init; } = string.Empty;

        [CommandOption("-f|--force")]
        [Description("Skip confirmation")]
        public bool Force { get; init; }

        // Hidden option - works but won't appear in help
        [CommandOption("--skip-hooks", IsHidden = true)]
        [Description("Skip deployment hooks (internal use)")]
        public bool SkipHooks { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Deploying to {settings.Environment}");
        if (settings.SkipHooks)
        {
            System.Console.WriteLine("(skipping hooks)");
        }
        return 0;
    }
}

internal class StatusCommand : Command<StatusCommand.Settings>
{
    public class Settings : CommandSettings;

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine("Status: OK");
        return 0;
    }
}

internal class DiagnosticsCommand : Command<DiagnosticsCommand.Settings>
{
    public class Settings : CommandSettings;

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine("Running diagnostics...");
        return 0;
    }
}
