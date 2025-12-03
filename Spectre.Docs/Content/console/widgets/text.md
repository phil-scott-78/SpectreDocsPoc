---
title: "Text Widget"
description: "Render styled text with precise control over formatting and overflow"
uid: "console-widget-text"
order: 3000
---

The Text widget renders text content with programmatic control over styling, justification, and overflow behavior.

## When to Use

Use Text when you need **programmatic control over styling** or when styles are determined at runtime. Common scenarios:

- **Status messages**: Apply colors based on success/failure state
- **Computed styles**: Build styles dynamically from variables or configuration
- **Container content**: Embed styled text inside panels, tables, or other widgets

For **inline markup syntax** like `[bold red]text[/]`, use [Markup](xref:console-widget-markup) instead.

## Basic Usage

Create text with a string and optional style.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.BasicTextExample
```

### Multi-line Text

Use newline characters (`\n`) to create multi-line output.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.MultiLineTextExample
```

### Static Members

Use `Text.Empty` and `Text.NewLine` for reusable empty text and line break instances.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.EmptyTextExample
```

## Justification

Use justification to align text within containers like panels.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextJustificationExample
```

Set justification via the property when you need to configure it separately from construction.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextJustificationPropertyExample
```

## Overflow

Control what happens when text exceeds available width.

- **Fold** - Wraps text to the next line (default)
- **Crop** - Truncates text at the boundary
- **Ellipsis** - Truncates and adds an ellipsis character (…)

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextOverflowExample
```

Set overflow via the property for separate configuration.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextOverflowPropertyExample
```

## Styling

### Colors

Apply colors to make text stand out or convey meaning.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextColorsExample
```

> [!NOTE]
> See the [Color Reference](xref:console-color-reference) for all available colors and the [Text Style Reference](xref:console-text-styles) for decoration options.

### Decorations

Use decorations to emphasize text: Bold, Italic, Underline, and Strikethrough.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextDecorationsExample
```

Advanced decorations like Dim, Invert, Conceal, and Blink are available but may not work in all terminals.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextAdvancedDecorationsExample
```

### Combined Styles

Combine multiple decorations using bitwise flags. Styles can include foreground, background, and decorations together.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextCombinedStylesExample
```

### Style Construction

Build styles with the `Style` constructor for full control, or use `Style.Parse()` for a compact string syntax.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextStyleConstructorExample
```

## Properties

Use `Length` and `Lines` to inspect text dimensions when building layouts or calculating sizes.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextPropertiesExample
```

## Working with Containers

Text widgets work well as content inside panels, tables, and other container widgets.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextExamples.TextInContainersExample
```

## See Also

- <xref:console-widget-markup> - Inline markup syntax alternative
- <xref:console-color-reference> - All available colors
- <xref:console-text-styles> - Decoration options (bold, italic, etc.)
- <xref:console-getting-started> - Learn markup and styling basics
- <xref:console-rendering-model> - How text rendering works

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Text" />
