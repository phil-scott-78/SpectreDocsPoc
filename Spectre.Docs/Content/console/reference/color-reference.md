---
title: "Color Reference"
description: "A comprehensive reference of color usage in Spectre.Console"
uid: "console-color-reference"
order: 7000
---

Spectre.Console provides a comprehensive set of named colors that can be used throughout the library.
These colors work with markup strings, styling, and all color-enabled components.

## Usage Example

```csharp
// Using named colors
AnsiConsole.MarkupLine("[red]Error:[/] Something went wrong");
AnsiConsole.MarkupLine("[blue on yellow]Highlighted text[/]");

// Using Color struct
var style = new Style(Color.Aqua, Color.Black);
AnsiConsole.Write("Styled text", style);
```

## Important Notes

- **Terminal Support:** Color rendering depends on your terminal's capabilities. Modern terminals support 256 colors or true color (24-bit).
- **Hex Values:** Each color has an associated hex value that represents its RGB components.
- **Fallback:** Spectre.Console automatically handles color degradation for terminals with limited color support.

## Available Colors

<ColorList />

## See Also

- <xref:console-markup-reference> - Using colors in markup syntax
- <xref:console-widget-text> - Programmatic color styling
- <xref:console-getting-started> - Learn color basics