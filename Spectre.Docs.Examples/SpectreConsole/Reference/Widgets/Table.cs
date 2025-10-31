using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class TableExamples
{
    /// <summary>
    /// Demonstrates creating a basic table with columns and rows.
    /// </summary>
    public static void BasicTableExample()
    {
        var table = new Table();

        // Add columns
        table.AddColumn("Name");
        table.AddColumn("Age");
        table.AddColumn("City");

        // Add rows
        table.AddRow("Alice", "28", "New York");
        table.AddRow("Bob", "35", "London");
        table.AddRow("Charlie", "42", "Tokyo");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates column alignment options: left, center, and right.
    /// </summary>
    public static void TableAlignmentExample()
    {
        var table = new Table();

        table.AddColumn("Left", col => col.LeftAligned());
        table.AddColumn("Center", col => col.Centered());
        table.AddColumn("Right", col => col.RightAligned());

        table.AddRow("Text", "Text", "Text");
        table.AddRow("Aligned", "Aligned", "Aligned");
        table.AddRow("Left", "Center", "Right");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Showcases the 18 built-in border styles available for tables.
    /// </summary>
    public static void TableBordersExample()
    {
        ShowBorder("Square", t => t.SquareBorder());
        ShowBorder("Rounded", t => t.RoundedBorder());
        ShowBorder("Minimal", t => t.MinimalBorder());
        ShowBorder("Heavy", t => t.HeavyBorder());
        ShowBorder("Double", t => t.DoubleBorder());
        ShowBorder("ASCII", t => t.AsciiBorder());

        static void ShowBorder(string name, Func<Table, Table> setBorder)
        {
            var table = new Table();
            setBorder(table);

            table.AddColumn("Border");
            table.AddColumn("Style");
            table.AddRow(name, "Example");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
    }

    /// <summary>
    /// Demonstrates border colors and styling options.
    /// </summary>
    public static void TableColorsExample()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Blue);

        table.AddColumn("[yellow]Product[/]");
        table.AddColumn("[yellow]Price[/]");

        table.AddRow("Widget", "[green]$9.99[/]");
        table.AddRow("Gadget", "[green]$19.99[/]");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates column configuration: width, padding, and text wrapping.
    /// </summary>
    public static void ColumnConfigurationExample()
    {
        var table = new Table();

        // Fixed width column
        table.AddColumn("ID", col => col.Width(5).Centered());

        // Column with custom padding
        table.AddColumn("Description", col => col.Width(30).PadLeft(2).PadRight(2));

        // NoWrap column for long text
        table.AddColumn("Status", col => col.NoWrap().RightAligned());

        table.AddRow("001", "This is a longer description that will wrap within the column width", "Active");
        table.AddRow("002", "Short text", "Pending");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates showing and hiding headers and footers with custom content.
    /// </summary>
    public static void HeadersAndFootersExample()
    {
        var table = new Table();

        // Add columns with footers
        table.AddColumn("Item");
        table.AddColumn("Quantity", col => col.RightAligned());
        table.AddColumn("Price", col => col.RightAligned());

        table.AddRow("Apples", "10", "$5.00");
        table.AddRow("Oranges", "5", "$3.50");
        table.AddRow("Bananas", "8", "$4.00");

        // Add footers
        table.Columns[0].Footer = new Text("Total", new Style(foreground: Color.Yellow));
        table.Columns[1].Footer = new Text("23", new Style(foreground: Color.Yellow));
        table.Columns[2].Footer = new Text("$12.50", new Style(foreground: Color.Green, decoration: Decoration.Bold));

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates hiding headers for data-only tables.
    /// </summary>
    public static void HiddenHeadersExample()
    {
        var table = new Table()
            .HideHeaders()
            .Border(TableBorder.None);

        table.AddColumn("Key");
        table.AddColumn("Value");

        table.AddRow("[blue]Name:[/]", "Application");
        table.AddRow("[blue]Version:[/]", "1.0.0");
        table.AddRow("[blue]Status:[/]", "[green]Running[/]");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates table titles and captions for context.
    /// </summary>
    public static void TitlesAndCaptionsExample()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Aqua)
            .Title("[yellow]Monthly Sales Report[/]")
            .Caption("[grey]Data as of October 2025[/]");

        table.AddColumn("Month");
        table.AddColumn("Revenue", col => col.RightAligned());

        table.AddRow("August", "$45,000");
        table.AddRow("September", "$52,000");
        table.AddRow("October", "$48,500");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates row separators for visually grouping data.
    /// </summary>
    public static void RowSeparatorsExample()
    {
        var table = new Table()
            .RoundedBorder()
            .ShowRowSeparators();

        table.AddColumn("Category");
        table.AddColumn("Item");
        table.AddColumn("Count", col => col.RightAligned());

        table.AddRow("Fruits", "Apples", "10");
        table.AddRow("Fruits", "Oranges", "5");
        table.AddRow("Vegetables", "Carrots", "15");
        table.AddRow("Vegetables", "Broccoli", "8");

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates table expansion to fill available console width.
    /// </summary>
    public static void ExpandModeExample()
    {
        AnsiConsole.Write(new Markup("[yellow]Normal (collapsed) table:[/]\n"));
        var normalTable = new Table();
        normalTable.AddColumn("Name");
        normalTable.AddColumn("Value");
        normalTable.AddRow("Setting", "Value");
        AnsiConsole.Write(normalTable);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup("[yellow]Expanded table:[/]\n"));
        var expandedTable = new Table()
            .Expand();
        expandedTable.AddColumn("Name");
        expandedTable.AddColumn("Value");
        expandedTable.AddRow("Setting", "Value");
        AnsiConsole.Write(expandedTable);
    }

    /// <summary>
    /// Demonstrates embedding tables within table cells for complex layouts.
    /// </summary>
    public static void NestedTablesExample()
    {
        // Create inner table
        var innerTable = new Table()
            .RoundedBorder()
            .BorderColor(Color.Grey);
        innerTable.AddColumn("Detail");
        innerTable.AddColumn("Value");
        innerTable.AddRow("CPU", "95%");
        innerTable.AddRow("Memory", "12GB");

        // Create outer table
        var outerTable = new Table()
            .SquareBorder();
        outerTable.AddColumn("Server");
        outerTable.AddColumn("Metrics");

        outerTable.AddRow(new Text("server-01"), innerTable);

        AnsiConsole.Write(outerTable);
    }

    /// <summary>
    /// Demonstrates using various renderables (Markup, Panels, Text) as cell content.
    /// </summary>
    public static void MixedContentExample()
    {
        var table = new Table()
            .RoundedBorder();

        table.AddColumn("Type");
        table.AddColumn("Example");

        // Markup in cells
        table.AddRow(new Text("Markup"), new Markup("[bold green]Success[/] :check_mark:"));

        // Panel in a cell
        var panel = new Panel("[yellow]Warning[/]")
            .BorderColor(Color.Yellow)
            .Padding(1, 0);
        table.AddRow(new Text("Panel"), panel);

        // Styled text
        table.AddRow(new Text("Text"), new Text("Styled Text", new Style(foreground: Color.Aqua, decoration: Decoration.Italic)));

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates dynamic table operations: updating cells and manipulating rows.
    /// </summary>
    public static void DynamicTableExample()
    {
        var table = new Table()
            .RoundedBorder();

        table.AddColumn("ID");
        table.AddColumn("Status");
        table.AddColumn("Progress");

        // Add initial rows
        table.AddRow("Task 1", "[yellow]Pending[/]", "0%");
        table.AddRow("Task 2", "[yellow]Pending[/]", "0%");
        table.AddRow("Task 3", "[yellow]Pending[/]", "0%");

        // Update cells dynamically
        table.UpdateCell(0, 1, new Markup("[green]Complete[/]"));
        table.UpdateCell(0, 2, new Markup("[green]100%[/]"));

        table.UpdateCell(1, 1, new Markup("[blue]In Progress[/]"));
        table.UpdateCell(1, 2, new Markup("[blue]45%[/]"));

        // Insert a new row
        table.InsertRow(3, new Markup("Task 4"), new Markup("[yellow]Pending[/]"), new Markup("0%"));

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates adding empty rows for visual spacing.
    /// </summary>
    public static void EmptyRowsExample()
    {
        var table = new Table()
            .RoundedBorder();

        table.AddColumn("Section");
        table.AddColumn("Value");

        table.AddRow("[yellow]Header Info[/]", "Data");
        table.AddEmptyRow();
        table.AddRow("[yellow]Body Info[/]", "Data");
        table.AddEmptyRow();
        table.AddRow("[yellow]Footer Info[/]", "Data");

        AnsiConsole.Write(table);
    }
}
