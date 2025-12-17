using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class CreatingCustomRenderablesHowTo
{
    /// <summary>
    /// Create a minimal custom renderable that displays styled text.
    /// </summary>
    public static void ImplementIRenderable()
    {
        var label = new Label("Success");
        AnsiConsole.Write(label);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Implement Measure() to report your widget's size constraints.
    /// </summary>
    public static void CalculateSizeConstraints()
    {
        var label = new Label("Processing");
        AnsiConsole.Write(label);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Implement Render() to yield Segment objects with styled text.
    /// </summary>
    public static void GenerateSegments()
    {
        var label = new Label("Warning");
        AnsiConsole.Write(label);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Apply foreground and background colors to your label.
    /// </summary>
    public static void ApplyStyles()
    {
        var success = new Label("OK", Color.White, Color.Green);
        var warning = new Label("WARN", Color.Black, Color.Yellow);
        var error = new Label("ERROR", Color.White, Color.Red);

        AnsiConsole.Write(success);
        AnsiConsole.Write(" ");
        AnsiConsole.Write(warning);
        AnsiConsole.Write(" ");
        AnsiConsole.Write(error);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Create a container that wraps another renderable.
    /// </summary>
    public static void WrapOtherRenderables()
    {
        var status = new LabeledValue(
            new Label("Status", Color.White, Color.Blue),
            new Text("All systems operational", new Style(Color.Green)));

        AnsiConsole.Write(status);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Complete example: styled labels with different colors.
    /// </summary>
    public static void RunAll()
    {
        AnsiConsole.MarkupLine("[dim]Step 1: Basic label[/]");
        ImplementIRenderable();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 2: Styled labels[/]");
        ApplyStyles();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 3: Container with label[/]");
        WrapOtherRenderables();
    }
}

/// <summary>
/// A custom renderable that displays text as a styled badge.
/// </summary>
internal sealed class Label : IRenderable
{
    private readonly string _text;
    private readonly Style _style;

    public Label(string text)
        : this(text, Color.Black, Color.Grey)
    {
    }

    public Label(string text, Color foreground, Color background)
    {
        _text = text;
        _style = new Style(foreground, background);
    }

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        // Add 2 for padding (space on each side)
        var width = _text.Length + 2;
        return new Measurement(width, width);
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        // Render padded text with our style
        yield return new Segment($" {_text} ", _style);
    }
}

/// <summary>
/// A container that displays a label followed by content.
/// </summary>
internal sealed class LabeledValue : IRenderable
{
    private readonly IRenderable _label;
    private readonly IRenderable _value;

    public LabeledValue(IRenderable label, IRenderable value)
    {
        _label = label;
        _value = value;
    }

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        var labelMeasure = _label.Measure(options, maxWidth);
        var remaining = maxWidth - labelMeasure.Max - 1; // -1 for space
        var valueMeasure = _value.Measure(options, Math.Max(0, remaining));

        var min = labelMeasure.Min + 1 + valueMeasure.Min;
        var max = labelMeasure.Max + 1 + valueMeasure.Max;
        return new Measurement(min, max);
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        // Render label
        var labelMeasure = _label.Measure(options, maxWidth);
        foreach (var segment in _label.Render(options, labelMeasure.Max))
        {
            yield return segment;
        }

        // Space between label and value
        yield return new Segment(" ");

        // Render value with remaining width
        var remaining = maxWidth - labelMeasure.Max - 1;
        foreach (var segment in _value.Render(options, Math.Max(0, remaining)))
        {
            yield return segment;
        }
    }
}
