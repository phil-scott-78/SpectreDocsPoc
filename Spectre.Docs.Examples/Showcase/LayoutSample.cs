using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates the Layout widget for complex screen layouts.</summary>
public class LayoutSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            CreateTwoColumnsLayout(),
            CreateTwoRowsLayout(),
            CreateNestedLayout(),
            CreateThreeColumnsLayout(),
            CreateSizedRegionsLayout(),
            CreateComplexGridLayout()
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

    private static Layout CreateTwoColumnsLayout()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right"));
        layout["Left"].Update(new Panel("[blue]Left[/]").Expand());
        layout["Right"].Update(new Panel("[yellow]Right[/]").Expand());
        return layout;
    }

    private static Layout CreateTwoRowsLayout()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Bottom"));
        layout["Top"].Update(new Panel("[green]Top[/]").Expand());
        layout["Bottom"].Update(new Panel("[red]Bottom[/]").Expand());
        return layout;
    }

    private static Layout CreateNestedLayout()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right")
                    .SplitRows(
                        new Layout("Top"),
                        new Layout("Bottom")));
        layout["Left"].Update(new Panel("[blue]Left[/]").Expand());
        layout["Right"]["Top"].Update(new Panel("[yellow]Top Right[/]").Expand());
        layout["Right"]["Bottom"].Update(new Panel("[green]Bottom Right[/]").Expand());
        return layout;
    }

    private static Layout CreateThreeColumnsLayout()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Middle"),
                new Layout("Right"));
        layout["Left"].Update(new Panel("[blue]Left[/]").Expand());
        layout["Middle"].Update(new Panel("[yellow]Middle[/]").Expand());
        layout["Right"].Update(new Panel("[green]Right[/]").Expand());
        return layout;
    }

    private static Layout CreateSizedRegionsLayout()
    {
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Sidebar").Size(15),
                new Layout("Content"));
        layout["Sidebar"].Update(new Panel("[blue]Side[/]").Expand());
        layout["Content"].Update(new Panel("[yellow]Content[/]").Expand());
        return layout;
    }

    private static Layout CreateComplexGridLayout()
    {
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Header"),
                new Layout("Body")
                    .SplitColumns(
                        new Layout("Left"),
                        new Layout("Right")),
                new Layout("Footer"));
        layout["Header"].Update(new Panel("[green]Header[/]").Expand());
        layout["Body"]["Left"].Update(new Panel("[blue]Left[/]").Expand());
        layout["Body"]["Right"].Update(new Panel("[yellow]Right[/]").Expand());
        layout["Footer"].Update(new Panel("[red]Footer[/]").Expand());
        return layout;
    }
}