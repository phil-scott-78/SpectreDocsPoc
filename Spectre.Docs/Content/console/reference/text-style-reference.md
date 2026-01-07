---
title: "Text Style Reference"
description: "A comprehensive reference of text styles and decoration options in Spectre.Console"
uid: "console-text-styles"
order: 7050
---

Spectre.Console provides various text styles that can be used to decorate and format console output.
These styles work with markup strings and the `Style` class to create visually rich terminal applications.

## Usage Example

```csharp
// Using markup with styles
AnsiConsole.MarkupLine("[bold red]Error:[/] Something went wrong");
AnsiConsole.MarkupLine("[italic]Emphasis text[/]");
AnsiConsole.MarkupLine("[underline blue]Link text[/]");

// Combining multiple styles
AnsiConsole.MarkupLine("[bold underline]Important[/]");

// Using Style class
var style = new Style(Color.White, decoration: Decoration.Bold | Decoration.Underline);
AnsiConsole.Write("Styled text", style);
```
> [!IMPORTANT]
> Terminal support for styles varies across different systems and terminal applications. Always test styles in your target environment to ensure they render as expected.

## Available Text Styles

| Style | Description |
|-------|-------------|
| `bold` | Bold text |
| `dim` | Dim or faint text |
| `italic` | Italic text |
| `underline` | Underlined text |
| `invert` | Swaps the foreground and background colors |
| `conceal` | Hides the text |
| `slowblink` | Makes text blink slowly |
| `rapidblink` | Makes text blink rapidly |
| `strikethrough` | Shows text with a horizontal line through the center |
| `link` | Creates a clickable link within text |

## See Also

- <xref:console-markup-reference> - Using styles in markup syntax
- <xref:console-color-reference> - Available colors
- <xref:console-widget-text> - Programmatic style control
- <xref:console-getting-started> - Learn styling basics
