using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class RuleExamples
{
    /// <summary>
    /// Demonstrates creating a basic horizontal rule as a visual divider.
    /// </summary>
    public static void BasicRuleExample()
    {
        AnsiConsole.WriteLine("Section content above");
        AnsiConsole.Write(new Rule());
        AnsiConsole.WriteLine("Section content below");
    }

    /// <summary>
    /// Demonstrates adding a title to a rule for section headers.
    /// </summary>
    public static void RuleTitleExample()
    {
        AnsiConsole.Write(new Rule("[blue]Configuration[/]"));
        AnsiConsole.WriteLine("App settings and environment variables");
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[green]Results[/]"));
        AnsiConsole.WriteLine("Test execution completed successfully");
    }

    /// <summary>
    /// Demonstrates different title alignment options for rules.
    /// </summary>
    public static void RuleTitleAlignmentExample()
    {
        AnsiConsole.Write(new Rule("[yellow]Left Aligned[/]")
        {
            Justification = Justify.Left
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[yellow]Center Aligned (default)[/]")
        {
            Justification = Justify.Center
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[yellow]Right Aligned[/]")
        {
            Justification = Justify.Right
        });
    }

    /// <summary>
    /// Demonstrates different border styles for rules.
    /// </summary>
    public static void RuleBorderStylesExample()
    {
        AnsiConsole.Write(new Rule("Single Line (default)")
        {
            Border = BoxBorder.Square
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("Double Line")
        {
            Border = BoxBorder.Double
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("Heavy Line")
        {
            Border = BoxBorder.Heavy
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("Rounded")
        {
            Border = BoxBorder.Rounded
        });
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("ASCII")
        {
            Border = BoxBorder.Ascii
        });
    }

    /// <summary>
    /// Demonstrates applying color to rule lines.
    /// </summary>
    public static void RuleColorExample()
    {
        AnsiConsole.Write(new Rule("[red]Error Section[/]")
        {
            Style = Style.Parse("red")
        });
        AnsiConsole.WriteLine("Error details go here");
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[green]Success Section[/]")
        {
            Style = Style.Parse("green")
        });
        AnsiConsole.WriteLine("Success details go here");
    }

    /// <summary>
    /// Demonstrates using rules as section dividers in reports.
    /// </summary>
    public static void RuleSectionDividersExample()
    {
        AnsiConsole.Write(new Rule("[bold blue]System Information[/]"));
        AnsiConsole.WriteLine("OS: Windows 11");
        AnsiConsole.WriteLine("Memory: 16 GB");
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[bold blue]Performance Metrics[/]"));
        AnsiConsole.WriteLine("CPU Usage: 45%");
        AnsiConsole.WriteLine("Disk I/O: 120 MB/s");
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Rule("[bold blue]Network Status[/]"));
        AnsiConsole.WriteLine("Connected: Yes");
        AnsiConsole.WriteLine("Latency: 12ms");
    }

    /// <summary>
    /// Demonstrates combining rules with fluent extension methods.
    /// </summary>
    public static void RuleFluentExample()
    {
        var rule = new Rule()
            .RuleTitle("[yellow]Deployment Status[/]")
            .RuleStyle(Style.Parse("dim"));

        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine("Deployment completed at 14:32:15");
    }

    /// <summary>
    /// Demonstrates using styled rules without titles as subtle separators.
    /// </summary>
    public static void RuleSubtleSeparatorExample()
    {
        AnsiConsole.WriteLine("Log entry 1: Application started");
        AnsiConsole.Write(new Rule { Style = Style.Parse("grey dim") });

        AnsiConsole.WriteLine("Log entry 2: Configuration loaded");
        AnsiConsole.Write(new Rule { Style = Style.Parse("grey dim") });

        AnsiConsole.WriteLine("Log entry 3: Database connected");
    }
}
