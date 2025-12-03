---
title: "Making Options Required"
description: "How to make command-line options required instead of optional in Spectre.Console.Cli"
uid: "cli-required-options"
order: 2015
---

By design, options (flags like `--name` or `-n`) are optional—that's why they're called "options." However, there are cases where you want a named option that users must provide, such as specifying a target environment or API key.

## Use the `required` Keyword

The simplest approach is to use C#'s `required` keyword on the property. Spectre.Console.Cli detects this and enforces the option, displaying "Required" in help output and showing a clear error when omitted.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.MakingOptionsRequired.DeployCommand.Settings
```

When users omit a required option:

```
Error: Option '--environment' is required.
```

Help output marks these options clearly:

```
OPTIONS:
    -e, --environment    Target environment (Required)
    -v, --version        Version to deploy (Required)
        --dry-run        Preview changes without applying them
```

## Validate Across Multiple Options

For more complex scenarios—like requiring at least one of several options, or preventing two options from being used together—override the `Validate()` method in your settings class:

```csharp
public override ValidationResult Validate()
{
    if (string.IsNullOrEmpty(ConnectionString) && string.IsNullOrEmpty(Host))
    {
        return ValidationResult.Error(
            "Provide either --connection-string or --host");
    }
    return ValidationResult.Success();
}
```

This gives you full control over error messages and allows for validation logic that can't be expressed with attributes alone.

## See Also

- [Defining Commands and Arguments](/cli/how--to/defining-commands-and-arguments) - Basic argument and option patterns
- [Handling Errors and Exit Codes](/cli/how--to/handling-errors-and-exit-codes) - Custom error handling
