using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class CalendarSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        Panel[] outputs =
        [
            new Panel(new Calendar(2024, 3))
                .Header("[yellow]Default calendar[/]"),

            new Panel(new Calendar(2024, 6)
                    .Border(TableBorder.Rounded)
                    .BorderColor(Color.Blue))
                .Header("[yellow]With border[/]"),

            new Panel(new Calendar(2024, 9)
                    .HeaderStyle(Style.Parse("blue bold")))
                .Header("[yellow]Styled header[/]"),

            new Panel(CreateCalendarWithEvents())
                .Header("[yellow]With events[/]"),

            new Panel(new Calendar(2024, 1).Culture("sv-SE"))
                .Header("[yellow]Different culture[/]"),

            new Panel(new Calendar(2024, 4)
                    .HideHeader()
                    .Border(TableBorder.Square))
                .Header("[yellow]Hide header[/]"),

            new Panel(CreateCalendarWithMultipleHighlights())
                .Header("[yellow]Multiple highlights[/]")
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                output.Expand = true;
                output.Padding = new Padding(1);
                output.Border = BoxBorder.Rounded;
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3000);
            }
        });
    }

    private static Calendar CreateCalendarWithEvents()
    {
        var cal = new Calendar(2024, 12)
            .HighlightStyle(Style.Parse("yellow bold"));
        cal.AddCalendarEvent(2024, 12, 25);
        cal.AddCalendarEvent(2024, 12, 31);
        return cal;
    }

    private static Calendar CreateCalendarWithMultipleHighlights()
    {
        var cal = new Calendar(2024, 7)
            .HighlightStyle(Style.Parse("green bold"))
            .Border(TableBorder.Rounded);
        cal.AddCalendarEvent(2024, 7, 4);
        cal.AddCalendarEvent(2024, 7, 14);
        cal.AddCalendarEvent(2024, 7, 21);
        return cal;
    }
}
