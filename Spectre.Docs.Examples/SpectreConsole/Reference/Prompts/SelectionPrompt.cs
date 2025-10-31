using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Prompts;

internal static class SelectionPromptExamples
{
    /// <summary>
    /// Demonstrates the simplest selection prompt with a title and choices.
    /// </summary>
    public static void BasicSelectionExample()
    {
        var fruit = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What's your favorite fruit?")
                .AddChoices("Apple", "Banana", "Orange", "Mango", "Strawberry"));

        AnsiConsole.MarkupLine($"You selected: [green]{fruit}[/]");
    }

    /// <summary>
    /// Demonstrates adding a styled title with markup to the selection prompt.
    /// </summary>
    public static void SelectionWithTitleExample()
    {
        var language = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [green]programming language[/] do you prefer?")
                .AddChoices("C#", "Python", "JavaScript", "Java", "Go"));

        AnsiConsole.MarkupLine($"Great choice! [blue]{language}[/] is awesome.");
    }

    /// <summary>
    /// Demonstrates different ways to add choices using params array and IEnumerable.
    /// </summary>
    public static void AddChoicesVariationsExample()
    {
        // Using params array
        var prompt1 = new SelectionPrompt<string>()
            .Title("Select a color (params)")
            .AddChoices("Red", "Green", "Blue");

        // Using IEnumerable
        var colors = new[] { "Red", "Green", "Blue", "Yellow", "Purple" };
        var prompt2 = new SelectionPrompt<string>()
            .Title("Select a color (IEnumerable)")
            .AddChoices(colors);

        var selectedColor = AnsiConsole.Prompt(prompt2);
        AnsiConsole.MarkupLine($"You selected: [bold]{selectedColor}[/]");
    }

    /// <summary>
    /// Demonstrates creating hierarchical choices with groups and children.
    /// </summary>
    public static void HierarchicalChoicesExample()
    {
        var country = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select your [green]destination[/]")
                .PageSize(15)
                .AddChoiceGroup("Europe", new[]
                {
                    "France", "Germany", "Italy", "Spain", "United Kingdom"
                })
                .AddChoiceGroup("Asia", new[]
                {
                    "China", "Japan", "South Korea", "Thailand", "Singapore"
                })
                .AddChoiceGroup("Americas", new[]
                {
                    "United States", "Canada", "Mexico", "Brazil", "Argentina"
                }));

        AnsiConsole.MarkupLine($"Bon voyage to [yellow]{country}[/]!");
    }

    /// <summary>
    /// Demonstrates configuring page size and the message shown for additional choices.
    /// </summary>
    public static void PageSizeExample()
    {
        var cities = new[]
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
            "Austin", "Jacksonville", "Fort Worth", "Columbus", "Charlotte"
        };

        var city = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a city")
                .PageSize(5)
                .MoreChoicesText("[grey](Use arrow keys to see more cities)[/]")
                .AddChoices(cities));

        AnsiConsole.MarkupLine($"You selected: [blue]{city}[/]");
    }

    /// <summary>
    /// Demonstrates enabling wrap-around navigation for circular list scrolling.
    /// </summary>
    public static void WrapAroundExample()
    {
        var season = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select your favorite [green]season[/]")
                .WrapAround()
                .AddChoices("Spring", "Summer", "Fall", "Winter"));

        AnsiConsole.MarkupLine($"You selected: [yellow]{season}[/]");
    }

    /// <summary>
    /// Demonstrates enabling search functionality to filter choices by typing.
    /// </summary>
    public static void SearchEnabledExample()
    {
        var countries = new[]
        {
            "Argentina", "Australia", "Brazil", "Canada", "China", "Egypt",
            "France", "Germany", "India", "Italy", "Japan", "Mexico",
            "Netherlands", "Russia", "South Africa", "South Korea", "Spain",
            "Sweden", "Switzerland", "Thailand", "Turkey", "United Kingdom",
            "United States", "Vietnam"
        };

        var country = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]country[/]")
                .PageSize(10)
                .EnableSearch()
                .SearchPlaceholderText("Type to search countries...")
                .AddChoices(countries));

        AnsiConsole.MarkupLine($"You selected: [blue]{country}[/]");
    }

    /// <summary>
    /// Demonstrates customizing the style of the highlighted selection.
    /// </summary>
    public static void HighlightStyleExample()
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .HighlightStyle(new Style(Color.Green, Color.Black, Decoration.Bold))
                .AddChoices("Option 1", "Option 2", "Option 3", "Option 4"));

        AnsiConsole.MarkupLine($"You selected: [green]{option}[/]");
    }

    /// <summary>
    /// Demonstrates customizing the style for disabled items (group headers).
    /// </summary>
    public static void DisabledStyleExample()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("Select a [green]sport[/]")
            .AddChoiceGroup("Team Sports", new[]
            {
                "Basketball", "Soccer", "Baseball", "Volleyball"
            })
            .AddChoiceGroup("Individual Sports", new[]
            {
                "Tennis", "Swimming", "Running", "Cycling"
            });

        prompt.DisabledStyle = new Style(Color.Grey, decoration: Decoration.Italic);

        var sport = AnsiConsole.Prompt(prompt);
        AnsiConsole.MarkupLine($"You chose: [yellow]{sport}[/]");
    }

    /// <summary>
    /// Demonstrates customizing the style for search result highlighting.
    /// </summary>
    public static void SearchHighlightStyleExample()
    {
        var technologies = new[]
        {
            "ASP.NET Core", "Angular", "React", "Vue.js", "Node.js",
            "Docker", "Kubernetes", "PostgreSQL", "MongoDB", "Redis",
            "GraphQL", "gRPC", "RabbitMQ", "Kafka", "Elasticsearch"
        };

        var prompt = new SelectionPrompt<string>()
            .Title("Select a [green]technology[/]")
            .PageSize(8)
            .EnableSearch()
            .SearchPlaceholderText("Type to filter...")
            .AddChoices(technologies);

        prompt.SearchHighlightStyle = new Style(Color.Yellow, decoration: Decoration.Underline);

        var tech = AnsiConsole.Prompt(prompt);
        AnsiConsole.MarkupLine($"You selected: [blue]{tech}[/]");
    }

    /// <summary>
    /// Demonstrates using a custom converter to display complex objects.
    /// </summary>
    public static void CustomConverterExample()
    {
        var books = new[]
        {
            new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925 },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960 },
            new Book { Title = "1984", Author = "George Orwell", Year = 1949 },
            new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813 },
            new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951 }
        };

        var selectedBook = AnsiConsole.Prompt(
            new SelectionPrompt<Book>()
                .Title("Select a [green]book[/]")
                .PageSize(10)
                .UseConverter(book => $"{book.Title} by {book.Author} ({book.Year})")
                .AddChoices(books));

        AnsiConsole.MarkupLine($"You selected: [yellow]{selectedBook.Title}[/]");
    }

    /// <summary>
    /// Demonstrates SelectionMode.Leaf which only allows selecting leaf nodes in hierarchies.
    /// </summary>
    public static void SelectionModeLeafExample()
    {
        var file = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]file[/]")
                .PageSize(15)
                .Mode(SelectionMode.Leaf)
                .AddChoiceGroup("Documents", new[]
                {
                    "Resume.pdf", "CoverLetter.docx", "References.txt"
                })
                .AddChoiceGroup("Images", new[]
                {
                    "Photo1.jpg", "Photo2.png", "Logo.svg"
                })
                .AddChoiceGroup("Code", new[]
                {
                    "Program.cs", "Helpers.cs", "README.md"
                }));

        AnsiConsole.MarkupLine($"Opening: [blue]{file}[/]");
    }

    /// <summary>
    /// Demonstrates SelectionMode.Independent which allows selecting both parent and child nodes.
    /// </summary>
    public static void SelectionModeIndependentExample()
    {
        var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]category or item[/]")
                .PageSize(15)
                .Mode(SelectionMode.Independent)
                .AddChoiceGroup("Electronics", new[]
                {
                    "Laptops", "Smartphones", "Tablets"
                })
                .AddChoiceGroup("Clothing", new[]
                {
                    "Shirts", "Pants", "Shoes"
                })
                .AddChoiceGroup("Books", new[]
                {
                    "Fiction", "Non-Fiction", "Science"
                }));

        AnsiConsole.MarkupLine($"You selected: [yellow]{category}[/]");
    }

    /// <summary>
    /// Demonstrates a comprehensive example combining multiple features for a realistic scenario.
    /// </summary>
    public static void CompleteExampleWithAllFeatures()
    {
        var projects = new[]
        {
            new Project { Name = "E-Commerce Platform", Status = "Active", Priority = "High" },
            new Project { Name = "Mobile App Redesign", Status = "Active", Priority = "Medium" },
            new Project { Name = "API Gateway", Status = "Planning", Priority = "High" },
            new Project { Name = "Customer Portal", Status = "Active", Priority = "Low" },
            new Project { Name = "Analytics Dashboard", Status = "Completed", Priority = "Medium" },
            new Project { Name = "Payment Integration", Status = "Active", Priority = "High" },
            new Project { Name = "User Authentication", Status = "Completed", Priority = "High" },
            new Project { Name = "Email Service", Status = "Planning", Priority = "Low" }
        };

        var prompt = new SelectionPrompt<Project>()
            .Title("Select a [green]project[/] to view details")
            .PageSize(6)
            .MoreChoicesText("[grey](Move up and down to see more projects)[/]")
            .EnableSearch()
            .SearchPlaceholderText("Type to search projects...")
            .WrapAround()
            .HighlightStyle(new Style(Color.Cyan1, decoration: Decoration.Bold))
            .UseConverter(p => $"[bold]{p.Name}[/] - {p.Status} [{GetPriorityColor(p.Priority)}]{p.Priority}[/]")
            .AddChoices(projects);

        prompt.SearchHighlightStyle = new Style(Color.Yellow, decoration: Decoration.Underline);

        var selected = AnsiConsole.Prompt(prompt);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold]{selected.Name}[/]\nStatus: {selected.Status}\nPriority: {selected.Priority}")
            .Header("Project Details")
            .BorderColor(Color.Cyan1));
    }

    private static string GetPriorityColor(string priority) => priority switch
    {
        "High" => "red",
        "Medium" => "yellow",
        "Low" => "green",
        _ => "white"
    };

    private class Book
    {
        public required string Title { get; init; }
        public required string Author { get; init; }
        public int Year { get; init; }
    }

    private class Project
    {
        public required string Name { get; init; }
        public required string Status { get; init; }
        public required string Priority { get; init; }
    }
}
