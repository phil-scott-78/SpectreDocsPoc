---
title: "Attribute and Parameter Reference"
description: "A summary of all attributes and parameter-related features in Spectre.Console.Cli"
uid: "cli-attributes-parameters"
order: 4020
---

This reference lists all attributes used to define command arguments, options, and their behaviors in Spectre.Console.Cli.

## Quick Reference

| Attribute | Purpose | Namespace |
|-----------|---------|-----------|
| `[CommandArgument]` | Define positional arguments | Spectre.Console.Cli |
| `[CommandOption]` | Define named options | Spectre.Console.Cli |
| `[Description]` | Help text for properties | System.ComponentModel |
| `[DefaultValue]` | Default value for options | System.ComponentModel |
| `[TypeConverter]` | Custom type conversion | System.ComponentModel |

## CommandArgumentAttribute

Marks a property as a positional command argument.

**Syntax:**
```csharp
[CommandArgument(int position, string template)]
```

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `position` | int | Zero-based position of the argument |
| `template` | string | Name template with required/optional indicator |

**Template Syntax:**

| Format | Meaning | Example |
|--------|---------|---------|
| `<name>` | Required argument | `<path>` |
| `[name]` | Optional argument | `[count]` |

**Example:**
```csharp
[CommandArgument(0, "<source>")]
public string Source { get; set; }

[CommandArgument(1, "[destination]")]
public string? Destination { get; set; }
```

## CommandOptionAttribute

Marks a property as a named command option.

**Syntax:**
```csharp
[CommandOption(string template)]
[CommandOption(string template, bool isRequired)]
```

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `template` | string | Option name template with aliases |
| `isRequired` | bool | Whether the option must be provided (default: false) |

**Template Syntax:**

Options use pipe-separated aliases. Short names use single dash, long names use double dash.

| Format | Example | Usage |
|--------|---------|-------|
| Short only | `-v` | `-v` |
| Long only | `--verbose` | `--verbose` |
| Both | `-v\|--verbose` | `-v` or `--verbose` |
| Multiple aliases | `-v\|--verbose\|--debug` | Any of the three |

**Properties:**

| Property | Type | Description |
|----------|------|-------------|
| `LongNames` | IReadOnlyList\<string\> | Long option names (read-only) |
| `ShortNames` | IReadOnlyList\<string\> | Short option names (read-only) |
| `ValueName` | string? | Name shown in help for the value |
| `IsRequired` | bool | Whether option is required |
| `IsHidden` | bool | Hide from help output |
| `ValueIsOptional` | bool | Value can be omitted |

**Example:**
```csharp
[CommandOption("-o|--output")]
public string? OutputPath { get; set; }

[CommandOption("-v|--verbose")]
public bool Verbose { get; set; }

[CommandOption("--count", isRequired: true)]
public int Count { get; set; }
```

**Boolean Flags:**

Boolean options do not require a value. Presence of the flag sets the property to `true`.

```csharp
// Usage: myapp --verbose
[CommandOption("--verbose")]
public bool Verbose { get; set; }
```

## DescriptionAttribute

Provides help text displayed in command help output. From `System.ComponentModel`.

**Syntax:**
```csharp
[Description(string description)]
```

**Example:**
```csharp
[Description("The output file path")]
[CommandOption("-o|--output")]
public string? Output { get; set; }
```

## DefaultValueAttribute

Sets a default value for an option or argument. From `System.ComponentModel`.

**Syntax:**
```csharp
[DefaultValue(object value)]
```

**Example:**
```csharp
[DefaultValue(10)]
[CommandOption("-c|--count")]
public int Count { get; set; }

[DefaultValue("output.txt")]
[CommandOption("-o|--output")]
public string Output { get; set; }
```

> [!NOTE]
> Default values appear in help output when `ShowOptionDefaultValues` is enabled in settings.

## TypeConverterAttribute

Specifies a custom type converter for parsing argument values. From `System.ComponentModel`.

**Syntax:**
```csharp
[TypeConverter(typeof(ConverterType))]
```

Apply to the property type definition, not the property itself.

**Example:**
```csharp
[TypeConverter(typeof(LogLevelConverter))]
public enum LogLevel { Debug, Info, Warning, Error }
```

See [Type Converters](/cli/reference/type-converters) for implementation details.

## CommandSettings Base Class

All settings classes must inherit from `CommandSettings`.

**Virtual Methods:**

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Validate()` | ValidationResult | Override to add custom validation |

**Example:**
```csharp
public class MySettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    public string Path { get; set; }

    public override ValidationResult Validate()
    {
        if (!File.Exists(Path))
            return ValidationResult.Error("File not found");
        return ValidationResult.Success();
    }
}
```

## Parameter Validation

For reusable validation logic, create custom validation attributes by inheriting from `ParameterValidationAttribute`.

**Syntax:**
```csharp
public class MyValidatorAttribute : ParameterValidationAttribute
{
    public override ValidationResult Validate(CommandParameterContext context)
    {
        // Return ValidationResult.Success() or ValidationResult.Error(message)
    }
}
```

**Example:**
```csharp
[FileExists]
[CommandArgument(0, "<path>")]
public string Path { get; set; }
```

## See Also

- [Type Converters](/cli/reference/type-converters) - Custom type conversion
- [Configuration API Reference](/cli/reference/configuration-api-reference) - Application configuration
- [Quick Start Tutorial](/cli/tutorials/quick-start-your-first-cli-app) - Getting started with commands
