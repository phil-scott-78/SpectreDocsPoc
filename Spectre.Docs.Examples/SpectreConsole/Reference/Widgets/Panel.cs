using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class PanelExamples
{
    /// <summary>
    /// Demonstrates creating a basic panel with text content.
    /// </summary>
    public static void BasicPanelExample()
    {
        var panel = new Panel("Hello, World!");
        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates adding a header to a panel.
    /// </summary>
    public static void PanelHeaderExample()
    {
        var panel = new Panel("This panel has a header")
            .Header("Information");

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates header alignment options: left, center, and right.
    /// </summary>
    public static void PanelHeaderAlignmentExample()
    {
        var left = new Panel("Left-aligned header")
            .Header("Left", Justify.Left);

        var center = new Panel("Center-aligned header")
            .Header("Center", Justify.Center);

        var right = new Panel("Right-aligned header")
            .Header("Right", Justify.Right);

        AnsiConsole.Write(left);
        AnsiConsole.Write(center);
        AnsiConsole.Write(right);
    }

    /// <summary>
    /// Demonstrates different border styles available for panels.
    /// </summary>
    public static void PanelBorderStylesExample()
    {
        var square = new Panel("Square border (default)")
            .SquareBorder();

        var rounded = new Panel("Rounded border")
            .RoundedBorder();

        var heavy = new Panel("Heavy border")
            .HeavyBorder();

        var doubleBorder = new Panel("Double border")
            .DoubleBorder();

        AnsiConsole.Write(square);
        AnsiConsole.Write(rounded);
        AnsiConsole.Write(heavy);
        AnsiConsole.Write(doubleBorder);
    }

    /// <summary>
    /// Demonstrates removing the border from a panel.
    /// </summary>
    public static void PanelNoBorderExample()
    {
        var panel = new Panel("Content without a border")
            .NoBorder()
            .Header("Still has a header");

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates applying color to the panel border.
    /// </summary>
    public static void PanelBorderColorExample()
    {
        var panel = new Panel("Colored border")
            .Header("Status")
            .BorderColor(Color.Green);

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates controlling padding inside the panel.
    /// </summary>
    public static void PanelPaddingExample()
    {
        var minimal = new Panel("Minimal padding")
            .Padding(0, 0);

        var spacious = new Panel("Extra padding")
            .Padding(4, 2);

        AnsiConsole.Write(minimal);
        AnsiConsole.Write(spacious);
    }

    /// <summary>
    /// Demonstrates expanding a panel to fill the available width.
    /// </summary>
    public static void PanelExpandExample()
    {
        var collapsed = new Panel("Fits content width");

        var expanded = new Panel("Fills available width")
            .Expand();

        AnsiConsole.Write(collapsed);
        AnsiConsole.Write(expanded);
    }

    /// <summary>
    /// Demonstrates setting an explicit width for a panel.
    /// </summary>
    public static void PanelWidthExample()
    {
        var panel = new Panel("Fixed width panel")
        {
            Width = 40
        };

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates nesting panels inside other panels.
    /// </summary>
    public static void PanelNestingExample()
    {
        var inner = new Panel("Inner content")
            .Header("Inner")
            .RoundedBorder()
            .BorderColor(Color.Yellow);

        var outer = new Panel(inner)
            .Header("Outer")
            .DoubleBorder()
            .BorderColor(Color.Blue);

        AnsiConsole.Write(outer);
    }

    /// <summary>
    /// Demonstrates wrapping a table inside a panel.
    /// </summary>
    public static void PanelWithTableExample()
    {
        var table = new Table()
            .AddColumn("Name")
            .AddColumn("Status")
            .AddRow("Server 1", "[green]Online[/]")
            .AddRow("Server 2", "[red]Offline[/]");

        var panel = new Panel(table)
            .Header("Server Status")
            .BorderColor(Color.Blue);

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates a fully styled panel with multiple options combined.
    /// </summary>
    public static void PanelFullySyledExample()
    {
        var panel = new Panel("[bold]Important Notice[/]\n\nThis message requires your attention.")
            .Header("[yellow]Warning[/]", Justify.Center)
            .RoundedBorder()
            .BorderColor(Color.Yellow)
            .Padding(2, 1)
            .Expand();

        AnsiConsole.Write(panel);
    }
}
