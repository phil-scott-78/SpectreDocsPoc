using Spectre.Console;
using Spectre.Docs.Examples.Showcase;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// A tutorial that builds a game loading screen step by step.
/// Teaches progress bars, multiple tasks, custom columns, and styling.
/// </summary>
public class ProgressBarsTutorial : BaseSample
{
    /// <summary>
    /// Shows a basic progress bar for loading a single game asset.
    /// </summary>
    public void LoadSingleAsset()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task = ctx.AddTask("Loading World Map");

                while (!ctx.IsFinished)
                {
                    task.Increment(1.5);
                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[green]Ready![/]");
    }

    /// <summary>
    /// Loads multiple game assets simultaneously with parallel progress bars.
    /// Uses MaxValue for different task sizes.
    /// </summary>
    public void LoadMultipleAssets()
    {
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                // World map is large (200 units) // [!code focus]
                var worldMap = ctx.AddTask("Loading World Map", maxValue: 200); // [!code focus]
                // Character data uses default (100 units)  // [!code focus]
                var character = ctx.AddTask("Loading Character Data"); // [!code focus]
                // Sound effects are small (50 units) // [!code focus]
                var sounds = ctx.AddTask("Loading Sound Effects", maxValue: 50); // [!code focus]

                var random = new Random(42); // [!code focus]
                while (!ctx.IsFinished)
                {
                    worldMap.Increment(random.NextDouble() * 3); // [!code focus]
                    character.Increment(random.NextDouble() * 1.5); // [!code focus]
                    sounds.Increment(random.NextDouble() * 1.5); // [!code focus]
                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[green]All assets loaded![/]");
    }

    /// <summary>
    /// Adds spinner and elapsed time columns to the loading display.
    /// </summary>
    public void CustomizeLoadingDisplay()
    {
        AnsiConsole.Progress()
            .Columns(  // [!code focus]
                new SpinnerColumn(),  // [!code focus]
                new TaskDescriptionColumn(),  // [!code focus]
                new ProgressBarColumn(),  // [!code focus]
                new PercentageColumn(),  // [!code focus]
                new ElapsedTimeColumn()) // [!code focus]
            .Start(ctx =>
            {
                var worldMap = ctx.AddTask("Loading World Map", maxValue: 200);
                var character = ctx.AddTask("Loading Character Data");
                var sounds = ctx.AddTask("Loading Sound Effects", maxValue: 50);

                var random = new Random(42);
                while (!ctx.IsFinished)
                {
                    worldMap.Increment(random.NextDouble() * 3);
                    character.Increment(random.NextDouble() * 1.5);
                    sounds.Increment(random.NextDouble() * 1.5);
                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[green]All assets loaded![/]");
    }

    /// <summary>
    /// Applies custom colors to create a game-themed loading screen.
    /// </summary>
    public void StyleTheLoadingScreen()
    {
        AnsiConsole.Progress()
            .Columns(
                new SpinnerColumn(),
                new TaskDescriptionColumn(),
                new ProgressBarColumn  // [!code focus]
                {  // [!code focus]
                    CompletedStyle = new Style(Color.Green),  // [!code focus]
                    FinishedStyle = new Style(Color.Lime),  // [!code focus]
                    RemainingStyle = new Style(Color.Grey)  // [!code focus]
                },  // [!code focus]
                new PercentageColumn(),
                new ElapsedTimeColumn())
            .Start(ctx =>
            {
                var worldMap = ctx.AddTask("Loading World Map", maxValue: 200);
                var character = ctx.AddTask("Loading Character Data");
                var sounds = ctx.AddTask("Loading Sound Effects", maxValue: 50);

                var random = new Random(42);
                while (!ctx.IsFinished)
                {
                    worldMap.Increment(random.NextDouble() * 3);
                    character.Increment(random.NextDouble() * 1.5);
                    sounds.Increment(random.NextDouble() * 1.5);
                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[lime]All assets loaded![/]");
    }

    /// <summary>
    /// Shows how to start tasks programmatically based on conditions.
    /// Textures wait for world map to reach 25% before starting.
    /// </summary>
    public void StartTaskProgrammatically()
    {
        AnsiConsole.Progress()
            .Columns(
                new SpinnerColumn(),
                new TaskDescriptionColumn(),
                new ProgressBarColumn
                {
                    CompletedStyle = new Style(Color.Green),
                    FinishedStyle = new Style(Color.Lime),
                    RemainingStyle = new Style(Color.Grey)
                },
                new PercentageColumn(),
                new ElapsedTimeColumn())
            .Start(ctx =>
            {
                var worldMap = ctx.AddTask("Loading World Map", maxValue: 200);
                var character = ctx.AddTask("Loading Character Data");
                var sounds = ctx.AddTask("Loading Sound Effects", maxValue: 50);
                // Textures won't start until we call StartTask() // [!code focus]
                var textures = ctx.AddTask("Loading Textures", autoStart: false); // [!code focus]

                var random = new Random(42);
                while (!ctx.IsFinished)
                {
                    worldMap.Increment(random.NextDouble() * 3);
                    character.Increment(random.NextDouble() * 1.5);
                    sounds.Increment(random.NextDouble() * 1.5);

                    // Start textures once world map reaches 25%  // [!code focus]
                    if (worldMap.Percentage >= 25 && !textures.IsStarted) // [!code focus]
                    {  // [!code focus]
                        textures.StartTask(); // [!code focus]
                    } // [!code focus]

                    if (textures.IsStarted) // [!code focus]
                    { // [!code focus]
                        textures.Increment(random.NextDouble() * 2); // [!code focus]
                    } // [!code focus]

                    Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[lime]All assets loaded![/]");
    }

    /// <summary>
    /// Demonstrates the complete game loading screen with all features combined.
    /// </summary>
    public override void Run(IAnsiConsole console)
    {
        AnsiConsole.MarkupLine("[bold yellow]LOADING GAME[/]");
        AnsiConsole.WriteLine();

        AnsiConsole.Progress()
            .Columns(
                new SpinnerColumn(),
                new TaskDescriptionColumn(),
                new ProgressBarColumn
                {
                    CompletedStyle = new Style(Color.Green),
                    FinishedStyle = new Style(Color.Lime),
                    RemainingStyle = new Style(Color.Grey)
                },
                new PercentageColumn(),
                new ElapsedTimeColumn())
            .Start(ctx =>
            {
                var worldMap = ctx.AddTask("Loading World Map", maxValue: 200);
                var character = ctx.AddTask("Loading Character Data");
                var sounds = ctx.AddTask("Loading Sound Effects", maxValue: 50);
                // Textures won't start until we call StartTask()
                var textures = ctx.AddTask("Loading Textures", autoStart: false);

                var random = new Random(42);
                while (!ctx.IsFinished)
                {
                    worldMap.Increment(random.NextDouble() * 3);
                    character.Increment(random.NextDouble() * 1.5);
                    sounds.Increment(random.NextDouble() * 1.5);

                    // Start textures once world map reaches 25%
                    if (worldMap.Percentage >= 25 && !textures.IsStarted)
                    {
                        textures.StartTask();
                    }

                    if (textures.IsStarted)
                    {
                        textures.Increment(random.NextDouble() * 2);
                    }

                    Thread.Sleep(50);
                }
            });

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold lime]PRESS ANY KEY TO START[/]");
    }
}
