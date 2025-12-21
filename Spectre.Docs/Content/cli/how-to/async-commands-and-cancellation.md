---
title: "Async Commands and Cancellation"
description: "How to create asynchronous commands and handle cancellation in Spectre.Console.Cli"
uid: "cli-async-commands"
order: 2040
---

When your command performs I/O-bound operations like HTTP requests, database queries, or file operations, use `AsyncCommand<TSettings>` instead of `Command<TSettings>`. This lets you use `async/await` and enables graceful shutdown when users press Ctrl+C.

## Wire Up Console Cancellation

To enable Ctrl+C handling, create a `CancellationTokenSource` and pass its token to `RunAsync`. The framework automatically propagates this token to your command's `ExecuteAsync` method.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.AsyncCommandsAndCancellation.Demo.RunAsync(System.String[])
```

## Create an Async Command

Inherit from `AsyncCommand<TSettings>` and override `ExecuteAsync`. The `CancellationToken` is passed automatically—forward it to any async operations so they can respond to cancellation requests.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.AsyncCommandsAndCancellation.FetchCommand
```

## See Also

- <xref:cli-error-handling> - Custom exception handling
- <xref:cli-di-tutorial> - Inject services like HttpClient
