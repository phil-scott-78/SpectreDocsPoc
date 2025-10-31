using System.Globalization;
using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class BreakdownChartExamples
{
    /// <summary>
    /// Demonstrates creating a basic breakdown chart with labeled data items.
    /// </summary>
    public static void BasicBreakdownChartExample()
    {
        var chart = new BreakdownChart()
            .AddItem("C#", 78, Color.Green)
            .AddItem("Python", 45, Color.Blue)
            .AddItem("JavaScript", 32, Color.Yellow)
            .AddItem("Rust", 15, Color.Red);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates using various colors to distinguish chart segments.
    /// </summary>
    public static void BreakdownChartColorsExample()
    {
        var chart = new BreakdownChart()
            .AddItem("Storage Used", 256, Color.Red)
            .AddItem("System Reserved", 64, Color.Orange1)
            .AddItem("Available", 192, Color.Green);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates controlling the visibility of tags and tag values.
    /// </summary>
    public static void BreakdownChartTagsExample()
    {
        AnsiConsole.MarkupLine("[yellow]With tags and values (default):[/]");
        var withTags = new BreakdownChart()
            .AddItem("Downloads", 1250, Color.Blue)
            .AddItem("Uploads", 340, Color.Green);
        AnsiConsole.Write(withTags);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]With tags, without values:[/]");
        var tagsOnly = new BreakdownChart()
            .HideTagValues()
            .AddItem("Downloads", 1250, Color.Blue)
            .AddItem("Uploads", 340, Color.Green);
        AnsiConsole.Write(tagsOnly);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Without tags:[/]");
        var noTags = new BreakdownChart()
            .HideTags()
            .AddItem("Downloads", 1250, Color.Blue)
            .AddItem("Uploads", 340, Color.Green);
        AnsiConsole.Write(noTags);
    }

    /// <summary>
    /// Demonstrates custom value formatting with a formatter function.
    /// </summary>
    public static void BreakdownChartFormattingExample()
    {
        var chart = new BreakdownChart()
            .UseValueFormatter((value, culture) => $"{value:N0} GB")
            .AddItem("Documents", 45, Color.Blue)
            .AddItem("Photos", 120, Color.Green)
            .AddItem("Videos", 280, Color.Purple);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates displaying values as percentages.
    /// </summary>
    public static void BreakdownChartPercentageExample()
    {
        var chart = new BreakdownChart()
            .ShowPercentage()
            .AddItem("Completed", 73, Color.Green)
            .AddItem("In Progress", 18, Color.Yellow)
            .AddItem("Not Started", 9, Color.Grey);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates compact versus full-size layout modes.
    /// </summary>
    public static void BreakdownChartCompactExample()
    {
        AnsiConsole.MarkupLine("[yellow]Compact mode (default):[/]");
        var compact = new BreakdownChart()
            .Compact()
            .AddItem("Used", 75, Color.Red)
            .AddItem("Free", 25, Color.Green);
        AnsiConsole.Write(compact);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Full-size mode:[/]");
        var fullSize = new BreakdownChart()
            .FullSize()
            .AddItem("Used", 75, Color.Red)
            .AddItem("Free", 25, Color.Green);
        AnsiConsole.Write(fullSize);
    }

    /// <summary>
    /// Demonstrates setting a fixed width for the chart.
    /// </summary>
    public static void BreakdownChartWidthExample()
    {
        var chart = new BreakdownChart()
            .Width(40)
            .AddItem("Category A", 60, Color.Blue)
            .AddItem("Category B", 40, Color.Green);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates customizing the value text color.
    /// </summary>
    public static void BreakdownChartValueColorExample()
    {
        var chart = new BreakdownChart()
            .WithValueColor(Color.Aqua)
            .AddItem("Revenue", 850000, Color.Green)
            .AddItem("Expenses", 620000, Color.Red)
            .AddItem("Profit", 230000, Color.Blue);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates adding multiple items from a collection.
    /// </summary>
    public static void BreakdownChartAddItemsExample()
    {
        var data = new[]
        {
            new BreakdownChartItem("Q1", 125, Color.Blue),
            new BreakdownChartItem("Q2", 180, Color.Green),
            new BreakdownChartItem("Q3", 165, Color.Yellow),
            new BreakdownChartItem("Q4", 210, Color.Red),
        };

        var chart = new BreakdownChart()
            .AddItems(data);

        AnsiConsole.Write(chart);
    }
}
