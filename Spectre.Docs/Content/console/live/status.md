---
title: "Status Display"
description: "Show animated status indicators with spinners for ongoing operations"
uid: "console-live-status"
order: 4050
---

The Status display renders an animated spinner with a status message for long-running operations.

<Screenshot src="/assets/status.svg" />


## When to Use

Use Status when you need to **indicate ongoing work without tracking specific progress**. Common scenarios:

- **Indeterminate operations**: Show activity when duration or completion percentage is unknown
- **Connection attempts**: Display feedback while connecting to services or loading resources
- **Background processing**: Indicate that work is happening behind the scenes

For **operations with measurable progress** (file downloads, batch processing), use [Progress](/console/live/progress) instead.

> [!CAUTION]
> Status display is not thread safe. Using it together with other interactive components such as prompts, progress displays, or other status displays is not supported.

## Basic Usage

Create a status display by calling `AnsiConsole.Status().Start()` with a message and a callback containing your work.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.BasicStatusExample
```

## Spinners

### Choosing a Spinner

Select a spinner animation that matches your application's style or the type of operation being performed.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusSpinnerExample
```

> [!NOTE]
> See the [Spinner Reference](/console/reference/spinner-reference) for a gallery of all available spinner animations.

### Styling the Spinner

Apply colors and styles to the spinner to convey meaning or match your application's theme.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusSpinnerStyleExample
```

## Async Operations

Use `StartAsync()` when working with asynchronous code to avoid blocking the UI thread.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusAsyncExample
```

### Returning Values

Status operations can return values from the callback, allowing you to retrieve results after completion.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusWithReturnValueExample
```

## Dynamic Updates

### Changing Status Text

Update the status message as your operation progresses through different stages.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusDynamicUpdateExample
```

### Changing the Spinner

Switch spinner animations at runtime to indicate different types of activity.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusSpinnerChangeExample
```

## Manual Refresh

Disable automatic refresh when you need precise control over when the status display updates.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusManualRefreshExample
```

## Markup Support

Use Spectre.Console's markup syntax in status text to add colors, styles, and emphasis.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Live.StatusExamples.StatusWithMarkupExample
```

## See Also

- [How to Show Activity Status](/console/how--to/showing-activity-status) - Step-by-step guide
- [Showing Status and Spinners Tutorial](/console/tutorials/status-spinners-tutorial) - Learn status basics
- [Progress Display](/console/live/progress) - For operations with measurable progress
- [Spinner Reference](/console/reference/spinner-reference) - Available spinner animations
- [Async Patterns](/console/explanation/async-patterns) - Best practices for async operations

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Status" />
