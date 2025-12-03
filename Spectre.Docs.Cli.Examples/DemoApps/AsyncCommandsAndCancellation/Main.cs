using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.AsyncCommandsAndCancellation;

/// <summary>
/// Demonstrates async commands with cancellation token support.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        // Create a cancellation token source to handle Ctrl+C
        var cancellationTokenSource = new CancellationTokenSource();

        // Wire up Console.CancelKeyPress to trigger cancellation
        System.Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // Prevent immediate process termination
            cancellationTokenSource.Cancel();
            System.Console.WriteLine("Cancellation requested...");
        };

        var app = new CommandApp<FetchCommand>();

        // Pass the cancellation token to RunAsync
        return await app.RunAsync(args, cancellationTokenSource.Token);
    }
}

/// <summary>
/// An async command that fetches data from a URL.
/// </summary>
internal class FetchCommand : AsyncCommand<FetchCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<url>")]
        [Description("The URL to fetch")]
        public string Url { get; init; } = string.Empty;
    }

    protected override async Task<int> ExecuteAsync(
        CommandContext context,
        Settings settings,
        CancellationToken cancellationToken)
    {
        System.Console.WriteLine($"Fetching {settings.Url}...");

        try
        {
            using var httpClient = new HttpClient();
            // Pass the cancellation token to async operations
            var response = await httpClient.GetStringAsync(settings.Url, cancellationToken);
            System.Console.WriteLine($"Fetched {response.Length} characters");
            return 0;
        }
        catch (OperationCanceledException)
        {
            System.Console.WriteLine("Request was cancelled.");
            return 1;
        }
    }
}
