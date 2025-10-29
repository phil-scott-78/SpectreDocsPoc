using Spectre.Console;

namespace Spectre.Docs.Examples.AsciiCast.Samples;

internal class HeroSample : BaseSample
{
    public override (int Cols, int Rows) ConsoleSize => (80, 12);

    public override void Run(IAnsiConsole console)
    {
        // Start with mundane terminal output
        console.WriteLine("test results log:");
        console.WriteLine("module_a: pass");
        console.WriteLine("module_b: pass");
        console.WriteLine("module_c: fail");
        console.WriteLine("module_d: pass");
        DelayHelper.Sleep(500);
        console.WriteLine("rethinking display format:");
        DelayHelper.Sleep(1000);

        // Science-y status messages

        console.Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Dots)
            .Start("[grey]Detecting suboptimal display format...[/]", ctx =>
            {
                DelayHelper.Sleep(1000);
                ctx.Spinner(Spinner.Known.Arc);
                ctx.Status("[yellow]Initializing Enhancement Protocol v2.1...[/]");
                DelayHelper.Sleep(1000);
                ctx.Spinner(Spinner.Known.Bounce);
                ctx.Status("[cyan]Calibrating visual enhancement matrices...[/]");
                DelayHelper.Sleep(1000);
            });

        DelayHelper.Sleep(300);

        console.Clear();

        // Progress with technical operations
        console.Progress()
            .AutoClear(false)
            .Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn(), new SpinnerColumn())
            .Start(ctx =>
            {
                var quantumTask = ctx.AddTask("[cyan]Quantum flux optimization[/]", maxValue: 100);
                var neuralTask = ctx.AddTask("[green]Neural interface calibration[/]", maxValue: 100);
                var photonTask = ctx.AddTask("[yellow]Photon emission tuning[/]", maxValue: 100);

                while (!ctx.IsFinished)
                {
                    DelayHelper.Sleep(25);
                    quantumTask.Increment(3.8);
                    DelayHelper.Sleep(25);
                    neuralTask.Increment(4.2);
                    DelayHelper.Sleep(25);
                    photonTask.Increment(5.5);
                }
            });

        DelayHelper.Sleep(800);
        console.Clear();

        // Rebuild with Live - science facility style
        var table = new Table();

        console.Live(table)
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .Start(ctx =>
            {
                void Update(int delay, Action action)
                {
                    action();
                    ctx.Refresh();
                    DelayHelper.Sleep(delay);
                }

                // Initialize data matrix
                Update(200, () => table.AddColumn("Test Module"));
                Update(200, () => table.AddColumn("Status"));
                Update(200, () => table.AddColumn("Efficiency"));
                Update(200, () => table.AddColumn("Notes"));

                // Populate test results
                Update(300, () => table.AddRow("Module Alpha", "[green]OPERATIONAL[/]", "[cyan]98.2%[/]", "Exceeding parameters"));
                Update(300, () => table.AddRow("Module Beta", "[green]OPERATIONAL[/]", "[cyan]94.7%[/]", "Within tolerance"));
                Update(300, () => table.AddRow("Module Gamma", "[red]ANOMALY[/]", "[yellow]43.1%[/]", "[yellow]Recalibration required[/]"));
                Update(300, () => table.AddRow("Module Delta", "[green]OPERATIONAL[/]", "[cyan]99.8%[/]", "Optimal performance"));

                // Apply facility styling
                Update(400, () => table.BorderColor(Color.Cyan2));
                Update(400, () => table.SimpleBorder());
                Update(400, () => table.Expand());

                // Configure headers
                Update(200, () => table.Columns[0].Header("[bold white]Test Module[/]"));
                Update(200, () => table.Columns[1].Header("[bold white]Status[/]"));
                Update(200, () => table.Columns[2].Header("[bold white]Efficiency[/]"));
                Update(200, () => table.Columns[3].Header("[bold white]Notes[/]"));

                // Add statistical footer
                Update(400, () => table.Columns[2].Footer("[bold cyan]83.95% AVG[/]"));

                // Encase in facility-standard panel
                Update(600, () =>
                {
                    var panel = new Panel(table)
                        .Header("[bold yellow][[ FACILITY MONITORING SYSTEM v4.7.2 ]][/]")
                        .BorderColor(Color.Grey)
                        .RoundedBorder()
                        .Expand();

                    ctx.UpdateTarget(panel);
                });
                ;
            });


        // Final status
        DelayHelper.Sleep(500);
        console.WriteLine();
        console.MarkupLine("[bold green]► Display optimization complete. Science continues.[/]");
    }
}