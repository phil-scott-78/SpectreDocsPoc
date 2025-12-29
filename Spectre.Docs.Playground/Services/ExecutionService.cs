using System.Reflection;
using System.Runtime.Loader;
using Spectre.Console;
using Spectre.Docs.Playground.Components;

namespace Spectre.Docs.Playground.Services;

public class ExecutionService
{
    public async Task ExecuteAsync(byte[] assemblyBytes, Terminal terminal, CancellationToken cancellationToken = default)
    {
        // Get actual terminal dimensions
        var (cols, rows) = await terminal.GetSize();

        // Create a bridge for thread-safe terminal I/O
        var bridge = new TerminalBridge(cancellationToken);

        // Create a custom IAnsiConsole that writes to the bridge with actual terminal size
        var console = new TerminalConsole(bridge, cols, rows);

        // Set the console as the default for Spectre.Console
        SetDefaultConsole(console);

        // Create a linked cancellation token that we can cancel when execution completes
        using var executionCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // Start the execution on a background thread
        var executionTask = Task.Run(() => ExecuteOnBackgroundThread(assemblyBytes, bridge, cancellationToken), cancellationToken);

        // Start forwarding keyboard input from terminal to bridge (uses linked token so it stops when execution completes)
        var inputTask = ForwardInputAsync(terminal, bridge, executionCts.Token);

        try
        {
            // Process output from the bridge and send to terminal (on main thread)
            await ProcessOutputAsync(bridge, terminal, executionTask, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            await terminal.WriteLine("\e[33mExecution cancelled.\e[0m");
        }
        finally
        {
            // Cancel the input forwarding task now that execution is complete
            await executionCts.CancelAsync();

            // Wait for input task to finish (it should exit quickly after cancellation)
            try
            {
                await inputTask;
            }
            catch (OperationCanceledException)
            {
                // Expected when we cancel
            }

            // Reset the default console
            ResetDefaultConsole();
        }
    }

    private void ExecuteOnBackgroundThread(byte[] assemblyBytes, TerminalBridge bridge, CancellationToken cancellationToken)
    {
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
                bridge.WriteOutput("\e[31mError: No entry point found in the compiled assembly.\e[0m\r\n");
                bridge.Complete();
                return;
            }

            // Execute the entry point
            var parameters = entryPoint.GetParameters();
            object?[] args = parameters.Length > 0
                ? [Array.Empty<string>()]
                : [];

            var result = entryPoint.Invoke(null, args);

            // Handle async entry points
            if (result is Task task)
            {
                task.GetAwaiter().GetResult(); // Safe to block on background thread
            }

            // Unload the assembly context
            context.Unload();
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            // Unwrap reflection exceptions
            bridge.WriteOutput($"\e[31mError: {ex.InnerException.Message}\e[0m\r\n");
            if (ex.InnerException.StackTrace != null)
            {
                bridge.WriteOutput($"\e[90m{ex.InnerException.StackTrace}\e[0m\r\n");
            }
        }
        catch (OperationCanceledException)
        {
            bridge.WriteOutput("\e[33mExecution cancelled.\e[0m\r\n");
        }
        catch (Exception ex)
        {
            bridge.WriteOutput($"\e[31mError: {ex.Message}\e[0m\r\n");
            if (ex.StackTrace != null)
            {
                bridge.WriteOutput($"\e[90m{ex.StackTrace}\e[0m\r\n");
            }
        }
        finally
        {
            bridge.Complete();
        }
    }

    private async Task ProcessOutputAsync(TerminalBridge bridge, Terminal terminal, Task executionTask, CancellationToken cancellationToken)
    {
        // Read from the output channel and write to terminal
        await foreach (var output in bridge.OutputReader.ReadAllAsync(cancellationToken))
        {
            // Check for special clear marker
            if (output == "\fLEAR\0")
            {
                await terminal.Clear();
            }
            else
            {
                await terminal.Write(output);
            }
        }

        // Wait for execution to complete
        await executionTask;
    }

    private async Task ForwardInputAsync(Terminal terminal, TerminalBridge bridge, CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var keyInfo = await terminal.ReadKeyAsync(cancellationToken);
                var consoleKeyInfo = new ConsoleKeyInfo(
                    keyInfo.KeyChar,
                    keyInfo.Key,
                    keyInfo.Shift,
                    keyInfo.Alt,
                    keyInfo.Control);
                bridge.WriteInput(consoleKeyInfo);
            }
        }
        catch (OperationCanceledException)
        {
            // Normal cancellation
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
