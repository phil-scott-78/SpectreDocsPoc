---
title: "Write ANSI"
description: "How to write ANSI/VT sequences and markup without the Spectre.Console library"
uid: "write-ansi"
order: 2180
---

As of version `0.54.1-alpha.0.33`, the Spectre.Console library relies on a library called
`Spectre.Console.Ansi` to emit ANSI/VT escape sequences and
[markup](xref:console-markup-reference) in the terminal.

## Creating an AnsiWriter

Create a new `AnsiWriter` that automatically detects capabilities.

```csharp
var writer = new AnsiWriter(Console.Out);
```

Create a new `AnsiWriter` with specific capabilities.  
Useful for testing, but usually not recommended.

```csharp
var writer = new AnsiWriter(
    Console.Out, 
    new AnsiCapabilities 
    {
        Ansi = true,
        Links = true,
        ColorSystem = ColorSystem.TrueColor,
        AlternateBuffer = true,
    });
```

> [!CAUTION]
> Creating an `AnsiWriter` can be expensive, so make sure that you cache
> and reuse them between writes, especially when detecting capabilities.

## Emitting ANSI

To emit ANSI, you can either write "raw" ANSI/VT escape sequences directly
or use the built-in fluent API.

```csharp
writer
    .BeginLink("https://spectreconsole.net", linkId: 123)
    .Decoration(Decoration.Bold | Decoration.Italic)
    .Foreground(Color.Yellow)
    .Write("Spectre Console")
    .ResetStyle()
    .EndLink();
```

## Emitting Markup

You can also emit [markup](xref:console-markup-reference) using the `AnsiWriter`.

```csharp
writer.Markup(
    "[yellow bold italic link=https://spectreconsole.net]" + 
    "Spectre.Console[/]");
```

## System.Console Extensions

If you're running .NET 10 with C# 14 enabled, you can use the
new functionality directly from `System.Console`.

```csharp
using System;

Console.Markup("[yellow]Hello[/] ");
Console.MarkupLine("[blue]World[/]");

Console.Ansi(writer => writer
    .BeginLink("https://spectreconsole.net", linkId: 123)
    .Decoration(Decoration.Bold | Decoration.Italic)
    .Foreground(Color.Yellow)
    .Write("Spectre Console")
    .ResetStyle()
    .EndLink());
```

## See Also

- <xref:console-markup-reference> - Markup reference
- <xref:console-color-reference> - All available color names
- <xref:console-text-styles> - All decoration options (bold, italic, etc.)
