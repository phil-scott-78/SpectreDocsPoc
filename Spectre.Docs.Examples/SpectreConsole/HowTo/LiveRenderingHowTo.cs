using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class LiveRenderingHowTo
{
    /// <summary>
    /// Update a table in-place without scrolling.
    /// </summary>
    public static void UpdateInPlace()
    {
        var table = new Table().AddColumn("Metric").AddColumn("Value");

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    table.AddRow($"Item {i}", $"{i * 10}%");
                    ctx.Refresh();
                    Thread.Sleep(500);
                }
            });
    }

    /// <summary>
    /// Replace the entire display with new content.
    /// </summary>
    public static void ReplaceContent()
    {
        AnsiConsole.Live(new Panel("Starting..."))
            .Start(ctx =>
            {
                Thread.Sleep(1000);
                ctx.UpdateTarget(new Panel("[yellow]Processing...[/]"));
                Thread.Sleep(1000);
                ctx.UpdateTarget(new Panel("[green]Complete![/]"));
                Thread.Sleep(500);
            });
    }

    /// <summary>
    /// Clear the display after completion.
    /// </summary>
    public static void AutoClearOnComplete()
    {
        var table = new Table().AddColumn("Status");

        AnsiConsole.Live(table)
            .AutoClear(true)
            .Start(ctx =>
            {
                table.AddRow("Working...");
                ctx.Refresh();
                Thread.Sleep(2000);
            });

        AnsiConsole.MarkupLine("[green]Done![/]");
    }

    /// <summary>
    /// Use async/await with live display.
    /// </summary>
    public static async Task UseAsync()
    {
        var table = new Table().AddColumn("Task").AddColumn("Status");

        await AnsiConsole.Live(table)
            .StartAsync(async ctx =>
            {
                table.AddRow("Fetching data", "[yellow]...[/]");
                ctx.Refresh();
                await Task.Delay(1000);

                table.Rows.Update(0, 1, new Markup("[green]Done[/]"));
                ctx.Refresh();
            });
    }
}
