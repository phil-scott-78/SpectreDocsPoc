---
title: "Create Custom Renderables"
description: "Build your own widgets by implementing IRenderable"
uid: "console-howto-creating-custom-renderables"
order: 2170
---

When you need a widget that doesn't exist, implement `IRenderable`.

## Implement IRenderable

To create a custom renderable, implement `Measure()` and `Render()`.

```csharp:xmldocid,bodyonly
T:Spectre.Docs.Examples.SpectreConsole.HowTo.Label
```

## Calculate Size Constraints

To report your widget's size requirements, return a `Measurement` from `Measure()`. The measurement specifies minimum and maximum width in console cells.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.Label.Measure(Spectre.Console.Rendering.RenderOptions,System.Int32)
```

## Generate Segments

To produce output, yield `Segment` objects from `Render()`. Each segment contains text and an optional style.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.Label.Render(Spectre.Console.Rendering.RenderOptions,System.Int32)
```

## Apply Styles

To add colors and formatting, pass a `Style` when creating segments.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.CreatingCustomRenderablesHowTo.ApplyStyles
```

## Wrap Other Renderables

To create container widgets, accept `IRenderable` and delegate rendering.

```csharp:xmldocid,bodyonly
T:Spectre.Docs.Examples.SpectreConsole.HowTo.LabeledValue
```

## See Also

- <xref:console-rendering-model> - How rendering works
- <xref:console-widget-canvas> - Pixel-level rendering example
- <xref:console-widget-panel> - Container widget example
