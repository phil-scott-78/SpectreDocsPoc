---
title: "Working with Multiple Command Hierarchies"
description: "How to create hierarchical (nested) commands using branching"
uid: "cli-command-hierarchies"
order: 2070
---

When you're building a CLI with grouped commands that share common options (like `dotnet tool install` and `dotnet tool uninstall`, or `git remote add` and `git remote remove`), use command branching to create hierarchies. Call `AddBranch<TSettings>("name", branch => { ... })` to define a parent command that groups related subcommands together.

The branch uses a base settings class containing shared options (like a `--verbose` flag), and each subcommand's settings class inherits from it to add command-specific options. This approach keeps your CLI organized and lets you reuse common parameters without duplication. You can nest branches multiple levels deep for complex command structures, making your CLI intuitive for users familiar with tools like Git or Docker.