---
title: "Getting Started: Building a Rich Console App"
description: "A beginner-friendly tutorial that walks through creating a simple console application using Spectre.Console"
uid: "console-getting-started"
order: 1030
---

In this tutorial, we'll build your first application with Spectre.Console together. By the end, you'll have a working console app that displays colorful text, formatted tables, and animated progress bars.

## What We're Building

We're going to create a console application that displays:
- Colorful text output with simple markup
- A formatted data table
- Styled text with multiple effects
- Animated progress bars

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- A text editor or IDE (Visual Studio, VS Code, or JetBrains Rider)

## Installation

Let's start by creating a new console application and adding Spectre.Console:

```bash
dotnet new console -n MySpectreApp
cd MySpectreApp
dotnet add package Spectre.Console
```

<Steps>
<Step stepNumber="1">
**Adding Color to Your Output**

Let's start by replacing the default "Hello World" with colored text:

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedExample.ShowColoredHelloWorld
```

Now run your application:

```bash
dotnet run
```

You should see "Hello" displayed in green and "World" in red, followed by an exclamation mark.

Notice how we use square brackets like `[green]` to apply colors? That's Spectre.Console's markup syntax at work. The `[/]` closes the styling.

Great! You've just created your first colorful console output.

</Step>
<Step stepNumber="2">
**Creating Your First Table**

Let's display some structured data in a table:

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedExample.ShowDataTable
```

Run it again:

```bash
dotnet run
```

You should see a neatly formatted table with three columns (Name, Language, Experience) and four rows of data, complete with borders.

Notice how the columns are automatically aligned and the borders are drawn for you? Spectre.Console handles all the spacing and formatting.

Well done! Your first table is rendering perfectly.

</Step>
<Step stepNumber="3">
**Making Text Stand Out**

Let's make an important message stand out using combined styling:

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedExample.ShowTextStyling
```

Run your app once more:

```bash
dotnet run
```

You should see multiple lines demonstrating different colors, bold and italic text, underlines, and even text with colored backgrounds.

Notice how you can combine multiple effects like `[bold red on yellow]`? The styling flows naturally: decoration, foreground color, then `on` followed by background color.

For a complete list of colors and styles, see the [Color Reference](/console/reference/color-reference) and [Text Styling How-To](/console/how--to/styling-text-with-markup-and-color).

Excellent! You're now styling text like a pro.

</Step>
<Step stepNumber="4">
**Showing Progress**

Let's add animated progress bars to show work happening:

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedExample.ShowProgressBar
```

Run your application one final time:

```bash
dotnet run
```

You should see three progress bars animating simultaneously: "Processing files" in green, "Uploading data" in blue, and "Finalizing" in yellow. Watch as they fill up at different speeds!

Notice how all three progress bars update at the same time? Each task runs independently, and Spectre.Console handles the rendering for you.

Fantastic! You've built a complete console application with all the core Spectre.Console features.

</Step>
</Steps>

## What You've Learned

You've successfully created a console application that uses:
- Colored text with markup syntax
- Formatted tables with automatic alignment
- Combined text styling (colors, decorations, backgrounds)
- Animated progress bars with multiple tasks

## Continue Your Journey

Ready for more? Try these tutorials next:

- [Asking User Questions](/console/tutorials/interactive-prompts-tutorial) - Ask for names, numbers, and let users choose from lists (15 minutes)
- [Showing Status and Spinners](/console/tutorials/status-spinners-tutorial) - Display animated spinners while work is happening (10 minutes)

You can also explore the [Widget Reference](/console/widgets/table) to see what else Spectre.Console can do.