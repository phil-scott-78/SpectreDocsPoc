using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Playground.Services;

/// <summary>
/// An IAnsiConsole implementation that bridges Spectre.Console with a terminal via TerminalBridge.
/// Uses synchronous blocking operations which are safe on background threads with WasmEnableThreads.
/// </summary>
public class TerminalConsole : IAnsiConsole
{
    private readonly TerminalBridge _bridge;
    private readonly Lock _lock = new();
    private int _cursorLeft;
    private int _cursorTop;

    public TerminalConsole(TerminalBridge bridge, int width = 80, int height = 24)
    {
        _bridge = bridge;
        Profile = new Profile(new TerminalOutput(_bridge, width, height), Encoding.UTF8)
        {
            Width = width, Height = height, Capabilities =
            {
                Ansi = true, Links = false, Legacy = false, Interactive = true,
                Unicode = true
            }
        };

        Input = new TerminalInput(_bridge);
        ExclusivityMode = new TerminalExclusivityMode();
        Cursor = new TerminalCursor(_bridge, () => _cursorLeft, l => _cursorLeft = l, () => _cursorTop, t => _cursorTop = t);
    }

    public Profile Profile { get; }
    public IAnsiConsoleCursor Cursor { get; }
    public IAnsiConsoleInput Input { get; }
    public IExclusivityMode ExclusivityMode { get; }
    public RenderPipeline Pipeline { get; } = new();

    public void Clear(bool home)
    {
        _bridge.WriteClear();
        if (home)
        {
            _cursorLeft = 0;
            _cursorTop = 0;
        }
    }

    public void Write(IRenderable renderable)
    {
        lock (_lock)
        {
            var options = RenderOptions.Create(this, Profile.Capabilities);

            // Process through the Pipeline to handle IRenderHook (required for Live rendering)
            var processedRenderables = Pipeline.Process(options, [renderable]);

            foreach (var processedRenderable in processedRenderables)
            {
                var segments = processedRenderable.Render(options, Profile.Width);
                foreach (var segment in segments)
                {
                    if (segment.IsControlCode)
                    {
                        WriteAnsi(segment.Text);
                    }
                    else
                    {
                        WriteText(segment);
                    }
                }
            }
        }
    }

    private void WriteText(Segment segment)
    {
        var builder = new StringBuilder();

        // Apply style using ANSI codes
        if (!segment.Style.Equals(Style.Plain))
        {
            builder.Append(GetAnsiStyle(segment.Style));
        }

        builder.Append(segment.Text);

        // Reset style
        if (!segment.Style.Equals(Style.Plain))
        {
            builder.Append("\e[0m");
        }

        // Write to bridge (thread-safe)
        _bridge.WriteOutput(builder.ToString());

        // Track cursor position
        foreach (var c in segment.Text)
        {
            if (c == '\n')
            {
                _cursorTop++;
                _cursorLeft = 0;
            }
            else if (c != '\r')
            {
                _cursorLeft++;
            }
        }
    }

    private void WriteAnsi(string ansi)
    {
        _bridge.WriteOutput(ansi);
    }

    private static string GetAnsiStyle(Style style)
    {
        var builder = new StringBuilder();
        var codes = new List<int>();

        if (style.Decoration.HasFlag(Decoration.Bold))
            codes.Add(1);
        if (style.Decoration.HasFlag(Decoration.Dim))
            codes.Add(2);
        if (style.Decoration.HasFlag(Decoration.Italic))
            codes.Add(3);
        if (style.Decoration.HasFlag(Decoration.Underline))
            codes.Add(4);
        if (style.Decoration.HasFlag(Decoration.SlowBlink))
            codes.Add(5);
        if (style.Decoration.HasFlag(Decoration.RapidBlink))
            codes.Add(6);
        if (style.Decoration.HasFlag(Decoration.Invert))
            codes.Add(7);
        if (style.Decoration.HasFlag(Decoration.Conceal))
            codes.Add(8);
        if (style.Decoration.HasFlag(Decoration.Strikethrough))
            codes.Add(9);

        if (style.Foreground != Color.Default)
        {
            var (r, g, b) = (style.Foreground.R, style.Foreground.G, style.Foreground.B);
            codes.Add(38);
            codes.Add(2);
            codes.Add(r);
            codes.Add(g);
            codes.Add(b);
        }

        if (style.Background != Color.Default)
        {
            var (r, g, b) = (style.Background.R, style.Background.G, style.Background.B);
            codes.Add(48);
            codes.Add(2);
            codes.Add(r);
            codes.Add(g);
            codes.Add(b);
        }

        if (codes.Count > 0)
        {
            builder.Append($"\e[{string.Join(";", codes)}m");
        }

        return builder.ToString();
    }

    private class TerminalOutput : IAnsiConsoleOutput
    {
        public TerminalOutput(TerminalBridge bridge, int width, int height)
        {
            Width = width;
            Height = height;
            Writer = new TerminalTextWriter(bridge);
        }

        public TextWriter Writer { get; }
        public bool IsTerminal => true;
        public int Width { get; }

        public int Height { get; }

        public void SetEncoding(Encoding encoding)
        {
            // Not needed for terminal emulator
        }
    }

    private class TerminalTextWriter : TextWriter
    {
        private readonly TerminalBridge _bridge;

        public TerminalTextWriter(TerminalBridge bridge)
        {
            _bridge = bridge;
        }

        public override Encoding Encoding => Encoding.UTF8;

        // Sync write methods
        public override void Write(char value)
        {
            _bridge.WriteOutput(value.ToString());
        }

        public override void Write(string? value)
        {
            if (value != null)
            {
                _bridge.WriteOutput(value);
            }
        }

        public override void Write(char[]? buffer)
        {
            if (buffer != null && buffer.Length > 0)
            {
                _bridge.WriteOutput(new string(buffer));
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count > 0)
            {
                _bridge.WriteOutput(new string(buffer, index, count));
            }
        }

        public override void Write(ReadOnlySpan<char> buffer)
        {
            if (buffer.Length > 0)
            {
                _bridge.WriteOutput(new string(buffer));
            }
        }

        public override void Write(StringBuilder? value)
        {
            if (value != null && value.Length > 0)
            {
                _bridge.WriteOutput(value.ToString());
            }
        }

        // Sync writeline methods
        public override void WriteLine(string? value)
        {
            _bridge.WriteOutput((value ?? "") + "\r\n");
        }

        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            _bridge.WriteOutput(new string(buffer) + "\r\n");
        }

        public override void WriteLine()
        {
            _bridge.WriteOutput("\r\n");
        }

        // Async write methods - these are critical for Live rendering
        public override Task WriteAsync(char value)
        {
            _bridge.WriteOutput(value.ToString());
            return Task.CompletedTask;
        }

        public override Task WriteAsync(string? value)
        {
            if (value != null)
            {
                _bridge.WriteOutput(value);
            }
            return Task.CompletedTask;
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            if (count > 0)
            {
                _bridge.WriteOutput(new string(buffer, index, count));
            }
            return Task.CompletedTask;
        }

        public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            if (buffer.Length > 0)
            {
                _bridge.WriteOutput(new string(buffer.Span));
            }
            return Task.CompletedTask;
        }

        public override Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            if (value != null && value.Length > 0)
            {
                _bridge.WriteOutput(value.ToString());
            }
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync()
        {
            _bridge.WriteOutput("\r\n");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(char value)
        {
            _bridge.WriteOutput(value.ToString() + "\r\n");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(string? value)
        {
            _bridge.WriteOutput((value ?? "") + "\r\n");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            if (buffer != null && count > 0)
            {
                _bridge.WriteOutput(new string(buffer, index, count) + "\r\n");
            }
            else
            {
                _bridge.WriteOutput("\r\n");
            }
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            _bridge.WriteOutput(new string(buffer.Span) + "\r\n");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            _bridge.WriteOutput((value?.ToString() ?? "") + "\r\n");
            return Task.CompletedTask;
        }

        public override void Flush()
        {
            // No-op - we write immediately
        }

        public override Task FlushAsync()
        {
            return Task.CompletedTask;
        }
    }

    private class TerminalInput : IAnsiConsoleInput
    {
        private readonly TerminalBridge _bridge;

        public TerminalInput(TerminalBridge bridge)
        {
            _bridge = bridge;
        }

        public bool IsKeyAvailable()
        {
            return _bridge.IsInputAvailable();
        }

        public ConsoleKeyInfo? ReadKey(bool intercept)
        {
            // Synchronous blocking read - safe on background thread with WasmEnableThreads
            try
            {
                return _bridge.ReadKey();
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        public async Task<ConsoleKeyInfo?> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
        {
            try
            {
                return await _bridge.ReadKeyAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }
    }

    private class TerminalExclusivityMode : IExclusivityMode
    {
        public T Run<T>(Func<T> func)
        {
            return func();
        }

        public async Task<T> RunAsync<T>(Func<Task<T>> func)
        {
            return await func();
        }
    }

    private class TerminalCursor : IAnsiConsoleCursor
    {
        private readonly TerminalBridge _bridge;
        private readonly Func<int> _getLeft;
        private readonly Action<int> _setLeft;
        private readonly Func<int> _getTop;
        private readonly Action<int> _setTop;

        public TerminalCursor(
            TerminalBridge bridge,
            Func<int> getLeft, Action<int> setLeft,
            Func<int> getTop, Action<int> setTop)
        {
            _bridge = bridge;
            _getLeft = getLeft;
            _setLeft = setLeft;
            _getTop = getTop;
            _setTop = setTop;
        }

        public void Show(bool show)
        {
            var code = show ? "\e[?25h" : "\e[?25l";
            _bridge.WriteOutput(code);
        }

        public void Move(CursorDirection direction, int steps)
        {
            var code = direction switch
            {
                CursorDirection.Up => $"\e[{steps}A",
                CursorDirection.Down => $"\e[{steps}B",
                CursorDirection.Right => $"\e[{steps}C",
                CursorDirection.Left => $"\e[{steps}D",
                _ => ""
            };

            if (!string.IsNullOrEmpty(code))
            {
                _bridge.WriteOutput(code);

                switch (direction)
                {
                    case CursorDirection.Up:
                        _setTop(Math.Max(0, _getTop() - steps));
                        break;
                    case CursorDirection.Down:
                        _setTop(_getTop() + steps);
                        break;
                    case CursorDirection.Left:
                        _setLeft(Math.Max(0, _getLeft() - steps));
                        break;
                    case CursorDirection.Right:
                        _setLeft(_getLeft() + steps);
                        break;
                }
            }
        }

        public void SetPosition(int column, int line)
        {
            _bridge.WriteOutput($"\e[{line + 1};{column + 1}H");
            _setLeft(column);
            _setTop(line);
        }
    }
}
