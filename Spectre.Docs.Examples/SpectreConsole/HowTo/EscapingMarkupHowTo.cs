using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class EscapingMarkupHowTo
{
    /// <summary>
    /// Use Markup.Escape() to safely display user-provided strings.
    /// </summary>
    public static void EscapeUserInput()
    {
        // User input that contains brackets
        var userInput = "Use [brackets] for indexing";
        var escaped = Markup.Escape(userInput);

        // Safe to use in markup string
        AnsiConsole.MarkupLine($"[blue]Input:[/] {escaped}");

        // Common patterns that need escaping
        var configKey = "settings[debug]";
        var arrayRef = "items[0]";

        AnsiConsole.MarkupLine($"[green]Config:[/] {Markup.Escape(configKey)}");
        AnsiConsole.MarkupLine($"[green]Array:[/] {Markup.Escape(arrayRef)}");
    }

    /// <summary>
    /// Use MarkupLineInterpolated() to automatically escape interpolated values.
    /// </summary>
    public static void UseSafeInterpolation()
    {
        // Values that contain brackets are escaped automatically
        var fileName = "config[backup].json";
        var userName = "admin[test]";
        var version = "v2.0[beta]";

        // No manual escaping needed
        AnsiConsole.MarkupLineInterpolated($"[blue]File:[/] {fileName}");
        AnsiConsole.MarkupLineInterpolated($"[green]User:[/] {userName}");
        AnsiConsole.MarkupLineInterpolated($"[yellow]Version:[/] {version}");

        // Works with multiple interpolated values
        AnsiConsole.MarkupLineInterpolated(
            $"[dim]Loaded[/] {fileName} [dim]for user[/] {userName}");
    }


    /// <summary>
    /// Use Markup.Remove() to strip formatting for logging or file output.
    /// </summary>
    public static void StripMarkupForLogging()
    {
        var styled = "[bold red]Error:[/] Connection to [blue]database[/] failed";

        // Remove all markup tags for plain text
        var plain = Markup.Remove(styled);

        AnsiConsole.MarkupLine("[dim]Styled output:[/]");
        AnsiConsole.MarkupLine(styled);

        AnsiConsole.MarkupLine("[dim]Plain text for logs:[/]");
        AnsiConsole.WriteLine(plain);
    }
}
