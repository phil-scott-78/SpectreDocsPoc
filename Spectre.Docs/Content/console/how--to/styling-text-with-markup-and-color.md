---
title: "Styling Text with Markup and Color"
description: "How to output text with rich styles and colors using Spectre.Console's markup language"
uid: "console-styling-text"
order: 2000
---

When you need to emphasize important information, highlight errors, or create visual hierarchy in console output, use Spectre.Console's markup language to apply colors and styles. Write inline markup like `[red]error[/]` for colored text or `[bold]warning[/]` for emphasis, combining multiple styles as needed.

Escape literal bracket characters with `Markup.Escape` to avoid parsing errors, and use `AnsiConsole.MarkupLine` for convenient styled output. Follow best practices like avoiding assumptions about terminal background colors and choosing color combinations with sufficient contrast. This lets you create clear, scannable console output where important messages stand out and users can quickly identify different types of information.

## See Also

- [Color Reference](/console/reference/color-reference) - Complete list of all available color names and their hex values
- [Text Style Reference](/console/reference/text-style-reference) - Reference for all text decoration options (bold, italic, underline, etc.)