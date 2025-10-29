---
title: "Intercepting Command Execution"
description: "How to use command interceptors to run logic before or after any command executes"
uid: "cli-command-interception"
order: 2080
---

When you need to apply cross-cutting concerns like logging, timing, or resource initialization across all commands without duplicating code, implement a command interceptor. Create a class implementing `ICommandInterceptor` and register it via `config.SetInterceptor(...)` or through your DI container.

Use the `Intercept` method to run setup logic before command execution—configure logging scopes, initialize databases, or modify settings. Then use `InterceptResult` after execution to perform cleanup, flush logs, or adjust exit codes based on the command outcome. This keeps individual commands focused on their core logic while centralizing concerns like instrumentation, authentication checks, or performance monitoring.