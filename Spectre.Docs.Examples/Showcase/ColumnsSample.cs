using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates the Columns widget for multi-column layouts.</summary>
public class ColumnsSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Columns(
                new Panel("[bold dim]← COLUMN 1[/]\n[dim]Left side[/]"),
                new Panel("[bold dim]COLUMN 2 →[/]\n[dim]Right side[/]")),

            new Columns(
                new Panel("[blue bold]FIRST[/]\n[dim]1/3 width[/]").BorderColor(Color.Blue),
                new Panel("[yellow bold]SECOND[/]\n[dim]1/3 width[/]").BorderColor(Color.Yellow),
                new Panel("[green bold]THIRD[/]\n[dim]1/3 width[/]").BorderColor(Color.Green)),

            new Columns(
                new Panel("[red]Panel A[/]").BorderColor(Color.Red),
                new Panel("[green]Panel B[/]").BorderColor(Color.Green),
                new Panel("[blue]Panel C[/]").BorderColor(Color.Blue)),

            new Columns(
                new Panel("[bold]Text Widget[/]\n[dim]Markup content[/]"),
                new Panel("[bold]Panel Widget[/]\n[dim]Boxed content[/]"),
                new Panel("[bold]Rule Widget[/]\n[dim]Divider line[/]")),

            new Columns(
                new Panel("[blue bold]Expanded[/]\n[dim].Expand() fills width[/]"),
                new Panel("[yellow bold]Columns[/]\n[dim]Full available space[/]")).Expand(),

            new Columns(
                new Panel("[blue]Short[/]"),
                new Panel("[yellow]This panel has much longer content that will naturally wrap to multiple lines[/]"),
                new Panel("[green]Medium text here[/]")),

            new Columns(
                new Panel(new Rows(
                    new Markup("[blue bold]Stacked[/]"),
                    new Markup("[dim]Row 1[/]"),
                    new Markup("[dim]Row 2[/]"))).BorderColor(Color.Blue),
                new Panel(new Rows(
                    new Markup("[green bold]Nested[/]"),
                    new Markup("[dim]- Item A[/]"),
                    new Markup("[dim]- Item B[/]"))).BorderColor(Color.Green))
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3000);
            }
        });
    }
}
