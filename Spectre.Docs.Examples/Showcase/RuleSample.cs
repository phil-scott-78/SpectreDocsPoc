using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class RuleSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule(),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]")),

            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule("[blue]← Left Aligned[/]").LeftJustified(),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]")),

            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule("[yellow]═══ Centered ═══[/]").Centered(),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]")),

            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule("[green]Right Aligned →[/]").RightJustified(),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]")),

            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule("[red bold]Important Section[/]").RuleStyle("red"),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]")),

            new Rows(
                new Markup("[dim]Lorem ipsum dolor sit amet, consectetur adipiscing elit.[/]\n[dim]Sed do eiusmod tempor incididunt ut labore et dolore.[/]"),
                new Rule("[blue]Blue Styled Rule[/]").RuleStyle("blue"),
                new Markup("[dim]Magna aliqua ut enim ad minim veniam quis nostrud.[/]\n[dim]Exercitation ullamco laboris nisi ut aliquip ex ea.[/]"))
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(2000);
            }
        });
    }
}
