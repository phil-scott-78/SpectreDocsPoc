using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

internal class InputSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        WriteHeader(console, "Strings", skipWriteLine: true);
        var name = AskName(console);

        WriteHeader(console, "Choices");
        var sport = AskSport(console);

        WriteHeader(console, "Integers");
        var age = AskAge(console);

        WriteHeader(console, "Secrets");
        var password = AskPassword(console);

        WriteHeader(console, "Optional");
        var color = AskColor(console);
        console.WriteLine();


        AnsiConsole.Write(new Rule("[yellow]Results[/]").RuleStyle("grey").LeftJustified());
        AnsiConsole.Write(new Table().AddColumns("[grey]Question[/]", "[grey]Answer[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow("[grey]Name[/]", name)
            .AddRow("[grey]Favorite sport[/]", sport)
            .AddRow("[grey]Age[/]", age.ToString())
            .AddRow("[grey]Password[/]", password)
            .AddRow("[grey]Favorite color[/]", string.IsNullOrEmpty(color) ? "Unknown" : color));
    }

    private static void WriteHeader(IAnsiConsole console, string title, bool skipWriteLine = false)
    {
        if (!skipWriteLine)
        {
            console.WriteLine();
        }
        console.Write(new Rule($"[yellow]{title}[/]").RuleStyle("grey").LeftJustified());
    }

    private static string AskName(IAnsiConsole console)
    {
        var name = console.Ask<string>("What's your [green]name[/]?");
        return name;
    }


    private static string AskSport(IAnsiConsole console)
    {
        var prompt = new TextPrompt<string>("What's your [green]favorite sport[/]?")
            .InvalidChoiceMessage("[red]That's not a sport![/]")
            .DefaultValue("Sport?")
            .AddChoice("Soccer")
            .AddChoice("Hockey")
            .AddChoice("Basketball");

        return console.Prompt(
            prompt);
    }

    private static int AskAge(IAnsiConsole console)
    {
        var prompt = new TextPrompt<int>("How [green]old[/] are you?")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]That's not a valid age[/]")
            .Validate(age =>
            {
                return age switch
                {
                    <= 0 => ValidationResult.Error("[red]You must at least be 1 years old[/]"),
                    >= 123 => ValidationResult.Error("[red]You must be younger than the oldest person alive[/]"),
                    _ => ValidationResult.Success(),
                };
            });

        return console.Prompt(
            prompt);
    }

    private static string AskPassword(IAnsiConsole console)
    {
        var prompt = new TextPrompt<string>("Enter [green]password[/]?")
            .PromptStyle("red")
            .Secret();

        return console.Prompt(
            prompt);
    }

    private static string AskColor(IAnsiConsole console)
    {
        var prompt = new TextPrompt<string>("[grey][[Optional]][/] What is your [green]favorite color[/]?")
            .AllowEmpty();

        return console.Prompt(prompt);
    }
}