---
title: "Padder Widget"
description: "Add padding around any renderable content"
uid: "console-widget-padder"
order: 3450
---

The Padder widget wraps any renderable content with configurable padding on all sides.

<Screenshot src="/assets/padder.svg" />

## When to Use

Use Padder when you need to **control whitespace and spacing around content**. Common scenarios:

- **Visual separation**: Add breathing room between consecutive widgets or sections
- **Indentation and hierarchy**: Create visual structure by indenting nested content
- **Fine-tuned layout**: Achieve precise spacing when containers like Panel have fixed padding

For **centering content horizontally or vertically**, use [Align](/console/widgets/align) instead. For **content with a border and built-in padding**, use [Panel](/console/widgets/panel) instead.

## Basic Usage

Wrap any renderable with `new Padder(content)`. By default, adds 1 space padding on all sides.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.BasicPadderExample
```

## Setting Padding

### Uniform Padding

Pass a single value to `new Padding()` to apply the same padding on all four sides.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderUniformPaddingExample
```

### Horizontal and Vertical

Use the two-parameter constructor to set horizontal (left/right) and vertical (top/bottom) padding independently.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderHorizontalVerticalPaddingExample
```

### Individual Sides

Use the four-parameter constructor to control each side precisely when you need asymmetric spacing.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderIndividualSidesExample
```

## Using Extension Methods

Use the fluent `PadLeft()`, `PadRight()`, `PadTop()`, and `PadBottom()` extension methods to adjust individual sides after construction.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderFluentExtensionsExample
```

## Working with Other Widgets

### With Panels

Wrap panels with padding to add outer spacing when the panel's internal padding isn't sufficient.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderWithPanelExample
```

### With Tables

Add padding around tables to separate them from surrounding content or create margins.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderWithTableExample
```

## Controlling Width

By default, Padder automatically calculates width based on content. Set `Expand = true` to fill the available width, which affects how padding appears.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderExpandExample
```

## Advanced Usage

### Nested Padding

Nest multiple Padder instances to create compound spacing effects or complex layouts.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderNestedExample
```

### Creating Visual Structure

Use targeted padding on specific sides to build structured layouts with headers, body content, and footers.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.PadderExamples.PadderVisualSeparationExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Padder" />
