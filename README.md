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
└── assets/           # Images, WebP animations, etc.
```

Each section has its own content service and front matter model, configured in `Program.cs:28-40`.

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

## Creating WebP Animated Samples

Animated samples show your library features in action. Here's the complete workflow:

### Step 1: Write a Sample Class

Create a new sample in `Spectre.Docs.Examples/AsciiCast/Samples/`:

```csharp
public class MyFeatureSample : BaseSample
{
    // Optional: Customize console dimensions (default is 82x24)
    public override (int Cols, int Rows) ConsoleSize => (82, 20);

    // Your sample code
    public override void Run(IAnsiConsole console)
    {
        console.Write(
            new Panel("[bold yellow]Hello, Spectre![/]")
                .BorderColor(Color.Blue));

        // Simulate work
        Thread.Sleep(2000);
    }
}
```

The class name determines the output filename (`MyFeatureSample` → `my-feature.webp`).

### Step 2: Install Prerequisites

Install the required tools:

- **agg**: `cargo install agg` or download from [GitHub](https://github.com/asciinema/agg)
- **ffmpeg**: Download from [ffmpeg.org](https://ffmpeg.org/download.html)

Verify installation:
```bash
agg --version
ffmpeg -version
```

### Step 3: Generate WebP Files

Run the build script from the `Spectre.Docs.Examples` directory:

```powershell
.\Build-Samples.ps1
```

This script:
1. Runs all `BaseSample` classes and captures output as `.cast` (AsciiCast JSON)
2. Converts `.cast` → `.gif` using `agg` (Dracula theme, Cascadia Code font, 18px, 0.75x speed)
3. Converts `.gif` → `.webp` using `ffmpeg` (lossy compression, quality 75)
4. Cleans up intermediate `.cast` and `.gif` files
5. Outputs final `.webp` files to `Content/assets/`

### Step 4: Reference in Documentation

Use standard markdown image syntax:

```markdown
![Feature Demo](/assets/my-feature.webp)
```

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
