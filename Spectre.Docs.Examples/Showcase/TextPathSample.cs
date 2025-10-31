using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class TextPathSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        Panel[] outputs =
        [
            new Panel(new TextPath("C:/Users/Alice/Documents/Projects/MyApp/src/Program.cs"))
                .Header("[yellow]Windows Path[/]"),

            new Panel(new TextPath("/home/user/projects/app/src/main.rs"))
                .Header("[yellow]Unix Path[/]"),

            new Panel(new TextPath("/very/long/path/to/some/deeply/nested/and/we/keep/going/to/make/sure/it/really/is/system32/long/file.txt"))
                .Header("[yellow]Long Path Truncated[/]"),

            new Panel(new TextPath("/custom/colored/path/file.txt")
                    .RootColor(Color.Green)
                    .SeparatorColor(Color.Grey)
                    .StemColor(Color.Blue)
                    .LeafColor(Color.Yellow))
                .Header("[yellow]Colored Components[/]"),

            new Panel(new TextPath("/aligned/to/left/file.txt").LeftJustified())
                .Header("[yellow]Left Aligned (.LeftJustified)[/]"),

            new Panel(new TextPath("/centered/path/file.txt").Centered())
                .Header("[yellow]Centered (.Centered)[/]"),

            new Panel(new TextPath("/aligned/to/right/file.txt").RightJustified())
                .Header("[yellow]Right Aligned (.RightJustified)[/]"),

            new Panel(new TextPath("/etc/config.json"))
                .Header("[yellow]Short Path[/]")
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                output.Width = 60;
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3000);
            }
        });
    }
}
