using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class TableSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            CreateBasicTable(),
            CreateBordersTable(),
            CreateColumnAlignmentTable(),
            CreateTitleAndCaptionTable(),
            CreateRowSeparatorsTable(),
            CreateNestedContentTable(),
            CreateExpandModeTable()
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(2000);
            }
        });
    }

    private static Table CreateBasicTable()
    {
        var table = new Table()
            .Width(70);
        table.AddColumn("[bold]Name[/]");
        table.AddColumn("[bold]Age[/]");
        table.AddColumn("[bold]City[/]");
        table.AddColumn("[bold]Department[/]");
        table.AddRow("Alice Johnson", "28", "New York City", "Engineering");
        table.AddRow("Bob Smith", "34", "Los Angeles", "Marketing");
        table.AddRow("Charlie Brown", "25", "Chicago", "Sales");
        return table;
    }

    private static Table CreateBordersTable()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .Title("[blue]Rounded Border (TableBorder.Rounded)[/]")
            .Width(70);
        table.AddColumn("Product");
        table.AddColumn("Price");
        table.AddColumn("Stock");
        table.AddColumn("Category");
        table.AddRow("Fresh Apples", "$1.50/lb", "In Stock", "Fruit");
        table.AddRow("Organic Bananas", "$0.75/lb", "Low Stock", "Fruit");
        table.AddRow("Navel Oranges", "$1.25/lb", "In Stock", "Fruit");
        return table;
    }

    private static Table CreateColumnAlignmentTable()
    {
        var table = new Table()
            .Title("[yellow]Column Alignment Demo[/]")
            .Width(70);
        table.AddColumn(new TableColumn("[dim]← Left Aligned[/]") { Alignment = Justify.Left });
        table.AddColumn(new TableColumn("[dim]Center Aligned[/]") { Alignment = Justify.Center });
        table.AddColumn(new TableColumn("[dim]Right Aligned →[/]") { Alignment = Justify.Right });
        table.AddRow("START", "MIDDLE", "END");
        table.AddRow("Longer text content", "Centered value", "123");
        table.AddRow("Left side", "Center", "→");
        return table;
    }

    private static Table CreateTitleAndCaptionTable()
    {
        var table = new Table()
            .Title("[yellow bold]Quarterly Sales Report[/]")
            .Caption("[grey]Q1 2024 (Title + Caption Demo)[/]")
            .Border(TableBorder.Double)
            .Width(70);
        table.AddColumn("Month");
        table.AddColumn("Revenue");
        table.AddColumn("Growth");
        table.AddColumn("Target");
        table.AddRow("January", "$10,000", "+5%", "$9,500");
        table.AddRow("February", "$15,000", "+50%", "$12,000");
        table.AddRow("March", "$20,000", "+33%", "$18,000");
        return table;
    }

    private static Table CreateRowSeparatorsTable()
    {
        var table = new Table()
            .Title("[green].ShowRowSeparators()[/]")
            .ShowRowSeparators()
            .Border(TableBorder.Rounded)
            .Width(70);
        table.AddColumn("Ticket ID");
        table.AddColumn("Status");
        table.AddColumn("Priority");
        table.AddColumn("Assignee");
        table.AddRow("TICKET-001", "[green]Active[/]", "High", "Alice");
        table.AddRow("TICKET-002", "[yellow]Pending[/]", "Medium", "Bob");
        table.AddRow("TICKET-003", "[red]Closed[/]", "Low", "Charlie");
        return table;
    }

    private static Table CreateNestedContentTable()
    {
        var table = new Table()
            .Title("[blue]Nested Widgets in Cells[/]")
            .Width(70);
        table.AddColumn("Widget Type");
        table.AddColumn("Description");
        table.AddColumn("Example");
        table.AddRow(
            new Text("[dim]Text[/]"),
            new Text("Simple text widget"),
            new Text("Plain content here"));
        table.AddRow(
            new Text("[dim]Panel[/]"),
            new Text("Boxed content widget"),
            new Panel("[blue]Boxed![/]").BorderColor(Color.Blue));
        table.AddRow(
            new Text("[dim]Markup[/]"),
            new Text("Styled markup widget"),
            new Markup("[red bold]Styled![/]"));
        return table;
    }

    private static Table CreateExpandModeTable()
    {
        var table = new Table()
            .Title("[green].Expand() - Fills Width[/]")
            .Expand()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Green);
        table.AddColumn("Feature");
        table.AddColumn("Description");
        table.AddColumn("Status");
        table.AddRow("Expand mode", "Table fills full available terminal width", "[green]Enabled[/]");
        table.AddRow("Auto-sizing", "Columns distribute space automatically", "[green]Active[/]");
        return table;
    }
}
