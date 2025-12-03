---
title: "Defining Commands and Arguments"
description: "How to declare command-line parameters (arguments and options) using Spectre.Console.Cli's attributes and settings classes"
uid: "cli-commands-arguments"
order: 2010
---

Every command in Spectre.Console.Cli receives its input through a `CommandSettings` class. Decorate properties with `[CommandArgument]` for positional parameters and `[CommandOption]` for named flags and options. The framework handles parsing, validation, and help generation automatically.

## Define Arguments and Options

Use `[CommandArgument]` with a position index and template: angle brackets `<name>` for required arguments, square brackets `[name]` for optional ones. For named parameters, use `[CommandOption]` with short and/or long forms separated by `|`.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments.FileCopyCommand.Settings
```

This settings class produces the following usage:

```
USAGE:
    myapp <source> [destination] [OPTIONS]

OPTIONS:
    -f, --force                 Overwrite existing files without prompting
    -b, --buffer-size <INT>     Buffer size in KB for the copy operation [default: 64]
        --preserve-timestamps   Preserve original file timestamps
    -v                          Enable verbose output
```

Boolean properties become flags—users include them to set `true`, omit them for `false`. Properties with `[DefaultValue]` show their defaults in help text.

## Accept Multiple Values

For commands that process multiple files or need repeatable options, use array types. An array argument captures all remaining positional values and must be the last argument. Array options can be specified multiple times.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments.MultiFileCommand.Settings
```

Users invoke this as:

```bash
myapp file1.txt file2.txt file3.txt --tag api --tag production
```

## Use Enums for Constrained Values

When an option should only accept specific values, use an enum type. The framework validates input and displays allowed values in help text.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments.BuildCommand.Settings
```

Invalid values produce a clear error message listing the allowed options.

## See Also

- [Making Options Required](/cli/how--to/making-options-required) - Force users to provide specific options
- [Attribute and Parameter Reference](/cli/reference/attribute-and-parameter-reference) - Complete attribute documentation