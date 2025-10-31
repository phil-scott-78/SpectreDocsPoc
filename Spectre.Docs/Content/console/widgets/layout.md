---
title: "Layout Widget"
description: "Create complex multi-section layouts with the flexible Layout widget"
uid: "console-widget-layout"
order: 3400
---

The Layout widget divides console space into sections that can be split horizontally or vertically, enabling complex multi-region interfaces.

<Screenshot src="/assets/layout.svg" />

## When to Use

Use Layout when you need to **divide the console into distinct regions** with precise control over sizing. Common scenarios:

- **Dashboard interfaces**: Headers, sidebars, content areas, and footers with fixed or proportional sizing
- **Multi-pane views**: Split screen layouts like file managers, IDE-style interfaces, or monitoring dashboards
- **Responsive layouts**: Sections that adapt proportionally to available space using ratios

For **simple side-by-side content** without nested regions, use [Columns](/console/widgets/columns) instead. For **tabular data with borders**, use [Grid](/console/widgets/grid).

## Basic Usage

Split a layout into columns or rows, then update each section by name.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.BasicLayoutExample
```

## Navigation and Updates

### Accessing Sections by Name

Use named sections to access and update specific regions, making your layout easier to manage and modify.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutNavigationExample
```

### Splitting into Rows

Use `SplitRows()` to create vertical sections like headers, content areas, and footers.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutRowsExample
```

## Size Control

### Fixed Sizes

Use `Size()` to set exact widths or heights for sections that need consistent dimensions, like sidebars or headers.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutFixedSizeExample
```

### Proportional Sizing with Ratios

Use `Ratio()` to distribute space proportionally when sections should scale relative to each other.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutRatioExample
```

### Minimum Sizes

Use `MinimumSize()` to ensure sections remain readable even when space is constrained.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutMinimumSizeExample
```

## Advanced Usage

### Nested Layouts

Create complex multi-level layouts by splitting sections recursively to build sophisticated interfaces.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutNestedExample
```

### Dashboard Layout

Combine multiple layout techniques to create a complete dashboard with headers, sidebars, content areas, and status bars.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutDashboardExample
```

### Three-Column Layout

Create a classic application layout with navigation, main content, and sidebar regions containing different widget types.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutThreeColumnExample
```

### Dynamic Visibility

Control section visibility at runtime to show or hide regions based on application state.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutVisibilityExample
```

### Dynamic Content Updates

Update section content in response to events or state changes for interactive interfaces.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.LayoutExamples.LayoutDynamicUpdateExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Layout" />
