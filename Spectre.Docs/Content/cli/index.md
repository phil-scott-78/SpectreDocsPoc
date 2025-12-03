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

1. [Quick Start: Your First CLI App](/cli/tutorials/quick-start-your-first-cli-app) - Create your first command with arguments and options
2. [Building a Multi-Command CLI Tool](/cli/tutorials/building-a-multi-command-cli-tool) - Add multiple commands with shared settings
3. [Dependency Injection in CLI Apps](/cli/tutorials/dependency-injection-in-cli-apps) - Integrate with DI containers

### Know the Basics?

Jump to task-focused how-to guides:

- [Defining Commands and Arguments](/cli/how--to/defining-commands-and-arguments) - Use attributes for type-safe parameters
- [Making Options Required](/cli/how--to/making-options-required) - Enforce required options and validate input
- [Handling Errors and Exit Codes](/cli/how--to/handling-errors-and-exit-codes) - Graceful error handling and custom exit codes
- [Testing Command-Line Applications](/cli/how--to/testing-command-line-applications) - Unit test your CLI apps

### Building Something Specific?

Browse the complete [How-To Guides](#how-to-guides) below, or explore:

- [Reference](#reference-documentation) - Attributes, type converters, and built-in behaviors
- [Explanation](#concepts-and-design) - Understand the design philosophy and command lifecycle

### Want to Understand How It Works?

Dive into the concepts:

- [Design Philosophy: Convention over Configuration](/cli/explanation/design-philosophy-convention-over-configuration) - Why Spectre.Console.Cli works the way it does
- [Command Lifecycle and Execution Flow](/cli/explanation/command-lifecycle-and-execution-flow) - How parsing, validation, and execution work

---

## Quick Reference

Essential lookups:

- [Attribute and Parameter Reference](/cli/reference/attribute-and-parameter-reference) - All attributes and their options
- [Type Converters](/cli/reference/type-converters) - Built-in types and custom converters

---

## How-To Guides

Practical guides for specific tasks:

### Defining and Configuring Commands
- [Defining Commands and Arguments](/cli/how--to/defining-commands-and-arguments) - Use `[CommandArgument]` and `[CommandOption]` attributes
- [Making Options Required](/cli/how--to/making-options-required) - Enforce required options with the `required` keyword
- [Configuring CommandApp and Commands](/cli/how--to/configuring-commandapp-and-commands) - Register commands, set aliases, and configure global settings
- [Working with Multiple Command Hierarchies](/cli/how--to/working-with-multiple-command-hierarchies) - Create nested command structures with `AddBranch`

### Advanced Features
- [Async Commands and Cancellation](/cli/how--to/async-commands-and-cancellation) - Use `AsyncCommand<T>` and handle Ctrl+C gracefully
- [Customizing Help Text and Usage](/cli/how--to/customizing-help-text-and-usage) - Application name, examples, and custom help providers
- [Intercepting Command Execution](/cli/how--to/intercepting-command-execution) - Implement `ICommandInterceptor` for cross-cutting concerns
- [Hiding Commands and Options](/cli/how--to/hiding-commands-and-options) - Hide internal commands from help output

### Error Handling and Testing
- [Handling Errors and Exit Codes](/cli/how--to/handling-errors-and-exit-codes) - Exception handling and custom exit codes
- [Testing Command-Line Applications](/cli/how--to/testing-command-line-applications) - Use `CommandAppTester` for unit testing

---

## Reference Documentation

API documentation and quick lookups:

- [Attribute and Parameter Reference](/cli/reference/attribute-and-parameter-reference) - Complete reference for `[CommandArgument]`, `[CommandOption]`, and more
- [Type Converters](/cli/reference/type-converters) - Built-in type support and custom type conversion
- [CommandContext Reference](/cli/reference/command-context) - Properties and usage of the `CommandContext` class
- [Built-in Command Behaviors](/cli/reference/built-in-command-behaviors) - The `version`, `explain`, `xmldoc`, and `opencli` commands

---

## Concepts and Design

Understand how Spectre.Console.Cli works:

- [Design Philosophy: Convention over Configuration](/cli/explanation/design-philosophy-convention-over-configuration) - The rationale behind library design choices
- [Command Lifecycle and Execution Flow](/cli/explanation/command-lifecycle-and-execution-flow) - Deep-dive into parsing, validation, and execution phases
