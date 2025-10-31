using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class FigletTextExamples
{
    /// <summary>
    /// Demonstrates creating basic figlet text with the default font.
    /// </summary>
    public static void BasicFigletTextExample()
    {
        var figlet = new FigletText("Hello, World!");

        AnsiConsole.Write(figlet);
    }

    /// <summary>
    /// Demonstrates applying colors to figlet text.
    /// </summary>
    public static void FigletTextColorExample()
    {
        var figlet = new FigletText("Spectre")
            .Color(Color.Blue);

        AnsiConsole.Write(figlet);
    }

    /// <summary>
    /// Demonstrates using centered alignment for figlet text.
    /// </summary>
    public static void FigletTextCenterAlignmentExample()
    {
        var figlet = new FigletText("CENTERED")
        {
            Justification = Justify.Center
        };

        AnsiConsole.Write(figlet);
    }

    /// <summary>
    /// Demonstrates different alignment options for figlet text.
    /// </summary>
    public static void FigletTextAlignmentExample()
    {
        AnsiConsole.MarkupLine("[yellow]Left alignment (default):[/]");
        var leftAligned = new FigletText("LEFT")
        {
            Justification = Justify.Left
        };
        AnsiConsole.Write(leftAligned);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Center alignment:[/]");
        var centerAligned = new FigletText("CENTER")
        {
            Justification = Justify.Center
        };
        AnsiConsole.Write(centerAligned);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Right alignment:[/]");
        var rightAligned = new FigletText("RIGHT")
        {
            Justification = Justify.Right
        };
        AnsiConsole.Write(rightAligned);
    }

    /// <summary>
    /// Demonstrates loading a custom FigletFont from a file.
    /// </summary>
    public static void FigletTextCustomFontExample()
    {
        var font = FigletFont.Load("path/to/custom.flf");
        var figlet = new FigletText(font, "Custom Font");

        AnsiConsole.Write(figlet);
    }

    /// <summary>
    /// Demonstrates combining colors and alignment for banners.
    /// </summary>
    public static void FigletTextBannerExample()
    {
        var banner = new FigletText("RELEASE v2.0")
        {
            Color = Color.Green,
            Justification = Justify.Center
        };

        AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("green dim")));
        AnsiConsole.Write(banner);
        AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("green dim")));
    }

    /// <summary>
    /// Demonstrates using figlet text within a panel.
    /// </summary>
    public static void FigletTextInPanelExample()
    {
        var figlet = new FigletText("SUCCESS")
        {
            Color = Color.Green,
            Justification = Justify.Center
        };

        var panel = new Panel(figlet)
        {
            Border = BoxBorder.Double,
            BorderStyle = new Style(Color.Green),
            Padding = new Padding(1, 1, 1, 1)
        };

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates creating multi-line welcome messages with figlet text.
    /// </summary>
    public static void FigletTextWelcomeExample()
    {
        var appName = new FigletText("MyApp")
        {
            Color = Color.Blue,
            Justification = Justify.Center
        };

        var version = new Text("Version 1.0.0", new Style(Color.Grey))
        {
            Justification = Justify.Center
        };

        AnsiConsole.Write(appName);
        AnsiConsole.Write(version);
        AnsiConsole.WriteLine();
    }
}
