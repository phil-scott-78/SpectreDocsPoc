---
title: "Showing Status and Spinners"
description: "Display animated spinners while operations are running"
uid: "console-status-spinners"
order: 1050
---

In this tutorial, we'll build a coffee brewing simulation that shows animated spinners while work happens. By the end, you'll know how to display status messages, update them as work progresses, and customize the spinner style.

## What We're Building

Here's what our coffee brewing simulation will look like:

<Screenshot Src="/assets/status-spinners-tutorial.svg" Alt="Status Spinner Tutorial" />

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- Completion of the [Getting Started](xref:console-getting-started) tutorial

<Steps>
<Step stepNumber="1">
**Show a Basic Spinner**

Let's start by showing a spinner while our "coffee grinder" runs. The `Status()` method displays an animated spinner with a message:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.StatusSpinnersTutorial.ShowBasicSpinner
```

Run the code:

```bash
dotnet run
```

An animated spinner appears next to "Grinding beans..." that runs for a few seconds, then "Done!" appears.

The spinner animates automatically - Spectre.Console handles the animation loop for you. Just put your work inside the callback.

Your first status spinner.

</Step>
<Step stepNumber="2">
**Update the Status Text**

Real tasks have multiple stages. Let's update the status message as our coffee progresses through grinding, brewing, and pouring:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.StatusSpinnersTutorial.UpdateStatusText
```

Run it:

```bash
dotnet run
```

The message changes from "Grinding beans..." to "Brewing coffee..." to "Pouring into cup..." - all while the spinner keeps animating.

We use `ctx.Status()` to change the message. The `ctx` parameter gives you control over the status display while it's running.

Your status now reflects what's actually happening.

</Step>
<Step stepNumber="3">
**Try Different Spinners**

Spectre.Console includes many spinner styles. Let's try a few to see the difference:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.StatusSpinnersTutorial.TryDifferentSpinners
```

Run it:

```bash
dotnet run
```

Two different spinner animations appear - the smooth `Dots` spinner for grinding, then the lively `Star` spinner (in yellow) for brewing.

`.Spinner()` sets the animation style while `.SpinnerStyle()` sets the color. Match the spinner to your app's personality.

You can now customize the look and feel of your spinners.

</Step>
<Step stepNumber="4">
**Complete Coffee Brew**

Let's put it all together into a complete brewing experience that changes both the message and spinner style at each stage:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.StatusSpinnersTutorial.Run(Spectre.Console.IAnsiConsole)
```

Run the complete application:

```bash
dotnet run
```

"Time for coffee!" appears, followed by an animated brewing sequence: yellow dots while grinding, blue stars while brewing, and a green arc while pouring - then the final success message.

Both spinner and color change using `ctx.Spinner()` and `ctx.SpinnerStyle()`, creating a dynamic, engaging experience.

A polished status display with some personality.

</Step>
</Steps>

## Congratulations!

You've created a coffee brewing simulation that demonstrates all the core status features. Your application shows animated spinners, updates messages as work progresses, and customizes the spinner style to match each stage.

Add these spinners to file uploads, API calls, database queries, build processes - anywhere users wait for work to complete.

## Next Steps

- <xref:console-howto-showing-progress-bars> - Track multiple operations with progress bars
- <xref:console-live-status> - Explore async operations, return values, and manual refresh
- <xref:console-spinner-styles> - See all available spinner styles
