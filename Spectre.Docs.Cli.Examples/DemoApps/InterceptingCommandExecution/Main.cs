using System.ComponentModel;
using System.Diagnostics;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.InterceptingCommandExecution;

/// <summary>
/// Demonstrates how to intercept command execution.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.SetInterceptor(new TimingInterceptor());

            config.AddCommand<ProcessCommand>("process")
                .WithDescription("Process files");
        });

        return await app.RunAsync(args);
    }
}

/// <summary>
/// An interceptor that measures command execution time.
/// </summary>
public class TimingInterceptor : ICommandInterceptor
{
    private Stopwatch? _stopwatch;

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        // Runs before command execution
        _stopwatch = Stopwatch.StartNew();
        System.Console.WriteLine($"Starting command: {context.Name}");
    }

    public void InterceptResult(CommandContext context, CommandSettings settings, ref int result)
    {
        // Runs after command execution
        _stopwatch?.Stop();
        System.Console.WriteLine($"Command completed in {_stopwatch?.ElapsedMilliseconds}ms (exit code: {result})");
    }
}

internal class ProcessCommand : Command<ProcessCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("Path to process")]
        public string Path { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Processing: {settings.Path}");
        Thread.Sleep(100); // Simulate work
        return 0;
    }
}
