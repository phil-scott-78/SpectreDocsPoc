---
title: "Emoji Reference"
description: "A list of all emoji shortcodes supported by Spectre.Console's Markup"
uid: "console-emoji-reference"
order: 7200
---

Spectre.Console supports emoji shortcodes in markup strings. Use the format `:emoji_name:`
to include emoji in your console output.

## Usage Example

```csharp
// Emoji colors work for monochrome.
AnsiConsole.Markup("Hello [yellow]:waving_hand:[/]!");
AnsiConsole.Markup("[green]:check_mark:[/] Task completed!");
AnsiConsole.Markup("[red]:x:[/] Error occurred [yellow]:warning:[/]");
```

## Important Notes

- **Font Support:** Emoji rendering depends on your console's font. Not all fonts support color emoji.
- **Console Support:** Some consoles may display emoji as monochrome symbols instead of color icons.
- **Cross-Platform:** Emoji support varies across different operating systems and terminal applications.

e.g Windows PowerShell

```csharp
Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
```

## Complete Emoji Reference

<EmojiList />

## See Also

- <xref:console-markup-reference> - Using emoji in markup syntax
- <xref:console-widget-markup> - Rich text with embedded emoji
