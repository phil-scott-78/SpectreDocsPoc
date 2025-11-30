---
title: "JsonText Widget"
description: "Render JSON data with syntax highlighting and customizable colors"
uid: "console-widget-json"
order: 3750
---

The JsonText widget displays JSON data with automatic syntax highlighting and formatting.

<Screenshot src="/assets/json.svg" />

## When to Use

Use JsonText when you need to **display structured JSON data in the console**. Common scenarios:

- **API responses**: Show JSON responses from web services with readable formatting
- **Configuration files**: Display JSON configuration files with syntax highlighting
- **Debug output**: Pretty-print JSON data structures during development
- **Logging**: Output structured log data in JSON format with visual distinction

For **hierarchical data** without JSON syntax, use [Tree](/console/widgets/tree) instead. For **plain text with styling**, use [Text](/console/widgets/text) instead.

## Basic Usage

Pass a JSON string to the constructor. The widget automatically parses and highlights the syntax.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.BasicJsonTextExample
```

## Working with Complex JSON

### Nested Objects and Arrays

JsonText automatically handles nested structures, maintaining proper indentation and syntax highlighting throughout.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.NestedJsonTextExample
```

### Different Value Types

JsonText applies distinct colors to strings, numbers, booleans, and null values for easy visual scanning.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextDataTypesExample
```

## Customizing Colors

### Member Names and Values

Use `MemberColor()` to change property name colors and value type methods to customize how data appears.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextMemberStylingExample
```

### Value Type Colors

Apply different colors to each JSON value type for custom color schemes or to match your application's theme.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextValueStylingExample
```

### Punctuation Colors

Customize braces, brackets, colons, and commas to adjust readability or visual weight.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextPunctuationStylingExample
```

## Advanced Styling

### Using Styles Instead of Colors

Use `Style` objects for additional control like bold, italic, or underline decorations on JSON elements.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextStylesExample
```

### Embedding in Containers

Combine JsonText with panels or other containers for better presentation and context.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextInPanelExample
```

## Real-World Examples

### API Response Display

Use JsonText to display API responses with clear visual distinction between property names and values.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextApiResponseExample
```

### Configuration File Display

Display configuration files with colors that make the structure easy to understand at a glance.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.JsonTextExamples.JsonTextConfigurationExample
```

## See Also

- [Tree Widget](/console/widgets/tree) - Hierarchical data without JSON syntax
- [Panel Widget](/console/widgets/panel) - Wrap JSON in bordered containers
- [Color Reference](/console/reference/color-reference) - Colors for syntax highlighting
- [Text Style Reference](/console/reference/text-style-reference) - Style decorations

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Json.JsonText" />
