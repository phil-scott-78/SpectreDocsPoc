using Spectre.Console;
using Spectre.Console.Json;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class JsonTextExamples
{
    /// <summary>
    /// Demonstrates creating a basic JSON text widget with syntax highlighting.
    /// </summary>
    public static void BasicJsonTextExample()
    {
        var json = """
        {
            "name": "Spectre.Console",
            "version": "0.54.0",
            "active": true,
            "downloads": 15000000
        }
        """;

        var jsonText = new JsonText(json);
        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates rendering nested JSON objects and arrays.
    /// </summary>
    public static void NestedJsonTextExample()
    {
        var json = """
        {
            "package": "Spectre.Console",
            "tags": ["cli", "console", "terminal"],
            "maintainers": [
                {
                    "name": "Patrik Svensson",
                    "role": "Lead Developer"
                },
                {
                    "name": "Community Contributors",
                    "role": "Contributors"
                }
            ],
            "license": "MIT"
        }
        """;

        var jsonText = new JsonText(json);
        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates customizing colors for JSON member names and values.
    /// </summary>
    public static void JsonTextMemberStylingExample()
    {
        var json = """
        {
            "status": "success",
            "message": "Operation completed",
            "code": 200
        }
        """;

        var jsonText = new JsonText(json)
            .MemberColor(Color.Cyan)
            .StringColor(Color.Yellow);

        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates customizing colors for different JSON value types.
    /// </summary>
    public static void JsonTextValueStylingExample()
    {
        var json = """
        {
            "string": "Hello World",
            "number": 42,
            "boolean": true,
            "null": null,
            "decimal": 3.14159
        }
        """;

        var jsonText = new JsonText(json)
            .StringColor(Color.Green)
            .NumberColor(Color.Blue)
            .BooleanColor(Color.Yellow)
            .NullColor(Color.Red);

        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates customizing punctuation colors (braces, brackets, colons, commas).
    /// </summary>
    public static void JsonTextPunctuationStylingExample()
    {
        var json = """
        {
            "items": [1, 2, 3],
            "nested": {
                "key": "value"
            }
        }
        """;

        var jsonText = new JsonText(json)
            .BracesColor(Color.Magenta)      // {}
            .BracketColor(Color.Purple)      // []
            .ColonColor(Color.Cyan)          // :
            .CommaColor(Color.Grey);         // ,

        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates using styles instead of colors for more control over formatting.
    /// </summary>
    public static void JsonTextStylesExample()
    {
        var json = """
        {
            "important": "This is critical",
            "count": 99
        }
        """;

        var jsonText = new JsonText(json)
            .MemberStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
            .StringStyle(new Style(Color.Green, decoration: Decoration.Italic));

        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates embedding JSON in a panel for better presentation.
    /// </summary>
    public static void JsonTextInPanelExample()
    {
        var json = """
        {
            "api": "https://api.example.com",
            "timeout": 30,
            "retries": 3,
            "enabled": true
        }
        """;

        var jsonText = new JsonText(json);

        var panel = new Panel(jsonText)
            .Header("[yellow]Configuration[/]")
            .BorderColor(Color.Grey)
            .Padding(1, 1);

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates displaying API response data with JSON highlighting.
    /// </summary>
    public static void JsonTextApiResponseExample()
    {
        var json = """
        {
            "status": "success",
            "data": {
                "id": "usr_1234567890",
                "email": "user@example.com",
                "verified": true,
                "created_at": "2024-01-15T10:30:00Z",
                "permissions": ["read", "write"]
            },
            "metadata": {
                "request_id": "req_abc123",
                "duration_ms": 45
            }
        }
        """;

        var jsonText = new JsonText(json)
            .MemberColor(Color.Cyan)
            .StringColor(Color.Green)
            .NumberColor(Color.Blue)
            .BooleanColor(Color.Yellow);

        AnsiConsole.MarkupLine("[bold]API Response:[/]");
        AnsiConsole.Write(jsonText);
    }

    /// <summary>
    /// Demonstrates displaying configuration files with custom color scheme.
    /// </summary>
    public static void JsonTextConfigurationExample()
    {
        var json = """
        {
            "database": {
                "host": "localhost",
                "port": 5432,
                "name": "myapp_db",
                "ssl": true
            },
            "cache": {
                "enabled": true,
                "ttl": 3600,
                "provider": "redis"
            },
            "logging": {
                "level": "info",
                "output": "console"
            }
        }
        """;

        var jsonText = new JsonText(json)
            .MemberColor(Color.Yellow)
            .StringColor(Color.White)
            .NumberColor(Color.Aqua)
            .BooleanColor(Color.Lime);

        var panel = new Panel(jsonText)
            .Header("[green]app.config.json[/]")
            .BorderColor(Color.Green);

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Demonstrates formatting JSON data with different value types.
    /// </summary>
    public static void JsonTextDataTypesExample()
    {
        var json = """
        {
            "string": "Text value",
            "integer": 42,
            "float": 3.14159,
            "boolean_true": true,
            "boolean_false": false,
            "null_value": null,
            "array": [1, 2, 3, 4, 5],
            "empty_array": [],
            "empty_object": {}
        }
        """;

        var jsonText = new JsonText(json);
        AnsiConsole.Write(jsonText);
    }
}
