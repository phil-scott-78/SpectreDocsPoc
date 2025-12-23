---
title: "Using Dictionary and Lookup Options"
description: "How to accept key-value pairs using IDictionary, ILookup, and IReadOnlyDictionary options"
uid: "cli-dictionary-options"
order: 2090
---

When your command needs to accept configuration values, environment variables, or other key-value pairs, use dictionary-based option types. Spectre.Console.Cli supports `IDictionary<TKey, TValue>`, `ILookup<TKey, TValue>`, and `IReadOnlyDictionary<TKey, TValue>`.

## What We're Building

A configuration command accepting key-value pairs like `--value port=8080`, plus lookups where one key can have multiple values:

<Screenshot Src="/assets/cli-dictionary-options.svg" Alt="Dictionary and lookup options demonstration" />

## Accept Key-Value Pairs

Use `IDictionary<string, T>` to collect key-value pairs. Users specify values in `key=value` format:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DictionaryOptions.ConfigCommand.Settings
```

Users invoke the command with repeated options:

```bash
myapp --value port=8080 --value timeout=30 --value retries=3
```

The framework parses the `key=value` format automatically and converts the value to the specified type (in this case, `int`).

## Collect Multiple Values per Key

When a key can have multiple associated values, use `ILookup<TKey, TValue>`. Unlike `IDictionary`, a lookup allows multiple entries for the same key:

```bash
myapp --lookup env=dev --lookup env=staging --lookup env=prod --lookup region=us
```

Access grouped values in your command:

```csharp
foreach (var group in settings.Lookups)
{
    Console.WriteLine($"{group.Key}:");
    foreach (var value in group)
    {
        Console.WriteLine($"  - {value}");
    }
}
```

Output:

```
env:
  - dev
  - staging
  - prod
region:
  - us
```

## Use Read-Only Dictionaries

For immutable configuration that shouldn't be modified after parsing, use `IReadOnlyDictionary<TKey, TValue>`:

```csharp
[CommandOption("--setting <VALUE>")]
public IReadOnlyDictionary<string, string>? Settings { get; set; }
```

This provides the same parsing behavior as `IDictionary` but returns a read-only collection.

## Supported Key-Value Types

Both the key and value types can use any type that Spectre.Console.Cli can convert:

| Pattern | Example Usage |
|---------|---------------|
| `IDictionary<string, int>` | `--opt key=42` |
| `IDictionary<string, bool>` | `--opt flag=true` |
| `IDictionary<int, string>` | `--opt 1=first` |
| `ILookup<string, string>` | `--opt tag=a --opt tag=b` |

## See Also

- <xref:cli-commands-arguments> - Basic argument and option patterns
- <xref:cli-type-converters> - Type conversion reference
