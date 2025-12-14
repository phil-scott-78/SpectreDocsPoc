---
title: "Logging in CLI Apps"
description: "Add structured logging to your CLI commands using Microsoft.Extensions.Logging"
uid: "cli-logging-tutorial"
order: 1070
---

In this tutorial, we'll add logging to a CLI application with configurable log levels. By the end, you'll have commands
that use structured logging with levels controllable via command-line options.

## What We're Building

Here's how our CLI will work when we're done:

<Screenshot Src="/assets/cli-logging-tutorial.svg" Alt="Screencast of the Logging Tutorial" />

A file processing command that logs its progress. Users can control verbosity with `--logLevel`.

## Prerequisites

- Completed the [Quick Start tutorial](xref:cli-quick-start)
- Basic familiarity with dependency injection (see <xref:cli-di-tutorial>)

<Steps>
<Step stepNumber="1">
## Starting Without Logging

Let's start with a simple file processing command:

```bash
dotnet new console -n LoggingApp
cd LoggingApp
dotnet add package Spectre.Console.Cli
```

Replace `Program.cs` with a command that processes files:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.NoLogging.ProcessSettings
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.NoLogging.ProcessCommand
```

Wire it up:

```csharp
using Spectre.Console.Cli;

var app = new CommandApp<ProcessCommand>();
return app.Run(args);
```

Run the application:

```bash
dotnet run -- myfile.txt
# Starting to process: myfile.txt
# Processing step 1...
# Processing step 2...
# Processing step 3...
# Processing complete!
```

This works, but we're using `Console.WriteLine` directly. We have no way to control verbosity or integrate with
logging infrastructure.

</Step>
<Step stepNumber="2">
## Adding Structured Logging

Add the logging packages:

```bash
dotnet add package Microsoft.Extensions.Logging
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Microsoft.Extensions.DependencyInjection
```

Create the DI bridge classes and update the command to inject `ILogger<T>`:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.WithLogging.TypeRegistrar
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.WithLogging.TypeResolver
```

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.WithLogging.ProcessCommand
```

Configure logging in your entry point:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});

var registrar = new TypeRegistrar(services);
var app = new CommandApp<ProcessCommand>(registrar);
return app.Run(args);
```

Run it again:

```bash
dotnet run -- myfile.txt
# info: ProcessCommand[0]
#       Starting to process: myfile.txt
# info: ProcessCommand[0]
#       Processing step 1...
```

Now we have structured logging with category names and log levels. But the level is hard-coded. Let's make it
configurable.

</Step>
<Step stepNumber="3">
## Configurable Log Level with Interceptor

Now we'll add command-line control over the log level using a base settings class and an interceptor.

First, create a `LogLevelSwitch` that holds the current minimum level, and a base `LogCommandSettings` class:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.LoggingComplete.LogLevelSwitch
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.LoggingComplete.LogCommandSettings
```

Create an interceptor that reads the settings and updates the switch before command execution:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.LoggingComplete.LogInterceptor
```

Update your settings to inherit from `LogCommandSettings`:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.Logging.LoggingComplete.ProcessSettings
```

Configure the logging filter to check the switch, and register the interceptor:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

var services = new ServiceCollection();

// Create a log level switch that can be modified at runtime
var logLevelSwitch = new LogLevelSwitch();
services.AddSingleton(logLevelSwitch);

// Configure logging with a filter that checks the switch
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddFilter((category, level) => level >= logLevelSwitch.MinimumLevel);
});

var registrar = new TypeRegistrar(services);
var app = new CommandApp<ProcessCommand>(registrar);

app.Configure(config =>
{
    // Set up the interceptor to configure logging before command execution
    config.SetInterceptor(new LogInterceptor(logLevelSwitch));
});

return app.Run(args);
```

The `TypeRegistrar` and `TypeResolver` stay the same as Step 2.

Try different log levels:

```bash
dotnet run -- myfile.txt
# info: Processing step 1...

dotnet run -- myfile.txt --logLevel Debug
# dbug: Detailed step 1 information
# info: Processing step 1...

dotnet run -- myfile.txt --logLevel Warning
# warn: This is a warning message
```

This pattern has several advantages:

- **Reusable**: Any command can inherit from `LogCommandSettings` to get the `--logLevel` option
- **Centralized**: The interceptor handles log configuration in one place
- **Runtime configurable**: Users control verbosity without recompiling
- **Structured**: Log messages include categories, levels, and structured parameters

</Step>
</Steps>

## Congratulations!

You've built a CLI application with configurable logging:

- Commands inject `ILogger<T>` for structured logging
- A `LogLevelSwitch` allows runtime log level changes
- An interceptor reads command settings and configures logging before execution
- Base settings classes provide reusable options across commands

## Next Steps

- <xref:cli-di-tutorial> - Learn more about dependency injection patterns
- <xref:cli-command-interception> - Other uses for command interceptors
