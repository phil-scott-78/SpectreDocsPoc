using Spectre.Console;
using Spectre.Console.Json;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Demonstrates JsonText for syntax-highlighted JSON.</summary>
public class JsonSample : BaseSample
{
    /// <inheritdoc />
    public override void Run(IAnsiConsole console)
    {
        Panel[] outputs =
        [
            new Panel(new JsonText("""{"name": "Alice", "age": 28}"""))
                .Header("[yellow]Simple JSON[/]"),

            new Panel(new JsonText("""
                {
                    "user": {
                        "name": "Bob",
                        "email": "bob@example.com"
                    }
                }
                """))
                .Header("[yellow]Nested Objects[/]"),

            new Panel(new JsonText("""
                {
                    "items": [1, 2, 3, 4, 5],
                    "tags": ["red", "blue", "green"]
                }
                """))
                .Header("[yellow]With Arrays[/]"),

            new Panel(new JsonText("""
                {
                    "id": 42,
                    "active": true,
                    "score": 98.6,
                    "metadata": {
                        "created": "2024-01-01",
                        "tags": ["new", "featured"]
                    }
                }
                """))
                .Header("[yellow]Complex Structure[/]"),

            new Panel(new JsonText("""
                {
                    "name": "Test",
                    "value": null,
                    "optional": null
                }
                """))
                .Header("[yellow]With Nulls[/]"),

            new Panel(new JsonText("""
                {
                    "count": 100,
                    "price": 49.99,
                    "enabled": true,
                    "verified": false
                }
                """))
                .Header("[yellow]Numbers & Booleans[/]")
        ];

        // Animate
        console.Live(new Layout("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                output.Expand = true;
                output.Padding = new Padding(1);
                output.Border = BoxBorder.Rounded;
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(3000);
            }
        });
    }
}
