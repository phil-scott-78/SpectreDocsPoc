using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates terminal capabilities with progress and TrueColor output.</summary>
public class CapabilitiesSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        // Quick single-row progress bar (~5 seconds)
        console.Progress()
            .AutoClear(true)
            .HideCompleted(false)
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Matriculating down the field");

                while (!task.IsFinished)
                {
                    task.Increment(2.5);
                    Thread.Sleep(50);
                }
            });

        // 6 lines of TrueColor gradient output
        console.WriteLine();
        for (var i = 0; i < 6; i++)
        {
            var hue = i * 60; // 0-360 degrees across 6 lines
            var (r, g, b) = HslToRgb(hue, 0.8, 0.6);

            console.MarkupLine($"[rgb({r},{g},{b})]Line {i + 1}: TrueColor RGB({r}, {g}, {b}) [/]");
        }
    }

    private static (int R, int G, int B) HslToRgb(double h, double s, double l)
    {
        var c = (1 - Math.Abs(2 * l - 1)) * s;
        var x = c * (1 - Math.Abs((h / 60) % 2 - 1));
        var m = l - c / 2;

        var (r, g, b) = h switch
        {
            < 60 => (c, x, 0.0),
            < 120 => (x, c, 0.0),
            < 180 => (0.0, c, x),
            < 240 => (0.0, x, c),
            < 300 => (x, 0.0, c),
            _ => (c, 0.0, x)
        };

        return ((int)((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));
    }
}
