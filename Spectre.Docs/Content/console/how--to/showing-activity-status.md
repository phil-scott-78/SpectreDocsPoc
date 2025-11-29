---
title: "How to Show Activity Status"
description: "Display a spinner for operations without measurable progress"
uid: "console-howto-showing-activity-status"
order: 2101
---

When you have an operation without measurable progress, use `AnsiConsole.Status()`.

## Show a Spinner

To indicate activity, wrap your operation in a status context.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingActivityStatusHowTo.ShowSpinner
```

## Update the Status Message

To show what's happening, call `ctx.Status()` with new text.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingActivityStatusHowTo.UpdateStatusMessage
```

## Change the Spinner Style

If you want a different animation, use `.Spinner()` and `.SpinnerStyle()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingActivityStatusHowTo.ChangeSpinnerStyle
```

## Use Async

To use with async operations, call `.StartAsync()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.ShowingActivityStatusHowTo.UseAsync
```

## See Also

- [Status](/console/live/status) - Full Status API reference
- [Spinner Reference](/console/reference/spinner-reference) - All spinner animations
