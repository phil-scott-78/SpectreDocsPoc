using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class OrganizingLayoutHowTo
{
    /// <summary>
    /// Wrap content in a bordered panel.
    /// </summary>
    public static void WrapInPanel()
    {
        var panel = new Panel("Important message")
            .Header("[yellow]Notice[/]")
            .BorderColor(Color.Yellow);

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Place content side by side in columns.
    /// </summary>
    public static void ArrangeSideBySide()
    {
        var left = new Panel("Left content").Header("Panel 1");
        var right = new Panel("Right content").Header("Panel 2");

        AnsiConsole.Write(new Columns(left, right));
    }

    /// <summary>
    /// Create a grid with rows and columns.
    /// </summary>
    public static void CreateGrid()
    {
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("Name", "Alice");
        grid.AddRow("Role", "Developer");
        grid.AddRow("Team", "Platform");

        AnsiConsole.Write(grid);
    }

    /// <summary>
    /// Center content horizontally.
    /// </summary>
    public static void CenterContent()
    {
        var panel = new Panel("Centered content")
            .Border(BoxBorder.Rounded);

        AnsiConsole.Write(Align.Center(panel));
    }
}
