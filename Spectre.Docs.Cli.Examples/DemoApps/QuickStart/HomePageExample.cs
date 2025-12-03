using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.QuickStart;

/// <summary>
/// Quick start example for the CLI documentation index page.
/// </summary>
public static class HomePageExample
{
    /// <summary>
    /// A complete CLI application demonstrating commands, arguments, and options.
    /// </summary>
    public static int Run(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return app.Run(args);
    }
}

/// <summary>
/// Settings for the greet command.
/// </summary>
public class GreetSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name to greet")]
    public required string Name { get; init; }

    [CommandOption("-c|--count")]
    [Description("Number of times to greet")]
    [DefaultValue(1)]
    public int Count { get; init; }
}

/// <summary>
/// A command that greets a user by name.
/// </summary>
public class GreetCommand : Command<GreetSettings>
{
    protected override int Execute(CommandContext context, GreetSettings settings, CancellationToken cancellation)
    {
        for (var i = 0; i < settings.Count; i++)
        {
            AnsiConsole.MarkupLine($"Hello, [green]{settings.Name}[/]!");
        }
        return 0;
    }
}
