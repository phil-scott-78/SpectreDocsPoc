---
title: "Async Commands and Cancellation"
description: "How to create asynchronous commands and handle cancellation in Spectre.Console.Cli"
uid: "cli-async-commands"
order: 2025
---

When your command performs I/O-bound operations like HTTP requests, database queries, or file operations, use `AsyncCommand<TSettings>` instead of `Command<TSettings>`. This allows you to use `async/await` in your `ExecuteAsync` method, keeping the application responsive and following .NET async best practices.

For proper cancellation support, accept a `CancellationToken` and pass it through to your async operations. This enables graceful shutdown when users press Ctrl+C. You can access the cancellation token through the command context or by configuring your `CommandApp` to propagate console cancellation signals. Always check the token periodically in long-running operations and clean up resources appropriately when cancellation is requested.
