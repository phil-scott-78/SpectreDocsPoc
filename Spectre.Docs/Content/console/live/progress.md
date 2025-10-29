---
title: "Progress Display"
description: "Show progress bars and task status for long-running operations"
uid: "console-live-progress"
order: 4000
---

The Progress display system provides animated progress bars and task tracking for long-running operations, keeping users informed about work status. It supports multiple concurrent tasks, percentage tracking, custom descriptions, and various visual styles.

![Progress Bar Demo](/assets/progress.webp)

## Sample

```csharp:xmldocid
M:Spectre.Docs.Examples.AsciiCast.Samples.ProgressSample.Run(Spectre.Console.IAnsiConsole)
M:Spectre.Docs.Examples.AsciiCast.Samples.ProgressSample.CreateTasks(Spectre.Console.ProgressContext,System.Random)
```

**Key Topics Covered:**

* **Creating progress contexts** - Using `AnsiConsole.Progress()` to create a progress tracking context
* **Adding tasks** - Defining tasks with `AddTask()` including descriptions and total work amounts
* **Updating progress** - Incrementing progress with `Increment()` or setting absolute values with `Value`
* **Task states** - Managing task lifecycle (not started, in progress, completed, failed)
* **Multiple concurrent tasks** - Tracking several operations simultaneously with individual progress bars
* **Indeterminate progress** - Showing activity for tasks without known completion percentages
* **Custom columns** - Configuring what information is displayed (percentage, speed, time remaining, etc.)
* **Styles and colors** - Customizing progress bar appearance and task descriptions
* **Auto-refresh** - Controlling update frequency and smooth animations





Examples demonstrate tracking file downloads with progress and speed, monitoring multi-step build processes, showing parallel task execution, displaying batch processing progress, creating installer-style progress displays, and building complex multi-task dashboards. The guide covers best practices for progress reporting, handling task failures, and choosing appropriate progress styles for different scenarios.
