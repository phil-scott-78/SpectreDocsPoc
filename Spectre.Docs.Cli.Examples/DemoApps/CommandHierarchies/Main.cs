using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies;

/// <summary>
/// Demonstrates nested command hierarchies using AddBranch.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.SetApplicationName("myapp");

            // Create a "remote" branch with nested commands
            config.AddBranch<RemoteSettings>("remote", remote =>
            {
                remote.SetDescription("Manage remote repositories");

                remote.AddCommand<RemoteAddCommand>("add")
                    .WithDescription("Add a new remote");

                remote.AddCommand<RemoteRemoveCommand>("remove")
                    .WithDescription("Remove a remote")
                    .WithAlias("rm");

                remote.AddCommand<RemoteListCommand>("list")
                    .WithDescription("List all remotes")
                    .WithAlias("ls");
            });
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// Base settings shared by all remote subcommands.
/// </summary>
public class RemoteSettings : CommandSettings
{
    [CommandOption("-v|--verbose")]
    [Description("Show detailed output")]
    public bool Verbose { get; init; }
}

/// <summary>
/// Settings for adding a remote, inherits shared options.
/// </summary>
public class RemoteAddSettings : RemoteSettings
{
    [CommandArgument(0, "<name>")]
    [Description("Name for the remote")]
    public string Name { get; init; } = string.Empty;

    [CommandArgument(1, "<url>")]
    [Description("URL of the remote repository")]
    public string Url { get; init; } = string.Empty;
}

/// <summary>
/// Settings for removing a remote.
/// </summary>
public class RemoteRemoveSettings : RemoteSettings
{
    [CommandArgument(0, "<name>")]
    [Description("Name of the remote to remove")]
    public string Name { get; init; } = string.Empty;
}

internal class RemoteAddCommand : Command<RemoteAddSettings>
{
    protected override int Execute(CommandContext context, RemoteAddSettings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Adding remote '{settings.Name}' -> {settings.Url}");
        if (settings.Verbose)
        {
            System.Console.WriteLine("(verbose mode enabled)");
        }
        return 0;
    }
}

internal class RemoteRemoveCommand : Command<RemoteRemoveSettings>
{
    protected override int Execute(CommandContext context, RemoteRemoveSettings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Removing remote '{settings.Name}'");
        return 0;
    }
}

internal class RemoteListCommand : Command<RemoteSettings>
{
    protected override int Execute(CommandContext context, RemoteSettings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine("Listing remotes...");
        if (settings.Verbose)
        {
            System.Console.WriteLine("origin -> https://github.com/user/repo.git");
        }
        return 0;
    }
}
