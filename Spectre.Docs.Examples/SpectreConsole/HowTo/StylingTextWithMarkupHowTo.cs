using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class StylingTextWithMarkupHowTo
{
    /// <summary>
    /// Write colored text using MarkupLine with basic color tags.
    /// </summary>
    public static void WriteColoredText()
    {
        AnsiConsole.MarkupLine("[green]Build succeeded[/]");
        AnsiConsole.MarkupLine("[red]Error: File not found[/]");
        AnsiConsole.MarkupLine("[yellow]Warning: Deprecated API usage[/]");
    }

    /// <summary>
    /// Use hex or RGB colors when named colors don't fit your needs.
    /// </summary>
    public static void UseCustomColors()
    {
        // Hex color notation
        AnsiConsole.MarkupLine("[#ff6600]Orange alert[/]");
        AnsiConsole.MarkupLine("[#00ccff]Cyan info[/]");

        // RGB color notation
        AnsiConsole.MarkupLine("[rgb(128,0,255)]Purple notice[/]");

        // Custom colors work with decorations too
        AnsiConsole.MarkupLine("[bold #ff0066]Hot pink heading[/]");
    }

    /// <summary>
    /// Add text decorations like bold, italic, and underline.
    /// </summary>
    public static void AddTextDecorations()
    {
        AnsiConsole.MarkupLine("[bold]Important notice[/]");
        AnsiConsole.MarkupLine("[italic]Additional context[/]");
        AnsiConsole.MarkupLine("[underline]Click here for details[/]");
        AnsiConsole.MarkupLine("[strikethrough]No longer available[/]");
    }

    /// <summary>
    /// Combine colors and decorations in a single tag.
    /// </summary>
    public static void CombineStyles()
    {
        // Color + decoration
        AnsiConsole.MarkupLine("[bold red]Critical failure[/]");
        AnsiConsole.MarkupLine("[italic blue]Processing request...[/]");

        // Multiple decorations
        AnsiConsole.MarkupLine("[bold underline]Required field[/]");

        // Foreground + background color
        AnsiConsole.MarkupLine("[white on red] ERROR [/] Connection refused");
        AnsiConsole.MarkupLine("[black on yellow] WARN [/] High memory usage");
    }

    /// <summary>
    /// Mix styled and unstyled text in the same line.
    /// </summary>
    public static void MixStylesInline()
    {
        AnsiConsole.MarkupLine("Status: [green]Online[/]");
        AnsiConsole.MarkupLine("Found [yellow]3[/] issues in [blue]src/Program.cs[/]");
        AnsiConsole.MarkupLine("[dim][[INFO]][/] Server started on [bold]port 5000[/]");
    }

    /// <summary>
    /// Add clickable links to your console output.
    /// </summary>
    public static void AddClickableLinks()
    {
        // URL displayed as the link text
        AnsiConsole.MarkupLine("Docs: [link]https://spectreconsole.net[/]");

        // Custom display text with a URL
        AnsiConsole.MarkupLine("See the [link=https://spectreconsole.net/markup]markup reference[/] for details.");

        // Links can be styled too
        AnsiConsole.MarkupLine("[blue][link=https://github.com]GitHub[/][/]");
    }

    /// <summary>
    /// Safely interpolate dynamic content that may contain brackets.
    /// </summary>
    public static void InterpolateDynamicContent()
    {
        // MarkupLineInterpolated auto-escapes interpolated values
        var userName = "admin[test]";
        var fileName = "config[backup].json";

        AnsiConsole.MarkupLineInterpolated($"[blue]User:[/] {userName}");
        AnsiConsole.MarkupLineInterpolated($"[green]File:[/] {fileName}");

        // For non-interpolated scenarios, use Markup.Escape()
        var query = "SELECT * FROM [users]";
        AnsiConsole.MarkupLine($"[yellow]Query:[/] {Markup.Escape(query)}");
    }

    /// <summary>
    /// Complete example: styled build output with multiple message types.
    /// </summary>
    public static void RunAll()
    {
        AnsiConsole.MarkupLine("[bold]Build Output[/]");
        AnsiConsole.MarkupLine(new string('-', 40));
        AnsiConsole.WriteLine();

        // Info messages with custom colors
        AnsiConsole.MarkupLine("[#888888][[INFO]][/] Loading project [blue]MyApp.csproj[/]");
        AnsiConsole.MarkupLine("[#888888][[INFO]][/] Restoring packages...");
        AnsiConsole.WriteLine();

        // Warning with background
        AnsiConsole.MarkupLine("[black on yellow] WARN [/] [yellow]Nullable reference types not enabled[/]");
        AnsiConsole.MarkupLine("         [dim]Consider adding[/] [italic]<Nullable>enable</Nullable>[/]");
        AnsiConsole.WriteLine();

        // Safe interpolation of dynamic content
        var className = "Config[Test]";
        AnsiConsole.MarkupLineInterpolated($"[#888888][[INFO]][/] Compiling {className}...");
        AnsiConsole.WriteLine();

        // Success with link
        AnsiConsole.MarkupLine("[white on green] OK [/] [bold green]Build succeeded[/]");
        AnsiConsole.MarkupLine("     [dim]Time elapsed:[/] [bold]1.24s[/]");
        AnsiConsole.MarkupLine("     [dim]See[/] [link=https://docs.myapp.com/build]build docs[/]");
    }
}
