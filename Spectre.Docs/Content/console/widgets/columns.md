---
title: "Columns Widget"
description: "Display content side-by-side in columns with automatic width distribution"
uid: "console-widget-columns"
order: 3200
---

The Columns widget arranges content side-by-side with automatic width calculation and wrapping.

<Screenshot src="/assets/columns.svg" />

## When to Use

Use Columns when you need to **display multiple items horizontally with automatic layout**. Common scenarios:

- **Dashboard layouts**: Show metrics, status panels, or cards side-by-side
- **Menu or navigation items**: Display options in a compact horizontal layout
- **Comparison views**: Place related information side-by-side for easy comparison
- **Responsive content**: Let items automatically wrap to new rows based on available width

For **precise control over column widths and alignment**, use [Grid](xref:console-widget-grid) instead. For **dividing a fixed height into proportional sections**, use [Layout](xref:console-widget-layout) instead.

## Basic Usage

Pass any collection of renderables to create columns. Columns automatically calculates how many items fit per row based on available width.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.BasicColumnsExample
```

## Creating Columns

### From Strings

Create columns directly from strings, which are automatically converted to markup.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsFromStringsExample
```

### From Mixed Content

Combine different renderable types (panels, tables, markup) in a single column layout.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsMixedContentExample
```

## Width Behavior

### Expand vs Collapse

Use `Expand` (default: `true`) to fill available width with evenly distributed columns. Use `Collapse()` to fit columns to their content width.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsExpandExample
```

## Controlling Spacing

Adjust the `Padding` property to control the gap between columns. Default padding is 1 space on the right.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsPaddingExample
```

## Automatic Wrapping

Columns automatically wraps items to new rows when they exceed available console width, adjusting the column count dynamically.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsWrappingExample
```

## Advanced Usage

### Building Dashboard Layouts

Combine multiple Columns widgets to create complex multi-row dashboard layouts.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsDashboardExample
```

### Using Fluent Extensions

Use extension methods like `Collapse()` and `Expand()` for cleaner configuration.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.ColumnsExamples.ColumnsFluentExample
```

## See Also

- <xref:console-howto-organizing-layout> - Layout patterns and recipes
- <xref:console-widget-grid> - Precise control over column widths
- <xref:console-widget-rows> - Vertical stacking
- <xref:console-widget-layout> - Complex multi-section layouts
- <xref:console-getting-started> - Learn Spectre.Console basics

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Columns" />
