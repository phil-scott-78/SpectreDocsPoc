using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class TextExamples
{
    /// <summary>
    /// Demonstrates basic text creation with and without styling.
    /// </summary>
    public static void BasicTextExample()
    {
        // Plain text
        var plain = new Text("Hello, World223!");
        AnsiConsole.Write(plain);
        AnsiConsole.WriteLine();

        // Text with style in constructor
        var styled = new Text("Styled Text", new Style(foreground: Color.Blue));
        AnsiConsole.Write(styled);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Demonstrates text with explicit line breaks and multi-line content.
    /// </summary>
    public static void MultiLineTextExample()
    {
        var multiLine = new Text("First line\nSecond line\nThird line");
        AnsiConsole.Write(multiLine);
    }

    /// <summary>
    /// Demonstrates using Text.Empty and Text.NewLine static members.
    /// </summary>
    public static void EmptyTextExample()
    {
        AnsiConsole.Write(new Text("Before"));
        AnsiConsole.Write(Text.NewLine);
        AnsiConsole.Write(Text.Empty);
        AnsiConsole.Write(new Text("After"));
    }

    /// <summary>
    /// Demonstrates text justification: left, center, and right alignment.
    /// </summary>
    public static void TextJustificationExample()
    {
        var panel = new Panel(new Rows(
                new Text("Left aligned text").LeftJustified(),
                Text.NewLine,
                new Text("Center aligned text").Centered(),
                Text.NewLine,
                new Text("Right aligned text").RightJustified()
            ))
            .Header("Text Justification")
            .Expand();

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates overflow behaviors: fold (wrap), crop (truncate), and ellipsis.
    /// </summary>
    public static void TextOverflowExample()
    {
        var longText = "This is a very long piece of text that will demonstrate different overflow behaviors";

        var panel = new Panel(new Rows(
            new Markup("[yellow]Fold (wrap):[/]"),
            new Text(longText).Overflow(Overflow.Fold),
            Text.NewLine,
            new Markup("[yellow]Crop (truncate):[/]"),
            new Text(longText).Overflow(Overflow.Crop),
            Text.NewLine,
            new Markup("[yellow]Ellipsis:[/]"),
            new Text(longText).Overflow(Overflow.Ellipsis)
        )) { Width = 40 };

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates foreground and background colors on text.
    /// </summary>
    public static void TextColorsExample()
    {
        // Foreground color only
        var foreground = new Text("Blue text", new Style(foreground: Color.Blue));
        AnsiConsole.Write(foreground);
        AnsiConsole.WriteLine();

        // Background color only
        var background = new Text("Yellow background", new Style(background: Color.Yellow));
        AnsiConsole.Write(background);
        AnsiConsole.WriteLine();

        // Both foreground and background
        var both = new Text("White on Red", new Style(foreground: Color.White, background: Color.Red));
        AnsiConsole.Write(both);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Demonstrates common text decorations: bold, italic, underline, and strikethrough.
    /// </summary>
    public static void TextDecorationsExample()
    {
        var decorations = new[]
        {
            ("Bold", Decoration.Bold), ("Italic", Decoration.Italic), ("Underline", Decoration.Underline),
            ("Strikethrough", Decoration.Strikethrough),
        };

        foreach (var (name, decoration) in decorations)
        {
            var text = new Text(name, new Style(decoration: decoration));
            AnsiConsole.Write(text);
            AnsiConsole.WriteLine();
        }
    }

    /// <summary>
    /// Demonstrates advanced text decorations: dim, invert, conceal, and blink.
    /// </summary>
    public static void TextAdvancedDecorationsExample()
    {
        var decorations = new[]
        {
            ("Dim", Decoration.Dim), ("Invert", Decoration.Invert), ("Conceal", Decoration.Conceal),
            ("Slow Blink", Decoration.SlowBlink), ("Rapid Blink", Decoration.RapidBlink),
        };

        foreach (var (name, decoration) in decorations)
        {
            var text = new Text($"{name} (may not be supported in all terminals)", new Style(decoration: decoration));
            AnsiConsole.Write(text);
            AnsiConsole.WriteLine();
        }
    }

    /// <summary>
    /// Demonstrates combining multiple styles: colors and decorations together.
    /// </summary>
    public static void TextCombinedStylesExample()
    {
        // Multiple decorations combined using flags
        var multiDeco = new Text(
            "Bold, Italic, and Underlined",
            new Style(
                foreground: Color.Green,
                decoration: Decoration.Bold | Decoration.Italic | Decoration.Underline
            )
        );
        AnsiConsole.Write(multiDeco);
        AnsiConsole.WriteLine();

        // Full style: foreground, background, and decoration
        var fullStyle = new Text(
            "Complete Style",
            new Style(
                foreground: Color.White,
                background: Color.DarkBlue,
                decoration: Decoration.Bold
            )
        );
        AnsiConsole.Write(fullStyle);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Demonstrates creating styled text using the Style constructor versus using Style.Parse.
    /// </summary>
    public static void TextStyleConstructorExample()
    {
        // Using Style constructor
        var constructed = new Text(
            "Constructed Style",
            new Style(foreground: Color.Red, decoration: Decoration.Bold)
        );
        AnsiConsole.Write(constructed);
        AnsiConsole.WriteLine();

        // Using Style.Parse
        var parsed = new Text("Parsed Style", Style.Parse("bold red"));
        AnsiConsole.Write(parsed);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Demonstrates reading the Length and Lines properties of Text.
    /// </summary>
    public static void TextPropertiesExample()
    {
        var singleLine = new Text("Hello, World!");
        var multiLine = new Text("Line 1\nLine 2\nLine 3");

        var table = new Table()
            .AddColumn("Text")
            .AddColumn("Length")
            .AddColumn("Lines");

        table.AddRow(
            new Text("\"Hello, World!\""),
            new Text(singleLine.Length.ToString()),
            new Text(singleLine.Lines.ToString())
        );

        table.AddRow(
            new Text("\"Line 1\\nLine 2\\nLine 3\""),
            new Text(multiLine.Length.ToString()),
            new Text(multiLine.Lines.ToString())
        );

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates using Text widgets within panels and tables.
    /// </summary>
    public static void TextInContainersExample()
    {
        // Text in a panel
        var panel = new Panel(
                new Text("This is text inside a panel", new Style(foreground: Color.Yellow))
            )
            .Header("Panel with Text")
            .BorderColor(Color.Blue);

        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();

        // Text in a table
        var table = new Table()
            .RoundedBorder()
            .AddColumn("Property")
            .AddColumn("Value");

        table.AddRow(
            new Text("Name", new Style(decoration: Decoration.Bold)),
            new Text("Application", new Style(foreground: Color.Green))
        );

        table.AddRow(
            new Text("Status", new Style(decoration: Decoration.Bold)),
            new Text("Running", new Style(foreground: Color.Green, decoration: Decoration.Bold))
        );

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates text justification using the Justification property directly.
    /// </summary>
    public static void TextJustificationPropertyExample()
    {
        var panel = new Panel(new Rows(
                new Text("Left") { Justification = Justify.Left },
                new Text("Center") { Justification = Justify.Center },
                new Text("Right") { Justification = Justify.Right }
            ))
            .Expand();

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates text overflow using the Overflow property directly.
    /// </summary>
    public static void TextOverflowPropertyExample()
    {
        var longText = "This is a very long piece of text";

        var fold = new Text(longText) { Overflow = Overflow.Fold };
        var crop = new Text(longText) { Overflow = Overflow.Crop };
        var ellipsis = new Text(longText) { Overflow = Overflow.Ellipsis };

        var panel = new Panel(new Rows(fold, crop, ellipsis)) { Width = 30 };

        AnsiConsole.Write(panel);
    }
}