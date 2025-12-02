namespace Spectre.Docs.Cli.Examples;

/// <summary>
/// Interface for CLI demo applications that can be discovered and executed.
/// Each demo should implement this interface in a Main.cs file within its DemoApps/{DemoName}/ folder.
/// </summary>
public interface IDemoApp
{
    /// <summary>
    /// The display name of the demo (used in listings).
    /// </summary>
    string Name { get; }

    /// <summary>
    /// A brief description of what the demo demonstrates.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Runs the demo application with the provided arguments.
    /// </summary>
    /// <param name="args">Command-line arguments (the demo name has already been consumed).</param>
    /// <returns>Exit code (0 for success).</returns>
    Task<int> RunAsync(string[] args);
}
