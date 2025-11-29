---
title: "How to Show Progress Bars"
description: "Display progress bars for long-running operations with percentage completion"
uid: "console-howto-showing-progress-bars"
order: 2100
---

When you have operations with measurable progress, use `AnsiConsole.Progress()`.

## Create a Progress Bar

To show progress, call `ctx.AddTask()` and update with `task.Increment()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingProgressBarsHowTo.CreateProgressBar
```

## Track Multiple Tasks

To track several operations at once, add multiple tasks to the context.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingProgressBarsHowTo.TrackMultipleTasks
```

## Customize Columns

If you want different columns, use `.Columns()` to configure the display.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingProgressBarsHowTo.CustomizeColumns
```

## Style the Progress Bar

To change colors, set styles on `ProgressBarColumn`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingProgressBarsHowTo.StyleProgressBar
```

## See Also

- [Progress](/console/live/progress) - Full Progress API reference
- [Spinner Reference](/console/reference/spinner-reference) - All spinner animations
