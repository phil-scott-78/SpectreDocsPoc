using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class PadderSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        Panel[] outputs =
        [
            new Panel(new Markup("[blue]Content[/]"))
                .Header("[yellow]No padding[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).Padding(2, 2, 2, 2))
                .Header("[yellow]Pad all sides (2)[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).PadLeft(4))
                .Header("[yellow]Pad left only (.PadLeft)[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).PadRight(4))
                .Header("[yellow]Pad right only (.PadRight)[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).PadTop(2))
                .Header("[yellow]Pad top only (.PadTop)[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).PadBottom(2))
                .Header("[yellow]Pad bottom only (.PadBottom)[/]"),

            new Panel(new Padder(new Markup("[blue]Content[/]")).Padding(1, 3, 5, 2))
                .Header("[yellow]Asymmetric padding (1,3,5,2)[/]"),

            new Panel(new Padder(new Panel("[yellow]Padded Panel[/]").BorderColor(Color.Blue))
                    .Padding(2, 1, 2, 1))
                .Header("[yellow]Pad a panel widget[/]")
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                output.Expand = true;
                output.Padding = new Padding(1);
                output.Border = BoxBorder.Rounded;
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(2000);
            }
        });
    }
}
