---
title: "Using Flag Arguments"
description: "How to use FlagValue for optional flag arguments that may or may not include a value"
uid: "cli-flag-arguments"
order: 2080
---

Sometimes you need a flag that can be used in three ways: not present, present without a value (using a default), or present with an explicit value. The `FlagValue<T>` type handles this pattern cleanly.

## What We're Building

The `--port` flag used three ways: omitted entirely, present without a value (uses default 3000), or with an explicit value like `--port 8080`:

<Screenshot Src="/assets/cli-flag-arguments.svg" Alt="Flag arguments demonstration" />

## Define a Flag Value

Use `FlagValue<T>` with square brackets in the template to indicate the value is optional. When users specify `--port` without a value, the flag is set but uses the type's default. When they specify `--port 8080`, the flag is set with that value.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.FlagArguments.ServerCommand.Settings
```

This produces the following usage:

```
USAGE:
    myapp [OPTIONS]

OPTIONS:
    --port [PORT]           The port to listen on (default: 3000 if flag present)
    --timeout [SECONDS]     Connection timeout in seconds
    -h, --host              The host to bind to [default: localhost]
```

## Check if a Flag Was Provided

The `FlagValue<T>` type has two properties: `IsSet` indicates whether the flag was present on the command line, and `Value` contains the parsed value (or the type's default if no value was given).

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.FlagArguments.ServerCommand.Execute(Spectre.Console.Cli.CommandContext,Spectre.Docs.Cli.Examples.DemoApps.FlagArguments.ServerCommand.Settings,System.Threading.CancellationToken)
```

This lets you distinguish between:
- `myapp` — flag not present (`IsSet` is false)
- `myapp --port` — flag present without value (`IsSet` is true, `Value` is 0)
- `myapp --port 8080` — flag present with value (`IsSet` is true, `Value` is 8080)


## See Also

- <xref:cli-commands-arguments> - Basic argument and option patterns
- <xref:cli-required-options> - Making options required
