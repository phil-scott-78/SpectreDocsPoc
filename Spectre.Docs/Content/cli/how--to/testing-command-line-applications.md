---
title: "Testing Command-Line Applications"
description: "How to test CLI apps built with Spectre.Console.Cli to ensure they parse and execute correctly"
uid: "cli-testing"
order: 2090
---

To test CLI applications, use `CommandAppTester` from `Spectre.Console.Cli.Testing`. It runs commands in-memory and captures exit codes and output for assertions.

## Make Commands Testable

Inject `IAnsiConsole` instead of using the static `AnsiConsole` directly. This allows tests to capture output:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.TestingCommands.GreetCommand
```

## Test with CommandAppTester

Install the testing package:

```bash
dotnet add package Spectre.Console.Cli.Testing
```

Configure the tester like a regular `CommandApp`, then call `Run` with arguments. The result provides `ExitCode`, `Output`, and `Settings` for assertions:

```csharp:xmldocid
M:Spectre.Docs.Cli.Examples.DemoApps.TestingCommands.GreetCommandTests.Greet_WithName_ReturnsZeroAndOutputsGreeting
```

You can also verify that command-line arguments were parsed correctly:

```csharp:xmldocid
M:Spectre.Docs.Cli.Examples.DemoApps.TestingCommands.GreetCommandTests.Greet_WithCount_ParsesSettingsCorrectly
```

## Test Interactive Prompts

For commands with prompts, use `TestConsole` to queue input before running:

```csharp:xmldocid
M:Spectre.Docs.Cli.Examples.DemoApps.TestingCommands.InteractiveCommandTests.Interactive_WithQueuedInput_ProcessesCorrectly
```

Use `PushKey` for arrow keys and enter, `PushTextWithEnter` for text input.

## See Also

- [Dependency Injection in CLI Commands](/cli/how--to/dependency-injection-in-cli-commands) - Inject IAnsiConsole via DI
- [Handling Errors and Exit Codes](/cli/how--to/handling-errors-and-exit-codes) - Test error scenarios