---
title: "Implementing Command Handlers"
description: "How to create the logic for commands by implementing the Command classes"
uid: "cli-command-handlers"
order: 2020
---

When you need to implement the actual logic for a command, inherit from `Command<TSettings>` for synchronous operations or `AsyncCommand<TSettings>` for async work. Write your business logic in the `Execute` method, accessing user input through the settings parameter and returning an exit code (0 for success, non-zero for errors).

If your command requires services, declare them in the constructor—Spectre.Console.Cli will inject them automatically when configured with a DI container. To catch invalid inputs early, override the `Validate` method to check preconditions (like verifying a file exists) and return `ValidationResult.Error` with a helpful message if validation fails. This prevents execution with bad inputs and provides clear feedback to users.