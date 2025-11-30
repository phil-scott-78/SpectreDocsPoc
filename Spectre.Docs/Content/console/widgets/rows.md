---
title: "Rows Widget"
description: "Stack multiple renderables vertically with consistent spacing"
uid: "console-widget-rows"
order: 3350
---

The Rows widget stacks multiple renderables vertically, creating organized layouts where each item appears on its own line.

<Screenshot src="/assets/rows.svg" />

## When to Use

Use Rows when you need to **arrange multiple widgets vertically in a single renderable unit**. Common scenarios:

- **Multi-section layouts**: Stack headers, content, and footers in a logical flow
- **Status dashboards**: Organize different information panels vertically
- **Form-like displays**: Present related information in a top-to-bottom sequence
- **Combining with other layouts**: Create grid-like structures by nesting Rows within [Columns](/console/widgets/columns)

For **horizontal arrangement**, use [Columns](/console/widgets/columns) instead. For **precise control over rows and columns with cell alignment**, use [Grid](/console/widgets/grid) instead.

## Basic Usage

Pass any collection of renderables to stack them vertically. Each item is rendered on a new line.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.BasicRowsExample
```

## Stacking Widgets

### Panels and Containers

Stack panels or other container widgets to create visually distinct sections.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsPanelsExample
```

### Mixed Content Types

Combine different widget types (tables, charts, rules) to build rich information displays.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsMixedContentExample
```

## Width Behavior

Use the `Expand` property to control whether rows fill the available console width or fit to their content. When `Expand` is `false`, each row's width matches its content.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsExpandExample
```

## Creating from Collections

Build rows dynamically from a collection of renderables, useful when the number of items varies.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsFromCollectionExample
```

## Advanced Usage

### Combining with Columns

Nest Rows and Columns to create complex grid-like layouts without using Grid's more verbose API.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsWithColumnsExample
```

### Building Dashboards

Create multi-section status dashboards by stacking different types of information displays.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RowsExamples.RowsDashboardExample
```

## See Also

- [How to Organize Layout with Panels and Grids](/console/how--to/organizing-layout-with-panels-and-grids) - Layout patterns and recipes
- [Columns Widget](/console/widgets/columns) - Horizontal arrangement
- [Grid Widget](/console/widgets/grid) - Precise row and column control
- [Layout Widget](/console/widgets/layout) - Complex multi-section layouts
- [Getting Started Tutorial](/console/tutorials/getting-started-building-rich-console-app) - Learn Spectre.Console basics

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Rows" />
