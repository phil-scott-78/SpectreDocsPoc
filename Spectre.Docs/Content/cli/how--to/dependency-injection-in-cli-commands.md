---
title: "Dependency Injection in CLI Commands"
description: "How to integrate a DI container with Spectre.Console.Cli for injecting services into commands"
uid: "cli-dependency-injection"
order: 2060
---

When your commands need access to services like databases, loggers, or HTTP clients, integrate your DI container with Spectre.Console.Cli. Implement `ITypeRegistrar` and `ITypeResolver` to bridge your container (such as Microsoft.Extensions.DependencyInjection) with the CLI framework—or use the provided base classes for common scenarios.

Register your services in the container, pass the registrar to the `CommandApp` constructor, and declare dependencies in your command's constructor. The framework will automatically resolve and inject these services when executing commands. You can verify your integration works correctly using `TypeRegistrarBaseTests.RunAllTests()` from Spectre.Console.Testing. This approach keeps commands clean, testable, and decoupled from infrastructure concerns.