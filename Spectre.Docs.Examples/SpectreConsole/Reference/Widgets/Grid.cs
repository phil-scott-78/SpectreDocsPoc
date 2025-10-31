using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class GridExamples
{
    /// <summary>
    /// Demonstrates creating a basic grid with two columns and multiple rows.
    /// </summary>
    public static void BasicGridExample()
    {
        var grid = new Grid();

        // Add columns
        grid.AddColumn();
        grid.AddColumn();

        // Add rows
        grid.AddRow("Name:", "[blue]John Doe[/]");
        grid.AddRow("Email:", "[blue]john.doe@example.com[/]");
        grid.AddRow("Phone:", "[blue]+1 (555) 123-4567[/]");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates configuring column widths using fixed sizes and auto-sizing.
    /// </summary>
    public static void GridColumnWidthExample()
    {
        var grid = new Grid();

        // Add columns with explicit widths
        grid.AddColumn(new GridColumn { Width = 15 });
        grid.AddColumn(new GridColumn { Width = 40 });

        grid.AddRow("[yellow]Label Column[/]", "[green]Content Column[/]");
        grid.AddRow("Short", "This column has a fixed width of 40 characters");
        grid.AddRow("Status", "Active");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates using column alignment to control content positioning.
    /// </summary>
    public static void GridAlignmentExample()
    {
        var grid = new Grid();

        // Right-align the first column, left-align the second
        grid.AddColumn(new GridColumn { Alignment = Justify.Right });
        grid.AddColumn(new GridColumn { Alignment = Justify.Left });

        grid.AddRow("Amount:", "$1,234.56");
        grid.AddRow("Tax:", "$123.46");
        grid.AddRow("Total:", "[bold green]$1,358.02[/]");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates controlling column padding to adjust spacing between columns.
    /// </summary>
    public static void GridPaddingExample()
    {
        var grid = new Grid();

        // Add columns with custom padding
        grid.AddColumn(new GridColumn { Padding = new Padding(0, 0, 4, 0) });
        grid.AddColumn(new GridColumn { Padding = new Padding(0, 0, 2, 0) });
        grid.AddColumn(new GridColumn());

        grid.AddRow("[yellow]Wide[/]", "[blue]Normal[/]", "[green]Default[/]");
        grid.AddRow("Col 1", "Col 2", "Col 3");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates using NoWrap to prevent text wrapping within columns.
    /// </summary>
    public static void GridNoWrapExample()
    {
        var grid = new Grid();

        grid.AddColumn(new GridColumn { NoWrap = true, Width = 20 });
        grid.AddColumn(new GridColumn());

        grid.AddRow(
            "This is a very long label that would normally wrap",
            "Value");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates using Expand to make the grid fill available console width.
    /// </summary>
    public static void GridExpandExample()
    {
        var grid = new Grid { Expand = true };

        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("[yellow]Left[/]", "[cyan]Right[/]");
        grid.AddRow("This grid expands", "to fill the console");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates setting a fixed width for the entire grid.
    /// </summary>
    public static void GridWidthExample()
    {
        var grid = new Grid();
        grid.Width(60);

        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("[yellow]Fixed Width[/]", "[cyan]Grid[/]");
        grid.AddRow("This grid is", "exactly 60 characters wide");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates adding empty rows for visual spacing between content.
    /// </summary>
    public static void GridEmptyRowsExample()
    {
        var grid = new Grid();

        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("[yellow]Section 1[/]", "");
        grid.AddRow("Item A", "Value A");
        grid.AddRow("Item B", "Value B");
        grid.AddEmptyRow();
        grid.AddRow("[yellow]Section 2[/]", "");
        grid.AddRow("Item C", "Value C");
        grid.AddRow("Item D", "Value D");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates using AddColumns extension method to add multiple columns at once.
    /// </summary>
    public static void GridAddColumnsExample()
    {
        var grid = new Grid();

        // Add 4 columns at once
        grid.AddColumns(4);

        grid.AddRow("[red]Q1[/]", "[green]Q2[/]", "[blue]Q3[/]", "[yellow]Q4[/]");
        grid.AddRow("$10,000", "$12,500", "$11,000", "$15,000");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates embedding other widgets like Panels within grid cells.
    /// </summary>
    public static void GridNestedContentExample()
    {
        var grid = new Grid();

        grid.AddColumn();
        grid.AddColumn();

        var leftPanel = new Panel("[yellow]Configuration[/]")
            .BorderColor(Color.Yellow)
            .RoundedBorder();

        var rightPanel = new Panel("[green]Status: Active[/]")
            .BorderColor(Color.Green)
            .RoundedBorder();

        grid.AddRow(leftPanel, rightPanel);

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates creating a complex layout with mixed content types.
    /// </summary>
    public static void GridComplexLayoutExample()
    {
        var grid = new Grid();

        // Configure columns
        grid.AddColumn(new GridColumn { Width = 20, Alignment = Justify.Right });
        grid.AddColumn(new GridColumn());

        // Add header
        grid.AddRow(
            new Text("System Information", new Style(Color.Yellow, decoration: Decoration.Bold)),
            new Text(""));

        grid.AddEmptyRow();

        // Add data rows
        grid.AddRow(new Markup("OS:"), new Markup("[blue]Linux[/]"));
        grid.AddRow(new Markup("CPU:"), new Markup("[green]8 cores @ 3.2GHz[/]"));
        grid.AddRow(
            new Markup("Memory:"),
            new BreakdownChart()
                .Width(40)
                .AddItem("Used", 12, Color.Red)
                .AddItem("Available", 4, Color.Green));
        grid.AddRow(
            new Markup("Disk:"),
            new Panel("[yellow]65% used[/]")
                .BorderColor(Color.Yellow));

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Demonstrates creating a dashboard-style layout with multiple grids.
    /// </summary>
    public static void GridDashboardExample()
    {
        // Create main grid
        var mainGrid = new Grid { Expand = true };
        mainGrid.AddColumn();

        // Create header
        var header = new Panel("[bold yellow]System Dashboard[/]")
            .BorderColor(Color.Yellow)
            .RoundedBorder();

        mainGrid.AddRow(header);
        mainGrid.AddEmptyRow();

        // Create metrics grid
        var metricsGrid = new Grid();
        metricsGrid.AddColumns(3);

        var cpuPanel = new Panel("[green]CPU: 45%[/]")
            .Header("Processor")
            .BorderColor(Color.Green);

        var memPanel = new Panel("[yellow]Memory: 8.2GB[/]")
            .Header("RAM")
            .BorderColor(Color.Yellow);

        var diskPanel = new Panel("[red]Disk: 85%[/]")
            .Header("Storage")
            .BorderColor(Color.Red);

        metricsGrid.AddRow(cpuPanel, memPanel, diskPanel);

        mainGrid.AddRow(metricsGrid);

        AnsiConsole.Write(mainGrid);
    }
}
