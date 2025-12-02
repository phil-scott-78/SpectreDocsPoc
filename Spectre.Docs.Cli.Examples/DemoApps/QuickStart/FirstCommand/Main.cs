using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand;

/// <summary>
/// A greeting CLI application for the Quick Start tutorial.
/// Demonstrates commands, arguments, and options.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return await app.RunAsync(args);
    }
}

internal class GreetCommand : Command<GreetCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Hello, {settings.Name}!");
        return 0;
    }
}
