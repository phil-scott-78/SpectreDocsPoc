---
title: "Tree Widget"
description: "Display hierarchical data structures with expandable tree views"
uid: "console-widget-tree"
order: 3150
---

The Tree widget visualizes hierarchical data structures with parent-child relationships using Unicode tree characters.

<Screenshot src="/assets/tree.svg" />

## When to Use

Use Tree when you need to display **hierarchical data with parent-child relationships**. Common scenarios:

- **File system navigation**: Show directory structures, project files
- **Organizational hierarchies**: Display team structures, reporting relationships
- **Data exploration**: Visualize nested JSON, XML documents, or object graphs
- **Menu systems**: Represent nested navigation menus or configuration options

For **tabular data with rows and columns**, use [Table](/console/widgets/table) instead. For **structured JSON visualization**, consider [Json](/console/widgets/json) which provides syntax highlighting.

## Basic Usage

Create a tree with a root label and add child nodes using `AddNode()`. The method returns the added node, allowing you to chain further children.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.BasicTreeExample
```

## Building Nested Structures

Call `AddNode()` on returned nodes to create deeper hierarchies. Each node can have its own children, creating multi-level trees.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.NestedTreeExample
```

## Styling Node Labels

### With Markup

Use markup in node labels to apply colors, styles, and formatting to individual nodes.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.MarkupTreeExample
```

### With Tree-Wide Styling

Use `Style()` to apply a consistent style to all tree guide lines, which helps create a cohesive visual appearance.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeStylingExample
```

## Guide Styles

The tree guide controls the appearance of the connecting lines between nodes. Choose a style based on your terminal capabilities and aesthetic preferences.

### Ascii Guide

Use `TreeGuide.Ascii` for maximum compatibility with terminals that don't support Unicode, or when output needs to be plain text.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeAsciiGuideExample
```

### Line Guide

Use `TreeGuide.Line` (the default) for clean Unicode box-drawing characters that work in most modern terminals.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeLineGuideExample
```

### DoubleLine Guide

Use `TreeGuide.DoubleLine` for a more prominent appearance with double-line Unicode characters.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeDoubleLineGuideExample
```

### BoldLine Guide

Use `TreeGuide.BoldLine` for heavy Unicode characters that stand out in dense tree structures.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeBoldLineGuideExample
```

> [!NOTE]
> See the [Tree Guide Reference](/console/reference/tree-guide-reference) for a complete visual comparison of all available guide styles.

## Controlling Node Expansion

### Collapsing Individual Nodes

Use `Collapse()` on individual nodes to hide their children, which is useful for large trees where you want to show only top-level structure initially.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeExpansionExample
```

### Collapsing the Entire Tree

Set `Expanded = false` on the tree itself to collapse all nodes, showing only the root.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeCollapseAllExample
```

## Advanced Usage

### Adding Multiple Nodes

Use `AddNodes()` to add several sibling nodes at once, which is more concise than multiple `AddNode()` calls.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeAddNodesExample
```

### Embedding Other Renderables

Add any `IRenderable` (panels, tables, text) as node content to create rich, composite visualizations.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeWithRenderablesExample
```

### Building from Data Structures

Dynamically construct trees from dictionaries, file systems, or other hierarchical data sources.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.TreeExamples.TreeFromDataExample
```

## See Also

- [How to Display Hierarchical Data](/console/how--to/displaying-hierarchical-data) - Step-by-step guide for tree tasks
- [Tree Guide Reference](/console/reference/tree-guide-reference) - All guide styles with visual examples
- [Getting Started Tutorial](/console/tutorials/getting-started-building-rich-console-app) - Learn Spectre.Console basics
- [Understanding the Rendering Model](/console/explanation/understanding-rendering-model) - How widgets measure and render

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Tree" />
