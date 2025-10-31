using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class BarChartSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new BarChart()
                .AddItem("Small", 10, Color.Blue)
                .AddItem("Medium", 25, Color.Yellow)
                .AddItem("Large", 15, Color.Green),

            new BarChart()
                .Label("[yellow]Quarterly Sales[/]")
                .AddItem("Q1", 45, Color.Red)
                .AddItem("Q2", 60, Color.Green)
                .AddItem("Q3", 35, Color.Blue),

            new BarChart()
                .Label("[green bold]═══ Test Results ═══[/]")
                .CenterLabel()
                .AddItem("Pass", 85, Color.Green)
                .AddItem("Fail", 15, Color.Red),

            new BarChart()
                .Label("[blue]Width = 50 chars[/]")
                .Width(50)
                .AddItem("Short", 20, Color.Blue)
                .AddItem("Medium", 40, Color.Yellow)
                .AddItem("Long", 60, Color.Green),

            new BarChart()
                .Label("[yellow]Fruit Sales[/]")
                .AddItem("Apple", 12, Color.Red)
                .AddItem("Banana", 33, Color.Yellow)
                .AddItem("Orange", 54, Color.Orange1)
                .AddItem("Grape", 8, Color.Purple)
                .AddItem("Kiwi", 21, Color.Green),

            new BarChart()
                .Label("[blue]Value Distribution[/]")
                .AddItem("Low (10)", 10, Color.Blue)
                .AddItem("Medium (30)", 30, Color.Yellow)
                .AddItem("High (20)", 20, Color.Red)
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
