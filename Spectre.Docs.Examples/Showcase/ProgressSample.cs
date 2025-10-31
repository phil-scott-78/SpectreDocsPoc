using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates progress bars with multiple tasks.</summary>
public class ProgressSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        // Show progress
        console.Progress()
            .AutoClear(false)
            .Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn(), new RemainingTimeColumn(), new SpinnerColumn())
            .Start(ctx =>
            {
                var random = new Random(122978);

                // Create some tasks
                var tasks = CreateTasks(ctx, random);
                var warpTask = ctx.AddTask("Going to warp", autoStart: false).IsIndeterminate();

                // Wait for all tasks (except the indeterminate one) to complete
                while (!ctx.IsFinished)
                {
                    // Increment progress
                    foreach (var (task, increment) in tasks)
                    {
                        task.Increment(random.NextDouble() * increment);
                    }

                    // Simulate some delay
                    Thread.Sleep(100);
                }

                // Now start the "warp" task
                warpTask.StartTask();
                warpTask.IsIndeterminate(false);
                while (!ctx.IsFinished)
                {
                    warpTask.Increment(12 * random.NextDouble());

                    // Simulate some delay
                    Thread.Sleep(100);
                }
            });
    }

    /// <summary>Creates sample progress tasks.</summary>
    /// <param name="progress">The progress context.</param>
    /// <param name="random">Random generator for delays.</param>
    /// <returns>List of tasks with their delay values.</returns>
    public List<(ProgressTask Task, int Delay)> CreateTasks(ProgressContext progress, Random random)
    {
        var tasks = new List<(ProgressTask, int)>();

        var names = new[]
        {
            "Retriculating algorithms", "Colliding splines", "Solving quarks", "Folding data structures",
            "Rerouting capacitators "
        };

        for (var i = 0; i < 5; i++)
        {
            tasks.Add((progress.AddTask(names[i]), random.Next(2, 10)));
        }

        return tasks;
    }
}