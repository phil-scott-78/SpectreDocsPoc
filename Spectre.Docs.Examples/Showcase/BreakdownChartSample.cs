using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class BreakdownChartSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new BreakdownChart()
                .AddItem("Small (30%)", 30, Color.Blue)
                .AddItem("Medium (50%)", 50, Color.Yellow)
                .AddItem("Large (20%)", 20, Color.Green),

            new BreakdownChart()
                .AddItem("C# (45.5%)", 45.5, Color.Blue)
                .AddItem("JavaScript (30.2%)", 30.2, Color.Yellow)
                .AddItem("HTML (15.8%)", 15.8, Color.Red)
                .AddItem("CSS (8.5%)", 8.5, Color.Purple),

            new BreakdownChart()
                .Width(50)
                .AddItem("Desktop (60%)", 60, Color.Green)
                .AddItem("Mobile (30%)", 30, Color.Blue)
                .AddItem("Tablet (10%)", 10, Color.Orange1),

            new BreakdownChart()
                .AddItem("Segment 1 (20%)", 20, Color.Red)
                .AddItem("Segment 2 (15%)", 15, Color.Blue)
                .AddItem("Segment 3 (25%)", 25, Color.Green)
                .AddItem("Segment 4 (18%)", 18, Color.Yellow)
                .AddItem("Segment 5 (12%)", 12, Color.Purple)
                .AddItem("Segment 6 (10%)", 10, Color.Orange1),

            new BreakdownChart()
                .AddItem("Major (85%)", 85, Color.Green)
                .AddItem("Minor (10%)", 10, Color.Yellow)
                .AddItem("Tiny (5%)", 5, Color.Red),

            new BreakdownChart()
                .AddItem("Quarter A (25%)", 25, Color.Blue)
                .AddItem("Quarter B (25%)", 25, Color.Green)
                .AddItem("Quarter C (25%)", 25, Color.Yellow)
                .AddItem("Quarter D (25%)", 25, Color.Red)
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
