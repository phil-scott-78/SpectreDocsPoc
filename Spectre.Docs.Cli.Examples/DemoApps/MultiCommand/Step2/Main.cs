using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step2;

/// <summary>
/// Multi-Command CLI Tutorial - Step 2: Organizing commands with branches.
/// Demonstrates using AddBranch() to create command hierarchies.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "multi-command";
    public string Description => "Multi-Command tutorial: package manager CLI with branched commands.";

    public async Task<int> RunAsync(string[] args)
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

internal class AddPackageCommand : Command<AddPackageCommand.Settings>
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

internal class AddReferenceCommand : Command<AddReferenceCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("The project reference path to add")]
        public string ReferencePath { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Added reference to {settings.ReferencePath}");
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
        System.Console.WriteLine("References:");
        System.Console.WriteLine("  (none yet)");
        return 0;
    }
}
