---
title: "Panel Widget"
description: "Create bordered boxes around content with customizable headers, padding, and styles"
uid: "console-widget-panel"
order: 3050
---

The Panel widget wraps content in a decorative border with optional header and styling.

<Screenshot src="/assets/panel.svg" />

## When to Use

Use Panel when you need to **visually group or highlight content**. Common scenarios:

- **Status displays**: Show system status, alerts, or important messages
- **Section headers**: Group related information with a titled border
- **Emphasized content**: Draw attention to specific output

For **tabular data with rows and columns**, use [Table](/console/widgets/table) instead. For **side-by-side content**, use [Columns](/console/widgets/columns).

## Basic Usage

Create a panel by passing text or any renderable content.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.BasicPanelExample
```

## Headers

Add a title to identify the panel's content.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelHeaderExample
```

### Header Alignment

Position headers on the left, center, or right of the panel border.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelHeaderAlignmentExample
```

## Borders

### Border Styles

Choose a border style to match your application's visual tone.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelBorderStylesExample
```

> [!NOTE]
> See the [Box Border Reference](/console/reference/box-border-reference) for all available border styles.

### Removing the Border

Use `NoBorder()` when you want padding and headers without a visible border.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelNoBorderExample
```

### Border Color

Apply color to make panels stand out or convey meaning.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelBorderColorExample
```

## Layout

### Padding

Control the space between content and the border.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelPaddingExample
```

### Expanding to Fill Width

By default, panels fit their content. Use `Expand()` to fill the available width.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelExpandExample
```

### Fixed Width

Set an explicit width when you need consistent sizing.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelWidthExample
```

## Nesting Content

### Nested Panels

Create visual hierarchy by placing panels inside panels.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelNestingExample
```

### Tables in Panels

Combine panels with other widgets like tables for structured displays.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelWithTableExample
```

## Combining Options

Panels support combining multiple styling options for rich displays.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PanelExamples.PanelFullySyledExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Panel" />
