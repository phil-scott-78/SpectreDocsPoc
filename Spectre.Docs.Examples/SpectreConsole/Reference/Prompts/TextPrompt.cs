using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Prompts;

internal static class TextPromptExamples
{
    /// <summary>
    /// Demonstrates the simplest way to ask for user input using AnsiConsole.Ask.
    /// </summary>
    public static void BasicAskExample()
    {
        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
        AnsiConsole.MarkupLine($"Hello, [blue]{name}[/]!");
    }

    /// <summary>
    /// Demonstrates asking for input with a default value using the Ask method.
    /// </summary>
    public static void AskWithDefaultExample()
    {
        var name = AnsiConsole.Ask("What's your [green]name[/]?", "Anonymous");
        AnsiConsole.MarkupLine($"Hello, [blue]{name}[/]!");
    }

    /// <summary>
    /// Demonstrates automatic type conversion for numeric types.
    /// </summary>
    public static void TypeConversionExample()
    {
        var age = AnsiConsole.Ask<int>("What's your [green]age[/]?");
        var height = AnsiConsole.Ask<decimal>("What's your [green]height[/] in meters?");

        AnsiConsole.MarkupLine($"You are [blue]{age}[/] years old and [blue]{height}m[/] tall.");
    }

    /// <summary>
    /// Demonstrates creating a TextPrompt instance directly for more control.
    /// </summary>
    public static void TextPromptBasicExample()
    {
        var prompt = new TextPrompt<string>("What's your [green]name[/]?");
        var name = AnsiConsole.Prompt(prompt);

        AnsiConsole.MarkupLine($"Hello, [blue]{name}[/]!");
    }

    /// <summary>
    /// Demonstrates setting a default value and controlling its display.
    /// </summary>
    public static void DefaultValueExample()
    {
        var name = new TextPrompt<string>("What's your [green]name[/]?")
            .DefaultValue("Anonymous")
            .ShowDefaultValue();

        var result = AnsiConsole.Prompt(name);

        AnsiConsole.MarkupLine($"Hello, [blue]{result}[/]!");
    }

    /// <summary>
    /// Demonstrates masking input for passwords using Secret mode.
    /// </summary>
    public static void SecretInputExample()
    {
        var password = new TextPrompt<string>("Enter your [green]password[/]:")
            .Secret();

        var result = AnsiConsole.Prompt(password);

        AnsiConsole.MarkupLine($"Password length: [blue]{result.Length}[/] characters");
    }

    /// <summary>
    /// Demonstrates using a custom mask character or null for completely hidden input.
    /// </summary>
    public static void CustomMaskExample()
    {
        var pin = new TextPrompt<string>("Enter your [green]PIN[/]:")
            .Secret('#');

        var invisible = new TextPrompt<string>("Enter [green]secret code[/]:")
            .Secret(null);

        var pinResult = AnsiConsole.Prompt(pin);
        var codeResult = AnsiConsole.Prompt(invisible);

        AnsiConsole.MarkupLine($"PIN entered (length: [blue]{pinResult.Length}[/])");
        AnsiConsole.MarkupLine($"Code entered (length: [blue]{codeResult.Length}[/])");
    }

    /// <summary>
    /// Demonstrates simple validation with a boolean function and error message.
    /// </summary>
    public static void SimpleValidationExample()
    {
        var email = new TextPrompt<string>("What's your [green]email[/]?")
            .Validate(input =>
                input.Contains("@") && input.Contains("."),
                "[red]Please enter a valid email address[/]");

        var result = AnsiConsole.Prompt(email);

        AnsiConsole.MarkupLine($"Email: [blue]{result}[/]");
    }

    /// <summary>
    /// Demonstrates validation using ValidationResult for more control over error messages.
    /// </summary>
    public static void RichValidationExample()
    {
        var age = new TextPrompt<int>("What's your [green]age[/]?")
            .Validate(age =>
            {
                if (age < 0)
                {
                    return ValidationResult.Error("[red]Age cannot be negative[/]");
                }

                if (age > 120)
                {
                    return ValidationResult.Error("[red]Please enter a realistic age[/]");
                }

                if (age < 18)
                {
                    return ValidationResult.Error("[yellow]You must be 18 or older[/]");
                }

                return ValidationResult.Success();
            });

        var result = AnsiConsole.Prompt(age);

        AnsiConsole.MarkupLine($"Age: [blue]{result}[/]");
    }

    /// <summary>
    /// Demonstrates restricting input to predefined choices with visible options.
    /// </summary>
    public static void ChoicesExample()
    {
        var color = new TextPrompt<string>("What's your favorite [green]color[/]?")
            .AddChoice("red")
            .AddChoice("green")
            .AddChoice("blue")
            .AddChoice("yellow")
            .ShowChoices();

        var result = AnsiConsole.Prompt(color);

        AnsiConsole.MarkupLine($"You chose: [blue]{result}[/]");
    }

    /// <summary>
    /// Demonstrates restricting input to predefined choices without showing them.
    /// </summary>
    public static void HiddenChoicesExample()
    {
        var secretWord = new TextPrompt<string>("Enter the [green]secret word[/]:")
            .AddChoice("opensesame")
            .AddChoice("abracadabra")
            .AddChoice("alakazam")
            .HideChoices();

        var result = AnsiConsole.Prompt(secretWord);

        AnsiConsole.MarkupLine($"Correct! The word was: [blue]{result}[/]");
    }

    /// <summary>
    /// Demonstrates allowing empty input for optional fields.
    /// </summary>
    public static void AllowEmptyExample()
    {
        var nickname = new TextPrompt<string>("Enter your [green]nickname[/] (optional):")
            .AllowEmpty();

        var result = AnsiConsole.Prompt(nickname);

        if (string.IsNullOrWhiteSpace(result))
        {
            AnsiConsole.MarkupLine("[yellow]No nickname provided[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"Nickname: [blue]{result}[/]");
        }
    }

    /// <summary>
    /// Demonstrates customizing colors for prompts, default values, and choices.
    /// </summary>
    public static void StylingExample()
    {
        var city = new TextPrompt<string>("What [yellow]city[/] do you live in?")
            .PromptStyle(new Style(Color.Yellow))
            .DefaultValue("London")
            .DefaultValueStyle(new Style(Color.Green, decoration: Decoration.Italic))
            .AddChoice("London")
            .AddChoice("New York")
            .AddChoice("Tokyo")
            .AddChoice("Paris")
            .ChoicesStyle(new Style(Color.Cyan));

        var result = AnsiConsole.Prompt(city);

        AnsiConsole.MarkupLine($"City: [blue]{result}[/]");
    }

    /// <summary>
    /// Demonstrates using a custom converter to control how values are displayed.
    /// </summary>
    public static void CustomConverterExample()
    {
        var priority = new TextPrompt<int>("Select [green]priority[/] level:")
            .AddChoice(1)
            .AddChoice(2)
            .AddChoice(3)
            .AddChoice(4)
            .AddChoice(5)
            .WithConverter(level => level switch
            {
                1 => "Critical",
                2 => "High",
                3 => "Medium",
                4 => "Low",
                5 => "Trivial",
                _ => level.ToString()
            });

        var result = AnsiConsole.Prompt(priority);

        AnsiConsole.MarkupLine($"Priority level: [blue]{result}[/]");
    }
}
