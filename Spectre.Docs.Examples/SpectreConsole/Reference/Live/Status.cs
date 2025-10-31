using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Live;

internal static class StatusExamples
{
    /// <summary>
    /// Demonstrates creating a basic status display with default settings.
    /// </summary>
    public static void BasicStatusExample()
    {
        AnsiConsole.Status()
            .Start("Processing data...", ctx =>
            {
                // Simulate work
                Thread.Sleep(3000);

                AnsiConsole.MarkupLine("[green]Processing complete![/]");
            });
    }

    /// <summary>
    /// Demonstrates choosing different spinner animations.
    /// </summary>
    public static void StatusSpinnerExample()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .Start("Loading resources...", ctx =>
            {
                Thread.Sleep(2000);
                AnsiConsole.MarkupLine("[green]Resources loaded![/]");
            });
    }

    /// <summary>
    /// Demonstrates applying style and color to the spinner.
    /// </summary>
    public static void StatusSpinnerStyleExample()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("bold yellow"))
            .Start("Connecting to server...", ctx =>
            {
                Thread.Sleep(2500);
                AnsiConsole.MarkupLine("[green]Connected successfully![/]");
            });
    }

    /// <summary>
    /// Demonstrates using StartAsync for asynchronous operations.
    /// </summary>
    public static async Task StatusAsyncExample()
    {
        await AnsiConsole.Status()
            .StartAsync("Downloading files...", async ctx =>
            {
                // Simulate async work
                await Task.Delay(3000);

                AnsiConsole.MarkupLine("[green]Download complete![/]");
            });
    }

    /// <summary>
    /// Demonstrates returning a value from a status operation.
    /// </summary>
    public static void StatusWithReturnValueExample()
    {
        var result = AnsiConsole.Status()
            .Start("Calculating results...", ctx =>
            {
                // Simulate calculation
                Thread.Sleep(2000);

                return 42;
            });

        AnsiConsole.MarkupLine($"[green]Result: {result}[/]");
    }

    /// <summary>
    /// Demonstrates dynamically updating the status text during execution.
    /// </summary>
    public static void StatusDynamicUpdateExample()
    {
        AnsiConsole.Status()
            .Start("Starting process...", ctx =>
            {
                ctx.Status("Loading configuration...");
                Thread.Sleep(1000);

                ctx.Status("Connecting to database...");
                Thread.Sleep(1500);

                ctx.Status("Fetching data...");
                Thread.Sleep(1000);

                ctx.Status("Processing records...");
                Thread.Sleep(1500);

                AnsiConsole.MarkupLine("[green]All tasks completed![/]");
            });
    }

    /// <summary>
    /// Demonstrates changing the spinner animation at runtime.
    /// </summary>
    public static void StatusSpinnerChangeExample()
    {
        AnsiConsole.Status()
            .Start("Initializing...", ctx =>
            {
                ctx.Status("Starting services...");
                ctx.Spinner(Spinner.Known.Dots);
                Thread.Sleep(1500);

                ctx.Status("Loading modules...");
                ctx.Spinner(Spinner.Known.Line);
                Thread.Sleep(1500);

                ctx.Status("Running diagnostics...");
                ctx.Spinner(Spinner.Known.BouncingBar);
                Thread.Sleep(1500);

                ctx.Status("Finalizing...");
                ctx.Spinner(Spinner.Known.Star);
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine("[green]System ready![/]");
            });
    }

    /// <summary>
    /// Demonstrates manual refresh control with AutoRefresh disabled.
    /// </summary>
    public static void StatusManualRefreshExample()
    {
        AnsiConsole.Status()
            .AutoRefresh(false)
            .Start("Processing batch...", ctx =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    ctx.Status($"Processing item {i} of 5...");
                    ctx.Refresh();

                    Thread.Sleep(800);

                    AnsiConsole.MarkupLine($"[grey]Item {i} processed[/]");
                }

                AnsiConsole.MarkupLine("[green]Batch complete![/]");
            });
    }

    /// <summary>
    /// Demonstrates using markup syntax in status text for formatting.
    /// </summary>
    public static void StatusWithMarkupExample()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("[yellow]Initializing[/]...", ctx =>
            {
                Thread.Sleep(1000);

                ctx.Status("[blue]Connecting to [bold]API server[/][/]...");
                Thread.Sleep(1500);

                ctx.Status("[cyan]Authenticating user[/]...");
                Thread.Sleep(1000);

                ctx.Status("[green]Loading [italic]user preferences[/][/]...");
                Thread.Sleep(1500);

                AnsiConsole.MarkupLine("[bold green]Startup complete![/]");
            });
    }
}
