using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class PromptingForUserInputHowTo
{
    /// <summary>
    /// Ask the user for text input.
    /// </summary>
    public static void AskForText()
    {
        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
        AnsiConsole.MarkupLine($"Hello, [blue]{name}[/]!");
    }

    /// <summary>
    /// Ask for confirmation with yes/no.
    /// </summary>
    public static void AskForConfirmation()
    {
        if (AnsiConsole.Confirm("Continue with installation?"))
        {
            AnsiConsole.MarkupLine("[green]Installing...[/]");
        }
    }

    /// <summary>
    /// Present a list of choices to select from.
    /// </summary>
    public static void PresentChoices()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an [green]environment[/]:")
                .AddChoices("Development", "Staging", "Production"));

        AnsiConsole.MarkupLine($"Deploying to [blue]{choice}[/]");
    }

    /// <summary>
    /// Allow selecting multiple items from a list.
    /// </summary>
    public static void AllowMultipleSelections()
    {
        var features = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]features[/] to enable:")
                .AddChoices("Logging", "Caching", "Authentication", "Analytics"));

        AnsiConsole.MarkupLine($"Enabled: [blue]{string.Join(", ", features)}[/]");
    }
}
