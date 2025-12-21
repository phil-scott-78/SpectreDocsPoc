---
title: "Using Custom Type Converters"
description: "How to create and apply custom type converters for complex command-line argument types"
uid: "cli-custom-type-converters"
order: 2100
---

When your command needs to accept complex types that aren't built-in—like points, colors, or domain-specific values—create a custom `TypeConverter` to parse the string input into your type.

## Create a Type Converter

Inherit from `System.ComponentModel.TypeConverter` and override `CanConvertFrom` and `ConvertFrom`:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.CustomTypeConverters.PointConverter
```

The `Point` type it converts to:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.CustomTypeConverters.Point
```

## Apply the Converter to an Option

Use the `[TypeConverter]` attribute on your settings property to specify which converter to use:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.CustomTypeConverters.DrawCommand.Settings
```

Users can now pass points on the command line:

```bash
myapp --point 10,20 --color red
myapp --point 100,200 --offset 5,5
```

## Handle Parsing Errors

When the input doesn't match your expected format, throw a `FormatException` with a helpful message. The framework displays this message to the user:

```csharp
if (parts.Length != 2 ||
    !int.TryParse(parts[0].Trim(), out var x) ||
    !int.TryParse(parts[1].Trim(), out var y))
{
    throw new FormatException(
        $"Invalid point format: '{str}'. Expected format: X,Y (e.g., 10,20)");
}
```

When a user provides invalid input like `--point abc`:

```
Error: Invalid point format: 'abc'. Expected format: X,Y (e.g., 10,20)
```

## Register Converters on Types You Own

For types you define, you can apply the converter directly to the type instead of each property:

```csharp
[TypeConverter(typeof(PointConverter))]
public readonly record struct Point(int X, int Y);
```

Then any property of type `Point` automatically uses the converter without needing the attribute.

## See Also

- <xref:cli-type-converters> - Complete type converter reference
- <xref:cli-commands-arguments> - Basic argument and option patterns
