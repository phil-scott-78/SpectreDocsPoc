using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class DrawingChartsHowTo
{
    /// <summary>
    /// Create a bar chart to compare values.
    /// </summary>
    public static void CreateBarChart()
    {
        AnsiConsole.Write(new BarChart()
            .Label("[green]Sales by Region[/]")
            .AddItem("North", 85, Color.Blue)
            .AddItem("South", 62, Color.Yellow)
            .AddItem("West", 94, Color.Green));
    }

    /// <summary>
    /// Create a breakdown chart for proportions.
    /// </summary>
    public static void CreateBreakdownChart()
    {
        AnsiConsole.Write(new BreakdownChart()
            .AddItem("C#", 65, Color.Green)
            .AddItem("TypeScript", 25, Color.Blue)
            .AddItem("Python", 10, Color.Yellow));
    }

    /// <summary>
    /// Display a calendar with highlighted dates.
    /// </summary>
    public static void ShowCalendar()
    {
        var calendar = new Calendar(2025, 1)
            .AddCalendarEvent(2025, 1, 15)
            .AddCalendarEvent(2025, 1, 20)
            .HighlightStyle(Style.Parse("yellow bold"));

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Add a visual separator with a rule.
    /// </summary>
    public static void AddRule()
    {
        AnsiConsole.Write(new Rule("[yellow]Results[/]"));
    }
}
