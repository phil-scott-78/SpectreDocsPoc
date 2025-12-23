---
title: "Hiding Commands and Options"
description: "How to hide commands and options from help output while keeping them functional"
uid: "cli-hidden-commands"
order: 2120
---

Sometimes you need commands or options that work but shouldn't appear in help output—internal debugging tools, deprecated features you're phasing out, or advanced options that would overwhelm typical users. Hidden items remain fully functional; users who know about them can still use them.

## What We're Building

Notice `diagnostics` doesn't appear in help but still works when invoked directly. The `--skip-hooks` option on `deploy` is similarly hidden:

<Screenshot Src="/assets/cli-hiding-commands.svg" Alt="Hiding commands and options demonstration" />

## Hide a Command

To hide a command from help output, chain `.IsHidden()` when configuring it:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.HidingCommandsAndOptions.Demo.RunAsync(System.String[])
```

Running `--help` shows only `deploy` and `status`, but `diagnostics` still works when invoked directly.

## Hide an Option

For options, set `IsHidden = true` on the attribute:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.HidingCommandsAndOptions.DeployCommand.Settings
```

The `--skip-hooks` option won't appear in `deploy --help`, but users can still pass it.

## See Also

- <xref:cli-help-customization> - Control what appears in help output
- <xref:cli-commands-arguments> - Command and option basics
