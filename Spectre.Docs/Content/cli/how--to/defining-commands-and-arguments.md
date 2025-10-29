---
title: "Defining Commands and Arguments"
description: "How to declare command-line parameters (arguments and options) using Spectre.Console.Cli's attributes and settings classes"
uid: "cli-commands-arguments"
order: 2010
---

When your CLI command needs to accept user input, create a `CommandSettings` class and decorate its properties with attributes to define the interface. Use `[CommandArgument]` for positional parameters (required with `<name>` or optional with `[name]`), positioning them by index. For named parameters, use `[CommandOption]` with short and long forms like `-c|--count`.

Add `[Description]` attributes to generate helpful usage text, and `[DefaultValue]` to provide sensible defaults. Boolean options automatically work as flags—users specify them to set `true`, omit them for `false`. If you need to accept multiple values, use an array type on the last argument. The framework automatically generates help and validates user input based on these declarations.