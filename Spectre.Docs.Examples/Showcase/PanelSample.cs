using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class PanelSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Panel("[bold]Basic Panel[/]\n[dim]Default border[/]"),

            new Panel("[bold]Rounded Border Style[/]\n[dim]Uses BoxBorder.Rounded[/]")
                .RoundedBorder()
                .BorderColor(Color.Green),

            new Panel("[bold]Double Border Style[/]\n[dim]Uses BoxBorder.Double[/]")
                .Border(BoxBorder.Double)
                .BorderColor(Color.Blue),

            new Panel("[bold]Panel with Header[/]\n[dim]Shows title at top[/]")
                .Header("[yellow]My Title[/]"),

            new Panel("[bold]Centered Header[/]\n[dim]Title positioned center[/]")
                .Header(new PanelHeader("[yellow]═══ Centered ═══[/]", Justify.Center)),

            new Panel("[bold]Padded Content[/]\n[dim]Extra spacing inside: 2,1,2,1[/]")
                .Padding(2, 1, 2, 1)
                .BorderColor(Color.Green),

            new Panel("[bold]Expanded Panel[/]\n[dim]Fills available width with .Expand()[/]")
                .Expand()
                .BorderColor(Color.Yellow),

            new Panel(
                new Panel("[blue]Inner Panel[/]\n[dim]Nested inside outer[/]")
                    .BorderColor(Color.Blue))
                .Header("[yellow]Outer Panel[/]")
                .BorderColor(Color.Yellow),

            new Panel(new Rows(
                new Markup("[bold]Document Structure[/]"),
                new Rule(),
                new Text("Body content with multiple elements"),
                new Markup("[dim]Footer information[/]")))
                .Header("[green]Complex Layout[/]")
                .BorderColor(Color.Green)
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
}
