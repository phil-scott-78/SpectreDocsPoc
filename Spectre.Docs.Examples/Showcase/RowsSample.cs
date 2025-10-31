using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class RowsSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Rows(
                new Markup("[dim]Simple rows - stacking items vertically[/]"),
                new Markup(""),
                new Markup("[blue]First row[/]"),
                new Markup("[yellow]Second row[/]"),
                new Markup("[green]Third row[/]")),

            new Rows(
                new Markup("[dim]Mixed content types - combining different widgets[/]"),
                new Markup(""),
                new Markup("[bold]Header[/]"),
                new Rule(),
                new Markup("Body text"),
                new Panel("Boxed content")),

            new Rows(
                new Markup("[dim]With panels - stacked panels in rows[/]"),
                new Markup(""),
                new Panel("[blue]Panel 1[/]").BorderColor(Color.Blue),
                new Panel("[yellow]Panel 2[/]").BorderColor(Color.Yellow),
                new Panel("[green]Panel 3[/]").BorderColor(Color.Green)),

            new Rows(
                new Markup("[dim]Nested rows - rows within rows for structure[/]"),
                new Markup(""),
                new Markup("[bold]Outer row 1[/]"),
                new Rows(
                    new Markup("  [blue]Inner row 1[/]"),
                    new Markup("  [blue]Inner row 2[/]")),
                new Markup("[bold]Outer row 2[/]")),

            new Rows(
                new Markup("[dim]With rules - visual separators between sections[/]"),
                new Markup(""),
                new Markup("[blue]Section 1[/]"),
                new Rule(),
                new Markup("[yellow]Section 2[/]"),
                new Rule(),
                new Markup("[green]Section 3[/]"))
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3500);
            }
        });
    }
}
