using Spectre.Console;
using Spectre.Console.Extensions;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class AlignExamples
{
    /// <summary>
    /// Demonstrates creating a basic center-aligned panel.
    /// </summary>
    public static void BasicAlignExample()
    {
        var panel = new Panel("Welcome to Spectre.Console!")
            .BorderColor(Color.Blue);

        var centered = Align.Center(panel);

        AnsiConsole.Write(centered);
    }

    /// <summary>
    /// Demonstrates horizontal alignment options (left, center, right).
    /// </summary>
    public static void HorizontalAlignmentExample()
    {
        var text = new Text("Left Aligned");
        AnsiConsole.Write(Align.Left(text));
        AnsiConsole.WriteLine();

        text = new Text("Center Aligned");
        AnsiConsole.Write(Align.Center(text));
        AnsiConsole.WriteLine();

        text = new Text("Right Aligned");
        AnsiConsole.Write(Align.Right(text));
    }

    /// <summary>
    /// Demonstrates vertical alignment with height specification.
    /// </summary>
    public static void VerticalAlignmentExample()
    {
        var text = new Text("Top Aligned").Centered();
        var aligned = Align.Center(text, VerticalAlignment.Top)
            .Height(10);

        AnsiConsole.Write(aligned);
        AnsiConsole.MarkupLine("[grey]---[/]");

        text = new Text("Middle Aligned").Centered();
        aligned = Align.Center(text, VerticalAlignment.Middle)
            .Height(10);

        AnsiConsole.Write(aligned);
        AnsiConsole.MarkupLine("[grey]---[/]");

        text = new Text("Bottom Aligned").Centered();
        aligned = Align.Center(text, VerticalAlignment.Bottom)
            .Height(10);

        AnsiConsole.Write(aligned);
    }

    /// <summary>
    /// Demonstrates combining horizontal and vertical alignment.
    /// </summary>
    public static void CombinedAlignmentExample()
    {
        var content = new Panel("Centered in Both Directions")
            .BorderColor(Color.Green)
            .RoundedBorder();

        var aligned = Align.Center(content, VerticalAlignment.Middle)
            .Height(15);

        AnsiConsole.Write(aligned);
    }

    /// <summary>
    /// Demonstrates using fluent extension methods for alignment.
    /// </summary>
    public static void FluentExtensionsExample()
    {
        var panel = new Panel("Using Fluent Extensions")
            .BorderColor(Color.Yellow);

        var aligned = Align.Left(panel)
            .MiddleAligned()
            .Height(8);

        AnsiConsole.Write(aligned);
    }

    /// <summary>
    /// Demonstrates controlling width behavior for alignment.
    /// </summary>
    public static void WidthControlExample()
    {
        var text = new Markup("[bold blue]Auto Width[/]");
        var autoWidth = Align.Center(text);

        AnsiConsole.Write(autoWidth);
        AnsiConsole.WriteLine();

        text = new Markup("[bold green]Fixed Width (60)[/]");
        var fixedWidth = Align.Center(text)
            .Width(60);

        AnsiConsole.Write(fixedWidth);
    }

    /// <summary>
    /// Demonstrates aligning multiple panels in a layout.
    /// </summary>
    public static void MultipleAlignmentsExample()
    {
        var leftPanel = new Panel("Left Panel")
            .BorderColor(Color.Red);
        AnsiConsole.Write(Align.Left(leftPanel));
        AnsiConsole.WriteLine();

        var centerPanel = new Panel("Center Panel")
            .BorderColor(Color.Green);
        AnsiConsole.Write(Align.Center(centerPanel));
        AnsiConsole.WriteLine();

        var rightPanel = new Panel("Right Panel")
            .BorderColor(Color.Blue);
        AnsiConsole.Write(Align.Right(rightPanel));
    }

    /// <summary>
    /// Demonstrates centering a table for presentation.
    /// </summary>
    public static void AlignTableExample()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Purple)
            .AddColumn("Product")
            .AddColumn("Price");

        table.AddRow("Coffee", "$3.50");
        table.AddRow("Tea", "$2.50");
        table.AddRow("Pastry", "$4.00");

        var centered = Align.Center(table);

        AnsiConsole.Write(centered);
    }

    /// <summary>
    /// Demonstrates using Align with nested renderables.
    /// </summary>
    public static void NestedAlignExample()
    {
        var innerText = new Markup("[bold yellow]Inner Content[/]");
        var innerPanel = new Panel(innerText)
            .BorderColor(Color.Yellow)
            .RoundedBorder();

        var outerPanel = new Panel(Align.Center(innerPanel))
            .Header("[bold blue]Outer Container[/]")
            .BorderColor(Color.Blue)
            .Expand();

        AnsiConsole.Write(outerPanel);
    }

    /// <summary>
    /// Demonstrates creating a title screen with centered content.
    /// </summary>
    public static void TitleScreenExample()
    {
        var title = new FigletText("Spectre")
            .Color(Color.Blue);

        var subtitle = new Markup("[italic grey]A modern .NET console library[/]");

        var titleAligned = Align.Center(title);
        var subtitleAligned = Align.Center(subtitle);

        AnsiConsole.Write(titleAligned);
        AnsiConsole.Write(subtitleAligned);
        AnsiConsole.WriteLine();

        var instructions = new Markup("[dim]Press any key to continue...[/]");
        AnsiConsole.Write(Align.Center(instructions));
    }
}
