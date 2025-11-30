---
title: "BreakdownChart Widget"
description: "Display proportional data as a colored bar chart with optional legend"
uid: "console-widget-breakdown-chart"
order: 3650
---

The BreakdownChart widget displays proportional data as a single horizontal bar divided into colored segments.

<Screenshot src="/assets/breakdown-chart.svg" />

## When to Use

Use BreakdownChart when you need to show **how parts make up a whole**. Common scenarios:

- **Resource allocation**: Disk space used vs. available, memory distribution
- **Composition**: Market share breakdown, budget allocation by category
- **Status overview**: Tasks completed vs. in progress vs. pending

For **comparing independent values** (where items don't sum to a meaningful total), use [BarChart](/console/widgets/bar-chart) instead.

## Basic Usage

Add items with a label, value, and color. The bar automatically scales segments proportionally.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BasicBreakdownChartExample
```

## Displaying Values

### As Percentages

Use `ShowPercentage()` when relative proportions matter more than absolute numbers.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartPercentageExample
```

### With Custom Formatting

Use `UseValueFormatter()` to add units or custom formatting.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartFormattingExample
```

## Controlling the Legend

The legend (tags) below the chart shows labels and values. Control visibility when space is limited or when the chart speaks for itself.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartTagsExample
```

## Layout

### Compact vs Full-Size

Use `Compact()` (default) for tight layouts. Use `FullSize()` to add spacing between the bar and legend for better readability.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartCompactExample
```

### Fixed Width

By default the chart expands to fill available width. Use `Width()` to constrain it.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartWidthExample
```

## Working with Data Collections

Use `AddItems()` to add multiple items from a collection of `BreakdownChartItem` objects.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BreakdownChartExamples.BreakdownChartAddItemsExample
```

## See Also

- [How to Draw Charts and Diagrams](/console/how--to/drawing-charts-and-diagrams) - Step-by-step guide for creating charts
- [BarChart Widget](/console/widgets/bar-chart) - For comparing independent values
- [Color Reference](/console/reference/color-reference) - Available colors for segments
- [Getting Started Tutorial](/console/tutorials/getting-started-building-rich-console-app) - Learn Spectre.Console basics

## API Reference

<WidgetApiReference TypeName="Spectre.Console.BreakdownChart" />
