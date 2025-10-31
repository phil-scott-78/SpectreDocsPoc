using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class RowsExamples
{
    /// <summary>
    /// Demonstrates creating a basic Rows widget with multiple renderables stacked vertically.
    /// </summary>
    public static void BasicRowsExample()
    {
        var rows = new Rows(
            new Markup("[blue]First row[/]"),
            new Markup("[green]Second row[/]"),
            new Markup("[yellow]Third row[/]"));

        AnsiConsole.Write(rows);
    }

    /// <summary>
    /// Demonstrates stacking panels to create a vertically organized layout.
    /// </summary>
    public static void RowsPanelsExample()
    {
        var rows = new Rows(
            new Panel("[blue]Header Section[/]").Header("Top"),
            new Panel("[green]Content Section[/]").Header("Middle"),
            new Panel("[yellow]Footer Section[/]").Header("Bottom"));

        AnsiConsole.Write(rows);
    }

    /// <summary>
    /// Demonstrates controlling width behavior with the Expand property.
    /// </summary>
    public static void RowsExpandExample()
    {
        AnsiConsole.MarkupLine("[yellow]Expand disabled (fit to content):[/]");
        var collapsed = new Rows(
            new Panel("Short"),
            new Panel("A longer panel with more text"))
        {
            Expand = false
        };
        AnsiConsole.Write(collapsed);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Expand enabled (fill width):[/]");
        var expanded = new Rows(
            new Panel("Short"),
            new Panel("A longer panel with more text"))
        {
            Expand = true
        };
        AnsiConsole.Write(expanded);
    }

    /// <summary>
    /// Demonstrates creating rows from a collection of renderables.
    /// </summary>
    public static void RowsFromCollectionExample()
    {
        var items = new List<IRenderable>
        {
            new Markup("[red]Task 1: Initialize project[/]"),
            new Markup("[yellow]Task 2: Configure dependencies[/]"),
            new Markup("[green]Task 3: Run tests[/]"),
            new Markup("[blue]Task 4: Deploy application[/]")
        };

        var rows = new Rows(items);
        AnsiConsole.Write(rows);
    }

    /// <summary>
    /// Demonstrates stacking different widget types to create complex layouts.
    /// </summary>
    public static void RowsMixedContentExample()
    {
        var table = new Table()
            .AddColumn("Name")
            .AddColumn("Value")
            .AddRow("CPU", "45%")
            .AddRow("Memory", "78%");

        var chart = new BreakdownChart()
            .AddItem("Used", 78, Color.Red)
            .AddItem("Free", 22, Color.Green);

        var rows = new Rows(
            new Rule("[yellow]System Status[/]"),
            table,
            new Text(""),
            new Rule("[yellow]Disk Usage[/]"),
            chart);

        AnsiConsole.Write(rows);
    }

    /// <summary>
    /// Demonstrates combining Rows with Columns for grid-like layouts.
    /// </summary>
    public static void RowsWithColumnsExample()
    {
        var topRow = new Columns(
            new Panel("[blue]Left Panel[/]").Header("Column 1"),
            new Panel("[green]Right Panel[/]").Header("Column 2"));

        var bottomRow = new Columns(
            new Panel("[yellow]Left Panel[/]").Header("Column 1"),
            new Panel("[red]Right Panel[/]").Header("Column 2"));

        var rows = new Rows(topRow, bottomRow);
        AnsiConsole.Write(rows);
    }

    /// <summary>
    /// Demonstrates building a status dashboard with multiple information sections.
    /// </summary>
    public static void RowsDashboardExample()
    {
        var header = new Panel("[bold yellow]Application Dashboard[/]")
            .BorderColor(Color.Yellow)
            .Expand();

        var metrics = new Columns(
            new Panel("[green]Uptime: 99.9%[/]").Header("Availability"),
            new Panel("[blue]Response: 42ms[/]").Header("Performance"),
            new Panel("[purple]Active: 1,247[/]").Header("Users"));

        var logs = new Panel(
            "[grey]INFO[/]  Application started\n" +
            "[green]OK[/]    Health check passed\n" +
            "[yellow]WARN[/]  High memory usage")
            .Header("Recent Logs");

        var rows = new Rows(header, metrics, logs);
        AnsiConsole.Write(rows);
    }
}
