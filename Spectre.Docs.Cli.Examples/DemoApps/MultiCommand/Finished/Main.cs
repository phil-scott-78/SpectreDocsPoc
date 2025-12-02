using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished;

/// <summary>
/// Multi-Command CLI Tutorial - Complete: Full CLI with shared settings.
/// Demonstrates settings inheritance for common options across all commands.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config =>
        {
            config.AddBranch("add", add =>
            {
                add.AddCommand<AddPackageCommand>("package");
                add.AddCommand<AddReferenceCommand>("reference");
            });
            config.AddCommand<ListCommand>("list");
        });
        return await app.RunAsync(args);
    }
}

internal class GlobalSettings : CommandSettings
{
    [CommandOption("-v|--verbose")]
    [Description("Enable verbose output")]
    [DefaultValue(false)]
    public bool Verbose { get; init; }
}

internal class AddPackageCommand : Command<AddPackageCommand.Settings>
{
    public class Settings : GlobalSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The package name to add")]
        public string PackageName { get; init; } = string.Empty;

        [CommandOption("--version")]
        [Description("The package version (default: latest)")]
        public string? Version { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var version = settings.Version ?? "latest";

        if (settings.Verbose)
        {
            System.Console.WriteLine($"Searching for package {settings.PackageName}...");
            System.Console.WriteLine($"Resolving version {version}...");
            System.Console.WriteLine($"Installing to ./packages...");
        }

        System.Console.WriteLine($"Added package {settings.PackageName} v{version}");
        return 0;
    }
}

internal class AddReferenceCommand : Command<AddReferenceCommand.Settings>
{
    public class Settings : GlobalSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("The project reference path to add")]
        public string ReferencePath { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        if (settings.Verbose)
        {
            System.Console.WriteLine($"Validating project at {settings.ReferencePath}...");
            System.Console.WriteLine($"Adding reference to project file...");
        }

        System.Console.WriteLine($"Added reference to {settings.ReferencePath}");
        return 0;
    }
}

internal class ListCommand : Command<ListCommand.Settings>
{
    public class Settings : GlobalSettings
    {
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        if (settings.Verbose)
        {
            System.Console.WriteLine("Reading project file...");
        }

        System.Console.WriteLine("Packages:");
        System.Console.WriteLine("  Newtonsoft.Json (13.0.1)");
        System.Console.WriteLine("References:");
        System.Console.WriteLine("  ../MyLib/MyLib.csproj");
        return 0;
    }
}
