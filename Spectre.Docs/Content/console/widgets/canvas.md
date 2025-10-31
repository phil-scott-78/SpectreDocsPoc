---
title: "Canvas Widget"
description: "Draw pixel-level graphics and patterns in the console"
uid: "console-widget-canvas"
order: 3850
---

The Canvas widget enables pixel-level drawing in the console, where you can set individual pixels with specific colors to create graphics, patterns, and visualizations.

<Screenshot src="/assets/canvas.svg" />

## When to Use

Use Canvas when you need to **create custom graphics or visualizations** at the pixel level. Common scenarios:

- **Custom data visualizations**: Build charts, graphs, or diagrams with precise pixel control
- **Patterns and effects**: Generate gradients, textures, or geometric patterns
- **Simple graphics**: Draw shapes, lines, or pixel art
- **Algorithm visualization**: Show the output of image processing or generation algorithms

For **displaying existing images**, use [CanvasImage](/console/widgets/canvas-image) instead, which handles image file loading and conversion automatically.

## Basic Usage

Create a canvas with specified dimensions and set individual pixels using `SetPixel(x, y, color)`. Pixels use zero-based coordinates starting from the top-left corner.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.BasicCanvasExample
```

## Creating Patterns

### Simple Patterns

Use loops to create repeating patterns across the canvas.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasPatternExample
```

### Gradients

Create smooth color transitions by calculating pixel colors based on position.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasGradientExample
```

## Controlling Size and Scaling

### Maximum Width

Use `MaxWidth` to constrain the rendered canvas width. The canvas automatically scales while maintaining the aspect ratio.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasScalingExample
```

### Pixel Width

Use `PixelWidth` to control how many console characters each pixel occupies. Larger values create wider, more visible pixels.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasPixelWidthExample
```

### Disabling Scaling

Set `Scale = false` to prevent automatic resizing when the canvas exceeds available space. This ensures pixel-perfect rendering but may cause clipping.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasNoScalingExample
```

## Advanced Usage

### Custom Visualizations

Combine pixel operations to create custom data visualizations like bar charts or graphs.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasBarVisualizationExample
```

### Drawing Lines

Create line patterns by calculating pixel positions along a path.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasDiagonalLineExample
```

### Complex Compositions

Build sophisticated graphics by combining multiple drawing operations with helper methods.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasExamples.CanvasComplexPatternExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Canvas" />
