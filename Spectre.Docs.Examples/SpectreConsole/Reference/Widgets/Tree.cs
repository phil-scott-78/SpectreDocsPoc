using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class TreeExamples
{
    /// <summary>
    /// Demonstrates creating a basic tree with hierarchical nodes.
    /// </summary>
    public static void BasicTreeExample()
    {
        var tree = new Tree("Project Files");

        var src = tree.AddNode("src");
        src.AddNode("Program.cs");
        src.AddNode("Config.cs");

        var docs = tree.AddNode("docs");
        docs.AddNode("README.md");
        docs.AddNode("API.md");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates building nested tree structures with multiple levels.
    /// </summary>
    public static void NestedTreeExample()
    {
        var tree = new Tree("Project Structure");

        var src = tree.AddNode("src");
        var controllers = src.AddNode("Controllers");
        controllers.AddNode("HomeController.cs");
        controllers.AddNode("UserController.cs");

        var models = src.AddNode("Models");
        models.AddNode("User.cs");
        models.AddNode("Product.cs");

        var tests = tree.AddNode("tests");
        tests.AddNode("UnitTests.cs");
        tests.AddNode("IntegrationTests.cs");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates using markup in tree node labels for styling.
    /// </summary>
    public static void MarkupTreeExample()
    {
        var tree = new Tree("[yellow]Web Application[/]");

        var frontend = tree.AddNode("[blue]Frontend[/]");
        frontend.AddNode("[green]index.html[/]");
        frontend.AddNode("[green]styles.css[/]");
        frontend.AddNode("[green]app.js[/]");

        var backend = tree.AddNode("[blue]Backend[/]");
        backend.AddNode("[red]server.js[/]");
        backend.AddNode("[red]database.js[/]");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates using the Ascii guide style for simple text-only rendering.
    /// </summary>
    public static void TreeAsciiGuideExample()
    {
        var tree = new Tree("Root")
            .Guide(TreeGuide.Ascii);

        var child1 = tree.AddNode("Child 1");
        child1.AddNode("Grandchild 1.1");
        child1.AddNode("Grandchild 1.2");

        var child2 = tree.AddNode("Child 2");
        child2.AddNode("Grandchild 2.1");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates using the Line guide style with Unicode box-drawing characters.
    /// </summary>
    public static void TreeLineGuideExample()
    {
        var tree = new Tree("Root")
            .Guide(TreeGuide.Line);

        var child1 = tree.AddNode("Child 1");
        child1.AddNode("Grandchild 1.1");
        child1.AddNode("Grandchild 1.2");

        var child2 = tree.AddNode("Child 2");
        child2.AddNode("Grandchild 2.1");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates using the DoubleLine guide style for a bolder appearance.
    /// </summary>
    public static void TreeDoubleLineGuideExample()
    {
        var tree = new Tree("Root")
            .Guide(TreeGuide.DoubleLine);

        var child1 = tree.AddNode("Child 1");
        child1.AddNode("Grandchild 1.1");

        var child2 = tree.AddNode("Child 2");
        child2.AddNode("Grandchild 2.1");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates using the BoldLine guide style with heavy Unicode characters.
    /// </summary>
    public static void TreeBoldLineGuideExample()
    {
        var tree = new Tree("Root")
            .Guide(TreeGuide.BoldLine);

        var child1 = tree.AddNode("Child 1");
        child1.AddNode("Grandchild 1.1");

        var child2 = tree.AddNode("Child 2");
        child2.AddNode("Grandchild 2.1");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates applying styling to the entire tree structure.
    /// </summary>
    public static void TreeStylingExample()
    {
        var tree = new Tree("Styled Tree")
            .Style(Style.Parse("blue bold"));

        tree.AddNode("Item 1");
        tree.AddNode("Item 2");
        tree.AddNode("Item 3");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates controlling node expansion state to hide or show child nodes.
    /// </summary>
    public static void TreeExpansionExample()
    {
        var tree = new Tree("Project");

        var expanded = tree.AddNode("Expanded Folder");
        expanded.AddNode("File1.cs");
        expanded.AddNode("File2.cs");

        var collapsed = tree.AddNode("Collapsed Folder");
        collapsed.Collapse();
        collapsed.AddNode("Hidden1.cs");
        collapsed.AddNode("Hidden2.cs");

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates embedding other renderables like panels within tree nodes.
    /// </summary>
    public static void TreeWithRenderablesExample()
    {
        var tree = new Tree("Components");

        var headerPanel = new Panel("Application Header")
            .BorderColor(Color.Blue)
            .Expand();
        tree.AddNode(headerPanel);

        var contentPanel = new Panel("Main Content Area")
            .BorderColor(Color.Green)
            .Expand();
        tree.AddNode(contentPanel);

        var footerPanel = new Panel("Footer")
            .BorderColor(Color.Grey)
            .Expand();
        tree.AddNode(footerPanel);

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates building a tree from hierarchical data like a file system.
    /// </summary>
    public static void TreeFromDataExample()
    {
        var fileSystem = new Dictionary<string, string[]>
        {
            ["Configuration"] = new[] { "appsettings.json", "secrets.json" },
            ["Source Code"] = new[] { "Main.cs", "Helper.cs", "Utilities.cs" },
            ["Documentation"] = new[] { "README.md", "CHANGELOG.md" },
        };

        var tree = new Tree("Application");

        foreach (var (folder, files) in fileSystem)
        {
            var folderNode = tree.AddNode($"[yellow]{folder}[/]");
            foreach (var file in files)
            {
                folderNode.AddNode($"[grey]{file}[/]");
            }
        }

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates adding multiple nodes at once using AddNodes.
    /// </summary>
    public static void TreeAddNodesExample()
    {
        var tree = new Tree("Shopping List");

        tree.AddNodes(
            "Fruits",
            "Vegetables",
            "Dairy",
            "Bakery"
        );

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Demonstrates collapsing the entire tree to show only the root.
    /// </summary>
    public static void TreeCollapseAllExample()
    {
        var tree = new Tree("Project")
        {
            Expanded = false
        };

        var src = tree.AddNode("src");
        src.AddNode("File1.cs");
        src.AddNode("File2.cs");

        var tests = tree.AddNode("tests");
        tests.AddNode("Test1.cs");

        AnsiConsole.Write(tree);
    }
}
