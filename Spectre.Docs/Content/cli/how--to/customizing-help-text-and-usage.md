---
title: "Customizing Help Text and Usage"
description: "How to tailor the automatically generated help output of Spectre.Console.Cli"
uid: "cli-help-customization"
order: 2100
---

Spectre.Console.Cli generates help text automatically from your commands and settings. When the defaults don't match your needs—whether for branding, accessibility, or clarity—you can customize the application name, add usage examples, adjust styling, or disable styling entirely for plain text output.

## Set Application Name and Add Examples

By default, help text shows the executable name (often ending in `.dll` during development). Use `SetApplicationName` to display a cleaner name, and `AddExample` to show users how to invoke your CLI with common argument patterns.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.CustomizingHelpText.Demo.RunAsync(System.String[])
```

This produces help output like:

```
USAGE:
    myapp <environment> [OPTIONS]

EXAMPLES:
    myapp production
    myapp staging --force
    myapp dev --dry-run --verbose
```

## Customize Help Styling

To change colors and formatting in help output, configure `HelpProviderStyles`. You can style descriptions, arguments, options, and examples independently using Spectre.Console markup syntax.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.CustomizingHelpText.StyledHelpDemo.RunAsync(System.String[])
```

Available style classes include `DescriptionStyle`, `ArgumentStyle`, `OptionStyle`, `CommandStyle`, and `ExampleStyle`—each with properties for different elements like headers, required vs optional items, and default values.

## Remove Styling for Plain Text

For maximum accessibility, piping to files, or environments without color support, disable all styling by setting `HelpProviderStyles` to `null`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.CustomizingHelpText.PlainTextHelpDemo.RunAsync(System.String[])
```

## Implement a Custom Help Provider

For complete control over help formatting, implement `IHelpProvider` and register it with `SetHelpProvider`. This lets you restructure sections, add custom content, or integrate with external documentation systems.

```csharp
app.Configure(config =>
{
    config.SetHelpProvider(new CustomHelpProvider(config.Settings));
});
```

The built-in `HelpProvider` class can also be extended if you only need to override specific sections.

## See Also

- [Hiding Commands and Options](/cli/how--to/hiding-commands-and-options) - Keep commands functional but hidden from help