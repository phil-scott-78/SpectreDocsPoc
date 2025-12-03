---
title: "Extending Spectre.Console with Custom Renderables"
description: "An explanation of how developers can create their own widgets or renderable components"
uid: "console-custom-renderables"
order: 6150
---

An explanation of how developers can create their own widgets or renderable components. It discusses the `IRenderable` interface and the expected implementation of the `Render` method to output a **Segment** sequence (or other renderable content). It might walk through a conceptual example of a custom renderable (e.g. a simple "progress pie" text graphic) without full code, explaining how to integrate it so that `AnsiConsole.Render(myRenderable)` will work. This section helps advanced users understand the extension points of the library and how Spectre.Console is designed for flexibility.

## See Also

- <xref:console-rendering-model> - How rendering works
- <xref:console-widget-canvas> - Pixel-level rendering example
- <xref:console-widget-panel> - Container widget example
- <xref:console-live-live-display> - Rendering dynamic content