---
title: "Configuration API Reference"
description: "A reference page enumerating the methods and properties available on the CommandApp configuration object and related classes"
uid: "cli-configuration-api"
order: 4030
---

This reference documents the configuration API for Spectre.Console.Cli applications, including `CommandApp`, configurator interfaces, and application settings.

## CommandApp Class

The main entry point for CLI applications.

### Constructors

```csharp
CommandApp()
CommandApp(ITypeRegistrar? registrar)
```

| Parameter | Description |
|-----------|-------------|
| `registrar` | Optional dependency injection registrar |

### Generic Variant

```csharp
CommandApp<TDefaultCommand>()
CommandApp<TDefaultCommand>(ITypeRegistrar? registrar)
```

Creates an app with a default command that runs when no subcommand is specified.

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Configure(Action<IConfigurator>)` | void | Configure commands and settings |
| `SetDefaultCommand<TCommand>()` | DefaultCommandConfigurator | Set default command (non-generic variant) |
| `Run(IEnumerable<string>)` | int | Run synchronously with arguments |
| `RunAsync(IEnumerable<string>)` | Task\<int\> | Run asynchronously with arguments |

**Example:**
```csharp
var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<GreetCommand>("greet");
});
return app.Run(args);
```

## IConfigurator Interface

Top-level configuration interface accessed in `CommandApp.Configure()`.

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `AddCommand<TCommand>(string name)` | ICommandConfigurator | Register a command |
| `AddBranch<TSettings>(string name, Action<IConfigurator<TSettings>>)` | IBranchConfigurator | Add a command branch |
| `AddDelegate<TSettings>(string name, Func<...>)` | ICommandConfigurator | Add inline command |
| `AddAsyncDelegate<TSettings>(string name, Func<...>)` | ICommandConfigurator | Add async inline command |
| `AddExample(params string[])` | IConfigurator | Add usage example |
| `SetHelpProvider<T>()` | IConfigurator | Set custom help provider |
| `SetHelpProvider(IHelpProvider)` | IConfigurator | Set help provider instance |

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Settings` | ICommandAppSettings | Application settings |

**Example:**
```csharp
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add");
    config.AddCommand<RemoveCommand>("remove");
    config.AddExample("add", "file.txt");
});
```

## IConfigurator\<TSettings\> Interface

Configuration interface for branches with typed settings.

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `AddCommand<TCommand>(string name)` | ICommandConfigurator | Add subcommand |
| `AddBranch<TDerivedSettings>(string name, Action<...>)` | IBranchConfigurator | Add nested branch |
| `SetDefaultCommand<TCommand>()` | void | Set branch default command |
| `SetDescription(string)` | void | Set branch description |
| `AddExample(params string[])` | void | Add branch example |
| `HideBranch()` | void | Hide branch from help |

## ICommandConfigurator Interface

Fluent interface for configuring individual commands. All methods return `ICommandConfigurator` for chaining.

| Method | Description |
|--------|-------------|
| `WithAlias(string name)` | Add command alias |
| `WithDescription(string description)` | Set command description |
| `WithExample(params string[] args)` | Add usage example |
| `WithData(object data)` | Attach data to command context |
| `IsHidden()` | Hide command from help |

**Example:**
```csharp
config.AddCommand<DeployCommand>("deploy")
    .WithAlias("d")
    .WithDescription("Deploy the application")
    .WithExample("deploy", "--env", "production")
    .IsHidden();
```

## IBranchConfigurator Interface

Fluent interface for configuring command branches.

| Method | Return Type | Description |
|--------|-------------|-------------|
| `WithAlias(string name)` | IBranchConfigurator | Add branch alias |

## ICommandAppSettings Properties

Configure application-wide behavior via `config.Settings`.

### Application Identity

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ApplicationName` | string? | null | App name in help output |
| `ApplicationVersion` | string? | null | Version for `--version` |

### Parsing Behavior

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `CaseSensitivity` | CaseSensitivity | All | Case sensitivity for parsing |
| `StrictParsing` | bool | true | Error on unknown options |
| `ConvertFlagsToRemainingArguments` | bool | false | Unknown flags to remaining args |

### Help Output

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `MaximumIndirectExamples` | int | 5 | Max examples in help |
| `ShowOptionDefaultValues` | bool | false | Show defaults in help |
| `TrimTrailingPeriod` | bool | false | Trim periods from descriptions |
| `HelpProviderStyles` | HelpProviderStyle? | null | Help formatting styles |

### Error Handling

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `PropagateExceptions` | bool | false | Let exceptions bubble up |
| `ExceptionHandler` | Func\<Exception, ITypeResolver?, int\>? | null | Custom exception handler |
| `CancellationExitCode` | int | 130 | Exit code on cancellation |

### Validation

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ValidateExamples` | bool | false | Validate examples at startup |

### Advanced

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Console` | IAnsiConsole? | null | Custom console instance |
| `Culture` | CultureInfo? | null | Parsing culture |
| `Registrar` | ITypeRegistrarFrontend | - | DI registration access |

**Example:**
```csharp
app.Configure(config =>
{
    config.Settings.ApplicationName = "myapp";
    config.Settings.ApplicationVersion = "1.0.0";
    config.Settings.CaseSensitivity = CaseSensitivity.None;
    config.Settings.PropagateExceptions = true;
});
```

## See Also

- [Attribute and Parameter Reference](/cli/reference/attribute-and-parameter-reference) - Command attributes
- [Extensibility Points](/cli/reference/extensibility-points) - Custom type registrars and interceptors
- [Built-in Command Behaviors](/cli/reference/built-in-command-behaviors) - Default behaviors
