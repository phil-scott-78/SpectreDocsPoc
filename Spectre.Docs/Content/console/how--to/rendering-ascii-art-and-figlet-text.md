---
title: "How to Render ASCII Art"
description: "Create large text banners and add emoji to console output"
uid: "console-howto-rendering-ascii-art"
order: 2350
---

When you want eye-catching headers or decorative text, use `FigletText`.

## Render Large Text

To create ASCII art text, use `FigletText`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RenderingAsciiArtHowTo.RenderFigletText
```

## Center the Text

To center figlet output, call `.Centered()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RenderingAsciiArtHowTo.CenterFigletText
```

## Add Emoji

To add emoji, use `:shortcode:` syntax in markup.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RenderingAsciiArtHowTo.AddEmoji
```

## Create a Banner

To make a title banner, combine figlet with a rule.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.RenderingAsciiArtHowTo.CreateBanner
```

## See Also

- [FigletText](/console/widgets/figlet) - FigletText widget reference
- [Emoji Reference](/console/reference/emoji-reference) - All available emoji
