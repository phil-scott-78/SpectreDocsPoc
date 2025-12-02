using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments;

/// <summary>
/// Demonstrates how to define commands, arguments, and options.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "defining-commands";
    public string Description => "Demonstrates defining commands, arguments, and options.";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<FileCopyCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A file copy command demonstrating various argument and option patterns.
/// </summary>
internal class FileCopyCommand : Command<FileCopyCommand.Settings>
{
    public class Settings : CommandSettings
    {
        // Required positional argument (angle brackets)
        [CommandArgument(0, "<source>")]
        [Description("The source file to copy")]
        public string Source { get; init; } = string.Empty;

        // Optional positional argument (square brackets)
        [CommandArgument(1, "[destination]")]
        [Description("The destination path (defaults to current directory)")]
        public string? Destination { get; init; }

        // Short and long option forms
        [CommandOption("-f|--force")]
        [Description("Overwrite existing files without prompting")]
        public bool Force { get; init; }

        // Option with a value
        [CommandOption("-b|--buffer-size")]
        [Description("Buffer size in KB for the copy operation")]
        [DefaultValue(64)]
        public int BufferSize { get; init; } = 64;

        // Long-form only option
        [CommandOption("--preserve-timestamps")]
        [Description("Preserve original file timestamps")]
        public bool PreserveTimestamps { get; init; }

        // Short-form only option
        [CommandOption("-v")]
        [Description("Enable verbose output")]
        public bool Verbose { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var dest = settings.Destination ?? ".";

        System.Console.WriteLine($"Source: {settings.Source}");
        System.Console.WriteLine($"Destination: {dest}");
        System.Console.WriteLine($"Force: {settings.Force}");
        System.Console.WriteLine($"Buffer Size: {settings.BufferSize} KB");
        System.Console.WriteLine($"Preserve Timestamps: {settings.PreserveTimestamps}");
        System.Console.WriteLine($"Verbose: {settings.Verbose}");

        if (settings.Verbose)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"[Verbose] Would copy '{settings.Source}' to '{dest}'");
        }

        return 0;
    }
}
