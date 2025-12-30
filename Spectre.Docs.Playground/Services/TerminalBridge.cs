using System.Threading.Channels;

namespace Spectre.Docs.Playground.Services;

/// <summary>
/// Thread-safe bridge for terminal I/O that allows background threads to communicate
/// with the main thread (which has JS interop access).
/// </summary>
public class TerminalBridge
{
    private readonly Channel<string> _outputChannel;
    private readonly Channel<ConsoleKeyInfo> _inputChannel;
    private readonly CancellationToken _cancellationToken;

    public TerminalBridge(CancellationToken cancellationToken = default)
    {
        _cancellationToken = cancellationToken;
        _outputChannel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });
        _inputChannel = Channel.CreateUnbounded<ConsoleKeyInfo>(new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = true
        });
    }

    /// <summary>
    /// Writer for output - used by the background thread (TerminalConsole).
    /// This blocks until the message is written to the channel.
    /// </summary>
    public void WriteOutput(string text)
    {
        _outputChannel.Writer.TryWrite(text);
    }

    /// <summary>
    /// Clear signal - sends a special marker.
    /// </summary>
    public void WriteClear()
    {
        _outputChannel.Writer.TryWrite("\fLEAR\0");
    }

    /// <summary>
    /// Reader for output - used by the main thread to send to xterm.js.
    /// </summary>
    public ChannelReader<string> OutputReader => _outputChannel.Reader;

    /// <summary>
    /// Write a key to the input channel - called by main thread when key is received.
    /// </summary>
    public void WriteInput(ConsoleKeyInfo keyInfo)
    {
        _inputChannel.Writer.TryWrite(keyInfo);
    }

    /// <summary>
    /// Read a key from the input channel - called by background thread (blocks until available).
    /// </summary>
    public ConsoleKeyInfo ReadKey()
    {
        // This blocks the current thread until input is available
        // Safe to do on background thread with WasmEnableThreads
        try
        {
            // Use synchronous read - this will block the background thread
            var task = _inputChannel.Reader.ReadAsync(_cancellationToken).AsTask();
            task.Wait(_cancellationToken);
            return task.Result;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// Async version of ReadKey for when async is available.
    /// </summary>
    public async ValueTask<ConsoleKeyInfo> ReadKeyAsync(CancellationToken cancellationToken = default)
    {
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationToken);
        return await _inputChannel.Reader.ReadAsync(linkedCts.Token);
    }

    /// <summary>
    /// Check if input is available without blocking.
    /// </summary>
    public bool IsInputAvailable()
    {
        return _inputChannel.Reader.TryPeek(out _);
    }

    /// <summary>
    /// Signal that execution is complete.
    /// </summary>
    public void Complete()
    {
        _outputChannel.Writer.TryComplete();
    }

}
