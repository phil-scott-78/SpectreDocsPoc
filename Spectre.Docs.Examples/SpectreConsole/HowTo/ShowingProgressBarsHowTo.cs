using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class ShowingProgressBarsHowTo
{
    /// <summary>
    /// Create a progress bar and update it as work completes.
    /// </summary>
    public static void CreateProgressBar()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing files", maxValue: 125);

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Track multiple operations with separate progress bars.
    /// </summary>
    public static void TrackMultipleTasks()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var download = ctx.AddTask("Downloading", maxValue: 125);
                var extract = ctx.AddTask("Extracting", maxValue: 150);
                var install = ctx.AddTask("Installing"); // maxValue defaults to 100

                while (!ctx.IsFinished)
                {
                    download.Increment(1.5);
                    extract.Increment(0.8);
                    install.Increment(1.2);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Configure which columns appear in the progress display.
    /// </summary>
    public static void CustomizeColumns()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new SpinnerColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Building project");

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Apply custom colors to the progress bar.
    /// </summary>
    public static void StyleProgressBar()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn
                {
                    CompletedStyle = new Style(Color.Green),
                    RemainingStyle = new Style(Color.Grey)
                },
                new PercentageColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Compiling");

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(40);
                }
            });
    }
}
