---
title: "How to Test Console Output"
description: "Write unit tests for console applications using TestConsole"
uid: "console-howto-testing-console-output"
order: 2160
---

When you need to test console output, use `TestConsole` from `Spectre.Console.Testing`.

## Accept IAnsiConsole

To enable testing, accept `IAnsiConsole` as a parameter.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.TestingConsoleOutputHowTo.AcceptConsoleAsParameter(Spectre.Console.IAnsiConsole)
```

## Structure for Testability

To test code, pass `TestConsole` instead of the real console.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.TestingConsoleOutputHowTo.StructureForTestability
```

## Write Testable Methods

To make methods testable, have them accept `IAnsiConsole`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.TestingConsoleOutputHowTo.PrintGreeting(Spectre.Console.IAnsiConsole,System.String)
```

## Test Prompts

To test prompts, queue input with `console.Input.PushTextWithEnter()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.TestingConsoleOutputHowTo.GetUserName(Spectre.Console.IAnsiConsole)
```

## See Also

- [Spectre.Console.Testing](https://www.nuget.org/packages/Spectre.Console.Testing) - Testing package
