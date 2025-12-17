---
title: "Creating a Custom IRenderable"
description: "Build a reusable Pill widget by implementing the IRenderable interface"
uid: "console-creating-custom-renderables-tutorial"
order: 1070
---

In this tutorial, we'll build a Pill widget from scratch. By the end, we'll have a reusable component that displays
styled labels with rounded end caps - and gracefully falls back on terminals without Unicode support.

## What We're Building

Here's the output we're creating:

<Screenshot Src="/assets/creating-custom-renderables-tutorial.svg" Alt="Colored pill widgets showing Success, Warning, Error, and Info labels" />

## Prerequisites

- .NET 6.0 or later
- Completion of the [Building a Rich Console App](xref:console-getting-started) tutorial
- Basic understanding of C# interfaces

<Steps>
<Step stepNumber="1">
**Create the Pill Class**

Let's start by defining a `PillType` enum and creating a class that implements `IRenderable`. This interface requires
two methods: `Measure()` to report size constraints and `Render()` to produce output.

```csharp
public enum PillType
{
    Success,
    Warning,
    Error,
    Info,
}

public sealed class Pill : IRenderable
{
    private readonly string _text;
    private readonly Style _style;

    /// <summary>
    /// Creates a new pill with the specified text and type.
    /// </summary>
    /// <param name="text">The text to display inside the pill.</param>
    /// <param name="type">The pill type which determines its color scheme.</param>
    public Pill(string text, PillType type)
    {
        _text = text;
        _style = GetStyleForType(type);
    }

    private static Style GetStyleForType(PillType type) => type switch
    {
        PillType.Success => new Style(Color.White, Color.Green),
        PillType.Warning => new Style(Color.Black, Color.Yellow),
        PillType.Error => new Style(Color.White, Color.Red),
        PillType.Info => new Style(Color.White, Color.Blue),
        _ => new Style(Color.White, Color.Grey),
    };

    /// <summary>
    /// Measures the pill's width in console cells.
    /// </summary>
    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        // todo
    }

    /// <summary>
    /// Renders the pill as a sequence of styled segments.
    /// </summary>
    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        // todo
    }
}
```

The `PillType` enum defines four semantic variants with predefined color schemes. The `Pill` class maps these types
to styles internally, making it easy to create consistent status indicators.

Our widget skeleton is ready.

</Step>
<Step stepNumber="2">
**Implement Measure**

The `Measure()` method tells Spectre.Console how wide our pill needs to be. Containers like `Table` and `Panel` call
this before rendering to calculate layouts.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.Pill.Measure(Spectre.Console.Rendering.RenderOptions,System.Int32)
```

Our pill width is the text length plus 4 characters: two for padding spaces and two for the rounded cap characters. We
return the same value for both minimum and maximum since our pill has a fixed width.

The measurement calculation is complete.

</Step>
<Step stepNumber="3">
**Implement Render**

The `Render()` method produces the actual output as `Segment` objects. Each segment contains text and an optional style.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.Pill.Render(Spectre.Console.Rendering.RenderOptions,System.Int32)
```

We yield three segments: the left cap, the padded text, and the right cap. The `yield return` pattern lets
Spectre.Console process segments efficiently without creating intermediate collections.

Notice the Unicode detection: `options.Capabilities.Unicode` tells us whether the terminal supports Unicode characters.
We use `U+E0B4` and  `U+E0B5` for nice rounded caps, falling back to spaces on limited terminals.

Our pill now renders with style.

</Step>
<Step stepNumber="4">
**Complete Pill Widget**

Let's put it all together with our final showcase:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.CreatingCustomRenderablesTutorial.Run(Spectre.Console.IAnsiConsole)
```

Run the code:

```bash
dotnet run
```

Four pills appear in a row: Success (green), Warning (yellow), Error (red), and Info (blue). Each pill renders with
rounded Unicode caps, proper padding, and distinct colors.

Our custom IRenderable is complete.

</Step>
</Steps>

## Congratulations!

You've built a custom `IRenderable` from scratch. Your Pill widget measures its own width, renders styled segments,
detects terminal capabilities, and displays beautifully colored labels with rounded caps.

Apply this pattern to create any custom widget: badges, progress indicators, sparklines, or domain-specific
visualizations. The `IRenderable` interface is the foundation of every Spectre.Console widget.

## Next Steps

- <xref:console-rendering-model> - Deep dive into how rendering works
- <xref:console-howto-creating-custom-renderables> - Quick reference for custom renderables
- <xref:console-widget-panel> - See how containers use `Measure()` and `Render()`
- <xref:console-capabilities-reference> - All detectable terminal capabilities
