---
title: "Markup Reference"
description: "Complete reference for Spectre.Console's inline markup syntax including tags, colors, styles, links, and escaping"
uid: "console-markup-reference"
order: 7010
---

Spectre.Console uses an inline markup syntax to style text with colors, decorations, and hyperlinks. This reference covers the complete grammar and all available options.

## Basic Syntax

Markup tags use square brackets: `[style]text[/]`

```csharp
AnsiConsole.MarkupLine("[red]Error message[/]");
AnsiConsole.MarkupLine("[bold]Important text[/]");
AnsiConsole.MarkupLine("[blue on white]Highlighted[/]");
```

The closing tag `[/]` returns to the default style. Every opening tag needs a matching close.

## Color Formats

### Named Colors

Use any of the 256 named colors directly:

```csharp
AnsiConsole.MarkupLine("[red]Red text[/]");
AnsiConsole.MarkupLine("[deepskyblue1]Sky blue text[/]");
AnsiConsole.MarkupLine("[grey]Muted text[/]");
```

See [Color Reference](/console/reference/color-reference) for the complete list.

### Hex Colors

Use `#RRGGBB` format for precise colors:

```csharp
AnsiConsole.MarkupLine("[#FF5733]Orange-red text[/]");
AnsiConsole.MarkupLine("[#00FF00]Bright green text[/]");
AnsiConsole.MarkupLine("[#7B68EE]Medium slate blue[/]");
```

### RGB Colors

Use `rgb(r,g,b)` format with values from 0-255:

```csharp
AnsiConsole.MarkupLine("[rgb(255,87,51)]Orange-red text[/]");
AnsiConsole.MarkupLine("[rgb(0,255,0)]Bright green text[/]");
```

### Background Colors

Use `on` to set the background color:

```csharp
AnsiConsole.MarkupLine("[white on red]White text on red background[/]");
AnsiConsole.MarkupLine("[black on #FFFF00]Black on yellow[/]");
AnsiConsole.MarkupLine("[on blue]Default text on blue background[/]");
```

## Text Decorations

Apply text decorations to change how text appears:

| Decoration | Example | Description |
|------------|---------|-------------|
| `bold` | `[bold]text[/]` | Bold/bright text |
| `dim` | `[dim]text[/]` | Dimmed/faint text |
| `italic` | `[italic]text[/]` | Italic text |
| `underline` | `[underline]text[/]` | Underlined text |
| `strikethrough` | `[strikethrough]text[/]` | Strikethrough text |
| `invert` | `[invert]text[/]` | Swap foreground/background |
| `conceal` | `[conceal]text[/]` | Hidden text (for passwords) |
| `slowblink` | `[slowblink]text[/]` | Slowly blinking text |
| `rapidblink` | `[rapidblink]text[/]` | Rapidly blinking text |

## Combining Styles

Separate multiple styles with spaces:

```csharp
AnsiConsole.MarkupLine("[bold red]Bold red text[/]");
AnsiConsole.MarkupLine("[italic underline blue]Styled text[/]");
AnsiConsole.MarkupLine("[bold yellow on darkblue]Warning banner[/]");
```

The order doesn't matter: `[bold red]` and `[red bold]` are equivalent.

## Hyperlinks

Create clickable links with the `link` attribute:

```csharp
// Link with URL as display text
AnsiConsole.MarkupLine("[link]https://spectreconsole.net[/]");

// Link with custom display text
AnsiConsole.MarkupLine("[link=https://spectreconsole.net]Spectre.Console Docs[/]");

// Link with styles
AnsiConsole.MarkupLine("[blue underline link=https://github.com]GitHub[/]");
```

> [!NOTE]
> Hyperlinks require terminal support. Windows Terminal, iTerm2, and many modern terminals support them. Legacy terminals display the text without the link functionality.

## Nesting Tags

Tags can be nested to apply additional styles:

```csharp
AnsiConsole.MarkupLine("[blue]Blue [bold]and bold[/] just blue[/]");
AnsiConsole.MarkupLine("[red]Error: [underline]file.txt[/] not found[/]");
```

Each `[/]` closes the most recent tag:

```
[blue]Blue text [bold]bold blue text[/] back to blue[/] default
```

## Escaping Special Characters

### Escaping Brackets

Double the brackets to display literal `[` and `]`:

```csharp
AnsiConsole.MarkupLine("Array syntax: [[0]], [[1]], [[2]]");
// Output: Array syntax: [0], [1], [2]

AnsiConsole.MarkupLine("Tags look like [[bold]]text[[/]]");
// Output: Tags look like [bold]text[/]
```

### Escaping Dynamic Content

Use `Markup.Escape()` for user input or dynamic strings that might contain brackets:

```csharp
string userInput = "Config [debug] enabled";
string safe = Markup.Escape(userInput);
AnsiConsole.MarkupLine($"[green]Status:[/] {safe}");
```

Without escaping, `[debug]` would be interpreted as a (invalid) style tag and throw an exception.

### Using EscapeInterpolated

For interpolated strings, use `MarkupLineInterpolated()` which automatically escapes interpolated values:

```csharp
string filename = "data[1].json";
int count = 42;
AnsiConsole.MarkupLineInterpolated($"[green]Processed[/] {filename}: {count} items");
```

This is safer and cleaner than manually escaping each value.

## Removing Markup

Strip all markup tags from a string with `Markup.Remove()`:

```csharp
string styled = "[bold red]Error:[/] Something failed";
string plain = Markup.Remove(styled);
// Result: "Error: Something failed"
```

Useful for logging plain text versions or calculating display width.

## Emoji Support

Use emoji shortcodes with colons:

```csharp
AnsiConsole.MarkupLine(":check_mark: Task completed");
AnsiConsole.MarkupLine(":warning: Proceed with caution");
AnsiConsole.MarkupLine(":rocket: Deploying...");
```

See [Emoji Reference](/console/reference/emoji-reference) for available shortcodes.

## Common Pitfalls

### Unclosed Tags

Every `[style]` needs a matching `[/]`:

```csharp
// Wrong - unclosed tag
AnsiConsole.MarkupLine("[red]Error message");

// Correct
AnsiConsole.MarkupLine("[red]Error message[/]");
```

### Unescaped Brackets

User input with brackets will cause parsing errors:

```csharp
string input = "array[0]";

// Wrong - throws exception
AnsiConsole.MarkupLine($"Value: {input}");

// Correct - escape the input
AnsiConsole.MarkupLineInterpolated($"Value: {input}");
// Or
AnsiConsole.MarkupLine($"Value: {Markup.Escape(input)}");
```

### Invalid Style Names

Misspelled or invalid style names throw exceptions:

```csharp
// Wrong - "rde" is not a valid color
AnsiConsole.MarkupLine("[rde]Error[/]");

// Correct
AnsiConsole.MarkupLine("[red]Error[/]");
```

### Case Sensitivity

Style names are **case-insensitive**:

```csharp
// All equivalent
AnsiConsole.MarkupLine("[RED]text[/]");
AnsiConsole.MarkupLine("[Red]text[/]");
AnsiConsole.MarkupLine("[red]text[/]");
```

## Quick Reference Table

| Syntax | Description |
|--------|-------------|
| `[red]text[/]` | Foreground color |
| `[on blue]text[/]` | Background color |
| `[red on white]text[/]` | Foreground and background |
| `[bold]text[/]` | Decoration |
| `[bold red]text[/]` | Combined styles |
| `[#FF0000]text[/]` | Hex color |
| `[rgb(255,0,0)]text[/]` | RGB color |
| `[link=url]text[/]` | Hyperlink |
| `[[text]]` | Escaped brackets |
| `:emoji:` | Emoji shortcode |

## See Also

- [Color Reference](/console/reference/color-reference) - Complete list of named colors
- [Text Style Reference](/console/reference/text-style-reference) - All text decorations
- [Emoji Reference](/console/reference/emoji-reference) - Emoji shortcodes
- [Markup Widget](/console/widgets/markup) - API reference and advanced usage
- [Terminal Compatibility](/console/reference/compatibility-matrix) - Feature support by terminal
