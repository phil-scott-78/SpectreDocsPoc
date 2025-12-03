using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>
/// Quick start examples for the Spectre.Console documentation index page.
/// </summary>
public class QuickStartSample : BaseSample
{
    /// <summary>
    /// Demonstrates basic Spectre.Console features including markup, tables, and status spinners.
    /// </summary>
    public override void Run(IAnsiConsole console)
    {
        // Styled text with markup
        AnsiConsole.MarkupLine("[bold blue]Welcome[/] to [green]Spectre.Console[/]!");

        // A simple table
        var table = new Table()
            .AddColumn("Feature")
            .AddColumn("Description")
            .AddRow("[green]Markup[/]", "Rich text with colors and styles")
            .AddRow("[blue]Tables[/]", "Structured data display")
            .AddRow("[yellow]Progress[/]", "Spinners and progress bars");
        AnsiConsole.Write(table);

        // Status spinner for work
        AnsiConsole.Status()
            .Start("Processing...", ctx =>
            {
                Thread.Sleep(2500);
            });

        AnsiConsole.MarkupLine("[green]Done![/]");    }
}
