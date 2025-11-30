using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Live;

internal static class ProgressExamples
{
    /// <summary>
    /// Demonstrates creating a basic progress bar for a single task.
    /// </summary>
    public static void BasicProgressExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing files", maxValue: 100);

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates tracking multiple concurrent tasks with individual progress bars.
    /// </summary>
    public static void ProgressMultipleTasksExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task1 = ctx.AddTask("Downloading images", maxValue: 125);
                var task2 = ctx.AddTask("Processing documents", maxValue: 50);
                var task3 = ctx.AddTask("Compiling code"); // maxValue defaults to 100

                while (!ctx.IsFinished)
                {
                    task1.Increment(1.5);
                    task2.Increment(0.8);
                    task3.Increment(1.2);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates incrementing progress vs directly setting values.
    /// </summary>
    public static void ProgressIncrementExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing records", maxValue: 100);

                // Increment by a specific amount
                for (int i = 0; i < 50; i++)
                {
                    task.Increment(2);
                    Thread.Sleep(20);
                }

                // Or set the value directly
                task.Value = 75;
                Thread.Sleep(500);

                task.Value = 100;
            });
    }

    /// <summary>
    /// Demonstrates indeterminate progress for tasks with unknown total duration.
    /// </summary>
    public static void ProgressIndeterminateExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Connecting to server")
                    .IsIndeterminate();

                Thread.Sleep(2000);

                // Once we know the total, switch to determinate
                task.IsIndeterminate(false);
                task.MaxValue = 100;

                while (!ctx.IsFinished)
                {
                    task.Increment(2);
                    Thread.Sleep(30);
                }
            });
    }

    /// <summary>
    /// Demonstrates customizing progress display columns.
    /// </summary>
    public static void ProgressCustomColumnsExample()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new RemainingTimeColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing data");

                while (!ctx.IsFinished)
                {
                    task.Increment(0.5);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates adding a spinner animation to progress display.
    /// </summary>
    public static void ProgressWithSpinnerExample()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new SpinnerColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Analyzing files");

                while (!ctx.IsFinished)
                {
                    task.Increment(0.8);
                    Thread.Sleep(60);
                }
            });
    }

    /// <summary>
    /// Demonstrates elapsed and remaining time columns for time estimation.
    /// </summary>
    public static void ProgressTimingColumnsExample()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new ElapsedTimeColumn(),
                new RemainingTimeColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Converting video", maxValue: 100);

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(100);
                }
            });
    }

    /// <summary>
    /// Demonstrates download-specific columns for file transfer operations.
    /// </summary>
    public static void ProgressDownloadExample()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new DownloadedColumn(),
                new TransferSpeedColumn(),
                new RemainingTimeColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("game-installer.exe", maxValue: 524288000); // 500 MB in bytes

                while (!ctx.IsFinished)
                {
                    task.Increment(2621440); // 2.5 MB per tick
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates styling the progress bar with custom colors.
    /// </summary>
    public static void ProgressBarStylingExample()
    {
        AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn()
                {
                    CompletedStyle = new Style(Color.Green),
                    FinishedStyle = new Style(Color.Lime),
                    RemainingStyle = new Style(Color.Grey)
                },
                new PercentageColumn())
            .Start(ctx =>
            {
                var task = ctx.AddTask("Building project");

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    Thread.Sleep(40);
                }
            });
    }

    /// <summary>
    /// Demonstrates automatically clearing progress display after completion.
    /// </summary>
    public static void ProgressAutoClearExample()
    {
        AnsiConsole.Progress()
            .AutoClear(true)
            .Start(ctx =>
            {
                var task = ctx.AddTask("Temporary operation");

                while (!ctx.IsFinished)
                {
                    task.Increment(2);
                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[green]Operation complete![/]");
    }

    /// <summary>
    /// Demonstrates hiding completed tasks while keeping active ones visible.
    /// </summary>
    public static void ProgressHideCompletedExample()
    {
        AnsiConsole.Progress()
            .HideCompleted(true)
            .Start(ctx =>
            {
                var task1 = ctx.AddTask("Step 1", maxValue: 50);
                var task2 = ctx.AddTask("Step 2", maxValue: 50);
                var task3 = ctx.AddTask("Step 3", maxValue: 50);

                // Complete task 1
                while (!task1.IsFinished)
                {
                    task1.Increment(2);
                    Thread.Sleep(50);
                }

                // Complete task 2
                while (!task2.IsFinished)
                {
                    task2.Increment(2);
                    Thread.Sleep(50);
                }

                // Complete task 3
                while (!task3.IsFinished)
                {
                    task3.Increment(2);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates adding new tasks dynamically during progress execution.
    /// </summary>
    public static void ProgressDynamicTasksExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var scan = ctx.AddTask("Scanning directory");

                while (!scan.IsFinished)
                {
                    scan.Increment(2);
                    Thread.Sleep(50);
                }

                // After scanning, add new tasks for found items
                var process1 = ctx.AddTask("Processing report.pdf");
                var process2 = ctx.AddTask("Processing data.xlsx");

                while (!ctx.IsFinished)
                {
                    process1.Increment(1.5);
                    process2.Increment(1.2);
                    Thread.Sleep(50);
                }
            });
    }

    /// <summary>
    /// Demonstrates updating task descriptions dynamically during execution.
    /// </summary>
    public static void ProgressTaskDescriptionUpdateExample()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing", maxValue: 100);

                for (int i = 1; i <= 100; i++)
                {
                    task.Description = $"Processing file {i} of 100";
                    task.Increment(1);
                    Thread.Sleep(30);
                }
            });
    }

    /// <summary>
    /// Demonstrates using async/await with progress tracking.
    /// </summary>
    public static async Task ProgressAsyncExample()
    {
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task1 = ctx.AddTask("Async operation 1");
                var task2 = ctx.AddTask("Async operation 2");

                var operations = new[]
                {
                    Task.Run(async () =>
                    {
                        while (!task1.IsFinished)
                        {
                            task1.Increment(1);
                            await Task.Delay(50);
                        }
                    }),
                    Task.Run(async () =>
                    {
                        while (!task2.IsFinished)
                        {
                            task2.Increment(0.8);
                            await Task.Delay(60);
                        }
                    })
                };

                await Task.WhenAll(operations);
            });
    }

    /// <summary>
    /// Demonstrates returning a value from progress execution.
    /// </summary>
    public static void ProgressReturnValueExample()
    {
        var result = AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Processing items", maxValue: 100);
                int processedCount = 0;

                while (!ctx.IsFinished)
                {
                    task.Increment(1);
                    processedCount++;
                    Thread.Sleep(20);
                }

                return processedCount;
            });

        AnsiConsole.MarkupLine($"[green]Processed {result} items[/]");
    }
}
