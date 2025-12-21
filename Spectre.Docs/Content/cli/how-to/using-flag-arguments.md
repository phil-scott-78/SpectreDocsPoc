---
title: "Using Flag Arguments"
description: "How to use FlagValue for optional flag arguments that may or may not include a value"
uid: "cli-flag-arguments"
order: 2080
---

Sometimes you need a flag that can be used in three ways: not present, present without a value (using a default), or present with an explicit value. The `FlagValue<T>` type handles this pattern cleanly.

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

```csharp
if (settings.Port?.IsSet == true)
{
    // Flag was provided, use the value
    var port = settings.Port.Value;
    Console.WriteLine($"Using port: {port}");
}
else
{
    // Flag was not provided at all
    Console.WriteLine("Using system-assigned port");
}
```

This lets you distinguish between:
- `myapp` — flag not present (`IsSet` is false)
- `myapp --port` — flag present without value (`IsSet` is true, `Value` is 0)
- `myapp --port 8080` — flag present with value (`IsSet` is true, `Value` is 8080)

## Handle Nullable Flag Values

For cases where you want to detect "flag present but no value specified" distinctly from "flag present with value 0", use `FlagValue<int?>`:

```csharp
[CommandOption("--timeout [SECONDS]")]
public FlagValue<int?>? Timeout { get; set; }
```

Now you can check:

```csharp
if (settings.Timeout?.IsSet == true)
{
    if (settings.Timeout.Value.HasValue)
    {
        Console.WriteLine($"Timeout: {settings.Timeout.Value} seconds");
    }
    else
    {
        Console.WriteLine("Timeout enabled with default");
    }
}
```

## See Also

- <xref:cli-commands-arguments> - Basic argument and option patterns
- <xref:cli-required-options> - Making options required
