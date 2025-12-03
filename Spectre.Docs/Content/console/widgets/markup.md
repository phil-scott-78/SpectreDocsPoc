---
title: "Markup Widget"
description: "Render styled text using an inline markup syntax"
uid: "console-widget-markup"
order: 3005
---

The Markup widget renders text with colors and decorations using an inline tag syntax like `[bold red]text[/]`.

## When to Use

Use Markup when you want **inline styling with readable syntax**. Common scenarios:

- **Status messages**: Quick colored output like `[green]Success[/]` or `[red]Error[/]`
- **Formatted output**: Mix styles naturally within text
- **Static styling**: When styles are known at compile time

For **programmatic control over styling** or when styles are determined at runtime, use [Text](xref:console-widget-text) instead.

## Basic Usage

Use `AnsiConsole.MarkupLine()` for quick styled output.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.MarkupExamples.BasicMarkupExample
```

## Creating Markup Objects

Create `Markup` objects when you need to embed styled text in containers like panels, tables, or layouts.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.MarkupExamples.MarkupObjectExample
```

## Escaping Content

Use `Markup.Escape()` when working with user-provided or dynamic content that might contain bracket characters. Without escaping, brackets are interpreted as markup tags.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.MarkupExamples.MarkupEscapeExample
```

## Working with Containers

Markup objects work well as content inside panels, tables, and other container widgets.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.MarkupExamples.MarkupInContainersExample
```

## Removing Markup

Use `Markup.Remove()` to strip all markup tags from a string, leaving only plain text.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.MarkupExamples.MarkupRemoveExample
```

## See Also

- <xref:console-widget-text> - Programmatic styling with Style objects
- <xref:console-color-reference> - All available color names
- <xref:console-text-styles> - All decoration options (bold, italic, etc.)
- <xref:console-getting-started> - Tutorial for learning the markup language

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Markup" />
