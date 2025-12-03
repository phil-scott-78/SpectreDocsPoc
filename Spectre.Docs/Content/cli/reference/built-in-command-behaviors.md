---
title: "Built-in Command Behaviors"
description: "A reference describing Spectre.Console.Cli's built-in behaviors and conventions for completeness"
uid: "cli-built-in-behaviors"
order: 4040
---

Spectre.Console.Cli includes several built-in commands for diagnostics, documentation generation, and version information. These commands are registered in a hidden `cli` branch.

## Quick Reference

| Command | Access | Purpose |
|---------|--------|---------|
| `version` | `cli version` or `--version` | Display library versions |
| `explain` | `cli explain` | Show CLI configuration diagnostics |
| `xmldoc` | `cli xmldoc` | Generate XML documentation |
| `opencli` | `cli opencli` or `--help-dump-opencli` | Generate OpenCli specification |

## Version Command

Displays version information for Spectre.Console libraries.

**Access:**
```
myapp cli version
myapp --version
myapp -v
```

**Output:**
```
Spectre.Console.Cli version 1.0.0
Spectre.Console version 1.0.0
```

The `-v` / `--version` global option requires `ApplicationVersion` to be configured in the application.

## Explain Command

Displays a diagnostic tree view of the CLI configuration, showing all registered commands, options, and arguments.

**Access:**
```
myapp cli explain
myapp cli explain <command>
```

**Options:**

| Option | Description |
|--------|-------------|
| `[command]` | Optional command path to explain |
| `-d`, `--detailed` | Include detailed parameter information |
| `--hidden` | Include hidden commands and options |

**Examples:**
```
myapp cli explain                    # Show all commands
myapp cli explain add                # Show specific command
myapp cli explain --detailed         # Show parameter details
myapp cli explain --hidden           # Include hidden items
```

**Output includes:**
- Application name and parsing mode
- Command tree with descriptions
- Parameter types and kinds (Option/Argument)
- Option aliases (short and long names)
- Argument positions
- Validators and default values
- Examples and aliases

## XmlDoc Command

Generates machine-readable XML documentation of the CLI configuration. Useful for automated documentation generation and tooling integration.

**Access:**
```
myapp cli xmldoc
```

**Output format:**

```xml
<?xml version="1.0" encoding="utf-8"?>
<Model>
  <Command Name="add" ClrType="AddCommand" Settings="AddSettings">
    <Description>Add a new item</Description>
    <Parameters>
      <Argument Name="name" Position="0" Required="true" Kind="Scalar" />
      <Option Short="v" Long="verbose" Required="false" Kind="Flag" />
    </Parameters>
    <Examples>
      <Example>add item --verbose</Example>
    </Examples>
  </Command>
</Model>
```

**Included metadata:**
- Command names, types, and settings classes
- Parameter positions, requirements, and CLR types
- Validators and type converters
- Examples and descriptions
- Nested command hierarchy

Hidden commands are excluded from output.

## OpenCli Command

Generates an [OpenCli specification](https://opencli.org/) document compatible with the OpenCli 0.1-draft standard for CLI tool interoperability.

**Access:**
```
myapp cli opencli
myapp --help-dump-opencli
```

**Output includes:**
- OpenCli version identifier
- Application info (name and version)
- Complete command listing with:
  - Names and aliases
  - Descriptions
  - Arguments and options with metadata
  - Examples
  - Nested subcommands

Built-in commands and empty branches are excluded from output.

## Built-in Help

The `-h` / `--help` option is automatically available on all commands.

**Access:**
```
myapp --help
myapp -h
myapp <command> --help
```

**Output includes:**
- Command description
- Usage syntax
- Available arguments with descriptions
- Available options with descriptions and defaults
- Subcommands (if any)
- Examples (if configured)

## See Also

- [CommandContext Reference](/cli/reference/command-context) - Runtime command information
