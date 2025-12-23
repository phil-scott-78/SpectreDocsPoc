---
title: "Intercepting Command Execution"
description: "How to use command interceptors to run logic before or after any command executes"
uid: "cli-command-interception"
order: 2130
---

When you need cross-cutting concerns like logging, timing, or authentication across all commands without duplicating code, implement a command interceptor. The interceptor runs before and after every command, keeping individual commands focused on their core logic.

## What We're Building

A timing interceptor wrapping every command, reporting execution duration automatically—no timing code in the commands themselves:

<Screenshot Src="/assets/cli-intercepting-execution.svg" Alt="Command interception demonstration" />

## Create an Interceptor

Implement `ICommandInterceptor` with two methods: `Intercept` runs before execution, `InterceptResult` runs after. The same instance handles both, so you can store state between them.

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.InterceptingCommandExecution.TimingInterceptor
```

## Register the Interceptor

Use `SetInterceptor` in your configuration:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Cli.Examples.DemoApps.InterceptingCommandExecution.Demo.RunAsync(System.String[])
```

## Common Use Cases

- **Logging**: Configure log scopes in `Intercept`, flush logs in `InterceptResult`
- **Timing**: Start a stopwatch before, report duration after
- **Authentication**: Validate credentials before any command runs
- **Resource management**: Initialize connections before, clean up after
- **Exit code adjustment**: Modify the result based on post-execution checks

The `InterceptResult` method receives the exit code by reference (`ref int result`), allowing you to modify it based on the command outcome.

## See Also

- <xref:cli-di-tutorial> - Inject services into interceptors
- <xref:cli-error-handling> - Custom error handling