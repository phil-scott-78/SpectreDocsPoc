using System.Globalization;
using System.Text;
using System.Text.Json;
using Spectre.Console;

namespace Spectre.Docs.Examples.AsciiCast;

public class AsciiCastOut : IAnsiConsoleOutput
{
    private sealed class AsciiCastWriter(TextWriter wrappedTextWriter) : TextWriter
    {
        private readonly StringBuilder _builder = new();
        private int? _firstTick;

        public override void Write(string? value)
        {
            if (value == null)
            {
                return;
            }

            Append(value);
            wrappedTextWriter.Write(value);
            base.Write(value);
        }

        public override Encoding Encoding => wrappedTextWriter.Encoding;

        private void Append(string value)
        {
            var tick = 0m;
            if (_firstTick.HasValue)
            {
                tick = Environment.TickCount - _firstTick.Value;
            }
            else
            {
                _firstTick = Environment.TickCount;
            }

            tick /= 1000m;

            _builder.Append('[')
                .Append(CultureInfo.InvariantCulture, $"{tick}")
                .Append(", \"o\", \"").Append(JsonEncodedText.Encode(value)).AppendLine("\"]");
        }

        public string GetJsonAndClearBuffer()
        {
            var json = _builder.ToString();

            // reset the buffer and also reset the first tick count
            _builder.Clear();
            _firstTick = null;
            return json;
        }
    }

    private readonly IAnsiConsoleOutput _wrappedAnsiConsole;
    private readonly AsciiCastWriter _asciiCastWriter;

    public AsciiCastOut(IAnsiConsoleOutput wrappedAnsiConsole)
    {
        _wrappedAnsiConsole = wrappedAnsiConsole ?? throw new ArgumentNullException(nameof(wrappedAnsiConsole));
        _asciiCastWriter = new AsciiCastWriter(_wrappedAnsiConsole.Writer);
    }

    public TextWriter Writer => _asciiCastWriter;

    public bool IsTerminal => _wrappedAnsiConsole.IsTerminal;

    public int Width => _wrappedAnsiConsole.Width;

    public int Height => _wrappedAnsiConsole.Height;

    public void SetEncoding(Encoding encoding)
    {
        _wrappedAnsiConsole.SetEncoding(encoding);
    }

    public string GetCastJson(string title, int? width = null, int? height = null)
    {
        var header = $"{{\"version\": 2, \"width\": {width ?? _wrappedAnsiConsole.Width}, \"height\": {height ?? _wrappedAnsiConsole.Height}, \"title\": \"{JsonEncodedText.Encode(title)}\", \"env\": {{\"TERM\": \"Spectre.Console\"}}}}";
        return $"{header}{Environment.NewLine}{_asciiCastWriter.GetJsonAndClearBuffer()}{Environment.NewLine}";
    }
}