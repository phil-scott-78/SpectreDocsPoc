using System.ComponentModel;
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

internal class GreetCommand : Command<GreetCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;

        [CommandOption("-f|--formal")]
        [Description("Use formal greeting")]
        [DefaultValue(false)]
        public bool Formal { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        // Greeting logic is hard-coded here - not ideal for testing or flexibility
        if (settings.Formal)
        {
            System.Console.WriteLine($"Good day, {settings.Name}.");
        }
        else
        {
            System.Console.WriteLine($"Hello, {settings.Name}!");
        }
        return 0;
    }
}
