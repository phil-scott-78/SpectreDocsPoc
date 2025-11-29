using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class RenderingAsciiArtHowTo
{
    /// <summary>
    /// Render large ASCII text with FigletText.
    /// </summary>
    public static void RenderFigletText()
    {
        AnsiConsole.Write(
            new FigletText("Hello")
                .Color(Color.Green));
    }

    /// <summary>
    /// Center the figlet text.
    /// </summary>
    public static void CenterFigletText()
    {
        AnsiConsole.Write(
            new FigletText("Welcome")
                .Centered()
                .Color(Color.Cyan1));
    }

    /// <summary>
    /// Add emoji to your output.
    /// </summary>
    public static void AddEmoji()
    {
        AnsiConsole.MarkupLine(":rocket: Launching application...");
        AnsiConsole.MarkupLine(":check_mark: Configuration loaded");
        AnsiConsole.MarkupLine(":star: Ready to go!");
    }

    /// <summary>
    /// Create a styled banner with a rule.
    /// </summary>
    public static void CreateBanner()
    {
        var title = new FigletText("MyApp")
            .Centered()
            .Color(Color.Yellow);

        AnsiConsole.Write(title);
        AnsiConsole.Write(new Rule("[grey]v1.0.0[/]").Centered());
    }
}
