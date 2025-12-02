using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.QuickStart.Complete;

/// <summary>
/// A greeting CLI application for the Quick Start tutorial.
/// Demonstrates commands, arguments, and options.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "quick-start";
    public string Description => "Quick Start tutorial: greeting CLI with arguments and options.";

    public async Task<int> RunAsync(string[] args)
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

        [CommandOption("-c|--count")]
        [Description("Number of times to greet")]
        [DefaultValue(1)]
        public int Count { get; init; } = 1;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        for (var i = 0; i < settings.Count; i++)
        {
            System.Console.WriteLine($"Hello, {settings.Name}!");
        }
        return 0;
    }
}
