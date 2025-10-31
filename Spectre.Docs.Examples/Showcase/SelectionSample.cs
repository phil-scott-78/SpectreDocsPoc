using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

internal class SelectionSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        // Ask for the user's favorite fruit
        var fruit = console.Prompt(
            new SelectionPrompt<string>()
                .Title("What's your [green]favorite fruit[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices("Apple", "Apricot", "Avocado", "Banana", "Blackcurrant", "Blueberry", "Cherry", "Cloudberry", "Coconut"));

        // Echo the fruit back to the terminal
        console.WriteLine($"I agree. {fruit} is tasty!");
    }
}