---
title: "Handling Errors and Exit Codes"
description: "How Spectre.Console.Cli deals with exceptions and how to customize error handling"
uid: "cli-error-handling"
order: 2050
---

When your CLI needs custom error handling or specific exit codes for different failure scenarios, override the default behavior (which returns -1 for any exception). Use `config.PropagateExceptions()` to let exceptions bubble to your `Main` method, where you can wrap `app.Run(args)` in a try-catch block and handle different exception types with appropriate exit codes and formatted output using `AnsiConsole.WriteException`.

Alternatively, use `config.SetExceptionHandler(...)` to centralize exception handling within the framework itself. This handler intercepts both parsing and execution exceptions, letting you format error messages consistently and return specific exit codes based on error types. This approach ensures your CLI communicates failures clearly to both users and automation scripts that check exit codes.