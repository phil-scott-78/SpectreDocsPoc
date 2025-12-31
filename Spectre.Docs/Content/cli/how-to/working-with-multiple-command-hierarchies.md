---
title: "Working with Multiple Command Hierarchies"
description: "How to create hierarchical (nested) commands using branching"
uid: "cli-command-hierarchies"
order: 2070
---

When building a CLI with grouped commands like `git remote add` and `git remote remove`, use `AddBranch` to create command hierarchies. The branch groups related subcommands together and can share common options through settings inheritance.

## What We're Building

A git-style CLI with nested `remote` subcommands—`add`, `remove`, and `list`—all sharing a `--verbose` flag inherited from the branch:

<Screenshot Src="/assets/cli-command-hierarchies.svg" Alt="Command hierarchies demonstration" />

## Create a Command Branch

Use `AddBranch` to define a parent command with nested subcommands. Use the generic overload when you want shared settings/options for the branch:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.Demo.RunAsync(System.String[])
```

This creates commands like `myapp remote add`, `myapp remote remove`, and `myapp remote list`. Running `myapp remote --help` shows all subcommands.

## Share Options with Settings Inheritance

Define a base settings class for the branch, then inherit from it in subcommand settings:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.RemoteSettings
T:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.RemoteAddSettings
T:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.RemoteListSettings
```

The `--verbose` flag is now available on all remote subcommands, and can be specified either before or after the subcommand name: `myapp remote --verbose add origin https://...` or `myapp remote add origin https://... --verbose`.

Rule: when you use `AddBranch<TSettings>`, each subcommand's settings type should inherit from `TSettings`. Even if a subcommand doesn't add any extra arguments/options (like `list`), give it a dedicated settings type (for example, `RemoteListSettings : RemoteSettings`).

## Nest Multiple Levels

For complex CLIs, branches can contain other branches (use the non-generic overload when you just want grouping):

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
