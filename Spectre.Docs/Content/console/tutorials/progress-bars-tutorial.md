---
title: "Showing Progress Bars"
description: "A beginner-friendly tutorial that walks through creating animated progress bars to track long-running operations"
uid: "console-tutorial-progress-bars"
order: 1060
---

In this tutorial, we'll build a game loading screen that tracks multiple assets loading simultaneously. By the end, you'll know how to create progress bars, run multiple tasks in parallel, customize the display columns, and style everything to match your application.

## What We're Building

Here's the loading screen we're creating:

<Screenshot Src="/assets/progress-bars-tutorial.svg" Alt="Progress Bar Tutorial" />


## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- Completion of the [Getting Started](xref:console-getting-started) tutorial

<Steps>
<Step stepNumber="1">
**Load a Single Asset**

Let's start with the simplest case - a single progress bar loading one asset. The `Progress()` method creates animated bars that fill as work completes:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.LoadSingleAsset
```

Run the code:

```bash
dotnet run
```

A progress bar appears for "Loading World Map" and fills from 0% to 100%, then "Ready!" appears.

The `AddTask()` method creates a trackable task, and `Increment()` advances the progress. Spectre.Console handles all the animation and redrawing automatically.

Your first progress bar is working.

</Step>
<Step stepNumber="2">
**Load Multiple Assets**

Games load many assets at once. Let's add parallel progress bars with different sizes - the world map is larger than sound effects:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.LoadMultipleAssets
```

Run it:

```bash
dotnet run
```

Three progress bars appear. Sound effects finish quickly (only 50 units), while the world map takes longer (200 units).

The `maxValue` parameter sets each task's total size - sound effects at 50 units complete faster than the 200-unit world map even with similar increment rates.

Now we're tracking multiple operations with realistic sizes.

</Step>
<Step stepNumber="3">
**Customize the Display**

Loading screens typically show more than just a bar. Let's add a spinner animation and elapsed time to make our display more informative:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.CustomizeLoadingDisplay
```

Run it:

```bash
dotnet run
```

Each row now shows a spinning animation on the left and elapsed time on the right. The percentage column shows exact completion.

The `.Columns()` method lets you pick exactly what to display. `SpinnerColumn` adds animation, `ElapsedTimeColumn` shows duration, and `PercentageColumn` shows completion percentage.

Your loading screen is getting more informative.

</Step>
<Step stepNumber="4">
**Style the Loading Screen**

Let's add some color to make completed progress stand out. We'll use green for the filled portion and gray for the remaining:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.StyleTheLoadingScreen
```

Run it:

```bash
dotnet run
```

The progress bars now show green for completed work and gray for remaining. When a task finishes, the entire bar turns lime green.

`CompletedStyle` colors the filled portion, `RemainingStyle` colors the empty portion, and `FinishedStyle` colors the bar when the task reaches 100%.

The loading screen has some visual polish now.

</Step>
<Step stepNumber="5">
**Start Tasks Programmatically**

Some assets depend on others - textures can't load until the world map is partially ready. Let's add textures that wait until the world map reaches 25%:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.StartTaskProgrammatically
```

Run it:

```bash
dotnet run
```

Four progress bars appear. World map, character, and sounds start immediately, but textures stay at 0% until the world map hits 25%. Then textures begin loading.

Setting `autoStart: false` creates a task that waits. Check `IsStarted` to avoid calling `StartTask()` multiple times, then trigger it when your condition is met.

Dependent loading sequences are now under control.

</Step>
<Step stepNumber="6">
**Complete Loading Screen**

Let's combine everything into a polished loading screen with all five assets, custom styling, and timing information:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.ProgressBarsTutorial.Run(Spectre.Console.IAnsiConsole)
```

Run the complete application:

```bash
dotnet run
```

"LOADING GAME" appears in bold yellow, followed by five styled progress bars with spinners and elapsed time. When everything finishes, "PRESS ANY KEY TO START" appears in lime.

The full combination: custom columns for information, styled colors for visual appeal, and multiple tasks showing parallel work.

A complete game loading screen experience.

</Step>
</Steps>

## Congratulations!

You've built a game loading screen that demonstrates all the core progress features. Your application tracks multiple assets loading in parallel, shows elapsed time and percentages, and uses custom colors to make the display visually appealing.

Add progress bars to file downloads, data processing, build pipelines, deployment scripts - anywhere you have long-running operations that users are waiting for.

## Next Steps

- <xref:console-live-progress> - Explore indeterminate progress, dynamic tasks, and async patterns
- <xref:console-howto-showing-progress-bars> - Quick recipes for common progress scenarios
- <xref:console-live-status> - Simpler alternative when you don't have measurable progress
