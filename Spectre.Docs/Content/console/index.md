---
title: "Spectre.Console Documentation"
description: "Create beautiful console applications with Spectre.Console"
uid: "console-index"
---

Welcome to the Spectre.Console documentation! Spectre.Console is a .NET library that makes it easier to create beautiful, cross-platform console applications.

## Start Here

Get Spectre.Console running in seconds:

```bash
dotnet add package Spectre.Console
```

Then try this quick example that demonstrates styled text, a table, and a status spinner:

```csharp
using Spectre.Console;

// Styled text with markup
AnsiConsole.MarkupLine("[bold blue]Welcome[/] to [green]Spectre.Console[/]!");

// A simple table
var table = new Table()
    .AddColumn("Feature")
    .AddColumn("Description")
    .AddRow("[green]Markup[/]", "Rich text with colors and styles")
    .AddRow("[blue]Tables[/]", "Structured data display")
    .AddRow("[yellow]Progress[/]", "Spinners and progress bars");
AnsiConsole.Write(table);

// Status spinner for async work
await AnsiConsole.Status()
    .StartAsync("Processing...", async ctx =>
    {
        await Task.Delay(2000);
    });

AnsiConsole.MarkupLine("[green]Done![/]");
```

---

## Choose Your Learning Path

### New to Spectre.Console?

Start with the tutorials to build foundational skills:

1. <xref:console-getting-started> - Learn markup, colors, and styles
2. <xref:console-interactive-prompts> - Add interactive prompts
3. <xref:console-status-spinners> - Display progress indicators

### Know the Basics?

Jump to task-focused how-to guides:

- <xref:console-howto-displaying-tabular-data> - Tables with formatting and borders
- <xref:console-howto-showing-progress-bars> - Track long-running operations
- <xref:console-howto-live-rendering> - Real-time updating displays
- <xref:console-howto-testing-console-output> - Unit test your console apps

### Building Something Specific?

Browse the [Widget Documentation](#widget-documentation) below, or explore:

- [How-To Guides](/console/how--to) - Step-by-step recipes for common tasks
- [Reference](/console/reference) - Colors, styles, borders, spinners, and emoji lookups

### Want to Understand How It Works?

Dive into the concepts:

- [Spectre.Console vs Traditional Output](/console/explanation/spectre-console-vs-traditional-output) - Why use Spectre.Console?
- <xref:console-rendering-model> - How measure, layout, and render work
- [Best Practices](/console/explanation/best-practices-for-console-applications) - Patterns for production apps
- <xref:console-custom-renderables> - Build your own widgets

---

## Quick Reference

Essential lookups:

- <xref:console-markup-reference> - Tags, colors, escaping, and nesting
- <xref:console-color-reference> - All named colors and hex formats

---

## Widget Documentation

Explore detailed documentation for each Spectre.Console widget:

### Layout Widgets
Build structured layouts and organize content:
- <xref:console-widget-table> - Tabular data with columns, rows, and borders
- <xref:console-widget-panel> - Bordered boxes with headers
- <xref:console-widget-grid> - Row and column layouts without borders
- <xref:console-widget-columns> - Side-by-side content
- <xref:console-widget-rows> - Vertical stacking
- <xref:console-widget-layout> - Complex multi-section layouts
- <xref:console-widget-padder> - Add padding around content
- <xref:console-widget-align> - Control content alignment

### Visual Widgets
Create visual elements and decorations:
- <xref:console-widget-rule> - Horizontal dividers and separators
- <xref:console-widget-tree> - Hierarchical data structures
- <xref:console-widget-canvas> - Pixel-level graphics with Braille characters
- <xref:console-widget-canvas-image> - Display images in console
- <xref:console-widget-figlet> - Large ASCII art text banners

### Chart Widgets
Visualize data with charts:
- <xref:console-widget-bar-chart> - Horizontal and vertical bar charts
- <xref:console-widget-breakdown-chart> - Proportional data segments
- <xref:console-widget-calendar> - Monthly calendars with events

### Special Widgets
Specialized content rendering:
- <xref:console-widget-json> - Syntax-highlighted JSON display
- <xref:console-widget-text-path> - Smart file path truncation
- <xref:console-widget-text> - Precise text rendering control

---

## Live Rendering

Create dynamic, updating console displays:
- <xref:console-live-progress> - Progress bars for long-running tasks
- <xref:console-live-status> - Animated status with spinners
- <xref:console-live-live-display> - Update any content in real-time
- <xref:console-explanation-async-patterns> - Best practices for async operations

---

## Interactive Prompts

Gather user input with validation:
- <xref:console-prompt-text> - Text input with validation
- <xref:console-prompt-selection> - Single-choice selection
- <xref:console-prompt-multi-selection> - Multiple-choice selection