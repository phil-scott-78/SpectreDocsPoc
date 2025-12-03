---
title: "CanvasImage Widget"
description: "Display image files in the console using pixel-based rendering"
uid: "console-widget-canvas-image"
order: 3900
---

The CanvasImage widget loads and displays image files in the console by converting them to colored character blocks.

## When to Use

Use CanvasImage when you need to **display visual content from image files** in your console application. Common scenarios:

- **Application branding**: Show logos or banners at startup
- **Data visualization**: Display charts, graphs, or diagrams generated as images
- **Preview functionality**: Show thumbnails or previews of image files
- **Visual feedback**: Display icons or status images during operations

For **drawing custom graphics programmatically** (shapes, lines, patterns), use [Canvas](xref:console-widget-canvas) instead. For **ASCII art from text**, use the [FigletText](xref:console-widget-figlet) widget.

## Basic Usage

Load an image from a file path. The widget automatically handles color conversion and scaling.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.BasicCanvasImageExample
```

## Loading Images

### From File Path

The simplest approach loads an image directly from the filesystem.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.BasicCanvasImageExample
```

### From Byte Array

Use byte arrays when working with images from memory, databases, or network sources.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageFromBytesExample
```

### From Stream

Use streams for efficient processing of large images or when reading from network resources.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageFromStreamExample
```

## Sizing the Image

### Setting Maximum Width

Use `MaxWidth()` to constrain images to fit within your console layout while maintaining aspect ratio.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageMaxWidthExample
```

### Removing Width Constraints

Use `NoMaxWidth()` to remove size constraints and display the image at full resolution (limited by console dimensions).

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageNoMaxWidthExample
```

### Adjusting Pixel Width

Use `PixelWidth()` to control the character-to-pixel ratio. Lower values create taller, narrower images; higher values create shorter, wider ones.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImagePixelWidthExample
```

## Resampling Methods

When images are scaled, different resampling algorithms affect quality and performance.

### Bicubic Resampling (Default)

Use `BicubicResampler()` for the highest quality when scaling images. This is the default and works well for most scenarios.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageBicubicResamplerExample
```

### Bilinear Resampling

Use `BilinearResampler()` for a balance between quality and performance when rendering many images.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageBilinearResamplerExample
```

### Nearest Neighbor Resampling

Use `NearestNeighborResampler()` for the fastest scaling, which creates a pixelated effect. Good for retro aesthetics or pixel art.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageNearestNeighborResamplerExample
```

### Comparing Resampling Methods

Compare the visual differences between resampling methods to choose the right one for your needs.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageResamplerComparisonExample
```

## Advanced Image Processing

### Basic Mutations

Use `Mutate()` to apply ImageSharp transformations like rotation, flipping, or cropping before rendering.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageMutateExample
```

### Combining Multiple Transformations

Chain multiple mutations together for complex image processing effects.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageAdvancedMutateExample
```

### Complete Configuration

Combine sizing, resampling, and mutations for complete control over image appearance.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CanvasImageExamples.CanvasImageCompleteExample
```

## See Also

- <xref:console-widget-canvas> - Draw custom graphics programmatically
- <xref:console-widget-figlet> - ASCII art text banners

## API Reference

<WidgetApiReference TypeName="Spectre.Console.CanvasImage" />
