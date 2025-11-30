---
title: "BarChart Widget"
description: "Display data as horizontal bars with labels, values, and colors"
uid: "console-widget-bar-chart"
order: 3600
---

The BarChart widget visualizes numeric data as horizontal bars with labels and optional values.

<Screenshot src="/assets/bar-chart.svg" />

## When to Use

Use BarChart when you need to **compare discrete values across categories**. Common scenarios:

- **Ranking or comparison**: Show sales by region, votes by candidate, or scores by team
- **Progress tracking**: Display completion percentages or metrics against targets
- **Survey results**: Visualize response counts or ratings

For **part-to-whole relationships** (showing how pieces make up a total), use [BreakdownChart](/console/widgets/breakdown-chart) instead.

## Basic Usage

Add items with a label, value, and optional color.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BasicBarChartExample
```

## Adding a Title

Use `Label()` to add context above the chart. Supports markup for styling.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartLabelExample
```

Align the label with `LeftAlignLabel()`, `CenterLabel()`, or `RightAlignLabel()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartLabelAlignmentExample
```

## Customizing Values

### Formatting

When displaying currency, percentages, or units, use `UseValueFormatter()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartFormattingExample
```

### Hiding Values

Use `HideValues()` when the visual comparison matters more than exact numbers.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartValuesExample
```

## Scaling

By default, bars scale relative to the largest value. Use `WithMaxValue()` to set a fixed scale—useful when comparing against a target or showing progress toward a goal.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartMaxValueExample
```

## Working with Data Collections

Use `AddItems()` to add multiple items from a collection of `BarChartItem` objects.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.BarChartExamples.BarChartAddItemsExample
```

## See Also

- [How to Draw Charts and Diagrams](/console/how--to/drawing-charts-and-diagrams) - Step-by-step guide for creating charts
- [BreakdownChart Widget](/console/widgets/breakdown-chart) - For part-to-whole relationships
- [Color Reference](/console/reference/color-reference) - Available colors for bars
- [Getting Started Tutorial](/console/tutorials/getting-started-building-rich-console-app) - Learn Spectre.Console basics

## API Reference

<WidgetApiReference TypeName="Spectre.Console.BarChart" />
