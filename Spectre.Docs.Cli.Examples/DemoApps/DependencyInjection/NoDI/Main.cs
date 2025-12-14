using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.NoDI;

/// <summary>
/// Dependency Injection Tutorial - Step 1: No DI.
/// Shows the starting point with greeting logic hard-coded in the command.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return await app.RunAsync(args);
    }
}

public class GreetSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name to greet")]
    public string Name { get; init; } = string.Empty;

    [CommandOption("-f|--formal")]
    [Description("Use formal greeting")]
    [DefaultValue(false)]
    public bool Formal { get; init; }
}

internal class GreetCommand : Command<GreetSettings>
{
    protected override int Execute(CommandContext context, GreetSettings settings, CancellationToken cancellation)
    {
        // Greeting logic is hard-coded here - not ideal for testing or flexibility
        if (settings.Formal)
        {
            AnsiConsole.WriteLine($"Good day, {settings.Name}.");
        }
        else
        {
            AnsiConsole.WriteLine($"Hello, {settings.Name}!");
        }
        return 0;
    }
}
