using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates status spinner for data processing operations.</summary>
public class StatusSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("blue"))
            .Start("Waking up the hamsters...", ctx =>
            {
                Thread.Sleep(1000);

                ctx.Status("[blue]Locating remote endpoints...[/]");
                ctx.Spinner(Spinner.Known.Dots2);
                Thread.Sleep(1200);

                ctx.Status("[cyan]Convincing firewall...[/]");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("cyan"));
                Thread.Sleep(1000);

                ctx.Status("[green]Parsing reluctant data...[/]");
                ctx.Spinner(Spinner.Known.Arrow3);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1400);

                ctx.Status("[yellow]Coercing legacy formats...[/]");
                ctx.Spinner(Spinner.Known.BouncingBar);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                Thread.Sleep(1200);

                ctx.Status("[magenta]Untangling dependencies...[/]");
                ctx.Spinner(Spinner.Known.Pipe);
                ctx.SpinnerStyle(Style.Parse("magenta"));
                Thread.Sleep(1100);

                ctx.Status("[green bold]Archiving for posterity...[/]");
                ctx.Spinner(Spinner.Known.Dots12);
                ctx.SpinnerStyle(Style.Parse("green bold"));
                Thread.Sleep(1000);
            });

        console.MarkupLine("[green]Data ingestion complete.[/] [dim]2,847.3 records processed.[/]");
    }
}
