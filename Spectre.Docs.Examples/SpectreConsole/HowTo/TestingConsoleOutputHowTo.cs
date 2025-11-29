using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

// NOTE: These examples demonstrate patterns for testing.
// Add Spectre.Console.Testing NuGet package to use TestConsole.

internal static class TestingConsoleOutputHowTo
{
    /// <summary>
    /// Accept IAnsiConsole as a parameter to enable testing.
    /// </summary>
    public static void AcceptConsoleAsParameter(IAnsiConsole console)
    {
        // Instead of: AnsiConsole.MarkupLine("[green]Hello![/]");
        // Use the injected console:
        console.MarkupLine("[green]Hello![/]");
    }

    /// <summary>
    /// Structure code to accept IAnsiConsole for testability.
    /// </summary>
    public static void StructureForTestability()
    {
        // Production code calls with the real console:
        PrintGreeting(AnsiConsole.Console, "World");

        // Test code would call with TestConsole:
        // var testConsole = new TestConsole();
        // PrintGreeting(testConsole, "Test");
        // Assert.Contains("Hello, Test!", testConsole.Output);
    }

    /// <summary>
    /// Example method that accepts IAnsiConsole for testing.
    /// </summary>
    public static void PrintGreeting(IAnsiConsole console, string name)
    {
        console.MarkupLine($"[green]Hello, {name}![/]");
    }

    /// <summary>
    /// Pattern for testing interactive prompts.
    /// </summary>
    public static void TestPromptPattern()
    {
        // In a test, you would:
        // var console = new TestConsole();
        // console.Input.PushTextWithEnter("Alice");
        // var result = GetUserName(console);
        // Assert.Equal("Alice", result);
    }

    /// <summary>
    /// Example prompt method that accepts IAnsiConsole.
    /// </summary>
    public static string GetUserName(IAnsiConsole console)
    {
        return console.Prompt(
            new TextPrompt<string>("What's your name?"));
    }
}
