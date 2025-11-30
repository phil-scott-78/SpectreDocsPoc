using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates multi-task progress tracking for deployments.</summary>
public class ProgressSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        console.Progress()
            .AutoClear(false)
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new RemainingTimeColumn(),
                new SpinnerColumn())
            .Start(ctx =>
            {
                var random = new Random(42);

                var tasks = new List<(ProgressTask Task, double Speed)>
                {
                    (ctx.AddTask("Reticulating splines"), random.NextDouble() * 2 + 1.1),
                    (ctx.AddTask("Hydrating caches"), random.NextDouble() * 2 + 1),
                    (ctx.AddTask("Consulting the oracle"), random.NextDouble() * 2 + 1.2),
                    (ctx.AddTask("Negotiating with upstream"), random.NextDouble() * 2 + 1.05),
                    (ctx.AddTask("Defenestrating legacy code"), random.NextDouble() * 2 + 1.4),
                };

                var launchTask = ctx.AddTask("Preparing for descent", autoStart: false);
                launchTask.IsIndeterminate();

                while (!ctx.IsFinished)
                {
                    foreach (var (task, speed) in tasks)
                    {
                        if (!task.IsFinished)
                        {
                            task.Increment(random.NextDouble() * speed);
                        }
                    }

                    if (tasks.All(t => t.Task.IsFinished) && !launchTask.IsStarted)
                    {
                        launchTask.StartTask();
                        launchTask.IsIndeterminate(false);
                    }

                    if (launchTask is { IsStarted: true, IsFinished: false })
                    {
                        launchTask.Increment(random.NextDouble() * 3 + 1);
                    }

                    Thread.Sleep(80);
                }
            });
    }
}
