---
title: "TextPath Widget"
description: "Display file paths with intelligent truncation and component styling"
uid: "console-widget-text-path"
order: 3050
---

The TextPath widget renders file system paths with smart truncation and granular styling for each path component.

<Screenshot src="/assets/text-path.svg" />

## When to Use

Use TextPath when you need to **display file system paths** with intelligent formatting. Common scenarios:

- **Build output**: Show source file locations in compiler messages
- **File browsers**: Display paths that may exceed available width
- **Breadcrumb navigation**: Render hierarchical location indicators

For **plain text without path semantics**, use [Text](/console/widgets/text) or [Markup](/console/widgets/markup) instead. For **directory tree structures**, use [Tree](/console/widgets/tree).

## Basic Usage

Pass a file path string to the constructor. TextPath normalizes separators automatically.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.BasicTextPathExample
```

## Styling Components

TextPath lets you style four distinct components independently:
- **Root**: Drive letter (`C:`) or Unix root (`/`)
- **Separator**: Path delimiters (`/`)
- **Stem**: Intermediate directories
- **Leaf**: Final segment (usually the filename)

### Colors

Use convenience methods to set foreground colors for each component.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.TextPathColorsExample
```

### Full Styles

Apply complete styles including background colors, decorations like bold or underline.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.TextPathStylesExample
```

## Alignment

Control text alignment with `LeftJustified()`, `Centered()`, or `RightJustified()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.TextPathAlignmentExample
```

## Smart Truncation

When a path exceeds available width, TextPath intelligently truncates by:
1. Preserving the root (if present)
2. Preserving the leaf (filename)
3. Replacing middle segments with ellipsis (`…`)

This ensures the most important parts—where the file is rooted and what file it is—remain visible.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.TextPathTruncationExample
```

## Cross-Platform Paths

TextPath handles both Windows and Unix path formats, normalizing separators for consistent display.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TextPathExamples.TextPathUnixExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.TextPath" />
