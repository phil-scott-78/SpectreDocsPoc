---
title: "Dependency Injection in CLI Apps"
description: "Inject services into your CLI commands using Microsoft.Extensions.DependencyInjection"
uid: "cli-di-tutorial"
order: 1060
---

In this tutorial, we'll add dependency injection to a CLI application. By the end, we'll have commands that receive
services through their constructors - making them easier to test and more flexible.

## What We're Building

Here's how our CLI will work when we're done:

<Screenshot Src="/assets/cli-di-tutorial.svg" Alt="Screencast of the Dependency Injection Tutorial" />

The greeting logic lives in an injectable service, not hard-coded in the command.

## Prerequisites

- Completed the [Quick Start tutorial](xref:cli-quick-start)
- .NET 8.0 or later (required for keyed services)
- Basic familiarity with dependency injection concepts

<Steps>
<Step stepNumber="1">
## Starting Without DI

Let's start by creating a project and building a simple greeting command:

```bash
dotnet new console -n GreetingApp
cd GreetingApp
dotnet add package Spectre.Console.Cli
```

Replace `Program.cs` with a greeting command that has the logic built right in:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.NoDI.GreetSettings
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

This works, but the greeting logic is embedded in the command. If we wanted to test this command, we'd have no way to
substitute different greeting behavior. Let's fix that.

</Step>
<Step stepNumber="2">
## Adding Dependency Injection

First, add the Microsoft DI package:

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

Now we'll create a service interface, an implementation, and the bridge classes that connect Microsoft's DI container to
Spectre.Console.Cli:

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

The output looks the same, but now the greeting logic is in a separate service. The framework automatically injects
`IGreetingService` into the command's constructor.

`IAnsiConsole` is automatically registered and can be injected into your commands.
Using it instead of the static `AnsiConsole` makes your commands testable. See <xref:console-howto-testing-console-output> to learn how to use `TestConsole` in unit tests.

</Step>
<Step stepNumber="3">
## Keyed Services with Factory Pattern

Now let's take it further
with [.NET 8+ keyed services](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#keyed-services).
Instead of one service handling all styles, we'll create separate implementations for each greeting style and use a
factory to select the right one at runtime.

First, define the greeting style enum and a simplified service interface:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetingStyle
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.IGreetingService
```

Create three service implementations - one for each style:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.CasualGreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.FormalGreetingService
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.EnthusiasticGreetingService
```

Now the key piece: a factory that receives the command's `Settings` through DI (Spectre.Console.Cli registers them
automatically) and uses keyed services to resolve the correct implementation:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.IGreetingFactory
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetingFactory
```

The command becomes very clean - it just asks the factory for a service:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.GreetCommand
```

Register everything with keyed services in your entry point:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var services = new ServiceCollection();

// Register keyed greeting services - each style gets its own implementation
services.AddKeyedSingleton<IGreetingService, CasualGreetingService>(GreetingStyle.Casual);
services.AddKeyedSingleton<IGreetingService, FormalGreetingService>(GreetingStyle.Formal);
services.AddKeyedSingleton<IGreetingService, EnthusiasticGreetingService>(GreetingStyle.Enthusiastic);

// Register the factory that resolves the appropriate service based on settings
services.AddScoped<IGreetingFactory, GreetingFactory>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<GreetCommand>(registrar);
return app.Run(args);
```

The `TypeRegistrar` and `TypeResolver` stay the same - they're reusable infrastructure.

Try all the greeting styles using the `--style` option:

```bash
dotnet run -- Alice
# Hello, Alice!

dotnet run -- Alice --style Formal
# Good day, Alice.

dotnet run -- Alice --style Enthusiastic
# Hey there, Alice! Great to see you!
```

This pattern has several advantages:

- **Single responsibility**: Each service implementation does one thing well
- **Extensible**: Add new styles by creating a new service and registering it with a key
- **Testable**: Mock the factory or individual services easily
- **Settings injection**: The factory receives parsed command settings via DI, keeping the command code clean

</Step>
</Steps>

## Congratulations!

You've built a CLI application with advanced dependency injection:

- Services are defined as interfaces and injected through constructors
- The `TypeRegistrar` bridges Microsoft's DI container to Spectre.Console.Cli
- Keyed services let you register multiple implementations of the same interface
- A factory pattern selects the right service at runtime based on command settings
- Commands are now easier to test - mock the factory or individual services

This same pattern works for any service: loggers, database connections, HTTP clients, configuration providers, and more.

## Next Steps

- <xref:cli-async-commands> - Handle long-running operations with async/await
- <xref:cli-app-configuration> - Add descriptions, examples, and aliases
