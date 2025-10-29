---
title: "Quick Start: Your First CLI App"
description: "A beginner tutorial that shows how to create a simple command-line application with Spectre.Console.Cli"
uid: "cli-quick-start"
order: 1010
---

A beginner tutorial that shows how to create a simple command-line application with Spectre.Console.Cli. It walks through installing the Spectre.Console.Cli NuGet package and setting up a single **Command**. The tutorial uses a "Hello World" style example: defining a `HelloCommand` with an option (e.g. `--name`) and a setting class with that option, then configuring a `CommandApp` to use this command. The steps include writing the `Execute` method to output a greeting (possibly using Spectre.Console for colored output) and running the app to parse arguments. By the end, the user can run the compiled app with `--name Alice` to see a personalized greeting. This tutorial introduces the basic patterns: creating a `Command<TSettings>` class, a nested `CommandSettings` class with `[CommandOption]` or `[CommandArgument]`, and using `CommandApp.Run(args)` to parse and execute.

## Related Console Tutorials

Looking to enhance your CLI app with rich console features? Check out these Spectre.Console tutorials:

- [Getting Started: Building a Rich Console App](/console/tutorials/getting-started-building-rich-console-app) - Add colorful text, tables, and progress bars to your CLI commands
- [Asking User Questions](/console/tutorials/interactive-prompts-tutorial) - Ask for names, numbers, and let users choose from lists
- [Showing Status and Spinners](/console/tutorials/status-spinners-tutorial) - Display animated spinners while work is happening