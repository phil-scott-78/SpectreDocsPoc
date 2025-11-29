---
title: "How to Run Tasks with a Spinner"
description: "Show a spinner animation while awaiting async operations"
uid: "console-howto-async-spinner"
order: 2500
---

When you have an async operation, use the `.Spinner()` extension.

## Show a Spinner While Waiting

To display a spinner during an await, call `.Spinner()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RunningTasksWithSpinnerHowTo.ShowSpinnerWhileWaiting
```

## Change the Animation

To use a different spinner, pass a `Spinner.Known` value.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RunningTasksWithSpinnerHowTo.ChangeSpin­nerAnimation
```

## Get a Result

To get a value back, call `.Spinner()` on a `Task<T>`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RunningTasksWithSpinnerHowTo.GetResultWithSpinner
```

## See Also

- [Spinner Reference](/console/reference/spinner-reference) - All spinner animations
