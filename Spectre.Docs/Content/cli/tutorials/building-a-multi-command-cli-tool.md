---
title: "Building a Multi-Command CLI Tool"
description: "Build a CLI application with multiple commands, subcommands, and shared settings"
uid: "cli-multi-command-tutorial"
order: 1020
---

In this tutorial, we'll build a package manager CLI together. By the end, we'll have a tool with multiple commands organized into a hierarchy, sharing common options across all of them.

## What We're Building

Here's how our CLI will work when we're done:

<Screenshot Src="/assets/cli-multi-command-tutorial.svg" Alt="Multi Command Tutorial Screen Recording" />

## Prerequisites

- Completed the [Quick Start tutorial](xref:cli-quick-start)
- .NET 6.0 or later

<Steps>
<Step stepNumber="1">
**Adding Multiple Commands**

Let's start by creating a new project and adding the Spectre.Console.Cli package:

```bash
dotnet new console -n PackageManager
cd PackageManager
dotnet add package Spectre.Console.Cli
```

Now replace `Program.cs` with two commands - one to add packages and one to list them:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step1.AddCommand
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step1.ListCommand
```

Wire them up using `CommandApp` with `Configure()`:

```csharp
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add");
    config.AddCommand<ListCommand>("list");
});
return app.Run(args);
```

Run the commands:

```bash
dotnet run -- add Newtonsoft.Json
# Added package Newtonsoft.Json

dotnet run -- list
# Packages:
#   (none yet)
```

Both commands work. Try running `dotnet run -- --help` to see the auto-generated help listing both commands. The CLI knows about `add` and `list` without any extra configuration.

</Step>
<Step stepNumber="2">
**Organizing Commands with Branches**

Our `add` command works, but real CLIs often have subcommands. Let's refactor so users can run `add package` and `add reference` separately.

Update `Program.cs` to use `AddBranch()`:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step2.AddPackageCommand
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step2.AddReferenceCommand
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Step2.ListCommand
```

Configure the branch structure:

```csharp
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddBranch("add", add =>
    {
        add.AddCommand<AddPackageCommand>("package");
        add.AddCommand<AddReferenceCommand>("reference");
    });
    config.AddCommand<ListCommand>("list");
});
return app.Run(args);
```

Now the commands are organized hierarchically:

```bash
dotnet run -- add package Newtonsoft.Json
# Added package Newtonsoft.Json

dotnet run -- add reference ../MyLib/MyLib.csproj
# Added reference to ../MyLib/MyLib.csproj

dotnet run -- add --help
# Shows 'package' and 'reference' as subcommands
```

The `add` branch groups related commands together. Users can run `add --help` to discover what's available.

</Step>
<Step stepNumber="3">
**Complete CLI with Shared Settings**

Most CLIs have options that apply everywhere - things like `--verbose` or `--quiet`. Let's add a shared `--verbose` flag to all our commands.

Create a base settings class that other settings inherit from:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished.GlobalSettings
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished.AddPackageCommand
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished.AddReferenceCommand
T:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished.ListCommand
```

The configuration stays the same - each command's settings inherit from `GlobalSettings`:

```csharp
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddBranch("add", add =>
    {
        add.AddCommand<AddPackageCommand>("package");
        add.AddCommand<AddReferenceCommand>("reference");
    });
    config.AddCommand<ListCommand>("list");
});
return app.Run(args);
```

The `--verbose` flag now works across all commands:

```bash
dotnet run -- add package Serilog --version 3.0.0 --verbose
# Searching for package Serilog...
# Resolving version 3.0.0...
# Installing to ./packages...
# Added package Serilog v3.0.0

dotnet run -- list --verbose
# Reading project file...
# Packages:
#   Newtonsoft.Json (13.0.1)
# References:
#   ../MyLib/MyLib.csproj
```

Settings inheritance keeps your code DRY. Define common options once, use them everywhere.

</Step>
</Steps>

## Congratulations!

You've built a multi-command CLI with:
- Multiple commands registered via `AddCommand<T>()`
- Hierarchical organization using `AddBranch()`
- Shared settings through inheritance
- Auto-generated help at every level

These same patterns scale to CLIs with dozens of commands and deep nesting.

## Next Steps

- <xref:cli-app-configuration> - Customize application behavior, add aliases, and set examples
- <xref:cli-command-hierarchies> - Build even deeper command structures
- <xref:cli-async-commands> - Handle long-running operations with proper cancellation support

## Related Console Tutorials

Looking to enhance your CLI output? Check out these Spectre.Console tutorials:

- <xref:console-getting-started> - Add tables, colors, and formatting to your command output
- <xref:console-status-spinners> - Display progress while packages install
