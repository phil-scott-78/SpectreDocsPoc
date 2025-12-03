using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.ConfiguringCommandApp;

/// <summary>
/// Demonstrates CommandApp configuration with multiple commands.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            // Set application identity for help output
            config.SetApplicationName("myapp");
            config.SetApplicationVersion("1.0.0");

            // Add commands with descriptions, aliases, and examples
            config.AddCommand<AddCommand>("add")
                .WithDescription("Add a new item")
                .WithAlias("a")
                .WithExample("add", "todo.txt")
                .WithExample("add", "notes.md", "--force");

            config.AddCommand<RemoveCommand>("remove")
                .WithDescription("Remove an item")
                .WithAlias("rm")
                .WithAlias("delete")
                .WithExample("remove", "old-file.txt");

            config.AddCommand<ListCommand>("list")
                .WithDescription("List all items")
                .WithAlias("ls");

#if DEBUG
            // Development-only settings
            config.PropagateExceptions();
            config.ValidateExamples();
#endif
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// Demonstrates global settings configuration.
/// </summary>
public class SettingsDemo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<ListCommand>();

        app.Configure(config =>
        {
            config.SetApplicationName("myapp");

            // Configure parsing behavior
            config.Settings.CaseSensitivity = CaseSensitivity.None;
            config.Settings.StrictParsing = false;
        });

        return await app.RunAsync(args);
    }
}

internal class AddCommand : Command<AddCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("Path to add")]
        public string Path { get; init; } = string.Empty;

        [CommandOption("-f|--force")]
        [Description("Overwrite if exists")]
        public bool Force { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Adding: {settings.Path}");
        return 0;
    }
}

internal class RemoveCommand : Command<RemoveCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("Path to remove")]
        public string Path { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Removing: {settings.Path}");
        return 0;
    }
}

internal class ListCommand : Command<ListCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-a|--all")]
        [Description("Show all items including hidden")]
        public bool All { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine("Listing items...");
        return 0;
    }
}
