using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class MarkupExamples
{
    /// <summary>
    /// Demonstrates basic markup output using AnsiConsole.MarkupLine.
    /// </summary>
    public static void BasicMarkupExample()
    {
        // Simple colored text
        AnsiConsole.MarkupLine("[green]Success![/]");
        AnsiConsole.MarkupLine("[red]Error occurred[/]");

        // Multiple colors in one line
        AnsiConsole.MarkupLine("[blue]Info:[/] Processing [yellow]3[/] items...");

        // Text decorations
        AnsiConsole.MarkupLine("[bold]Bold text[/]");
        AnsiConsole.MarkupLine("[italic]Italic text[/]");
        AnsiConsole.MarkupLine("[underline]Underlined text[/]");

        // Combined styles
        AnsiConsole.MarkupLine("[bold red]Critical error[/]");
        AnsiConsole.MarkupLine("[bold green on black]Highlighted success[/]");
    }

    /// <summary>
    /// Demonstrates creating Markup objects for use in layouts and containers.
    /// </summary>
    public static void MarkupObjectExample()
    {
        // Create a Markup object
        var message = new Markup("[bold blue]Important Notice[/]");
        AnsiConsole.Write(message);
        AnsiConsole.WriteLine();

        // Markup with multi-line content
        var multiLine = new Markup(
            "[yellow]Warning:[/] Multiple issues detected.\n" +
            "[dim]See log for details.[/]"
        );
        AnsiConsole.Write(multiLine);
    }

    /// <summary>
    /// Demonstrates escaping user input to prevent markup parsing errors.
    /// </summary>
    public static void MarkupEscapeExample()
    {
        // User input that contains brackets
        var userInput = "Use [brackets] for arrays";
        var escaped = Markup.Escape(userInput);

        // Safe to use in markup string
        AnsiConsole.MarkupLine($"[blue]User said:[/] {escaped}");

        // Without escaping, brackets would be interpreted as markup tags
        var fileName = "config[backup].json";
        AnsiConsole.MarkupLine($"[green]Processing:[/] {Markup.Escape(fileName)}");
    }

    /// <summary>
    /// Demonstrates using Markup objects within panels, tables, and other containers.
    /// </summary>
    public static void MarkupInContainersExample()
    {
        // Markup in a Panel
        var panel = new Panel(new Markup("[bold]Welcome![/]\n\nThis panel contains [green]styled[/] text."))
            .Header("Message")
            .BorderColor(Color.Blue);

        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();

        // Markup in a Table
        var table = new Table()
            .AddColumn("Status")
            .AddColumn("Message");

        table.AddRow(
            new Markup("[green]OK[/]"),
            new Markup("All systems operational")
        );
        table.AddRow(
            new Markup("[yellow]WARN[/]"),
            new Markup("High memory usage detected")
        );
        table.AddRow(
            new Markup("[red]ERROR[/]"),
            new Markup("[bold]Connection failed[/]")
        );

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates removing markup tags from a string.
    /// </summary>
    public static void MarkupRemoveExample()
    {
        var styled = "[bold red]Error:[/] File [underline]not found[/]";

        // Remove all markup tags to get plain text
        var plain = Markup.Remove(styled);

        AnsiConsole.MarkupLine("[dim]Original:[/]");
        AnsiConsole.MarkupLine(styled);
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Without markup:[/]");
        AnsiConsole.WriteLine(plain);
    }
}
