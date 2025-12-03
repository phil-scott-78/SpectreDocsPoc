---
title: "Dependency Injection in CLI Apps"
description: "Inject services into your CLI commands using Microsoft.Extensions.DependencyInjection"
uid: "cli-di-tutorial"
order: 1060
---

In this tutorial, we'll add dependency injection to a CLI application. By the end, we'll have commands that receive services through their constructors - making them easier to test and more flexible.

## What We're Building

Here's how our CLI will work when we're done:

<Screenshot Src="/assets/cli-di-tutorial.svg" Alt="Screencast of the Dependency Injection Tutorial" />

The greeting logic lives in an injectable service, not hard-coded in the command.

## Prerequisites

- Completed the [Quick Start tutorial](xref:cli-quick-start)
- .NET 6.0 or later
- Basic familiarity with dependency injection concepts

<Steps>
<Step stepNumber="1">
**Starting Without DI**

Let's start by creating a project and building a simple greeting command:

```bash
dotnet new console -n GreetingApp
cd GreetingApp
dotnet add package Spectre.Console.Cli
```

Replace `Program.cs` with a greeting command that has the logic built right in:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.NoDI.GreetCommand
```

Wire it up in your entry point:

```csharp
using Spectre.Console.Cli;

var app = new CommandApp<GreetCommand>();
return app.Run(args);
```

Run the application:

```bash
dotnet run -- Alice
# Hello, Alice!

dotnet run -- Alice --formal
# Good day, Alice.
```

This works, but the greeting logic is embedded in the command. If we wanted to test this command, we'd have no way to substitute different greeting behavior. Let's fix that.

</Step>
<Step stepNumber="2">
**Adding Dependency Injection**

First, add the Microsoft DI package:

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

Now we'll create a service interface, an implementation, and the bridge classes that connect Microsoft's DI container to Spectre.Console.Cli:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.WithService.IGreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.WithService.GreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.WithService.TypeRegistrar
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.WithService.TypeResolver
```

Update the command to accept the service through its constructor:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.WithService.GreetCommand
```

Finally, configure the DI container and pass the registrar to `CommandApp`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var services = new ServiceCollection();
services.AddSingleton<IGreetingService, GreetingService>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<GreetCommand>(registrar);
return app.Run(args);
```

Run it again:

```bash
dotnet run -- Alice
# Hello, Alice!
```

The output looks the same, but now the greeting logic is in a separate service. The framework automatically injects `IGreetingService` into the command's constructor.

</Step>
<Step stepNumber="3">
**Complete Example with Multiple Styles**

Let's expand the service to support multiple greeting styles, showing the flexibility that DI provides:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetingStyle
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.IGreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetCommand
```

The `TypeRegistrar` and `TypeResolver` stay the same - they're reusable infrastructure.

Try all the greeting styles:

```bash
dotnet run -- Alice
# Hello, Alice!

dotnet run -- Alice --formal
# Good day, Alice.

dotnet run -- Alice --enthusiastic
# Hey there, Alice! Great to see you!
```

Because the greeting logic is in an injectable service, you could easily swap in a different implementation - perhaps one that reads greetings from a configuration file, or one that returns mock responses during testing.

</Step>
</Steps>

## Congratulations!

You've built a CLI application with proper dependency injection:
- Services are defined as interfaces and injected through constructors
- The `TypeRegistrar` bridges Microsoft's DI container to Spectre.Console.Cli
- Commands are now easier to test - just inject mock services

This same pattern works for any service: loggers, database connections, HTTP clients, configuration providers, and more.

## Next Steps

- <xref:cli-async-commands> - Handle long-running operations with async/await
- <xref:cli-app-configuration> - Add descriptions, examples, and aliases
