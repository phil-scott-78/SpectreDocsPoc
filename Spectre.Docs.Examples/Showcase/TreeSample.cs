using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class TreeSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Panel(CreateBasicTree())
                .Header("[yellow]Basic Tree[/]"),

            new Panel(CreateNestedTree())
                .Header("[yellow]Nested Tree Structure[/]"),

            new Panel(CreateTreeGuideStylesTree())
                .Header("[yellow]Tree Guide Styles (TreeGuide.Line)[/]"),

            new Panel(CreateStyledTree())
                .Header("[yellow]Styled Tree with BoldLine Guide[/]"),

            new Panel(CreateNestedPanelsTree())
                .Header("[yellow]Tree with Nested Panel Widget[/]"),

            new Panel(CreateNestedTableTree())
                .Header("[yellow]Tree with Nested Table Widget[/]"),

            new Panel(CreateComplexHierarchyTree())
                .Header("[yellow]Complex Hierarchy (DoubleLine Guide)[/]")
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(2000);
            }
        });
    }

    private static Tree CreateBasicTree()
    {
        var tree = new Tree("Root");
        tree.AddNode("[blue]Child 1[/]");
        tree.AddNode("[yellow]Child 2[/]");
        tree.AddNode("[green]Child 3[/]");
        return tree;
    }

    private static Tree CreateNestedTree()
    {
        var tree = new Tree("Root");
        var node1 = tree.AddNode("[blue]Branch 1[/]");
        node1.AddNode("Leaf 1.1");
        node1.AddNode("Leaf 1.2");
        var node2 = tree.AddNode("[yellow]Branch 2[/]");
        node2.AddNode("Leaf 2.1");
        return tree;
    }

    private static Tree CreateTreeGuideStylesTree()
    {
        var tree = new Tree("Root")
            .Guide(TreeGuide.Line);
        tree.AddNode("Line guide");
        tree.AddNode("Another node");
        return tree;
    }

    private static Tree CreateStyledTree()
    {
        var tree = new Tree("[red bold]Styled Root[/]")
            .Style(Style.Parse("red"))
            .Guide(TreeGuide.BoldLine);
        tree.AddNode("[blue]Blue node[/]");
        tree.AddNode("[yellow]Yellow node[/]");
        return tree;
    }

    private static Tree CreateNestedPanelsTree()
    {
        var tree = new Tree("Root");
        var node = tree.AddNode("[yellow]Branch with panel[/]");
        node.AddNode(new Panel("[blue]Nested Panel[/]").BorderColor(Color.Blue));
        return tree;
    }

    private static Tree CreateNestedTableTree()
    {
        var tree = new Tree("Data");
        var node = tree.AddNode("[yellow]Table node[/]");
        var table = new Table()
            .RoundedBorder()
            .AddColumn("Col 1")
            .AddColumn("Col 2")
            .AddRow("A", "1")
            .AddRow("B", "2");
        node.AddNode(table);
        return tree;
    }

    private static Tree CreateComplexHierarchyTree()
    {
        var tree = new Tree("[bold]Project[/]")
            .Style(Style.Parse("blue"))
            .Guide(TreeGuide.DoubleLine);

        var src = tree.AddNode("[yellow]src/[/]");
        src.AddNode("Program.cs");
        src.AddNode("App.cs");

        var tests = tree.AddNode("[green]tests/[/]");
        tests.AddNode("UnitTests.cs");

        tree.AddNode("README.md");
        return tree;
    }
}
