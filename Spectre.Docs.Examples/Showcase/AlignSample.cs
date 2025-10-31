using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class AlignSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Align(
                new Panel("[bold dim]← LEFT[/]\n[dim]HorizontalAlignment.Left[/]"),
                HorizontalAlignment.Left,
                VerticalAlignment.Top),

            new Align(
                new Panel("[bold dim]CENTER[/]\n[dim]HorizontalAlignment.Center[/]"),
                HorizontalAlignment.Center,
                VerticalAlignment.Top),

            new Align(
                new Panel("[bold dim]RIGHT →[/]\n[dim]HorizontalAlignment.Right[/]"),
                HorizontalAlignment.Right,
                VerticalAlignment.Top),

            new Align(
                new Panel("[yellow bold]Centered Panel[/]\n[dim]Both H + V Middle[/]").BorderColor(Color.Blue),
                HorizontalAlignment.Center,
                VerticalAlignment.Middle),

            new Align(
                new Panel("[green bold]TOP CENTER[/]\n[dim]VerticalAlignment.Top[/]").BorderColor(Color.Green),
                HorizontalAlignment.Center,
                VerticalAlignment.Top) { Height = 5 },

            new Align(
                new Panel("[red bold]BOTTOM RIGHT[/]\n[dim]Both alignments[/]").BorderColor(Color.Red),
                HorizontalAlignment.Right,
                VerticalAlignment.Bottom) { Height = 5 },

            new Align(
                new Rows(
                    new Text("[blue bold]Multiple[/]"),
                    new Text("[yellow bold]Lines[/]"),
                    new Text("[green bold]Centered[/]")
                ),
                HorizontalAlignment.Center,
                VerticalAlignment.Middle) { Height = 7 }
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
