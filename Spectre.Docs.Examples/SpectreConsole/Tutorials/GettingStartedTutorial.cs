using Spectre.Console;
using Spectre.Docs.Examples.Showcase;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// A beginner tutorial that builds a styled build-output display.
/// Demonstrates markup syntax, custom colors, combined styles, links, and safe interpolation.
/// </summary>
public class GettingStartedTutorial : BaseSample
{
    /// <inheritdoc />
    // we need to inject the console here to make sure
    // we get a consistent output size, so we'll uppercase
    // the parameter to cheat a bit on the output so it looks like the static class
    public override void Run(IAnsiConsole AnsiConsole)
    {
        var fileName = "Authentication.cs";
        var dependency = "Newtonsoft.Json";

        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        AnsiConsole.MarkupLineInterpolated($"[#FFA500]⚠[/] [yellow]3 warnings[/] in {fileName}");
        AnsiConsole.MarkupLineInterpolated($"[bold red]✗ Error:[/] Missing dependency '{dependency}'");
        AnsiConsole.MarkupLine("  → See: [link=https://docs.example.com/dependencies]documentation[/]");
    }

    /// <summary>Shows a success message.</summary>
    public static void ShowSuccessMessage()
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
    }

    /// <summary>Shows a warning with the file name.</summary>
    /// <param name="fileName">The file with warnings.</param>
    public static void ShowWarningMessage(string fileName)
    {
        AnsiConsole.MarkupLineInterpolated($"[#FFA500]⚠[/] [yellow]3 warnings[/] in {fileName}");

    }

    /// <summary>Shows an error for a missing dependency.</summary>
    /// <param name="dependency">The missing dependency name.</param>
    public static void ShowErrorMessage(string dependency)
    {
        AnsiConsole.MarkupLineInterpolated($"[bold red]✗ Error:[/] Missing dependency '{dependency}'");
    }

    /// <summary>Shows a documentation link.</summary>
    /// <param name="dependency">The dependency to link.</param>
    public static void ShowDocumentationLink(string dependency)
    {
        AnsiConsole.MarkupLine("  → See: [link=https://docs.example.com/dependencies]documentation[/]");
    }
}
