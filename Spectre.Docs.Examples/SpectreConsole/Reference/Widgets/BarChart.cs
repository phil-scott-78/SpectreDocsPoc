using System.Globalization;
using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class BarChartExamples
{
    /// <summary>
    /// Demonstrates creating a basic bar chart with labeled values.
    /// </summary>
    public static void BasicBarChartExample()
    {
        var chart = new BarChart()
            .AddItem("Apple", 12, Color.Green)
            .AddItem("Orange", 8, Color.Orange1)
            .AddItem("Banana", 5, Color.Yellow);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates adding a label to the bar chart.
    /// </summary>
    public static void BarChartLabelExample()
    {
        var chart = new BarChart()
            .Label("[bold underline]Fruit Sales[/]")
            .AddItem("Apple", 12, Color.Green)
            .AddItem("Orange", 8, Color.Orange1)
            .AddItem("Banana", 5, Color.Yellow);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates different label alignment options.
    /// </summary>
    public static void BarChartLabelAlignmentExample()
    {
        AnsiConsole.MarkupLine("[yellow]Left aligned:[/]");
        var left = new BarChart()
            .Label("Sales Data")
            .LeftAlignLabel()
            .AddItem("Q1", 100, Color.Blue)
            .AddItem("Q2", 150, Color.Green);
        AnsiConsole.Write(left);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Center aligned (default):[/]");
        var center = new BarChart()
            .Label("Sales Data")
            .CenterLabel()
            .AddItem("Q1", 100, Color.Blue)
            .AddItem("Q2", 150, Color.Green);
        AnsiConsole.Write(center);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Right aligned:[/]");
        var right = new BarChart()
            .Label("Sales Data")
            .RightAlignLabel()
            .AddItem("Q1", 100, Color.Blue)
            .AddItem("Q2", 150, Color.Green);
        AnsiConsole.Write(right);
    }

    /// <summary>
    /// Demonstrates showing and hiding bar values.
    /// </summary>
    public static void BarChartValuesExample()
    {
        AnsiConsole.MarkupLine("[yellow]With values (default):[/]");
        var withValues = new BarChart()
            .AddItem("Downloads", 1250, Color.Blue)
            .AddItem("Uploads", 340, Color.Green);
        AnsiConsole.Write(withValues);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Without values:[/]");
        var noValues = new BarChart()
            .HideValues()
            .AddItem("Downloads", 1250, Color.Blue)
            .AddItem("Uploads", 340, Color.Green);
        AnsiConsole.Write(noValues);
    }

    /// <summary>
    /// Demonstrates custom value formatting.
    /// </summary>
    public static void BarChartFormattingExample()
    {
        var chart = new BarChart()
            .Label("[bold]Revenue by Region[/]")
            .UseValueFormatter((value, culture) => value.ToString("C0", culture))
            .AddItem("North", 125000, Color.Blue)
            .AddItem("South", 98000, Color.Green)
            .AddItem("East", 145000, Color.Yellow)
            .AddItem("West", 112000, Color.Red);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates setting a fixed maximum value for consistent scaling.
    /// </summary>
    public static void BarChartMaxValueExample()
    {
        var chart = new BarChart()
            .Label("[bold]Progress to Goal (100)[/]")
            .WithMaxValue(100)
            .AddItem("Team A", 85, Color.Green)
            .AddItem("Team B", 62, Color.Yellow)
            .AddItem("Team C", 45, Color.Red);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates setting a fixed width for the chart.
    /// </summary>
    public static void BarChartWidthExample()
    {
        var chart = new BarChart()
            .Width(50)
            .AddItem("Category A", 75, Color.Blue)
            .AddItem("Category B", 50, Color.Green)
            .AddItem("Category C", 25, Color.Yellow);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates adding multiple items from a collection.
    /// </summary>
    public static void BarChartAddItemsExample()
    {
        var data = new[]
        {
            new BarChartItem("January", 45, Color.Blue),
            new BarChartItem("February", 62, Color.Blue),
            new BarChartItem("March", 58, Color.Blue),
            new BarChartItem("April", 71, Color.Green),
        };

        var chart = new BarChart()
            .Label("[bold]Monthly Performance[/]")
            .AddItems(data);

        AnsiConsole.Write(chart);
    }

    /// <summary>
    /// Demonstrates using colors to indicate positive and negative values.
    /// </summary>
    public static void BarChartColorsExample()
    {
        var chart = new BarChart()
            .Label("[bold]Quarterly Growth[/]")
            .AddItem("Q1", 15, Color.Green)
            .AddItem("Q2", 8, Color.Green)
            .AddItem("Q3", 3, Color.Yellow)
            .AddItem("Q4", 22, Color.Green);

        AnsiConsole.Write(chart);
    }
}
