using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step1;

/// <summary>
/// Multi-Command CLI Tutorial - Step 1: Two top-level commands.
/// Demonstrates using CommandApp with Configure() to add multiple commands.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "multi-command";
    public string Description => "Multi-Command tutorial: package manager CLI with multiple commands.";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config =>
        {
            config.AddCommand<AddCommand>("add");
            config.AddCommand<ListCommand>("list");
        });
        return await app.RunAsync(args);
    }
}

internal class AddCommand : Command<AddCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The package name to add")]
        public string PackageName { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Added package {settings.PackageName}");
        return 0;
    }
}

internal class ListCommand : Command<ListCommand.Settings>
{
    public class Settings : CommandSettings
    {
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine("Packages:");
        System.Console.WriteLine("  (none yet)");
        return 0;
    }
}
