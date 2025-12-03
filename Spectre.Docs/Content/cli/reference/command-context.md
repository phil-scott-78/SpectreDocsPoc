---
title: "CommandContext Reference"
description: "Reference documentation for the CommandContext class in Spectre.Console.Cli"
uid: "cli-command-context"
order: 4035
---

The `CommandContext` class provides runtime information about the executing command. It is passed to your command's `Execute` or `ExecuteAsync` method alongside your settings.

## Properties

| Property | Type | Description |
|----------|------|-------------|
| `Name` | string | The command name as invoked |
| `Arguments` | IReadOnlyList\<string\> | All arguments passed to the application |
| `Remaining` | IRemainingArguments | Unmatched arguments after parsing |
| `Data` | object? | Custom data passed during configuration |

## Name Property

The name of the currently executing command as it was invoked by the user.

```csharp
public override int Execute(CommandContext context, MySettings settings)
{
    Console.WriteLine($"Running command: {context.Name}");
    return 0;
}
```

## Arguments Property

A read-only list of all arguments passed to the application, before parsing.

```csharp
// myapp add file.txt --verbose
// Arguments: ["add", "file.txt", "--verbose"]
```

## Remaining Property

An `IRemainingArguments` instance containing arguments that weren't matched to defined options or arguments.

### IRemainingArguments Interface

| Property | Type | Description |
|----------|------|-------------|
| `Parsed` | ILookup\<string, string?\> | Parsed key-value pairs from remaining arguments |
| `Raw` | IReadOnlyList\<string\> | Raw remaining arguments after `--` delimiter |

**Parsed Arguments:**

Arguments that look like options but weren't defined:

```
myapp --known-option value --unknown-option foo
```

`Remaining.Parsed["unknown-option"]` returns `["foo"]`.

**Raw Arguments:**

Arguments after the `--` delimiter are available in `Raw`:

```
myapp --verbose -- arg1 arg2 --not-parsed
```

`Remaining.Raw` returns `["arg1", "arg2", "--not-parsed"]`.

### Pass-Through Scenario

Use remaining arguments when wrapping another CLI tool:

```csharp
public override int Execute(CommandContext context, MySettings settings)
{
    var args = string.Join(" ", context.Remaining.Raw);
    Process.Start("other-tool", args);
    return 0;
}
```

## Data Property

Arbitrary data passed through the command execution pipeline. Set during configuration or by interceptors.

### Setting Data During Configuration

```csharp
app.Configure(config =>
{
    config.AddCommand<MyCommand>("run")
        .WithData(new { Environment = "production" });
});
```

### Accessing Data in Commands

```csharp
public override int Execute(CommandContext context, MySettings settings)
{
    dynamic data = context.Data;
    Console.WriteLine($"Environment: {data.Environment}");
    return 0;
}
```

### Setting Data via Interceptors

Interceptors can populate `Data` before command execution:

```csharp
public class TimingInterceptor : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        // Data is typically set during configuration, not in interceptors
        // Use interceptor fields for timing/state tracking instead
    }
}
```

## Read-Only Behavior

The context is read-only during command execution. You cannot modify `Name`, `Arguments`, or `Remaining` from within a command.

## See Also

- <xref:cli-built-in-behaviors> - Parsing rules for remaining arguments
