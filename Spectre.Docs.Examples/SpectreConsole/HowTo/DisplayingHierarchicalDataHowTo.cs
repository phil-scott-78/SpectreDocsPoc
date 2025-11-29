using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class DisplayingHierarchicalDataHowTo
{
    /// <summary>
    /// Create a basic tree with a root and child nodes.
    /// </summary>
    public static void CreateBasicTree()
    {
        var tree = new Tree("MyProject");

        tree.AddNode("src");
        tree.AddNode("tests");
        tree.AddNode("docs");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Build deeper hierarchies by nesting nodes within nodes.
    /// </summary>
    public static void AddNestedLevels()
    {
        var tree = new Tree("MyProject");

        var src = tree.AddNode("src");
        src.AddNode("Program.cs");
        src.AddNode("Startup.cs");

        var controllers = src.AddNode("Controllers");
        controllers.AddNode("HomeController.cs");
        controllers.AddNode("ApiController.cs");

        var tests = tree.AddNode("tests");
        tests.AddNode("UnitTests.cs");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Apply colors and choose a guide style to match your output.
    /// </summary>
    public static void StyleTheTree()
    {
        var tree = new Tree("[yellow]MyProject[/]")
            .Guide(TreeGuide.BoldLine)
            .Style(Style.Parse("dim"));

        var src = tree.AddNode("[blue]src[/]");
        src.AddNode("[grey]Program.cs[/]");
        src.AddNode("[grey]Startup.cs[/]");

        var controllers = src.AddNode("[blue]Controllers[/]");
        controllers.AddNode("[grey]HomeController.cs[/]");
        controllers.AddNode("[grey]ApiController.cs[/]");

        var tests = tree.AddNode("[green]tests[/]");
        tests.AddNode("[grey]UnitTests.cs[/]");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Embed panels or other widgets as node content for rich displays.
    /// </summary>
    public static void EmbedRichContent()
    {
        var tree = new Tree("[yellow]Components[/]")
            .Guide(TreeGuide.Line);

        var header = new Panel("[bold]Navigation Bar[/]")
            .BorderColor(Color.Blue)
            .Padding(1, 0);
        tree.AddNode(header);

        var content = new Panel("[bold]Main Content[/]")
            .BorderColor(Color.Green)
            .Padding(1, 0);
        tree.AddNode(content);

        var footer = new Panel("[bold]Footer[/]")
            .BorderColor(Color.Grey)
            .Padding(1, 0);
        tree.AddNode(footer);

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Complete example: a styled project structure with nested folders and files.
    /// </summary>
    public static void RunAll()
    {
        AnsiConsole.MarkupLine("[dim]Step 1: Basic tree[/]");
        CreateBasicTree();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 2: Nested levels[/]");
        AddNestedLevels();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 3: Styled tree[/]");
        StyleTheTree();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[dim]Step 4: Rich content[/]");
        EmbedRichContent();
    }
}
