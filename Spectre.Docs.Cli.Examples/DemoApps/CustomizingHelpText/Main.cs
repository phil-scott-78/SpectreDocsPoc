using System.ComponentModel;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;

namespace Spectre.Docs.Cli.Examples.DemoApps.CustomizingHelpText;

/// <summary>
/// Demonstrates basic help customization with app name, version, and examples.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DeployCommand>();

        app.Configure(config =>
        {
            // Set application name (overrides default exe/dll name)
            config.SetApplicationName("myapp");

            // Set version shown in --version output
            config.SetApplicationVersion("1.2.0");

            // Add examples shown in top-level help
            config.AddExample("production");
            config.AddExample("staging", "--force");
            config.AddExample("dev", "--dry-run", "--verbose");
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// Demonstrates customizing help styling.
/// </summary>
public class StyledHelpDemo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DeployCommand>();

        app.Configure(config =>
        {
            config.SetApplicationName("myapp");

            // Customize help text styling
            config.Settings.HelpProviderStyles = new HelpProviderStyle
            {
                Description = new DescriptionStyle
                {
                    Header = "bold blue",
                },
                Options = new OptionStyle
                {
                    RequiredOption = "bold red",
                    DefaultValue = "dim",
                },
                Arguments = new ArgumentStyle
                {
                    RequiredArgument = "bold green",
                    OptionalArgument = "dim green",
                },
            };
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// Demonstrates removing all styling for plain text output.
/// </summary>
public class PlainTextHelpDemo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DeployCommand>();

        app.Configure(config =>
        {
            config.SetApplicationName("myapp");

            // Remove all styling for plain text output (accessibility/piping)
            config.Settings.HelpProviderStyles = null;
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// A deployment command with various options for help demonstration.
/// </summary>
internal class DeployCommand : Command<DeployCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<environment>")]
        [Description("Target environment (production, staging, dev)")]
        public string Environment { get; init; } = string.Empty;

        [CommandOption("-f|--force")]
        [Description("Skip confirmation prompts")]
        public bool Force { get; init; }

        [CommandOption("-n|--dry-run")]
        [Description("Show what would be deployed without making changes")]
        public bool DryRun { get; init; }

        [CommandOption("-v|--verbose")]
        [Description("Enable verbose output")]
        public bool Verbose { get; init; }

        [CommandOption("--timeout")]
        [Description("Deployment timeout in seconds")]
        [DefaultValue(300)]
        public int Timeout { get; init; } = 300;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Deploying to {settings.Environment}");
        if (settings.DryRun)
        {
            System.Console.WriteLine("(dry run - no changes made)");
        }
        return 0;
    }
}
