using System.Globalization;
using Spectre.Console;
using Calendar = Spectre.Console.Calendar;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class CalendarExamples
{
    /// <summary>
    /// Demonstrates creating a basic calendar for a specific month and year.
    /// </summary>
    public static void BasicCalendarExample()
    {
        var calendar = new Calendar(2025, 11);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates creating a calendar from a DateTime object.
    /// </summary>
    public static void CalendarFromDateExample()
    {
        var today = DateTime.Now;
        var calendar = new Calendar(today);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates highlighting specific dates with calendar events.
    /// </summary>
    public static void CalendarEventsExample()
    {
        var calendar = new Calendar(2025, 11)
            .AddCalendarEvent(2025, 11, 15)
            .AddCalendarEvent(2025, 11, 20)
            .AddCalendarEvent(2025, 11, 28);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates adding calendar events with DateTime objects.
    /// </summary>
    public static void CalendarEventDateTimeExample()
    {
        var calendar = new Calendar(2025, 11)
            .AddCalendarEvent(new DateTime(2025, 11, 7))
            .AddCalendarEvent(new DateTime(2025, 11, 14))
            .AddCalendarEvent(new DateTime(2025, 11, 21));

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates customizing the default highlight style for events.
    /// </summary>
    public static void CalendarHighlightStyleExample()
    {
        var calendar = new Calendar(2025, 11)
            .HighlightStyle(new Style(foreground: Color.Red, decoration: Decoration.Bold))
            .AddCalendarEvent(2025, 11, 1)
            .AddCalendarEvent(2025, 11, 15)
            .AddCalendarEvent(2025, 11, 25);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates using custom styles for individual events.
    /// </summary>
    public static void CalendarCustomEventStylesExample()
    {
        var calendar = new Calendar(2025, 11)
            .AddCalendarEvent(2025, 11, 5, new Style(Color.Green))
            .AddCalendarEvent(2025, 11, 15, new Style(Color.Yellow))
            .AddCalendarEvent(2025, 11, 25, new Style(Color.Red));

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates customizing the calendar header style.
    /// </summary>
    public static void CalendarHeaderStyleExample()
    {
        var calendar = new Calendar(2025, 11)
            .HeaderStyle(new Style(foreground: Color.Blue, decoration: Decoration.Bold))
            .AddCalendarEvent(2025, 11, 10);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates hiding the calendar header.
    /// </summary>
    public static void CalendarHideHeaderExample()
    {
        var calendar = new Calendar(2025, 11)
            .HideHeader()
            .AddCalendarEvent(2025, 11, 15);

        AnsiConsole.Write(calendar);
    }

    /// <summary>
    /// Demonstrates customizing the calendar border style.
    /// </summary>
    public static void CalendarBorderExample()
    {
        AnsiConsole.MarkupLine("[yellow]Rounded border:[/]");
        var rounded = new Calendar(2025, 11)
            .Border(TableBorder.Rounded)
            .AddCalendarEvent(2025, 11, 10);
        AnsiConsole.Write(rounded);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Double border:[/]");
        var double_ = new Calendar(2025, 11)
            .Border(TableBorder.Double)
            .AddCalendarEvent(2025, 11, 10);
        AnsiConsole.Write(double_);
    }

    /// <summary>
    /// Demonstrates using a culture to control calendar formatting and week start day.
    /// </summary>
    public static void CalendarCultureExample()
    {
        AnsiConsole.MarkupLine("[yellow]US culture (Sunday start):[/]");
        var usCulture = new Calendar(2025, 11)
        {
            Culture = new CultureInfo("en-US")
        };
        usCulture.AddCalendarEvent(2025, 11, 15);
        AnsiConsole.Write(usCulture);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]French culture (Monday start):[/]");
        var frenchCulture = new Calendar(2025, 11)
        {
            Culture = new CultureInfo("fr-FR")
        };
        frenchCulture.AddCalendarEvent(2025, 11, 15);
        AnsiConsole.Write(frenchCulture);
    }

    /// <summary>
    /// Demonstrates displaying multiple calendars side by side using columns.
    /// </summary>
    public static void MultipleCalendarsExample()
    {
        var november = new Calendar(2025, 11)
            .AddCalendarEvent(2025, 11, 15);

        var december = new Calendar(2025, 12)
            .AddCalendarEvent(2025, 12, 25);

        var january = new Calendar(2026, 1)
            .AddCalendarEvent(2026, 1, 1);

        var columns = new Columns(november, december, january);
        AnsiConsole.Write(columns);
    }
}
