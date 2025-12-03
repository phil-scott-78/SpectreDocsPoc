using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Testing;
using Spectre.Console.Testing;
using Xunit;

namespace Spectre.Docs.Cli.Examples.DemoApps.TestingCommands;

/// <summary>
/// Demonstrates a testable command that injects IAnsiConsole.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A testable command that uses injected IAnsiConsole instead of static AnsiConsole.
/// </summary>
public class GreetCommand : Command<GreetCommand.Settings>
{
    private readonly IAnsiConsole _console;

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("Name to greet")]
        public string Name { get; init; } = string.Empty;

        [CommandOption("-c|--count")]
        [Description("Number of times to greet")]
        [DefaultValue(1)]
        public int Count { get; init; } = 1;
    }

    public GreetCommand(IAnsiConsole console)
    {
        _console = console;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        for (var i = 0; i < settings.Count; i++)
        {
            _console.MarkupLine($"Hello, [green]{settings.Name}[/]!");
        }
        return 0;
    }
}

/// <summary>
/// Example tests demonstrating CommandAppTester usage.
/// </summary>
public class GreetCommandTests
{
    /// <summary>
    /// Tests that the greet command returns success and outputs the greeting.
    /// </summary>
    [Fact]
    public void Greet_WithName_ReturnsZeroAndOutputsGreeting()
    {
        // Arrange
        var app = new CommandAppTester();
        app.SetDefaultCommand<GreetCommand>();

        // Act
        var result = app.Run("World");

        // Assert
        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Hello", result.Output);
        Assert.Contains("World", result.Output);
    }

    /// <summary>
    /// Tests that settings are parsed correctly from command-line arguments.
    /// </summary>
    [Fact]
    public void Greet_WithCount_ParsesSettingsCorrectly()
    {
        // Arrange
        var app = new CommandAppTester();
        app.SetDefaultCommand<GreetCommand>();

        // Act
        var result = app.Run("Alice", "--count", "3");

        // Assert
        Assert.Equal(0, result.ExitCode);

        // Verify settings were parsed correctly
        var settings = result.Settings as GreetCommand.Settings;
        Assert.NotNull(settings);
        Assert.Equal("Alice", settings!.Name);
        Assert.Equal(3, settings.Count);
    }
}

/// <summary>
/// Example test demonstrating interactive prompt testing with TestConsole.
/// </summary>
public class InteractiveCommandTests
{
    /// <summary>
    /// Tests that interactive prompts can be tested by queuing input.
    /// </summary>
    [Fact]
    public void Interactive_WithQueuedInput_ProcessesCorrectly()
    {
        // Arrange - queue up user input before running
        var console = new TestConsole();
        console.Profile.Capabilities.Interactive = true;
        console.Input.PushTextWithEnter("yes");

        var app = new CommandAppTester(console: console);
        app.SetDefaultCommand<ConfirmCommand>();

        // Act
        var result = app.Run();

        // Assert
        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Confirmed", result.Output);
    }
}

/// <summary>
/// A command that prompts for confirmation.
/// </summary>
public class ConfirmCommand : Command<ConfirmCommand.Settings>
{
    private readonly IAnsiConsole _console;

    public class Settings : CommandSettings;

    public ConfirmCommand(IAnsiConsole console)
    {
        _console = console;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        var confirm = _console.Confirm("Continue?");
        _console.WriteLine(confirm ? "Confirmed!" : "Cancelled.");
        return 0;
    }
}
