---
title: "Configuring CommandApp and Commands"
description: "How to register commands with the CommandApp and configure global settings"
uid: "cli-app-configuration"
order: 2090
---

When building a CLI with multiple commands, use `CommandApp.Configure` to register commands, set up aliases, and customize how your application appears in help output.

## Register Commands with Metadata

Use `AddCommand<T>("name")` to register each command, then chain methods to add descriptions, aliases, and examples:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.ConfiguringCommandApp.Demo.RunAsync(System.String[])
```

This produces help output like:

```
USAGE:
    myapp [COMMAND] [OPTIONS]

COMMANDS:
    add (a)              Add a new item
    remove (rm, delete)  Remove an item
    list (ls)            List all items
```

Users can invoke commands by name or any alias: `myapp rm file.txt` works the same as `myapp remove file.txt`.

## Configure Global Settings

Access `config.Settings` to adjust parsing behavior:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.ConfiguringCommandApp.SettingsDemo.RunAsync(System.String[])
```

Common settings include:

| Setting | Purpose |
|---------|---------|
| `CaseSensitivity` | Control whether commands/options are case-sensitive |
| `StrictParsing` | When `false`, unknown flags become remaining arguments instead of errors |

## Development Settings

During development, enable additional validation:

```csharp
#if DEBUG
    config.PropagateExceptions();  // Get full stack traces
    config.ValidateExamples();     // Verify all WithExample calls are valid
#endif
```

`ValidateExamples()` catches typos in your examples at startup rather than confusing users at runtime.

## See Also

- [Working with Multiple Command Hierarchies](/cli/how--to/working-with-multiple-command-hierarchies) - Nested commands with `AddBranch`
- [Customizing Help Text and Usage](/cli/how--to/customizing-help-text-and-usage) - Further help customization