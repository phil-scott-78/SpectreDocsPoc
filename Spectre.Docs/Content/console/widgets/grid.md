---
title: "Grid Widget"
description: "Arrange content in rows and columns without visible borders for flexible layouts"
uid: "console-widget-grid"
order: 3250
---

The Grid widget arranges content in rows and columns without visible borders, providing flexible layouts for console applications.

<Screenshot src="/assets/grid.svg" />

## When to Use

Use Grid when you need to **arrange content in invisible columns** without the visual structure of borders. Common scenarios:

- **Configuration displays**: Property names and values, settings lists, key-value pairs
- **Dashboard layouts**: Arranging panels, charts, or widgets in columns
- **Form-like output**: Labels aligned with corresponding data
- **Multi-column text**: Side-by-side content without table borders

For **structured data with visible borders**, use [Table](/console/widgets/table) instead. For **automatically flowing items into columns**, use [Columns](/console/widgets/columns) which handles wrapping and sizing automatically.

## Basic Usage

Add columns first, then populate rows with content. Each row must have the same number of cells as columns.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.BasicGridExample
```

## Column Configuration

### Column Widths

Use fixed widths to control column sizing precisely, or omit width for auto-sizing based on content.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridColumnWidthExample
```

### Alignment

Align content within columns using `Justify.Left`, `Justify.Right`, or `Justify.Center`. Right-align numeric data for easier comparison.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridAlignmentExample
```

### Padding

Control spacing between columns with custom padding. Adjust the right padding to increase or decrease column separation.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridPaddingExample
```

### Preventing Text Wrap

Use `NoWrap` when content should be truncated rather than wrapped to multiple lines.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridNoWrapExample
```

## Grid Layout

### Expanding to Fill Width

Use `Expand = true` to make the grid fill available console width—useful for dashboards or full-width layouts.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridExpandExample
```

### Fixed Grid Width

Constrain the entire grid to a specific width when you need precise sizing.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridWidthExample
```

## Working with Rows

### Empty Rows

Insert empty rows to create visual separation between groups of content.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridEmptyRowsExample
```

### Adding Multiple Columns

Use the `AddColumns()` extension method to quickly add several columns at once.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridAddColumnsExample
```

## Advanced Usage

### Nested Content

Embed other widgets like Panels, Charts, or Progress bars within grid cells for rich layouts.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridNestedContentExample
```

### Complex Layouts

Combine multiple features to create sophisticated layouts with mixed content types.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridComplexLayoutExample
```

### Dashboard Layouts

Create multi-level layouts by nesting grids to build complex dashboard-style interfaces.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.GridExamples.GridDashboardExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Grid" />
