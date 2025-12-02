using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class GridSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        Panel[] outputs =
        [
            new Panel(CreateSimple2x2Grid())
                .Header("[yellow]Simple 2x2 Grid[/]"),

            new Panel(CreateThreeColumnsGrid())
                .Header("[yellow]Three Columns Grid[/]"),

            new Panel(CreateColumnPaddingGrid())
                .Header("[yellow]Column Padding (.PadLeft/.PadRight)[/]"),

            new Panel(CreateNoPaddingGrid())
                .Header("[yellow]No Padding (.NoWrap)[/]"),

            new Panel(CreateNestedPanelsGrid())
                .Header("[yellow]Grid with Nested Panel Widgets[/]"),

            new Panel(CreateExpandToFillGrid())
                .Header("[yellow]Expand to Fill (.Expand)[/]"),

            new Panel(CreateComplexLayoutGrid())
                .Header("[yellow]Complex Layout with Merged Cells[/]")
        ];

        // Animate
        AnsiConsole.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                output.Expand = true;
                output.Border = BoxBorder.Rounded;
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3500);
            }
        });
    }

    private static Grid CreateSimple2x2Grid()
    {
        var grid = new Grid()
            .AddColumn()
            .AddColumn();

        grid.AddRow(new Markup("[blue]Row 1, Col 1[/]"), new Markup("[yellow]Row 1, Col 2[/]"));
        grid.AddRow(new Markup("[green]Row 2, Col 1[/]"), new Markup("[red]Row 2, Col 2[/]"));
        return grid;
    }

    private static Grid CreateThreeColumnsGrid()
    {
        var grid = new Grid()
            .AddColumn()
            .AddColumn()
            .AddColumn();

        grid.AddRow("[blue]Left[/]", "[yellow]Center[/]", "[green]Right[/]");
        grid.AddRow("A", "B", "C");
        return grid;
    }

    private static Grid CreateColumnPaddingGrid()
    {
        var grid = new Grid()
            .AddColumn(new GridColumn().PadLeft(2).PadRight(2))
            .AddColumn(new GridColumn().PadLeft(2).PadRight(2));

        grid.AddRow("[blue]Padded[/]", "[yellow]Columns[/]");
        grid.AddRow("More", "Space");
        return grid;
    }

    private static Grid CreateNoPaddingGrid()
    {
        var grid = new Grid()
            .AddColumn(new GridColumn().NoWrap())
            .AddColumn(new GridColumn().NoWrap());

        grid.AddRow("[blue]Tight[/]", "[yellow]Layout[/]");
        grid.AddRow("No", "Padding");
        return grid;
    }

    private static Grid CreateNestedPanelsGrid()
    {
        var grid = new Grid()
            .AddColumn()
            .AddColumn();

        grid.AddRow(
            new Panel("[blue]Left Panel[/]").BorderColor(Color.Blue),
            new Panel("[yellow]Right Panel[/]").BorderColor(Color.Yellow));
        return grid;
    }

    private static Grid CreateExpandToFillGrid()
    {
        var grid = new Grid()
            .AddColumn(new GridColumn().Width(20))
            .AddColumn();

        grid.AddRow("[blue]Fixed 20[/]", "[yellow]Auto width[/]");
        grid.Expand();
        return grid;
    }

    private static Grid CreateComplexLayoutGrid()
    {
        var grid = new Grid()
            .AddColumn()
            .AddColumn()
            .AddColumn();

        var panel1 = new Panel("[blue]Header[/]").BorderColor(Color.Blue);
        var panel2 = new Panel("[yellow]Content[/]").BorderColor(Color.Yellow);
        var panel3 = new Panel("[green]Sidebar[/]").BorderColor(Color.Green);

        grid.AddRow(panel1, new Text(""), panel3);
        grid.AddRow(panel2, panel2, panel3);
        return grid;
    }
}
