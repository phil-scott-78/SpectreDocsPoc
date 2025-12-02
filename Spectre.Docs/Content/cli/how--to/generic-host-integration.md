---
title: "Generic Host Integration"
description: "How to integrate Spectre.Console.Cli with Microsoft.Extensions.Hosting"
uid: "cli-generic-host"
order: 2065
---

When building CLI applications that need the full power of .NET's hosting infrastructure—configuration from multiple sources, logging, dependency injection with scopes, and hosted services—you can integrate Spectre.Console.Cli with `Microsoft.Extensions.Hosting` (the Generic Host).

The integration typically involves creating a custom `ITypeRegistrar` that wraps the host's `IServiceCollection`, then running your `CommandApp` within the host's lifetime. This gives you access to `IConfiguration`, `ILogger<T>`, and all registered services in your commands. You can also run background services alongside your CLI commands, which is useful for scenarios like progress reporting or cleanup tasks. The key is ensuring proper lifetime management—the host should start before commands run and shut down gracefully when the command completes.
