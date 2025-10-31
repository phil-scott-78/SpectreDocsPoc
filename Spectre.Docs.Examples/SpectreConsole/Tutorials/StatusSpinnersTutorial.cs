using Spectre.Console;
using Spectre.Docs.Examples.Showcase;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// A tutorial that builds a coffee brewing simulation step by step.
/// Teaches status displays, dynamic text updates, and spinner customization.
/// </summary>
public class StatusSpinnersTutorial : BaseSample
{
    /// <summary>
    /// Shows a basic spinner while simulating work.
    /// </summary>
    public void ShowBasicSpinner()
    {
        AnsiConsole.Status()
            .Start("Grinding beans...", ctx =>
            {
                // Simulate grinding
                Thread.Sleep(3000);
            });

        AnsiConsole.MarkupLine("[green]Done![/]");
    }

    /// <summary>
    /// Updates the status text as work progresses through different stages.
    /// </summary>
    public void UpdateStatusText()
    {
        AnsiConsole.Status()
            .Start("Grinding beans...", ctx =>
            {
                Thread.Sleep(1500);

                ctx.Status("Brewing coffee...");
                Thread.Sleep(2000);

                ctx.Status("Pouring into cup...");
                Thread.Sleep(1000);
            });

        AnsiConsole.MarkupLine("[green]Coffee is ready![/]");
    }

    /// <summary>
    /// Demonstrates different spinner styles for various moods.
    /// </summary>
    public void TryDifferentSpinners()
    {
        // Calm and steady
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Grinding beans...", ctx =>
            {
                Thread.Sleep(2000);
            });

        // Energetic
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .SpinnerStyle(Style.Parse("yellow"))
            .Start("Brewing coffee...", ctx =>
            {
                Thread.Sleep(2000);
            });

        AnsiConsole.MarkupLine("[green]Done![/]");
    }

    /// <summary>
    /// Demonstrates the complete coffee brewing flow with styled output.
    /// </summary>
    public override void Run(IAnsiConsole AnsiConsole)
    {
        AnsiConsole.MarkupLine("[yellow bold]Time for coffee![/]");
        AnsiConsole.WriteLine();

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("yellow"))
            .Start("Grinding beans...", ctx =>
            {
                Thread.Sleep(2000);

                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("blue"));
                ctx.Status("Brewing coffee...");
                Thread.Sleep(2500);

                ctx.Spinner(Spinner.Known.Arc);
                ctx.SpinnerStyle(Style.Parse("green"));
                ctx.Status("Pouring into cup...");
                Thread.Sleep(1500);
            });

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]Your coffee is ready! Enjoy![/]");
    }
}
