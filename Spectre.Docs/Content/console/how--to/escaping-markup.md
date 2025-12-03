---
title: "How to Escape Markup"
description: "Safely display user input, array indexers, and JSON without markup parsing errors"
uid: "console-howto-escaping-markup"
order: 2090
---

When displaying dynamic content that might contain square brackets, escape it to prevent markup parsing errors.

## Escape User Input

To safely display user-provided strings, use `Markup.Escape()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.EscapingMarkupHowTo.EscapeUserInput
```

## Use Safe Interpolation

For cleaner code with multiple dynamic values, use `MarkupLineInterpolated()` which escapes automatically.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.EscapingMarkupHowTo.UseSafeInterpolation
```

## Strip Markup for Plain Text

To get plain text without markup tags (for logging or file output), use `Markup.Remove()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.EscapingMarkupHowTo.StripMarkupForLogging
```

## See Also

- [Markup Reference](/console/reference/markup-reference) - Complete markup syntax including escaping rules
- [Markup Widget](/console/widgets/markup) - Markup API reference
