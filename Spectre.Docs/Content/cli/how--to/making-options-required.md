---
title: "Making Options Required"
description: "How to make command-line options required instead of optional in Spectre.Console.Cli"
uid: "cli-required-options"
order: 2015
---

By design, options (flags like `--name` or `-n`) are optional—that's why they're called "options." Arguments are typically required. However, there are cases where you want a named option that users must provide, such as specifying a target environment or API key.

## Use the Required Keyword

The simplest approach is to use C#'s `required` keyword on the property:

```csharp
public class Settings : CommandSettings
{
    [CommandOption("-e|--environment")]
    [Description("Target environment")]
    public required string Environment { get; init; }
}
```

When users omit this option, Spectre.Console.Cli displays an error:

```
Error: Option '--environment' is required.
```

## Combine Multiple Required Options

You can have several required options on the same command. Each one must be provided:

```csharp
public class Settings : CommandSettings
{
    [CommandOption("-e|--environment")]
    [Description("Target environment")]
    public required string Environment { get; init; }

    [CommandOption("-v|--version")]
    [Description("Version to deploy")]
    public required string Version { get; init; }

    [CommandOption("--dry-run")]
    [Description("Preview changes without applying")]
    public bool DryRun { get; init; }
}
```

## Validate with Custom Logic

For more complex validation—like ensuring at least one of several options is provided—implement `IValidatableSettings`:

```csharp
public class Settings : CommandSettings, IValidatableSettings
{
    [CommandOption("--config-file")]
    [Description("Path to configuration file")]
    public string? ConfigFile { get; init; }

    [CommandOption("--config-url")]
    [Description("URL to configuration endpoint")]
    public string? ConfigUrl { get; init; }

    public ValidationResult Validate()
    {
        if (string.IsNullOrEmpty(ConfigFile) && string.IsNullOrEmpty(ConfigUrl))
        {
            return ValidationResult.Error(
                "Either --config-file or --config-url must be specified.");
        }

        if (!string.IsNullOrEmpty(ConfigFile) && !string.IsNullOrEmpty(ConfigUrl))
        {
            return ValidationResult.Error(
                "Specify either --config-file or --config-url, not both.");
        }

        return ValidationResult.Success();
    }
}
```

This approach gives you full control over the error message and allows for cross-option validation.

## Complete Example

Here's a deployment CLI that requires both environment and version:

```csharp
using System.ComponentModel;
using Spectre.Console.Cli;

var app = new CommandApp<DeployCommand>();
return app.Run(args);

class DeployCommand : Command<DeployCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-e|--environment")]
        [Description("Target environment (staging, production)")]
        public required string Environment { get; init; }

        [CommandOption("-v|--version")]
        [Description("Version to deploy (e.g., 1.2.3)")]
        public required string Version { get; init; }

        [CommandOption("--dry-run")]
        [Description("Preview changes without applying them")]
        public bool DryRun { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Deploying version {settings.Version} to {settings.Environment}");

        if (settings.DryRun)
        {
            System.Console.WriteLine("(Dry run - no changes applied)");
        }

        return 0;
    }
}
```

Usage:
```bash
# Both required options must be provided
deploy --environment staging --version 1.2.3

# Missing required option shows an error
deploy --version 1.2.3
# Error: Option '--environment' is required.
```

## See Also

- [Defining Commands and Arguments](/cli/how--to/defining-commands-and-arguments) - Basics of arguments and options
- [Attribute and Parameter Reference](/cli/reference/attribute-and-parameter-reference) - All available attributes
- [Handling Errors and Exit Codes](/cli/how--to/handling-errors-and-exit-codes) - Custom error handling
