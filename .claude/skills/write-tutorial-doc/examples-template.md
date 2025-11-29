# C# Example Templates for Tutorials

Create example files at:
- Console: `Spectre.Docs.Examples/SpectreConsole/Tutorials/{TutorialName}Tutorial.cs`
- CLI: `Spectre.Docs.Examples/SpectreCli/Tutorials/{TutorialName}Tutorial.cs`

## Key Concept: Visible Progress

Unlike how-to guides (which build incrementally toward a solution), tutorial examples focus on **immediate, visible results**. Each method should produce output that the learner can see and verify—building their confidence step by step.

## File Structure Template

```csharp
using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// {Brief description of what this tutorial builds}.
/// </summary>
public class {TutorialName}Tutorial
{
    /// <summary>
    /// {What this step teaches - matches step title}.
    /// </summary>
    public void {StepMethodName}()
    {
        // Implementation that produces visible output
        AnsiConsole.MarkupLine("[green]Hello[/] [red]World[/]!");
    }

    /// <summary>
    /// {What the next step teaches}.
    /// </summary>
    public void {NextStepMethodName}()
    {
        // Next step implementation with visible output
    }

    // Additional steps...

    /// <summary>
    /// Complete example demonstrating all tutorial concepts.
    /// </summary>
    public void ShowComplete{Feature}()
    {
        // Combined implementation showing the full output
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        // ... rest of the complete output
    }
}
```

## Rules

1. **XML Doc Required**: Every public method and the class MUST have a `<summary>` comment
2. **Class Signature**: Always `public class {TutorialName}Tutorial`
3. **Method Signature**: Always `public void` (instance methods)
4. **Visible Output**: Every step MUST produce output the learner can see
5. **Self-Contained Steps**: Each method should work independently (no shared state)
6. **Descriptive Names**: Use names that match the tutorial step titles
7. **Complete Method Required**: Include `ShowComplete{Feature}()` demonstrating the full output
8. **Realistic Data**: Use meaningful sample data that tells a story

## Example: Markup Styling Tutorial

```csharp
using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Tutorials;

/// <summary>
/// A tutorial that builds a styled build-output display step by step.
/// Teaches markup syntax, custom colors, combined styles, links, and safe interpolation.
/// </summary>
public class MarkupStylingTutorial
{
    /// <summary>
    /// Displays a success message in green using basic markup syntax.
    /// </summary>
    public void ShowSuccessMessage()
    {
        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
    }

    /// <summary>
    /// Displays a warning message using a custom hex color and mixed styling.
    /// </summary>
    public void ShowWarningMessage()
    {
        AnsiConsole.MarkupLine("[#FFA500]⚠[/] [yellow]3 warnings[/] in Authentication.cs");
    }

    /// <summary>
    /// Displays an error message using combined bold and red styling.
    /// </summary>
    public void ShowErrorMessage()
    {
        AnsiConsole.MarkupLine("[bold red]✗ Error:[/] Missing dependency 'Newtonsoft.Json'");
    }

    /// <summary>
    /// Displays a clickable documentation link.
    /// </summary>
    public void ShowDocumentationLink()
    {
        AnsiConsole.MarkupLine("  → See: [link=https://docs.example.com/dependencies]documentation[/]");
    }

    /// <summary>
    /// Demonstrates safe interpolation for dynamic content that might contain brackets.
    /// </summary>
    public void ShowDynamicContent()
    {
        var fileName = "Authentication.cs";
        var warningCount = 3;
        AnsiConsole.MarkupLineInterpolated($"[#FFA500]⚠[/] [yellow]{warningCount} warnings[/] in {fileName}");
    }

    /// <summary>
    /// Displays the complete build output combining all techniques.
    /// </summary>
    public void ShowCompleteBuildOutput()
    {
        var fileName = "Authentication.cs";
        var dependency = "Newtonsoft.Json";

        AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
        AnsiConsole.MarkupLineInterpolated($"[#FFA500]⚠[/] [yellow]3 warnings[/] in {fileName}");
        AnsiConsole.MarkupLineInterpolated($"[bold red]✗ Error:[/] Missing dependency '{dependency}'");
        AnsiConsole.MarkupLine("  → See: [link=https://docs.example.com/dependencies]documentation[/]");
    }
}
```

## Method Naming Guidelines

Method names should match the step titles in the tutorial:

| Step Title | Method Name |
|------------|-------------|
| "Display a Success Message" | `ShowSuccessMessage` |
| "Add a Warning Message" | `ShowWarningMessage` |
| "Show an Error Message" | `ShowErrorMessage` |
| "Include a Documentation Link" | `ShowDocumentationLink` |
| "Complete Build Output" | `ShowCompleteBuildOutput` |

Use action verbs that describe what the learner will see:
- `Show{Feature}` - Displays something
- `Create{Thing}` - Builds and displays something
- `ShowComplete{Feature}` - The final method showing everything together

## Visible Output Requirements

Every tutorial step must produce output. This is critical for learner confidence:

**Good - produces visible output:**
```csharp
public void ShowBasicTable()
{
    var table = new Table();
    table.AddColumn("Name");
    table.AddRow("Alice");
    AnsiConsole.Write(table); // Learner sees the table
}
```

**Bad - no visible output:**
```csharp
public void CreateTable()
{
    var table = new Table();
    table.AddColumn("Name");
    table.AddRow("Alice");
    // Nothing displayed - learner can't verify it worked!
}
```

## Self-Contained Steps

Each method should work independently. Don't rely on state from previous steps:

**Good - self-contained:**
```csharp
public void ShowStyledTable()
{
    var table = new Table()
        .Border(TableBorder.Rounded)
        .BorderColor(Color.Blue);
    table.AddColumn("Name");
    table.AddRow("Alice");
    AnsiConsole.Write(table);
}
```

**Bad - depends on previous state:**
```csharp
private Table _table; // Shared state

public void CreateTable()
{
    _table = new Table();
    _table.AddColumn("Name");
}

public void StyleTable()
{
    _table.Border(TableBorder.Rounded); // Fails if CreateTable wasn't called first
    AnsiConsole.Write(_table);
}
```

## XmlDocId Reference

The XmlDocId for referencing examples in markdown:

```
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.{TutorialName}Tutorial.{MethodName}
M:Spectre.Docs.Examples.SpectreCli.Tutorials.{TutorialName}Tutorial.{MethodName}
```

Examples:
```
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.MarkupStylingTutorial.ShowSuccessMessage
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.MarkupStylingTutorial.ShowCompleteBuildOutput
```

## Checklist

- [ ] File is in correct Tutorials folder (`SpectreConsole/Tutorials/` or `SpectreCli/Tutorials/`)
- [ ] Class is `public class {TutorialName}Tutorial`
- [ ] Namespace matches folder structure
- [ ] Class has XML doc `<summary>`
- [ ] Every method has XML doc `<summary>`
- [ ] Every method is `public void` (instance method)
- [ ] Every method produces visible output
- [ ] Methods are self-contained (no shared state)
- [ ] Method names match tutorial step titles
- [ ] `ShowComplete{Feature}()` method demonstrates the full output
- [ ] Examples use realistic, meaningful data
