---
title: "Spinner Styles Reference"
description: "A reference of built-in spinner animations available for the Status and Spinner APIs"
uid: "console-spinner-styles"
order: 7150
---

Spectre.Console includes various built-in spinner types that can be used with `AnsiConsole.Status()`
or when creating progress displays. Each spinner has a unique animation style.

## Usage Example

```csharp
await AnsiConsole.Status()
    .Spinner(Spinner.Known.Dots)
    .StartAsync("Thinking...", async ctx =>
    {
        await Task.Delay(3000);
    });
```

## Important Notes

- **Frame Rate:** Spinners cycle through frames at regular intervals (typically 100ms).
- **Custom Spinners:** You can create custom spinners by inheriting from the `Spinner` class.
- **Known Spinners:** Access pre-defined spinners via `Spinner.Known` static class.

## Available Spinners

<SpinnerList />
