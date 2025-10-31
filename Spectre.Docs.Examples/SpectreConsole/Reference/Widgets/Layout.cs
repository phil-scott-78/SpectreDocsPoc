using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class LayoutExamples
{
    /// <summary>
    /// Demonstrates creating a basic layout with horizontal sections.
    /// </summary>
    public static void BasicLayoutExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right"));

        layout["Left"].Update(
            new Panel("Left Panel")
                .BorderColor(Color.Green));

        layout["Right"].Update(
            new Panel("Right Panel")
                .BorderColor(Color.Blue));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates splitting a layout into rows.
    /// </summary>
    public static void LayoutRowsExample()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Bottom"));

        layout["Top"].Update(
            new Panel("Header Section")
                .Header("Top")
                .BorderColor(Color.Yellow));

        layout["Bottom"].Update(
            new Panel("Content Section")
                .Header("Bottom")
                .BorderColor(Color.Cyan1));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates accessing and updating sections by name.
    /// </summary>
    public static void LayoutNavigationExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Center"),
                new Layout("Right"));

        // Update sections individually by name
        layout["Left"].Update(new Markup("[blue]Navigation[/]"));
        layout["Center"].Update(new Markup("[green]Main Content[/]"));
        layout["Right"].Update(new Markup("[yellow]Sidebar[/]"));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates controlling section sizes with fixed widths.
    /// </summary>
    public static void LayoutFixedSizeExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Sidebar").Size(20),
                new Layout("Content"));

        layout["Sidebar"].Update(
            new Panel("Fixed\n20 chars\nwide")
                .BorderColor(Color.Purple));

        layout["Content"].Update(
            new Panel("This section takes remaining space")
                .BorderColor(Color.Green));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates proportional sizing using ratios.
    /// </summary>
    public static void LayoutRatioExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left").Ratio(1),
                new Layout("Center").Ratio(2),
                new Layout("Right").Ratio(1));

        layout["Left"].Update(new Panel("1/4 width").BorderColor(Color.Blue));
        layout["Center"].Update(new Panel("2/4 width (half)").BorderColor(Color.Green));
        layout["Right"].Update(new Panel("1/4 width").BorderColor(Color.Yellow));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates setting minimum sizes to prevent sections from becoming too small.
    /// </summary>
    public static void LayoutMinimumSizeExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Navigation").MinimumSize(15),
                new Layout("Content"));

        layout["Navigation"].Update(
            new Panel("Navigation\nMin 15 chars")
                .BorderColor(Color.Cyan1));

        layout["Content"].Update(
            new Panel("Content area that flexes")
                .BorderColor(Color.Green));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates creating nested layouts with multiple levels of splitting.
    /// </summary>
    public static void LayoutNestedExample()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Header").Size(3),
                new Layout("Body"),
                new Layout("Footer").Size(3));

        layout["Header"].Update(
            new Panel("[yellow]Application Header[/]")
                .BorderColor(Color.Yellow));

        layout["Body"].SplitColumns(
            new Layout("Sidebar").Size(20),
            new Layout("Content"));

        layout["Sidebar"].Update(
            new Panel("[blue]Menu[/]\n• Home\n• Settings\n• About")
                .BorderColor(Color.Blue));

        layout["Content"].Update(
            new Panel("[green]Main Content Area[/]")
                .BorderColor(Color.Green));

        layout["Footer"].Update(
            new Panel("[grey]Status Bar[/]")
                .BorderColor(Color.Grey));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates creating a complex dashboard layout with multiple nested sections.
    /// </summary>
    public static void LayoutDashboardExample()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Header").Size(3),
                new Layout("Main"),
                new Layout("Footer").Size(3));

        layout["Header"].Update(
            new Panel("[bold yellow]System Dashboard[/]")
                .BorderColor(Color.Yellow));

        layout["Main"].SplitColumns(
            new Layout("Left").Ratio(1),
            new Layout("Right").Ratio(2));

        layout["Left"].SplitRows(
            new Layout("Metrics"),
            new Layout("Logs"));

        layout["Metrics"].Update(
            new Panel("[green]CPU: 45%\nRAM: 62%\nDisk: 78%[/]")
                .Header("System Metrics")
                .BorderColor(Color.Green));

        layout["Logs"].Update(
            new Panel("[dim]12:03 Process started\n12:05 Connected\n12:07 Ready[/]")
                .Header("Recent Logs")
                .BorderColor(Color.Blue));

        layout["Right"].Update(
            new Panel("[white]Main application content area\nDisplays real-time data and visualizations[/]")
                .Header("Content")
                .BorderColor(Color.Cyan1));

        layout["Footer"].Update(
            new Panel("[dim]Connected | Uptime: 2h 34m | Last Update: 12:07:45[/]")
                .BorderColor(Color.Grey));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates controlling section visibility dynamically.
    /// </summary>
    public static void LayoutVisibilityExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Middle"),
                new Layout("Right"));

        layout["Left"].Update(new Panel("Always visible").BorderColor(Color.Green));
        layout["Middle"].Update(new Panel("Hidden section").BorderColor(Color.Red));
        layout["Right"].Update(new Panel("Also visible").BorderColor(Color.Blue));

        // Hide the middle section
        layout["Middle"].IsVisible = false;

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates updating layout content dynamically.
    /// </summary>
    public static void LayoutDynamicUpdateExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Status"),
                new Layout("Content"));

        // Initial state
        layout["Status"].Update(
            new Panel("[yellow]Loading...[/]")
                .BorderColor(Color.Yellow));

        layout["Content"].Update(
            new Panel("Waiting for data...")
                .BorderColor(Color.Grey));

        // Simulate update (in real use, you'd re-render)
        layout["Status"].Update(
            new Panel("[green]Ready[/]")
                .BorderColor(Color.Green));

        layout["Content"].Update(
            new Panel("Data loaded successfully!")
                .BorderColor(Color.Green));

        AnsiConsole.Write(layout);
    }

    /// <summary>
    /// Demonstrates a three-column layout with different content types.
    /// </summary>
    public static void LayoutThreeColumnExample()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Navigation").Size(18),
                new Layout("Main"),
                new Layout("Sidebar").Size(22));

        layout["Navigation"].Update(
            new Panel(new Markup(
                "[blue]Menu[/]\n" +
                "├─ Dashboard\n" +
                "├─ Reports\n" +
                "└─ Settings"))
                .BorderColor(Color.Blue));

        var table = new Table()
            .BorderColor(Color.Green)
            .AddColumn("Item")
            .AddColumn("Value")
            .AddRow("Orders", "1,234")
            .AddRow("Revenue", "$45,678");

        layout["Main"].Update(
            new Panel(table)
                .Header("Statistics")
                .BorderColor(Color.Green));

        layout["Sidebar"].Update(
            new Panel("[yellow]Quick Actions[/]\n• New Report\n• Export Data\n• Refresh")
                .BorderColor(Color.Yellow));

        AnsiConsole.Write(layout);
    }
}
