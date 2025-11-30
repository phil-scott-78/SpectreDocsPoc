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

**Having issues?** Check [Terminal Compatibility](/console/reference/compatibility-matrix) for ANSI support, Unicode rendering, and CI environment tips.

---

## Choose Your Learning Path

### New to Spectre.Console?

Start with the tutorials to build foundational skills:

1. [Getting Started: Building a Rich Console App](/console/tutorials/getting-started-building-rich-console-app) - Learn markup, colors, and styles
2. [Asking User Questions](/console/tutorials/interactive-prompts-tutorial) - Add interactive prompts
3. [Showing Status and Spinners](/console/tutorials/status-spinners-tutorial) - Display progress indicators

### Know the Basics?

Jump to task-focused how-to guides:

- [Displaying Tabular Data](/console/how--to/displaying-tabular-data) - Tables with formatting and borders
- [Showing Progress Bars](/console/how--to/showing-progress-bars) - Track long-running operations
- [Live Rendering and Dynamic Updates](/console/how--to/live-rendering-and-dynamic-updates) - Real-time updating displays
- [Testing Console Output](/console/how--to/testing-console-output) - Unit test your console apps

### Building Something Specific?

Browse the [Widget Documentation](#widget-documentation) below, or explore:

- [How-To Guides](/console/how--to) - Step-by-step recipes for common tasks
- [Reference](/console/reference) - Colors, styles, borders, spinners, and emoji lookups

### Want to Understand How It Works?

Dive into the concepts:

- [Spectre.Console vs Traditional Output](/console/explanation/spectre-console-vs-traditional-output) - Why use Spectre.Console?
- [Understanding the Rendering Model](/console/explanation/understanding-rendering-model) - How measure, layout, and render work
- [Best Practices](/console/explanation/best-practices-for-console-applications) - Patterns for production apps
- [Extending with Custom Renderables](/console/explanation/extending-with-custom-renderables) - Build your own widgets

---

## Quick Reference

Essential lookups:

- [Markup Reference](/console/reference/markup-reference) - Tags, colors, escaping, and nesting
- [Color Reference](/console/reference/color-reference) - All named colors and hex formats

---

## Widget Documentation

Explore detailed documentation for each Spectre.Console widget:

### Layout Widgets
Build structured layouts and organize content:
- [Table](/console/widgets/table) - Tabular data with columns, rows, and borders
- [Panel](/console/widgets/panel) - Bordered boxes with headers
- [Grid](/console/widgets/grid) - Row and column layouts without borders
- [Columns](/console/widgets/columns) - Side-by-side content
- [Rows](/console/widgets/rows) - Vertical stacking
- [Layout](/console/widgets/layout) - Complex multi-section layouts
- [Padder](/console/widgets/padder) - Add padding around content
- [Align](/console/widgets/align) - Control content alignment

### Visual Widgets
Create visual elements and decorations:
- [Rule](/console/widgets/rule) - Horizontal dividers and separators
- [Tree](/console/widgets/tree) - Hierarchical data structures
- [Canvas](/console/widgets/canvas) - Pixel-level graphics with Braille characters
- [CanvasImage](/console/widgets/canvas-image) - Display images in console
- [FigletText](/console/widgets/figlet) - Large ASCII art text banners

### Chart Widgets
Visualize data with charts:
- [BarChart](/console/widgets/bar-chart) - Horizontal and vertical bar charts
- [BreakdownChart](/console/widgets/breakdown-chart) - Proportional data segments
- [Calendar](/console/widgets/calendar) - Monthly calendars with events

### Special Widgets
Specialized content rendering:
- [Json](/console/widgets/json) - Syntax-highlighted JSON display
- [TextPath](/console/widgets/text-path) - Smart file path truncation
- [Text](/console/widgets/text) - Precise text rendering control

---

## Live Rendering

Create dynamic, updating console displays:
- [Progress](/console/live/progress) - Progress bars for long-running tasks
- [Status](/console/live/status) - Animated status with spinners
- [Live Display](/console/live/live-display) - Update any content in real-time
- [Async Patterns](/console/explanation/async-patterns) - Best practices for async operations

---

## Interactive Prompts

Gather user input with validation:
- [TextPrompt](/console/prompts/text-prompt) - Text input with validation
- [SelectionPrompt](/console/prompts/selection-prompt) - Single-choice selection
- [MultiSelectionPrompt](/console/prompts/multi-selection-prompt) - Multiple-choice selection