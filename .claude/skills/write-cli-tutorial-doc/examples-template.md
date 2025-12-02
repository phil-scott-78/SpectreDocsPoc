# Demo Application Templates for CLI Tutorials

Create demo applications at: `Spectre.Docs.Cli.Examples/DemoApps/{TutorialName}/`

## Key Concept: Step-Based Code Organization

CLI tutorials build **complete, runnable applications** with code that evolves across steps. Each step gets its own subfolder and namespace, enabling:
- Roslyn-powered `csharp:xmldocid` code extraction in markdown
- Clean separation between tutorial progression stages
- No duplicate class name conflicts

## Folder Structure

```
Spectre.Docs.Cli.Examples/DemoApps/{TutorialName}/
  ├── {Step1Name}/Main.cs    → namespace ...{TutorialName}.{Step1Name}
  ├── {Step2Name}/Main.cs    → namespace ...{TutorialName}.{Step2Name}
  └── Complete/Main.cs       → namespace ...{TutorialName}.Complete
```

Each step folder contains a `Main.cs` with its own namespace. The markdown references code via xmldocid:

````markdown
```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand.GreetCommand
```
````

## File Template

```csharp
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.{TutorialName}.{StepName};

/// <summary>
/// {Brief description of what this step demonstrates}.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "{tutorial-name}";
    public string Description => "{One-line description for the demo list}";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<{MainCommand}>();
        return await app.RunAsync(args);
    }
}

// Command implementations follow...
```

## Rules

1. **Step Folders**: Each tutorial step gets its own folder (e.g., `FirstCommand/`, `Complete/`)
2. **Namespace Convention**: `Spectre.Docs.Cli.Examples.DemoApps.{TutorialName}.{StepName}`
3. **IDemoApp Required**: Every step must implement `IDemoApp`
4. **Class Name**: Always `Demo` (namespace provides uniqueness)
5. **Name Property**: Kebab-case matching tutorial folder (e.g., `"quick-start"`)
6. **Complete Application**: Each step must be fully runnable
7. **Use System.Console**: Use `System.Console.WriteLine` for output

## Example: Quick Start Tutorial

### Step 1: FirstCommand (basic argument only)

**File:** `Spectre.Docs.Cli.Examples/DemoApps/QuickStart/FirstCommand/Main.cs`

```csharp
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand;

/// <summary>
/// Step 1: Basic command with a required argument.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "quick-start";
    public string Description => "Quick Start tutorial: greeting CLI with arguments and options.";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return await app.RunAsync(args);
    }
}

internal class GreetCommand : Command<GreetCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Hello, {settings.Name}!");
        return 0;
    }
}
```

### Step 2: Complete (argument + options)

**File:** `Spectre.Docs.Cli.Examples/DemoApps/QuickStart/Complete/Main.cs`

```csharp
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.QuickStart.Complete;

/// <summary>
/// Step 2: Full command with argument and options.
/// </summary>
public class Demo : IDemoApp
{
    public string Name => "quick-start";
    public string Description => "Quick Start tutorial: greeting CLI with arguments and options.";

    public async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<GreetCommand>();
        return await app.RunAsync(args);
    }
}

internal class GreetCommand : Command<GreetCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name to greet")]
        public string Name { get; init; } = string.Empty;

        [CommandOption("-c|--count")]
        [Description("Number of times to greet")]
        [DefaultValue(1)]
        public int Count { get; init; } = 1;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        for (var i = 0; i < settings.Count; i++)
        {
            System.Console.WriteLine($"Hello, {settings.Name}!");
        }
        return 0;
    }
}
```

## Referencing Code in Markdown

Use `csharp:xmldocid` to pull code directly from the demo apps:

````markdown
```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand.GreetCommand
```
````

This extracts the entire `GreetCommand` class from the `FirstCommand` step.

**XML Doc ID patterns:**
- `T:Namespace.ClassName` - Entire class
- `M:Namespace.ClassName.MethodName` - Specific method
- `P:Namespace.ClassName.PropertyName` - Specific property

## Running Demo Applications

```bash
# From the Spectre.Docs.Cli.Examples directory
cd Spectre.Docs.Cli.Examples

# List all available demos
dotnet run

# Run a specific demo (uses the Complete step by default)
dotnet run -- quick-start Alice

# Run with options
dotnet run -- quick-start Alice --count 3
```

## Folder Naming Guidelines

| Tutorial Topic | Tutorial Folder | Step Folders | Demo Name |
|----------------|-----------------|--------------|-----------|
| Quick Start | `QuickStart` | `FirstCommand`, `Complete` | `quick-start` |
| Multi-Command CLI | `MultiCommand` | `SingleCommand`, `Complete` | `multi-command` |
| Adding Validation | `Validation` | `Basic`, `WithValidation` | `validation` |

Use PascalCase for folder names; the demo `Name` property uses kebab-case.

## Console Output Guidelines

CLI tutorials use standard console output, not Spectre.Console rich formatting:

**Good - plain console output:**
```csharp
System.Console.WriteLine($"Hello, {settings.Name}!");
System.Console.WriteLine("Items:");
System.Console.WriteLine("  - Item 1");
```

**Avoid - Spectre.Console rich output:**
```csharp
AnsiConsole.MarkupLine("[green]Hello[/], [blue]{settings.Name}[/]!");
```

The focus is on CLI structure and behavior, not visual presentation. Console tutorials cover rich output.

## Command Signature Notes

The current version of Spectre.Console.Cli requires `CancellationToken` in the `Execute` signature:

```csharp
// Correct signature for Spectre.Console.Cli
protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)

// For commands without settings
protected override int Execute(CommandContext context, CancellationToken cancellation)
```

## Checklist

- [ ] Each step has its own folder under `DemoApps/{TutorialName}/`
- [ ] Each step has its own namespace: `...DemoApps.{TutorialName}.{StepName}`
- [ ] Each step implements `IDemoApp`
- [ ] Demo class is named `Demo`
- [ ] `Name` property uses kebab-case matching tutorial folder
- [ ] `Description` is brief and descriptive
- [ ] Each step is complete and runnable
- [ ] Uses `System.Console.WriteLine` for output
- [ ] Command `Execute` methods include `CancellationToken` parameter
- [ ] All settings properties have `[Description]` attributes
- [ ] Markdown uses `csharp:xmldocid` to reference step code
