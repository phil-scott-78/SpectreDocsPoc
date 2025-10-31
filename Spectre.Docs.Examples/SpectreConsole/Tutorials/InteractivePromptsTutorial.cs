using Spectre.Console;
using Spectre.Docs.Examples.Showcase;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// A tutorial that builds a pizza ordering system step by step.
/// Teaches text prompts, selection prompts, multi-selection, and confirmations.
/// </summary>
public class InteractivePromptsTutorial : BaseSample
{
    /// <summary>
    /// Asks for the customer's name using a simple text prompt.
    /// </summary>
    public void AskCustomerName()
    {
        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
        AnsiConsole.MarkupLine($"Welcome, [blue]{name}[/]!");
    }

    /// <summary>
    /// Lets the customer choose a pizza size using a selection prompt.
    /// </summary>
    public void ChoosePizzaSize()
    {
        var size = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What [green]size pizza[/] would you like?")
                .AddChoices("Small", "Medium", "Large", "Extra Large"));

        AnsiConsole.MarkupLine($"You selected: [yellow]{size}[/]");
    }

    /// <summary>
    /// Lets the customer select multiple toppings using a multi-selection prompt.
    /// </summary>
    public void SelectToppings()
    {
        var toppings = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("What [green]toppings[/] would you like?")
                .NotRequired()
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
                .AddChoices("Pepperoni", "Mushrooms", "Sausage",
                            "Onions", "Green Peppers", "Black Olives",
                            "Extra Cheese", "Bacon", "Pineapple"));

        if (toppings.Count == 0)
        {
            AnsiConsole.MarkupLine("A plain cheese pizza - classic choice!");
        }
        else
        {
            AnsiConsole.MarkupLine($"Toppings: [yellow]{string.Join(", ", toppings)}[/]");
        }
    }

    /// <summary>
    /// Asks for confirmation before placing the order.
    /// </summary>
    public void ConfirmOrder()
    {
        var confirmed = AnsiConsole.Confirm("Place this order?");

        if (confirmed)
        {
            AnsiConsole.MarkupLine("[green]Order placed![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Order cancelled.[/]");
        }
    }

    /// <summary>
    /// Demonstrates the complete pizza ordering flow with a styled summary.
    /// </summary>
    public override void Run(IAnsiConsole console)
    {
        AnsiConsole.MarkupLine("[bold yellow]Welcome to Spectre Pizza![/]");
        AnsiConsole.WriteLine();

        // Ask for name
        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");

        // Choose size
        var size = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What [green]size pizza[/] would you like?")
                .AddChoices("Small", "Medium", "Large", "Extra Large"));

        // Select toppings
        var toppings = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("What [green]toppings[/] would you like?")
                .NotRequired()
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
                .AddChoices("Pepperoni", "Mushrooms", "Sausage",
                    "Onions", "Green Peppers", "Black Olives",
                    "Extra Cheese", "Bacon", "Pineapple"));

        // Show order summary
        AnsiConsole.WriteLine();
        var panel = new Panel(
                new Rows(
                    new Markup($"[bold]Customer:[/] {name}"),
                    new Markup($"[bold]Size:[/]     {size}"),
                    new Markup($"[bold]Toppings:[/] {(toppings.Count > 0 ? string.Join(", ", toppings) : "Plain cheese")}")))
            .Header("[yellow]Order Summary[/]")
            .Border(BoxBorder.Rounded);
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();

        // Confirm order
        if (AnsiConsole.Confirm("Place this order?"))
        {
            AnsiConsole.MarkupLine($"[green]Order placed! Thanks, {name}![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Order cancelled.[/]");
        }    }
}
