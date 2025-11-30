---
title: "Extending Spectre.Console with Custom Renderables"
description: "An explanation of how developers can create their own widgets or renderable components"
uid: "console-custom-renderables"
order: 6150
---

An explanation of how developers can create their own widgets or renderable components. It discusses the `IRenderable` interface and the expected implementation of the `Render` method to output a **Segment** sequence (or other renderable content). It might walk through a conceptual example of a custom renderable (e.g. a simple "progress pie" text graphic) without full code, explaining how to integrate it so that `AnsiConsole.Render(myRenderable)` will work. This section helps advanced users understand the extension points of the library and how Spectre.Console is designed for flexibility.

## See Also

- [Understanding the Rendering Model](/console/explanation/understanding-rendering-model) - How rendering works
- [Canvas Widget](/console/widgets/canvas) - Pixel-level rendering example
- [Panel Widget](/console/widgets/panel) - Container widget example
- [Live Display](/console/live/live-display) - Rendering dynamic content