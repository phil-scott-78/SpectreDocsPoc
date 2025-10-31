using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class PadderExamples
{
    /// <summary>
    /// Demonstrates creating a basic padder with uniform padding.
    /// </summary>
    public static void BasicPadderExample()
    {
        var text = new Markup("[bold blue]Important Message[/]");
        var padded = new Padder(text);

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates setting uniform padding on all sides.
    /// </summary>
    public static void PadderUniformPaddingExample()
    {
        var text = new Markup("[yellow]Status: Active[/]");

        var padded = new Padder(text, new Padding(3));

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates setting different padding for horizontal and vertical sides.
    /// </summary>
    public static void PadderHorizontalVerticalPaddingExample()
    {
        var text = new Markup("[green]Success![/]");

        // 4 spaces left/right, 2 lines top/bottom
        var padded = new Padder(text, new Padding(4, 2));

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates setting individual padding for each side.
    /// </summary>
    public static void PadderIndividualSidesExample()
    {
        var text = new Markup("[red]Error detected[/]");

        // Left: 2, Top: 1, Right: 6, Bottom: 1
        var padded = new Padder(text, new Padding(2, 1, 6, 1));

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates using fluent extension methods to set padding per side.
    /// </summary>
    public static void PadderFluentExtensionsExample()
    {
        var text = new Markup("[cyan]Notification[/]");

        var padded = new Padder(text)
            .PadLeft(5)
            .PadRight(5)
            .PadTop(2)
            .PadBottom(2);

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates wrapping a panel with padding for precise spacing control.
    /// </summary>
    public static void PadderWithPanelExample()
    {
        var panel = new Panel("[yellow]Warning:[/] Low disk space")
            .BorderColor(Color.Yellow)
            .Header("System Alert");

        var padded = new Padder(panel, new Padding(3, 1));

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates wrapping a table with padding for layout control.
    /// </summary>
    public static void PadderWithTableExample()
    {
        var table = new Table()
            .AddColumn("Name")
            .AddColumn("Status")
            .AddRow("Service A", "[green]Running[/]")
            .AddRow("Service B", "[red]Stopped[/]");

        var padded = new Padder(table, new Padding(2, 1, 2, 1));

        AnsiConsole.Write(padded);
    }

    /// <summary>
    /// Demonstrates using the Expand property to fill available width.
    /// </summary>
    public static void PadderExpandExample()
    {
        var text = new Markup("[blue]Left aligned text[/]");

        AnsiConsole.MarkupLine("[yellow]Auto-width (default):[/]");
        var autoWidth = new Padder(text, new Padding(2));
        AnsiConsole.Write(autoWidth);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Expanded to fill width:[/]");
        var expanded = new Padder(text, new Padding(2))
        {
            Expand = true
        };
        AnsiConsole.Write(expanded);
    }

    /// <summary>
    /// Demonstrates nesting multiple padders for complex spacing requirements.
    /// </summary>
    public static void PadderNestedExample()
    {
        var content = new Markup("[bold white on blue] Inner Content [/]");

        var innerPadder = new Padder(content, new Padding(2, 1));
        var outerPadder = new Padder(innerPadder, new Padding(4, 2));

        AnsiConsole.Write(outerPadder);
    }

    /// <summary>
    /// Demonstrates using padding to create visual separation in a layout.
    /// </summary>
    public static void PadderVisualSeparationExample()
    {
        var header = new Padder(
            new Markup("[bold underline]Application Dashboard[/]"),
            new Padding(0, 0, 0, 1));

        var body = new Padder(
            new Markup("System running normally\nAll services operational"),
            new Padding(2, 1));

        var footer = new Padder(
            new Markup("[dim]Last updated: 2024-11-24[/]"),
            new Padding(0, 1, 0, 0));

        AnsiConsole.Write(header);
        AnsiConsole.Write(body);
        AnsiConsole.Write(footer);
    }
}
