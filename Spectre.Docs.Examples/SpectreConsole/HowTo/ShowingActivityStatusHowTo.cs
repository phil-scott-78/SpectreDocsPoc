using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class ShowingActivityStatusHowTo
{
    /// <summary>
    /// Show a spinner while work is in progress.
    /// </summary>
    public static void ShowSpinner()
    {
        AnsiConsole.Status()
            .Start("Connecting to server...", ctx =>
            {
                Thread.Sleep(2000);
            });
    }

    /// <summary>
    /// Update the status message as work progresses.
    /// </summary>
    public static void UpdateStatusMessage()
    {
        AnsiConsole.Status()
            .Start("Initializing...", ctx =>
            {
                Thread.Sleep(1000);
                ctx.Status("Loading configuration...");
                Thread.Sleep(1000);
                ctx.Status("Starting services...");
                Thread.Sleep(1000);
            });
    }

    /// <summary>
    /// Choose a different spinner animation.
    /// </summary>
    public static void ChangeSpinnerStyle()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green"))
            .Start("Processing...", ctx =>
            {
                Thread.Sleep(2000);
            });
    }

    /// <summary>
    /// Use async/await with the status spinner.
    /// </summary>
    public static async Task UseAsync()
    {
        await AnsiConsole.Status()
            .StartAsync("Fetching data...", async ctx =>
            {
                await Task.Delay(2000);
            });
    }
}
