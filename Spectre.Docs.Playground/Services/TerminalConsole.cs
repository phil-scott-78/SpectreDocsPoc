using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;
using Spectre.Docs.Playground.Components;

namespace Spectre.Docs.Playground.Services;

/// <summary>
/// An IAnsiConsole implementation that bridges Spectre.Console with xterm.js terminal.
/// Uses fire-and-forget for async operations since WASM doesn't support blocking.
/// </summary>
public class TerminalConsole : IAnsiConsole
{
    private readonly Terminal _terminal;
    private readonly CancellationToken _cancellationToken;
    private readonly object _lock = new();
    private int _cursorLeft;
    private int _cursorTop;

    public TerminalConsole(Terminal terminal, CancellationToken cancellationToken = default)
    {
        _terminal = terminal;
        _cancellationToken = cancellationToken;
        Profile = new Profile(new TerminalOutput(terminal), Encoding.UTF8);
        Profile.Width = 80;
        Profile.Height = 24;
        Profile.Capabilities.Ansi = true;
        Profile.Capabilities.Links = false;
        Profile.Capabilities.Legacy = false;
        Profile.Capabilities.Interactive = true;
        Profile.Capabilities.Unicode = true;

        Input = new TerminalInput(terminal, cancellationToken);
        ExclusivityMode = new TerminalExclusivityMode();
        Cursor = new TerminalCursor(terminal, () => _cursorLeft, l => _cursorLeft = l, () => _cursorTop, t => _cursorTop = t);
    }

    public Profile Profile { get; }
    public IAnsiConsoleCursor Cursor { get; }
    public IAnsiConsoleInput Input { get; }
    public IExclusivityMode ExclusivityMode { get; }
    public RenderPipeline Pipeline { get; } = new();

    public void Clear(bool home)
    {
        // Fire and forget - WASM can't block
        _ = _terminal.Clear();
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
            var segments = renderable.Render(options, Profile.Width);
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

    private void WriteText(Segment segment)
    {
        var builder = new StringBuilder();

        // Apply style using ANSI codes
        if (segment.Style != Style.Plain)
        {
            builder.Append(GetAnsiStyle(segment.Style));
        }

        builder.Append(segment.Text);

        // Reset style
        if (segment.Style != Style.Plain)
        {
            builder.Append("\x1b[0m");
        }

        // Fire and forget - WASM can't block
        _ = _terminal.Write(builder.ToString());

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
        // Fire and forget - WASM can't block
        _ = _terminal.Write(ansi);
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
            builder.Append($"\x1b[{string.Join(";", codes)}m");
        }

        return builder.ToString();
    }

    private class TerminalOutput : IAnsiConsoleOutput
    {
        private readonly Terminal _terminal;

        public TerminalOutput(Terminal terminal)
        {
            _terminal = terminal;
            Writer = new TerminalTextWriter(terminal);
        }

        public TextWriter Writer { get; }
        public bool IsTerminal => true;
        public int Width => 80;
        public int Height => 24;

        public void SetEncoding(Encoding encoding)
        {
            // Not needed for xterm.js
        }
    }

    private class TerminalTextWriter : TextWriter
    {
        private readonly Terminal _terminal;

        public TerminalTextWriter(Terminal terminal)
        {
            _terminal = terminal;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            // Fire and forget - WASM can't block
            _ = _terminal.Write(value.ToString());
        }

        public override void Write(string? value)
        {
            if (value != null)
            {
                // Fire and forget - WASM can't block
                _ = _terminal.Write(value);
            }
        }

        public override void WriteLine(string? value)
        {
            // Fire and forget - WASM can't block
            _ = _terminal.WriteLine(value ?? "");
        }
    }

    private class TerminalInput : IAnsiConsoleInput
    {
        private readonly Terminal _terminal;
        private readonly CancellationToken _cancellationToken;

        public TerminalInput(Terminal terminal, CancellationToken cancellationToken)
        {
            _terminal = terminal;
            _cancellationToken = cancellationToken;
        }

        public bool IsKeyAvailable()
        {
            // In WASM, we can't synchronously check for key availability
            return false;
        }

        public ConsoleKeyInfo? ReadKey(bool intercept)
        {
            // Can't do synchronous reads in WASM - return null
            // Interactive features will use ReadKeyAsync instead
            return null;
        }

        public async Task<ConsoleKeyInfo?> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
        {
            try
            {
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationToken);
                var keyInfo = await _terminal.ReadKeyAsync(linkedCts.Token);
                return new ConsoleKeyInfo(
                    keyInfo.KeyChar,
                    keyInfo.Key,
                    keyInfo.Shift,
                    keyInfo.Alt,
                    keyInfo.Control);
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
        private readonly Terminal _terminal;
        private readonly Func<int> _getLeft;
        private readonly Action<int> _setLeft;
        private readonly Func<int> _getTop;
        private readonly Action<int> _setTop;

        public TerminalCursor(
            Terminal terminal,
            Func<int> getLeft, Action<int> setLeft,
            Func<int> getTop, Action<int> setTop)
        {
            _terminal = terminal;
            _getLeft = getLeft;
            _setLeft = setLeft;
            _getTop = getTop;
            _setTop = setTop;
        }

        public void Show(bool show)
        {
            var code = show ? "\x1b[?25h" : "\x1b[?25l";
            // Fire and forget - WASM can't block
            _ = _terminal.Write(code);
        }

        public void Move(CursorDirection direction, int steps)
        {
            var code = direction switch
            {
                CursorDirection.Up => $"\x1b[{steps}A",
                CursorDirection.Down => $"\x1b[{steps}B",
                CursorDirection.Right => $"\x1b[{steps}C",
                CursorDirection.Left => $"\x1b[{steps}D",
                _ => ""
            };

            if (!string.IsNullOrEmpty(code))
            {
                // Fire and forget - WASM can't block
                _ = _terminal.Write(code);

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
            // Fire and forget - WASM can't block
            _ = _terminal.Write($"\x1b[{line + 1};{column + 1}H");
            _setLeft(column);
            _setTop(line);
        }
    }
}
