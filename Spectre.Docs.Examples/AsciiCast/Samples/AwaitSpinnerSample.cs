using Spectre.Console;
using Spectre.Console.Extensions;

namespace Spectre.Docs.Examples.AsciiCast.Samples;

public class AwaitSpinnerSample : BaseSample
{
    private static async Task DoSomethingAsync(int value)
    {
        await DelayHelper.Delay(value);
    }

    public override (int Cols, int Rows) ConsoleSize { get; } = (40, 4);

    public override void Run(IAnsiConsole console)
    {
        Task.Run(async () =>
        {
            AnsiConsole.Write("Loading the rocket ship ");
            await DoSomethingAsync(3500).Spinner(Spinner.Known.Dots);
            AnsiConsole.MarkupLine("[green]Done[/]");

            AnsiConsole.Write("Firing up the engines ");
            await DoSomethingAsync(3400).Spinner(Spinner.Known.BouncingBar);
            AnsiConsole.MarkupLine("[green]Done[/]");

            AnsiConsole.Write("Blasting into orbit ");
            await DoSomethingAsync(3025).Spinner(Spinner.Known.Hamburger);
            AnsiConsole.MarkupLine("[red]Oh no[/]");

        }).Wait();

    }
}