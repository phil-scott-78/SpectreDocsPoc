using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class TextPathExamples
{
    /// <summary>
    /// Demonstrates rendering a basic file path.
    /// </summary>
    public static void BasicTextPathExample()
    {
        var path = new TextPath("C:/Users/Phil/Documents/project/src/main.cs");
        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Demonstrates coloring each path component differently.
    /// </summary>
    public static void TextPathColorsExample()
    {
        var path = new TextPath("C:/Users/Phil/Documents/project/src/main.cs")
            .RootColor(Color.Red)
            .SeparatorColor(Color.Grey)
            .StemColor(Color.Blue)
            .LeafColor(Color.Green);

        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Demonstrates applying full styles to path components.
    /// </summary>
    public static void TextPathStylesExample()
    {
        var path = new TextPath("/home/user/projects/app/Program.cs")
            .RootStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
            .SeparatorStyle(new Style(Color.Grey))
            .StemStyle(new Style(Color.Blue))
            .LeafStyle(new Style(Color.Green, decoration: Decoration.Underline));

        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Demonstrates text alignment options for paths.
    /// </summary>
    public static void TextPathAlignmentExample()
    {
        var leftPath = new TextPath("src/components/Button.tsx")
            .LeftJustified()
            .LeafColor(Color.Green);

        var centerPath = new TextPath("src/components/Button.tsx")
            .Centered()
            .LeafColor(Color.Green);

        var rightPath = new TextPath("src/components/Button.tsx")
            .RightJustified()
            .LeafColor(Color.Green);

        AnsiConsole.MarkupLine("[grey]Left aligned:[/]");
        AnsiConsole.Write(leftPath);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[grey]Center aligned:[/]");
        AnsiConsole.Write(centerPath);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[grey]Right aligned:[/]");
        AnsiConsole.Write(rightPath);
    }

    /// <summary>
    /// Demonstrates how TextPath handles long paths with smart truncation.
    /// </summary>
    public static void TextPathTruncationExample()
    {
        // This long path will be truncated to fit, preserving root and leaf
        var path = new TextPath("C:/Users/Developer/Documents/Projects/MyCompany/Application/src/components/forms/validation/rules/StringValidator.cs")
            .RootColor(Color.Yellow)
            .StemColor(Color.Blue)
            .LeafColor(Color.Green);

        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Demonstrates rendering Unix-style paths.
    /// </summary>
    public static void TextPathUnixExample()
    {
        var path = new TextPath("/var/log/nginx/access.log")
            .RootColor(Color.Yellow)
            .StemColor(Color.Blue)
            .LeafColor(Color.Green);

        AnsiConsole.Write(path);
    }
}
