using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments;

/// <summary>
/// Demonstrates how to define commands, arguments, and options.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
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

/// <summary>
/// Demonstrates accepting multiple values with array arguments and options.
/// </summary>
internal class MultiFileCommand : Command<MultiFileCommand.Settings>
{
    public class Settings : CommandSettings
    {
        // Array argument captures all remaining positional values
        // Must be the last argument (highest position index)
        [CommandArgument(0, "<files>")]
        [Description("One or more files to process")]
        public string[] Files { get; init; } = [];

        // Array option - specify multiple times: --tag api --tag v2
        [CommandOption("-t|--tag <TAG>")]
        [Description("Tags to apply (can be specified multiple times)")]
        public string[] Tags { get; init; } = [];
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Files: {string.Join(", ", settings.Files)}");
        System.Console.WriteLine($"Tags: {string.Join(", ", settings.Tags)}");
        return 0;
    }
}

/// <summary>
/// Log level for output verbosity.
/// </summary>
public enum LogLevel
{
    Quiet,
    Normal,
    Verbose,
    Debug
}

/// <summary>
/// Demonstrates using enum types for constrained option values.
/// </summary>
internal class BuildCommand : Command<BuildCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[project]")]
        [Description("The project to build")]
        public string? Project { get; init; }

        // Enum option - framework validates and shows allowed values in help
        [CommandOption("-l|--log-level")]
        [Description("Set the logging verbosity")]
        [DefaultValue(LogLevel.Normal)]
        public LogLevel LogLevel { get; init; } = LogLevel.Normal;

        // Enum option for build configuration
        [CommandOption("-c|--configuration")]
        [Description("Build configuration")]
        [DefaultValue(Configuration.Debug)]
        public Configuration Configuration { get; init; } = Configuration.Debug;
    }

    public enum Configuration
    {
        Debug,
        Release
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var project = settings.Project ?? "current directory";
        System.Console.WriteLine($"Building {project}");
        System.Console.WriteLine($"Configuration: {settings.Configuration}");
        System.Console.WriteLine($"Log level: {settings.LogLevel}");
        return 0;
    }
}
