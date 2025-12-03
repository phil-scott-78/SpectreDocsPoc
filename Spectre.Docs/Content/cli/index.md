---
title: "Spectre.Console.Cli Documentation"
description: "Build powerful command-line applications with Spectre.Console.Cli"
uid: "cli-index"
---

Welcome to the Spectre.Console.Cli documentation! Spectre.Console.Cli is a modern framework for building command-line applications in .NET with a focus on type safety and developer experience.

## Start Here

Get Spectre.Console.Cli running in seconds:

```bash
dotnet add package Spectre.Console.Cli
```

Then try this quick example that creates a simple greeting command:

```csharp
using Spectre.Console.Cli;
using System.ComponentModel;

var app = new CommandApp<GreetCommand>();
return app.Run(args);

public class GreetSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name to greet")]
    public required string Name { get; init; }

    [CommandOption("-c|--count")]
    [Description("Number of times to greet")]
    [DefaultValue(1)]
    public int Count { get; init; }
}

public class GreetCommand : Command<GreetSettings>
{
    public override int Execute(CommandContext context, GreetSettings settings)
    {
        for (int i = 0; i < settings.Count; i++)
        {
            AnsiConsole.MarkupLine($"Hello, [green]{settings.Name}[/]!");
        }
        return 0;
    }
}
```

Run it with `dotnet run -- World --count 3` to see typed argument parsing in action.

---

## Choose Your Learning Path

### New to Spectre.Console.Cli?

Start with the tutorials to build foundational skills:

1. <xref:cli-quick-start> - Create your first command with arguments and options
2. <xref:cli-multi-command-tutorial> - Add multiple commands with shared settings
3. <xref:cli-di-tutorial> - Integrate with DI containers

### Know the Basics?

Jump to task-focused how-to guides:

- <xref:cli-commands-arguments> - Use attributes for type-safe parameters
- <xref:cli-required-options> - Enforce required options and validate input
- <xref:cli-error-handling> - Graceful error handling and custom exit codes
- <xref:cli-testing> - Unit test your CLI apps

### Building Something Specific?

Browse the complete [How-To Guides](#how-to-guides) below, or explore:

- [Reference](#reference-documentation) - Attributes, type converters, and built-in behaviors
- [Explanation](#concepts-and-design) - Understand the design philosophy and command lifecycle

### Want to Understand How It Works?

Dive into the concepts:

- <xref:cli-design-philosophy> - Why Spectre.Console.Cli works the way it does
- <xref:cli-command-lifecycle> - How parsing, validation, and execution work

---

## Quick Reference

Essential lookups:

- <xref:cli-attributes-parameters> - All attributes and their options
- <xref:cli-type-converters> - Built-in types and custom converters

---

## How-To Guides

Practical guides for specific tasks:

### Defining and Configuring Commands
- <xref:cli-commands-arguments> - Use `[CommandArgument]` and `[CommandOption]` attributes
- <xref:cli-required-options> - Enforce required options with the `required` keyword
- <xref:cli-app-configuration> - Register commands, set aliases, and configure global settings
- <xref:cli-command-hierarchies> - Create nested command structures with `AddBranch`

### Advanced Features
- <xref:cli-async-commands> - Use `AsyncCommand<T>` and handle Ctrl+C gracefully
- <xref:cli-help-customization> - Application name, examples, and custom help providers
- <xref:cli-command-interception> - Implement `ICommandInterceptor` for cross-cutting concerns
- <xref:cli-hidden-commands> - Hide internal commands from help output

### Error Handling and Testing
- <xref:cli-error-handling> - Exception handling and custom exit codes
- <xref:cli-testing> - Use `CommandAppTester` for unit testing

---

## Reference Documentation

API documentation and quick lookups:

- <xref:cli-attributes-parameters> - Complete reference for `[CommandArgument]`, `[CommandOption]`, and more
- <xref:cli-type-converters> - Built-in type support and custom type conversion
- <xref:cli-command-context> - Properties and usage of the `CommandContext` class
- <xref:cli-built-in-behaviors> - The `version`, `explain`, `xmldoc`, and `opencli` commands

---

## Concepts and Design

Understand how Spectre.Console.Cli works:

- <xref:cli-design-philosophy> - The rationale behind library design choices
- <xref:cli-command-lifecycle> - Deep-dive into parsing, validation, and execution phases
