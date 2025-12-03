# Spectre.Docs - Documentation Site Guide

This is the documentation website for **Spectre.Console** (rich console UI library) and **Spectre.CLI** (command-line application framework). This guide will help you contribute documentation, add samples, and work with the site effectively.

## Technology Stack

- **.NET 9.0** - Blazor Server application
- **MyLittleContentEngine** - Markdown-based content management framework
- **MonorailCss** - Utility-first CSS framework
- **Roslyn** - Code analysis for live code sample integration

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- For creating animated samples (optional):
  - [agg](https://github.com/asciinema/agg) - AsciiCast to GIF converter
  - [ffmpeg](https://ffmpeg.org/download.html) - Media conversion tool

### Running the Site Locally

Navigate to the `Spectre.Docs` directory and run:

```bash
dotnet run
```

The application will be available at `https://localhost:5001` (or check console output for the exact port).

The project is configured with **hot reload** - changes to markdown files in `Content/**/*` will automatically refresh the site during development.

### Deploying

```bash
dotnet run -- build
```

## Content Organization

The site uses a **multi-service content architecture** with three main sections:

```
Content/
├── console/          # Spectre.Console documentation
│   ├── how--to/
│   ├── tutorials/
│   ├── widgets/
│   ├── live/
│   ├── prompts/
│   ├── reference/
│   └── explanation/
├── cli/              # Spectre.CLI documentation
│   ├── how--to/
│   ├── tutorials/
│   ├── reference/
│   └── explanation/
├── blog/             # Blog posts and release notes
└── assets/           # Images, SVG animations, etc.
```

Each section has its own content service and front matter model, configured in `Program.cs`.

## Adding Documentation

### Console or CLI Documentation

Create a `.md` file in the appropriate subdirectory (e.g., `Content/console/how--to/my-guide.md`):

```markdown
---
title: "Your Page Title"
description: "SEO-friendly description of the page"
date: 2025-01-15
tags: ["tag1", "tag2"]
section: "Console"
uid: "console-unique-id"
order: 100
---

# Your Content Here

Start writing your documentation using standard markdown...
```

**Front Matter Fields:**

- `title` (required) - Page title shown in navigation and browser
- `description` (required) - Used for SEO and metadata
- `order` (required) - Sidebar navigation order (lower = higher in list)
- `section` - Section name for breadcrumbs
- `uid` - Unique identifier for cross-referencing
- `date` - Publication date
- `tags` - Array of topic tags
- `isDraft` - Set to `true` to hide from production
- `redirectUrl` - Redirect old URLs to new locations

The file path determines the URL:
- `Content/console/how--to/example.md` → `/console/how--to/example`

### Blog Posts

Create a `.md` file in `Content/blog/`:

```markdown
---
title: "Spectre.Console 0.51.0: Amazing New Features"
author: "Your Name"
description: "Brief summary of the blog post"
date: 2025-01-15
tags: ["release", "features"]
series: "Release Notes"
isDraft: false
---

Your blog content here...
```

**Additional Blog Fields:**

- `author` - Author name
- `series` - Groups related posts together (optional)

Blog posts are automatically sorted by date (newest first) and appear at `/blog/your-file-name`.

## Creating SVG Animated Samples

Animated samples show your library features in action using VCR tape recordings. The project supports two types of samples: **Console samples** (widget demonstrations) and **CLI samples** (command-line application tutorials).

### Prerequisites

Install VCR from [GitHub](https://github.com/phil-scott-78/vcr).

### Console Samples

Console samples demonstrate Spectre.Console widgets and features. They run with no command-line arguments and assume the project has been built.

#### Step 1: Write a Sample Class

Create a new sample in `Spectre.Docs.Examples/Showcase/`:

```csharp
public class MyFeatureSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        console.Write(
            new Panel("[bold yellow]Hello, Spectre![/]")
                .BorderColor(Color.Blue));

        Thread.Sleep(2000);
    }
}
```

The class name determines the sample name (`MyFeatureSample` → `my-feature`).

#### Step 2: Create a VCR Tape

Create a `.tape` file in `Spectre.Docs.Examples/VCR/`:

```
Output "Spectre.Docs/Content/assets/my-feature.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 12
Set EndBuffer 5s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase my-feature"
```

**Key points:**
- Use `--no-build` because the build script compiles the project first
- The sample name in `showcase <name>` matches the kebab-case class name (minus "Sample")

### CLI Samples

CLI samples demonstrate Spectre.CLI command-line application patterns. They use an environment variable to select which demo to run from the compiled `example.exe`.

#### Step 1: Create a Demo App

Create a demo in `Spectre.Docs.Cli.Examples/DemoApps/[Tutorial]/[SubFolder]/`:

```csharp
namespace Spectre.Docs.Cli.Examples.DemoApps.MyTutorial.Complete;

public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<MyCommand>();
        return await app.RunAsync(args);
    }
}
```

#### Step 2: Create a VCR Tape

Create a `.tape` file in `Spectre.Docs.Cli.Examples/VCR/`:

```
Output "Spectre.Docs/Content/assets/cli-my-tutorial.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 70
Set Rows 12
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.MyTutorial.Complete.Demo.RunAsync(System.String[])"

Type "./example --help"
Sleep 400ms
Enter
Sleep 2s

Type "./example Alice"
Sleep 400ms
Enter
Sleep 1s
```

**Key points:**
- `SPECTRE_APP` is set to the XmlDocId of the demo's `RunAsync` method
- Use `Type` and `Enter` commands to simulate interactive terminal input
- The working directory points to the compiled binary location

### Generate SVG Files

Run the build script from the repository root:

```powershell
.\Generate-WidgetScreenshots.ps1
```

This script:
1. Builds both `Spectre.Docs.Examples` and `Spectre.Docs.Cli.Examples`
2. Finds all `.tape` files in both VCR directories
3. Executes each tape with VCR to generate SVG files
4. Outputs SVG files to `Spectre.Docs/Content/assets/`

### Reference in Documentation

Use the <Screenshot> razor component

<Screenshot Src="/assets/my-image.svg" Alt="Alt Text" />

## Linking to Code with xmldocid

The site uses **Roslyn integration** to embed live code from your source files directly into documentation. This ensures code samples are always up-to-date with the actual implementation.

### Syntax

Use the special code fence `` ```csharp:xmldocid ``:

````markdown
```csharp:xmldocid
M:Spectre.Docs.Examples.AsciiCast.Samples.ProgressSample.Run(Spectre.Console.IAnsiConsole)
M:Spectre.Docs.Examples.AsciiCast.Samples.ProgressSample.CreateTasks(Spectre.Console.ProgressContext,System.Random)
```
````

### XML Doc ID Format

Use standard .NET XML documentation identifiers:

| Type | Prefix | Example |
|------|--------|---------|
| Method | `M:` | `M:Namespace.ClassName.MethodName(Param.Type)` |
| Type/Class | `T:` | `T:Spectre.Console.Examples.MyExample` |
| Property | `P:` | `P:Namespace.ClassName.PropertyName` |
| Field | `F:` | `F:Namespace.ClassName.FieldName` |

**Finding XML Doc IDs:**

1. Navigate to the source file containing the code
2. For methods: `M:` + full namespace + class + method name + parameter types
3. For classes: `T:` + full namespace + class name

**Example from progress.md:**

````markdown
```csharp:xmldocid
M:Spectre.Docs.Examples.AsciiCast.Samples.ProgressSample.Run(Spectre.Console.IAnsiConsole)
```
````

This extracts the `Run` method from `ProgressSample.cs` and renders it with syntax highlighting.

### How It Works

The site is configured to analyze the solution via Roslyn (`Program.cs:42-45`):

```csharp
.WithConnectedRoslynSolution(_ => new CodeAnalysisOptions
{
    SolutionPath = "../Spectre.Docs.sln",
})
```

When MyLittleContentEngine encounters `csharp:xmldocid`:
1. Parses the XML Doc ID
2. Uses Roslyn to find the symbol in the compiled solution
3. Extracts the source code
4. Renders as syntax-highlighted HTML

This approach provides:
- **Always up-to-date code** - Changes in source automatically reflect in docs
- **Type safety** - Broken references fail at build time
- **No duplication** - Single source of truth for code


## Project Architecture

### Key Files

- **Program.cs** - Application startup, content service registration, Roslyn configuration
- **Components/Pages/BlogDetails.razor** - Blog post rendering
- **Components/Layout/MainLayout.razor** - Documentation layout with sidebar
- **Content/** - All markdown content
- **Spectre.Docs.Examples/** - Sample code project

### Content Engine Integration

The site uses three separate `IMarkdownContentService` instances:

1. **Console Service** - TOC key: `"console"`
2. **CLI Service** - TOC key: `"cli"`
3. **Blog Service** - No TOC, date-sorted listing

Services are registered using the fluent API in `Program.cs:28-40`.

### Navigation Generation

The `ITableOfContentService` generates hierarchical navigation from:
- File structure (folders become sections)
- Front matter `order` property (controls position)
- Front matter `title` property (display name)
