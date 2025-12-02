---
title: "Making Options Required"
description: "How to make command-line options required instead of optional in Spectre.Console.Cli"
uid: "cli-required-options"
order: 2015
---

By design, options (flags like `--name` or `-n`) are optional—that's why they're called "options." However, there are cases where you want a named option that users must provide, such as specifying a target environment or API key. The simplest approach is to use C#'s `required` keyword on the property in your settings class. When users omit a required option, Spectre.Console.Cli displays a clear error message.

For more complex validation—like ensuring at least one of several options is provided, or that two options aren't used together—implement `IValidatableSettings` on your settings class. This gives you full control over the error message and allows for cross-option validation logic that can't be expressed with attributes alone.
