---
title: "Understanding Spectre.Console's Rendering Model"
description: "An in-depth explanation of how Spectre.Console renders text and widgets to the terminal"
uid: "console-rendering-model"
order: 6050
---

An in-depth explanation of how Spectre.Console renders text and widgets to the terminal. This article discusses the console **Capabilities** detection (how it checks for ANSI support, Unicode capability, terminal type, etc.) and how that influences rendering. It explains concepts like measuring content width and auto-adjusting to console window size, and how Spectre.Console avoids flicker by rendering off-screen (if applicable) then updating. It also covers how **IRenderable** objects work – describing that most widgets implement `IRenderable` and the console simply calls their render logic. This section might also touch on performance considerations and how updating works (e.g. the render loop for live widgets). It gives readers a mental model of what happens when they call `AnsiConsole.Write` or update a live widget, enhancing their understanding of the library's internals.

## See Also

- [Terminal Compatibility](/console/reference/compatibility-matrix) - Capabilities detection details
- [Extending with Custom Renderables](/console/explanation/extending-with-custom-renderables) - Build custom widgets
- [Live Display](/console/live/live-display) - Real-time rendering in practice
- [Best Practices](/console/explanation/best-practices-for-console-applications) - Production guidelines