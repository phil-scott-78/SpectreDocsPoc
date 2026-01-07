---
title: "Building a Rich Console App"
description: "A beginner-friendly tutorial that walks through creating a simple console application using Spectre.Console"
uid: "console-getting-started"
order: 1030
---

In this tutorial, we'll build a styled build-output display together. By the end, we'll have a complete status reporter that shows success messages in green, warnings in orange, errors in bold red, and includes clickable documentation links.

## What We're Building

Here's the output we're creating:

<Screenshot Src="/assets/getting-started-tutorial.svg" Alt="Getting Started Output" />

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- A text editor or IDE (Visual Studio, VS Code, or JetBrains Rider)

<Steps>
<Step stepNumber="1">
**Create a New Project**

Let's start by creating a new console application:

```bash
dotnet new console -n MySpectreApp
cd MySpectreApp
```

A new folder called `MySpectreApp` appears with a basic console application inside.

Our project is ready.

</Step>
<Step stepNumber="2">
**Add Spectre.Console**

Now let's add the Spectre.Console package:

```bash
dotnet add package Spectre.Console
```

The output confirms the package was installed. Now we're ready to write some code.

</Step>
<Step stepNumber="3">
**Display a Success Message**

Let's start with a simple success message. We wrap text in color tags using `[green]text[/]`:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.ShowSuccessMessage
```

Run the code:

```bash
dotnet run
```

"✓ Build completed successfully" appears in green. See how `[/]` closes the style? Any text after it returns to the default color.

Done - our first styled message.

</Step>
<Step stepNumber="4">
**Add a Warning Message**

Now let's add a warning. We'll use a hex color code `[#FFA500]` for the orange symbol, and mix styled and plain text on the same line:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.ShowWarningMessage(System.String)
```

Run the code:

```bash
dotnet run
```

The "⚠" appears in orange, "3 warnings" in yellow, and "in Authentication.cs" in the default color. Each style closes with `[/]` before the next one begins.

Now we're mixing colors and plain text on a single line.

</Step>
<Step stepNumber="5">
**Show an Error Message**

Errors need to stand out. Let's combine bold and red in a single tag by separating them with a space:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.ShowErrorMessage(System.String)
```

Run the code:

```bash
dotnet run
```

"✗ Error:" appears in bold red, followed by the dependency name in the default style. The `[bold red]` tag applies both styles at once.

Our error message really stands out now.

</Step>
<Step stepNumber="6">
**Include a Documentation Link**

Let's help users find more information by adding a clickable link. We use `[link=URL]text[/]` to show friendly text instead of a raw URL:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.ShowDocumentationLink(System.String)
```

Run the code:

```bash
dotnet run
```

Look for "→ See: documentation" where "documentation" is a clickable link (in terminals that support it). The link opens the URL when clicked.

We've added helpful navigation to our output.

</Step>
<Step stepNumber="7">
**Handle Dynamic Content**

Real build tools display filenames and counts that come from variables. Let's use `MarkupLineInterpolated()` to safely include dynamic values:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.ShowDocumentationLink(System.String)
```

Run the code:

```bash
dotnet run
```

The same warning message appears, but now the filename and count come from variables. `MarkupLineInterpolated()` automatically escapes any brackets in the interpolated values, preventing markup parsing errors.

See the [Markup Widget](xref:console-widget-markup) reference for more on escaping.

</Step>
<Step stepNumber="8">
**Complete Build Output**

Now let's combine everything into our final build-output display:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.GettingStartedTutorial.Run(Spectre.Console.IAnsiConsole)
```

Run the code:

```bash
dotnet run
```

All four lines of our build output appear: the green success message, the orange/yellow warning with the filename, the bold red error with the dependency name, and the clickable documentation link.

Our build-output display is complete.

</Step>
</Steps>

## Congratulations!

You've created a styled build-output display from scratch. Your reporter shows success in green, warnings in custom orange, errors in bold red, and includes clickable documentation links - all with safe handling of dynamic content.

Apply these patterns to any console application: log viewers, deployment scripts, test runners, and more.

## Next Steps

- <xref:console-interactive-prompts> - Add interactive prompts to collect user input
- <xref:console-status-spinners> - Display animated spinners while work is happening
- <xref:console-markup-reference> - Complete markup syntax guide
- <xref:console-color-reference> - Explore all available color names and formats
