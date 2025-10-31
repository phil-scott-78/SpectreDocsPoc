using System.Diagnostics;
using Spectre.Console;
using Spectre.Console.Extensions;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates async spinner patterns.</summary>
public class AwaitSpinnerSample : BaseSample
{
    private static async Task DoSomethingAsync(int value)
    {
        await Task.Delay(value);
    }

    /// <inheritdoc />
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

/// <summary>Demonstrates a simple counter with timing.</summary>
public class CounterSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        AnsiConsole.WriteLine("+--------++--------++--------++--------++--------++--------++--------++--------+");

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < 25; i++)
        {
            AnsiConsole.WriteLine($"{i}, elapsed - {sw.ElapsedMilliseconds}ms");
            Thread.Sleep(200);
        }

    }
}