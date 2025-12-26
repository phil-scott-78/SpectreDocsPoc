using System.Reflection;
using System.Runtime.Loader;
using Spectre.Console;
using Spectre.Docs.Playground.Components;

namespace Spectre.Docs.Playground.Services;

public class ExecutionService
{
    public async Task ExecuteAsync(byte[] assemblyBytes, Terminal terminal, CancellationToken cancellationToken = default)
    {
        // Create a custom IAnsiConsole that writes to the terminal
        var console = new TerminalConsole(terminal, cancellationToken);

        // Set the console as the default for Spectre.Console
        // This is a bit hacky but necessary for top-level statement style code
        SetDefaultConsole(console);

        try
        {
            // Load the assembly
            using var ms = new MemoryStream(assemblyBytes);
            var context = new CollectibleAssemblyLoadContext();
            var assembly = context.LoadFromStream(ms);

            // Find the entry point
            var entryPoint = assembly.EntryPoint;
            if (entryPoint == null)
            {
                throw new InvalidOperationException("No entry point found in the compiled assembly.");
            }

            // Execute the entry point
            var parameters = entryPoint.GetParameters();
            object?[] args = parameters.Length > 0
                ? new object?[] { Array.Empty<string>() }
                : Array.Empty<object?>();

            var result = entryPoint.Invoke(null, args);

            // Handle async entry points
            if (result is Task task)
            {
                await task;
            }

            // Unload the assembly context
            context.Unload();
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            // Unwrap reflection exceptions
            await terminal.WriteLine($"\x1b[31mError: {ex.InnerException.Message}\x1b[0m");
            if (ex.InnerException.StackTrace != null)
            {
                await terminal.WriteLine($"\x1b[90m{ex.InnerException.StackTrace}\x1b[0m");
            }
        }
        catch (OperationCanceledException)
        {
            await terminal.WriteLine("\x1b[33mExecution cancelled.\x1b[0m");
        }
        catch (Exception ex)
        {
            await terminal.WriteLine($"\x1b[31mError: {ex.Message}\x1b[0m");
        }
        finally
        {
            // Reset the default console
            ResetDefaultConsole();
        }
    }

    private static void SetDefaultConsole(IAnsiConsole console)
    {
        // Use reflection to set the internal console
        // This allows code using AnsiConsole.WriteLine etc. to work
        var field = typeof(AnsiConsole).GetField("_console", BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, new Lazy<IAnsiConsole>(() => console));
    }

    private static void ResetDefaultConsole()
    {
        // Reset to null so Spectre.Console recreates its default
        var field = typeof(AnsiConsole).GetField("_console", BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, null);
    }

    /// <summary>
    /// An assembly load context that can be unloaded to free memory.
    /// </summary>
    private class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        public CollectibleAssemblyLoadContext() : base(isCollectible: true)
        {
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            // Return null to fall back to the default context
            return null;
        }
    }
}
