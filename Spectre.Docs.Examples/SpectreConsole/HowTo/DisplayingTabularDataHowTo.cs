using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class DisplayingTabularDataHowTo
{
    /// <summary>
    /// Create a basic table with columns and rows of data.
    /// </summary>
    public static void CreateBasicTable()
    {
        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Department");
        table.AddColumn("Sales");

        table.AddRow("Alice", "North", "$12,400");
        table.AddRow("Bob", "South", "$8,750");
        table.AddRow("Carol", "West", "$15,200");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Apply a rounded border for a cleaner, modern look.
    /// </summary>
    public static void ApplyBorderStyle()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Grey);

        table.AddColumn("Name");
        table.AddColumn("Department");
        table.AddColumn("Sales");

        table.AddRow("Alice", "North", "$12,400");
        table.AddRow("Bob", "South", "$8,750");
        table.AddRow("Carol", "West", "$15,200");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Right-align numeric columns for easier scanning.
    /// </summary>
    public static void AlignColumns()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Grey);

        table.AddColumn("Name");
        table.AddColumn("Department", col => col.Centered());
        table.AddColumn("Sales", col => col.RightAligned());

        table.AddRow("Alice", "North", "$12,400");
        table.AddRow("Bob", "South", "$8,750");
        table.AddRow("Carol", "West", "$15,200");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Add a title for context and a footer row for totals.
    /// </summary>
    public static void AddTitleAndFooter()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .Title("[yellow bold]Q4 Sales Report[/]");

        table.AddColumn("Name");
        table.AddColumn("Department", col => col.Centered());
        table.AddColumn("Sales", col => col.RightAligned());

        table.AddRow("Alice", "North", "$12,400");
        table.AddRow("Bob", "South", "$8,750");
        table.AddRow("Carol", "West", "$15,200");

        // Add footer with totals
        table.Columns[0].Footer = new Text("Total", new Style(decoration: Decoration.Bold));
        table.Columns[1].Footer = new Text("");
        table.Columns[2].Footer = new Text("$36,350", new Style(Color.Green, decoration: Decoration.Bold));

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Complete example: a styled sales report with borders, alignment, title, and totals.
    /// </summary>
    public static void RunAll()
    {
        AnsiConsole.MarkupLine("[dim]Step 1: Basic table[/]");
        CreateBasicTable();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 2: With rounded border[/]");
        ApplyBorderStyle();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 3: Aligned columns[/]");
        AlignColumns();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 4: Title and footer[/]");
        AddTitleAndFooter();
    }
}
