using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class ColumnsExamples
{
    /// <summary>
    /// Demonstrates creating basic columns from a collection of renderables.
    /// </summary>
    public static void BasicColumnsExample()
    {
        var columns = new Columns(
            new Panel("First column"),
            new Panel("Second column"),
            new Panel("Third column"));

        AnsiConsole.Write(columns);
    }

    /// <summary>
    /// Demonstrates creating columns from string items.
    /// </summary>
    public static void ColumnsFromStringsExample()
    {
        var items = new[]
        {
            "[blue]Azure[/]",
            "[green]AWS[/]",
            "[yellow]GCP[/]",
            "[red]Oracle Cloud[/]",
            "[purple]IBM Cloud[/]"
        };

        var columns = new Columns(items);

        AnsiConsole.Write(columns);
    }

    /// <summary>
    /// Demonstrates mixing different renderable types in columns.
    /// </summary>
    public static void ColumnsMixedContentExample()
    {
        var columns = new Columns(new Spectre.Console.Rendering.IRenderable[]
        {
            new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("Feature")
                .AddColumn("Status")
                .AddRow("Auth", "[green]Done[/]")
                .AddRow("API", "[yellow]In Progress[/]"),
            new Panel("[blue]Next up:[/]\n• Deployment\n• Testing"),
            new Markup("[red]Blocked:[/]\n• Code review\n• QA approval")
        });

        AnsiConsole.Write(columns);
    }

    /// <summary>
    /// Demonstrates using Expand to control width behavior.
    /// </summary>
    public static void ColumnsExpandExample()
    {
        AnsiConsole.MarkupLine("[yellow]Expanded (fills available width):[/]");
        var expanded = new Columns(
            new Panel("Item 1"),
            new Panel("Item 2"),
            new Panel("Item 3"))
        {
            Expand = true
        };
        AnsiConsole.Write(expanded);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Collapsed (fits content width):[/]");
        var collapsed = new Columns(
            new Panel("Item 1"),
            new Panel("Item 2"),
            new Panel("Item 3"))
        {
            Expand = false
        };
        AnsiConsole.Write(collapsed);
    }

    /// <summary>
    /// Demonstrates adjusting padding between columns.
    /// </summary>
    public static void ColumnsPaddingExample()
    {
        AnsiConsole.MarkupLine("[yellow]Default padding (1 space on right):[/]");
        var defaultPadding = new Columns(
            new Panel("Alpha"),
            new Panel("Beta"),
            new Panel("Gamma"));
        AnsiConsole.Write(defaultPadding);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Custom padding (2 spaces on left and right):[/]");
        var customPadding = new Columns(
            new Panel("Alpha"),
            new Panel("Beta"),
            new Panel("Gamma"))
        {
            Padding = new Padding(2, 0, 2, 0)
        };
        AnsiConsole.Write(customPadding);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]No padding:[/]");
        var noPadding = new Columns(
            new Panel("Alpha"),
            new Panel("Beta"),
            new Panel("Gamma"))
        {
            Padding = new Padding(0, 0, 0, 0)
        };
        AnsiConsole.Write(noPadding);
    }

    /// <summary>
    /// Demonstrates automatic wrapping when items exceed available width.
    /// </summary>
    public static void ColumnsWrappingExample()
    {
        var items = new[]
        {
            new Panel("[blue]Dashboard[/]"),
            new Panel("[green]Reports[/]"),
            new Panel("[yellow]Settings[/]"),
            new Panel("[red]Users[/]"),
            new Panel("[purple]Analytics[/]"),
            new Panel("[cyan]Notifications[/]"),
            new Panel("[white]Profile[/]"),
            new Panel("[grey]Help[/]")
        };

        var columns = new Columns(items);

        AnsiConsole.Write(columns);
    }

    /// <summary>
    /// Demonstrates building a dashboard layout with multiple column groups.
    /// </summary>
    public static void ColumnsDashboardExample()
    {
        // Metrics row
        var metrics = new Columns(
            new Panel("[green]Active Users\n1,234[/]").Header("Metrics"),
            new Panel("[blue]Page Views\n45,678[/]").Header("Metrics"),
            new Panel("[yellow]Revenue\n$12,345[/]").Header("Metrics"));

        // Status row
        var status = new Columns(
            new Panel("API: [green]Operational[/]\nDB: [green]Operational[/]").Header("System Status"),
            new Panel("CPU: 45%\nRAM: 67%").Header("Resources"));

        AnsiConsole.Write(metrics);
        AnsiConsole.WriteLine();
        AnsiConsole.Write(status);
    }

    /// <summary>
    /// Demonstrates using fluent extension methods for configuration.
    /// </summary>
    public static void ColumnsFluentExample()
    {
        var columns = new Columns(
            new Panel("Fits content"),
            new Panel("Auto-sized"),
            new Panel("Compact"))
            .Collapse();

        AnsiConsole.Write(columns);
    }
}
