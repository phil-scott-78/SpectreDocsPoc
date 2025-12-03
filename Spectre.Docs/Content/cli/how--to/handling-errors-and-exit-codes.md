---
title: "Handling Errors and Exit Codes"
description: "How Spectre.Console.Cli deals with exceptions and how to customize error handling"
uid: "cli-error-handling"
order: 2070
---

By default, Spectre.Console.Cli catches exceptions, displays a user-friendly message, and returns exit code `-1`. When you need more control—different exit codes for different error types, custom formatting, or integration with logging—you have two options: `SetExceptionHandler` for centralized handling within the framework, or `PropagateExceptions` for full manual control with try-catch blocks.

## Centralize Error Handling with SetExceptionHandler

For most applications, `SetExceptionHandler` provides the cleanest approach. It intercepts exceptions from both the parsing phase and command execution, letting you format errors consistently and return specific exit codes.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.HandlingErrorsAndExitCodes.Demo.RunAsync(System.String[])
```

The handler receives the exception and an `ITypeResolver` (which is `null` during parsing, before the command is resolved). Use pattern matching to return different exit codes based on exception type—automation scripts can then distinguish between "file not found" (exit 3), "invalid operation" (exit 2), and general errors (exit 1).

## Use PropagateExceptions for Manual Control

When you need full control over exception flow—perhaps to integrate with a logging framework, perform cleanup, or handle exceptions at different layers—use `PropagateExceptions()`. This re-throws exceptions from `app.Run()`, letting you catch them in your own try-catch block.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.HandlingErrorsAndExitCodes.PropagateExceptionsDemo
```

This approach requires more code but gives you complete flexibility. You can catch specific exception types, access their properties for detailed messages, and integrate with any error reporting system.

> [!IMPORTANT]
> The exception handler configured via `SetExceptionHandler` will **not** be called when `PropagateExceptions()` is set—exceptions go straight to your catch blocks.

## See Also

- [Async Commands and Cancellation](/cli/how--to/async-commands-and-cancellation) - Handle cancellation gracefully