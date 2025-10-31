using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Prompts;

internal static class MultiSelectionPromptExamples
{
    /// <summary>
    /// Demonstrates creating a basic multi-selection prompt with minimal configuration.
    /// </summary>
    public static void BasicMultiSelectionPromptExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which [green]features[/] do you want to enable?")
                .AddChoices("Logging", "Caching", "Compression", "Authentication"));

        AnsiConsole.WriteLine("You selected:");
        foreach (var item in selected)
        {
            AnsiConsole.WriteLine($"- {item}");
        }
    }

    /// <summary>
    /// Demonstrates bulk adding choices with AddChoices.
    /// </summary>
    public static void AddChoicesExample()
    {
        var availablePlugins = new[]
        {
            "Email Notifications",
            "SMS Alerts",
            "Slack Integration",
            "Discord Webhooks",
            "Teams Connector",
            "PagerDuty"
        };

        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]notification plugins[/] to install:")
                .AddChoices(availablePlugins));

        AnsiConsole.MarkupLine($"[blue]Installing {selected.Count} plugin(s)...[/]");
    }

    /// <summary>
    /// Demonstrates pre-selecting items with Select() for default checked items.
    /// </summary>
    public static void PreSelectItemsExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which [green]files[/] do you want to include?")
                .AddChoices("README.md", "LICENSE", "CHANGELOG.md", ".gitignore", "docs/")
                .Select("README.md")
                .Select("LICENSE"));

        AnsiConsole.WriteLine("Files to include:");
        foreach (var file in selected)
        {
            AnsiConsole.WriteLine($"- {file}");
        }
    }

    /// <summary>
    /// Demonstrates Required() to enforce at least one selection vs NotRequired() to allow none.
    /// </summary>
    public static void RequiredSelectionExample()
    {
        // Required: User must select at least one
        var requiredSelection = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select at least [red]one[/] export format:")
                .Required()
                .AddChoices("PDF", "CSV", "JSON", "XML"));

        // Not required: User can select none
        var optionalSelection = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]optional[/] post-processing steps:")
                .NotRequired()
                .AddChoices("Validate data", "Send email", "Archive results"));

        AnsiConsole.MarkupLine($"[blue]Required selections: {requiredSelection.Count}[/]");
        AnsiConsole.MarkupLine($"[blue]Optional selections: {optionalSelection.Count}[/]");
    }

    /// <summary>
    /// Demonstrates organizing choices into groups with AddChoiceGroup.
    /// </summary>
    public static void GroupedChoicesExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]components[/] to install:")
                .AddChoiceGroup("Frontend", new[] { "React UI", "Vue UI", "Angular UI" })
                .AddChoiceGroup("Backend", new[] { "REST API", "GraphQL API", "WebSockets" })
                .AddChoiceGroup("Database", new[] { "PostgreSQL", "MySQL", "MongoDB" }));

        AnsiConsole.WriteLine("Selected components:");
        foreach (var component in selected)
        {
            AnsiConsole.WriteLine($"- {component}");
        }
    }

    /// <summary>
    /// Demonstrates creating hierarchical structures with AddChild for nested choices.
    /// </summary>
    public static void HierarchicalChoicesExample()
    {
        var prompt = new MultiSelectionPrompt<string>()
            .Title("Select [green]permissions[/] to grant:");

        prompt.AddChoice("Admin");
        prompt.AddChoice("Users")
            .AddChild("View Users")
            .AddChild("Create Users")
            .AddChild("Delete Users");
        prompt.AddChoice("Content")
            .AddChild("View Content")
            .AddChild("Edit Content")
            .AddChild("Publish Content");

        var selected = AnsiConsole.Prompt(prompt);

        AnsiConsole.WriteLine("Granted permissions:");
        foreach (var permission in selected)
        {
            AnsiConsole.WriteLine($"- {permission}");
        }
    }

    /// <summary>
    /// Demonstrates Mode(SelectionMode.Leaf) where only leaf nodes can be selected (default behavior).
    /// </summary>
    public static void SelectionModeLeafExample()
    {
        var prompt = new MultiSelectionPrompt<string>()
            .Title("Select [green]specific files[/] to backup:")
            .Mode(SelectionMode.Leaf); // Only leaf nodes (files) can be selected

        prompt.AddChoice("Documents")
            .AddChild("report.pdf")
            .AddChild("notes.txt");
        prompt.AddChoice("Photos")
            .AddChild("vacation.jpg")
            .AddChild("family.jpg");

        var selected = AnsiConsole.Prompt(prompt);

        AnsiConsole.WriteLine("Files to backup:");
        foreach (var file in selected)
        {
            AnsiConsole.WriteLine($"- {file}");
        }
    }

    /// <summary>
    /// Demonstrates Mode(SelectionMode.Independent) where any node can be selected independently.
    /// </summary>
    public static void SelectionModeIndependentExample()
    {
        var prompt = new MultiSelectionPrompt<string>()
            .Title("Select [green]folders or individual files[/]:")
            .Mode(SelectionMode.Independent); // Any node can be selected

        prompt.AddChoice("src")
            .AddChild("main.cs")
            .AddChild("config.json");
        prompt.AddChoice("tests")
            .AddChild("unit-tests.cs")
            .AddChild("integration-tests.cs");

        var selected = AnsiConsole.Prompt(prompt);

        AnsiConsole.WriteLine("Selected items:");
        foreach (var item in selected)
        {
            AnsiConsole.WriteLine($"- {item}");
        }
    }

    /// <summary>
    /// Demonstrates PageSize() to control how many items are visible at once.
    /// </summary>
    public static void PageSizeExample()
    {
        var frameworks = new[]
        {
            ".NET 6", ".NET 7", ".NET 8", ".NET 9",
            "Java 17", "Java 21",
            "Python 3.9", "Python 3.10", "Python 3.11", "Python 3.12",
            "Node 18", "Node 20", "Node 22",
            "Go 1.20", "Go 1.21", "Go 1.22"
        };

        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]supported runtimes[/]:")
                .PageSize(5) // Show 5 items at a time
                .AddChoices(frameworks));

        AnsiConsole.MarkupLine($"[blue]Selected {selected.Count} runtime(s)[/]");
    }

    /// <summary>
    /// Demonstrates WrapAround() to enable circular navigation from bottom to top.
    /// </summary>
    public static void WrapAroundExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]build configurations[/]:")
                .WrapAround() // Pressing down on last item goes to first
                .AddChoices("Debug", "Release", "Staging", "Production"));

        AnsiConsole.WriteLine("Build configurations:");
        foreach (var config in selected)
        {
            AnsiConsole.WriteLine($"- {config}");
        }
    }

    /// <summary>
    /// Demonstrates HighlightStyle() to customize the appearance of the highlighted item.
    /// </summary>
    public static void HighlightStyleExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]log levels[/] to enable:")
                .HighlightStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
                .AddChoices("Trace", "Debug", "Information", "Warning", "Error", "Critical"));

        AnsiConsole.WriteLine("Enabled log levels:");
        foreach (var level in selected)
        {
            AnsiConsole.WriteLine($"- {level}");
        }
    }

    /// <summary>
    /// Demonstrates UseConverter() to customize how items are displayed.
    /// </summary>
    public static void UseConverterExample()
    {
        var environments = new Dictionary<string, string>
        {
            { "dev", "Development (localhost)" },
            { "qa", "Quality Assurance (qa.example.com)" },
            { "stage", "Staging (staging.example.com)" },
            { "prod", "Production (example.com)" }
        };

        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]deployment targets[/]:")
                .UseConverter(key => environments[key])
                .AddChoices(environments.Keys));

        AnsiConsole.WriteLine("Deploying to:");
        foreach (var env in selected)
        {
            AnsiConsole.WriteLine($"- {env}: {environments[env]}");
        }
    }

    /// <summary>
    /// Demonstrates customizing instruction text with InstructionsText() and MoreChoicesText().
    /// </summary>
    public static void CustomInstructionsExample()
    {
        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select [green]modules[/] to load:")
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
                .MoreChoicesText("[grey](Move up and down to see more modules)[/]")
                .PageSize(3)
                .AddChoices("Core", "Security", "Analytics", "Reporting", "Integration", "Automation"));

        AnsiConsole.WriteLine("Modules to load:");
        foreach (var module in selected)
        {
            AnsiConsole.WriteLine($"- {module}");
        }
    }

    /// <summary>
    /// Demonstrates a complete example using custom objects with a record type.
    /// </summary>
    public static void ComplexObjectExample()
    {
        var services = new[]
        {
            new CloudService("compute", "EC2 Instances", 0.10m),
            new CloudService("storage", "S3 Storage", 0.023m),
            new CloudService("database", "RDS Database", 0.17m),
            new CloudService("cache", "ElastiCache", 0.05m),
            new CloudService("cdn", "CloudFront CDN", 0.085m),
            new CloudService("lambda", "Lambda Functions", 0.0000002m)
        };

        var selected = AnsiConsole.Prompt(
            new MultiSelectionPrompt<CloudService>()
                .Title("Select [green]cloud services[/] to provision:")
                .UseConverter(service => $"{service.Name} (${service.HourlyRate:F3}/hr)")
                .Required()
                .AddChoices(services)
                .Select(services[0])); // Pre-select EC2

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[yellow]Selected Services[/]"));

        var totalCost = 0m;
        foreach (var service in selected)
        {
            AnsiConsole.MarkupLine($"[blue]{service.Name}[/] ({service.Id})");
            AnsiConsole.MarkupLine($"  Rate: [green]${service.HourlyRate:F4}/hour[/]");
            totalCost += service.HourlyRate;
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold]Estimated hourly cost: [green]${totalCost:F4}[/][/]");
        AnsiConsole.MarkupLine($"[dim]Monthly estimate (730 hours): ${totalCost * 730:F2}[/]");
    }

    private record CloudService(string Id, string Name, decimal HourlyRate);
}
