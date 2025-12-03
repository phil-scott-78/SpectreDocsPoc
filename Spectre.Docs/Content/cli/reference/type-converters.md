---
title: "Type Converters"
description: "Reference for type conversion in Spectre.Console.Cli command-line parsing"
uid: "cli-type-converters"
order: 4025
---

Spectre.Console.Cli uses .NET's `TypeConverter` system to parse command-line strings into strongly-typed properties. This reference covers supported types, custom converters, and registration methods.

## Built-in Type Support

These types are converted automatically without additional configuration.

### Primitive Types

| Type | Example Input | Notes |
|------|---------------|-------|
| `string` | `hello` | Direct pass-through |
| `int` | `42` | 32-bit signed integer |
| `long` | `9223372036854775807` | 64-bit signed integer |
| `short` | `32767` | 16-bit signed integer |
| `byte` | `255` | 8-bit unsigned integer |
| `float` | `3.14` | Single precision |
| `double` | `3.14159265359` | Double precision |
| `decimal` | `123.45` | High precision decimal |
| `bool` | `true` or `false` | Also accepts flag syntax |
| `char` | `a` | Single character |

### Special Types

| Type | Example Input | Notes |
|------|---------------|-------|
| `FileInfo` | `./file.txt` | File system path |
| `DirectoryInfo` | `./folder` | Directory path |
| `Uri` | `https://example.com` | URI/URL |
| `Guid` | `550e8400-e29b-41d4-a716-446655440000` | GUID format |
| `DateTime` | `2024-01-15` | Date/time parsing |
| `TimeSpan` | `01:30:00` | Time duration |

### Enums

Enums are parsed by name (case-insensitive by default) or numeric value:

```csharp
public enum LogLevel { Debug, Info, Warning, Error }

[CommandOption("--level")]
public LogLevel Level { get; set; }
```

```
myapp --level warning    # By name
myapp --level 2          # By numeric value
```

### Nullable Types

Any supported type can be nullable for optional values:

```csharp
[CommandOption("--count")]
public int? Count { get; set; }
```

When not provided, the property remains `null`.

### Arrays and Collections

Multiple values are collected into arrays:

```csharp
[CommandArgument(0, "[files]")]
public string[] Files { get; set; }
```

```
myapp file1.txt file2.txt file3.txt
```

## Custom Type Converters

For types without built-in support, implement a custom `TypeConverter`.

### Implementation

Inherit from `System.ComponentModel.TypeConverter` and override `ConvertFrom`:

```csharp
public class ConnectionStringConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str)
        {
            // Parse connection string format: "host:port"
            var parts = str.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[1], out var port))
                throw new InvalidOperationException($"Invalid format: {str}. Expected host:port");
            return new ConnectionInfo(parts[0], port);
        }
        return base.ConvertFrom(context, culture, value);
    }
}
```

### Registration via Attribute

Apply `[TypeConverter]` to your type definition:

```csharp
[TypeConverter(typeof(ConnectionStringConverter))]
public class ConnectionInfo
{
    public string Host { get; }
    public int Port { get; }

    public ConnectionInfo(string host, int port)
    {
        Host = host;
        Port = port;
    }
}
```

Usage in settings:

```csharp
[CommandOption("--connection")]
public ConnectionInfo? Connection { get; set; }
```

```
myapp --connection localhost:5432
```

### Registration via DI

For types you don't control, register converters through the type registrar:

```csharp
services.AddSingleton<TypeConverter, ThirdPartyTypeConverter>();
var app = new CommandApp(new MyRegistrar(services));
```

## Error Handling

When conversion fails, throw `InvalidOperationException` with a descriptive message:

```csharp
public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
{
    if (value is string str)
    {
        if (!TryParse(str, out var result))
            throw new InvalidOperationException(
                $"Could not convert '{str}' to MyType. Expected format: X-Y-Z");
        return result;
    }
    return base.ConvertFrom(context, culture, value);
}
```

The error message appears in CLI output when parsing fails.

## Culture Handling

The `culture` parameter in `ConvertFrom` contains the parsing culture from `Settings.Culture`. Use it for locale-aware parsing:

```csharp
public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
{
    if (value is string str)
    {
        // Use provided culture for number parsing
        if (decimal.TryParse(str, NumberStyles.Any, culture, out var result))
            return new Money(result);
    }
    return base.ConvertFrom(context, culture, value);
}
```

## See Also

- <xref:cli-attributes-parameters> - TypeConverterAttribute usage
