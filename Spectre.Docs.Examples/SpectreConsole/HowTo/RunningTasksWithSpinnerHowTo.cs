using Spectre.Console;
using Spectre.Console.Extensions;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class RunningTasksWithSpinnerHowTo
{
    /// <summary>
    /// Run an async task with a spinner.
    /// </summary>
    public static async Task ShowSpinnerWhileWaiting()
    {
        await Task.Delay(2000).Spinner(Spinner.Known.Dots);
    }

    /// <summary>
    /// Use a different spinner animation.
    /// </summary>
    public static async Task ChangeSpin­nerAnimation()
    {
        await Task.Delay(2000).Spinner(Spinner.Known.BouncingBar);
    }

    /// <summary>
    /// Get a result from an async task with a spinner.
    /// </summary>
    public static async Task GetResultWithSpinner()
    {
        var result = await FetchDataAsync().Spinner(Spinner.Known.Star);
        AnsiConsole.MarkupLine($"[green]Got result: {result}[/]");
    }

    private static async Task<string> FetchDataAsync()
    {
        await Task.Delay(2000);
        return "data loaded";
    }
}
