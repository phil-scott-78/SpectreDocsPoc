---
title: "Working with Multiple Command Hierarchies"
description: "How to create hierarchical (nested) commands using branching"
uid: "cli-command-hierarchies"
order: 2070
---

When building a CLI with grouped commands like `git remote add` and `git remote remove`, use `AddBranch` to create command hierarchies. The branch groups related subcommands together and can share common options through settings inheritance.

## Create a Command Branch

Use `AddBranch<TSettings>("name", ...)` to define a parent command with nested subcommands:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.Demo.RunAsync(System.String[])
```

This creates commands like `myapp remote add`, `myapp remote remove`, and `myapp remote list`. Running `myapp remote --help` shows all subcommands.

## Share Options with Settings Inheritance

Define a base settings class for the branch, then inherit from it in subcommand settings:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.RemoteSettings
T:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.RemoteAddSettings
```

The `--verbose` flag is now available on all remote subcommands: `myapp remote add origin https://... --verbose`.

## Nest Multiple Levels

For complex CLIs, branches can contain other branches:

```csharp
config.AddBranch("cloud", cloud =>
{
    cloud.AddBranch("storage", storage =>
    {
        storage.AddCommand<UploadCommand>("upload");
        storage.AddCommand<DownloadCommand>("download");
    });
});
```

This creates deeply nested commands like `myapp cloud storage upload`.

## See Also

- <xref:cli-app-configuration> - Basic command registration
- <xref:cli-commands-arguments> - Settings class patterns