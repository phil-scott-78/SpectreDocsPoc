---
title: "Live Display"
description: "Update and refresh any renderable content dynamically in real-time"
uid: "console-live-live-display"
order: 4100
---

The LiveDisplay renders content that can be updated in place without scrolling the console, perfect for dashboards, real-time monitoring, and dynamic status displays.

<Screenshot Src="/assets/live.svg" />

## When to Use

Use LiveDisplay when you need to **update arbitrary content in place without creating new output lines**. Common scenarios:

- **Custom dashboards**: Display real-time metrics, server stats, or system monitors with any widget combination
- **Dynamic tables**: Build tables incrementally or update existing rows as data changes
- **Status transitions**: Show multi-step processes with changing panels or formatted text
- **Real-time data**: Update charts, gauges, or custom visualizations continuously

For **progress tracking with multiple tasks**, use [Progress](/console/live/progress) instead. For **simple spinner animations**, use [Status](/console/live/status).

> [!CAUTION]
> Live display is not thread safe. Using it together with other interactive components such as prompts, progress displays, or status displays is not supported.

## Basic Usage

Create a live display by passing any renderable to `AnsiConsole.Live()`, then update it within the context.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.BasicLiveDisplayExample
```

## Updating Content

### Modifying Mutable Renderables

Modify properties of mutable widgets like Table, then call `ctx.Refresh()` to update the display.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayWithTableExample
```

### Replacing the Target

Use `ctx.UpdateTarget()` to completely replace the displayed renderable with a different widget.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayUpdateTargetExample
```

### Displaying Panels

Wrap dynamic content in panels for polished status displays.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayWithPanelExample
```

## Handling Overflow

When content exceeds the console height, LiveDisplay provides several overflow strategies.

### Ellipsis Mode

Show an ellipsis indicator when content is truncated.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayOverflowEllipsisExample
```

### Crop Mode

Silently crop content that doesn't fit, combined with cropping direction control.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayOverflowCropExample
```

### Visible Mode

Allow content to scroll naturally when it exceeds console height.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayOverflowVisibleExample
```

## Cropping Direction

Control which part of overflowing content remains visible.

### Crop from Top

Keep the most recent content visible by removing old content from the top.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayCroppingTopExample
```

### Crop from Bottom

Keep the initial content visible by removing new content from the bottom.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayCroppingBottomExample
```

## Auto Clear

Remove the live display from the console when the context completes.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayAutoClearExample
```

## Async Operations

Use `StartAsync()` for asynchronous work within the live display context.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayAsyncExample
```

## Returning Values

Return results from the live display context using the generic `Start<T>()` method.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayReturnValueExample
```

## Combining Widgets

Create sophisticated dashboards by combining multiple widgets in layouts.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.LiveDisplayExamples.LiveDisplayCompositeExample
```

## See Also

- [How to Live Rendering and Dynamic Updates](/console/how--to/live-rendering-and-dynamic-updates) - Step-by-step guide
- [Progress Display](/console/live/progress) - Tracking task progress
- [Status Display](/console/live/status) - Simple spinner animations
- [Layout Widget](/console/widgets/layout) - Complex dashboard layouts
- [Async Patterns](/console/explanation/async-patterns) - Best practices for async operations

## API Reference

<WidgetApiReference TypeName="Spectre.Console.LiveDisplay" />
