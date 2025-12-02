---
title: "Extensibility Points"
description: "A reference page on extending or integrating with Spectre.Console.Cli beyond the basics"
uid: "cli-extensibility"
order: 4050
---

This reference documents the interfaces and extension points for customizing Spectre.Console.Cli behavior.

## Dependency Injection

### ITypeRegistrar Interface

Implement to integrate your DI container with Spectre.Console.Cli.

| Method | Description |
|--------|-------------|
| `Register(Type service, Type implementation)` | Register type mapping |
| `RegisterInstance(Type service, object implementation)` | Register singleton instance |
| `RegisterLazy(Type service, Func<object> factory)` | Register factory function |
| `Build()` | Build and return ITypeResolver |

**Example Implementation:**
```csharp
public class MyRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _services;

    public MyRegistrar(IServiceCollection services) => _services = services;

    public void Register(Type service, Type implementation)
        => _services.AddSingleton(service, implementation);

    public void RegisterInstance(Type service, object implementation)
        => _services.AddSingleton(service, implementation);

    public void RegisterLazy(Type service, Func<object> factory)
        => _services.AddSingleton(service, _ => factory());

    public ITypeResolver Build()
        => new MyResolver(_services.BuildServiceProvider());
}
```

### ITypeResolver Interface

Resolves services from your DI container.

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Resolve(Type? type)` | object? | Resolve service, null if not found |

**Example Implementation:**
```csharp
public class MyResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public MyResolver(IServiceProvider provider) => _provider = provider;

    public object? Resolve(Type? type)
        => type == null ? null : _provider.GetService(type);
}
```

### ITypeRegistrarFrontend Interface

Generic wrapper for type registration, accessible via `Settings.Registrar`.

| Method | Description |
|--------|-------------|
| `Register<TService, TImplementation>()` | Register generic type mapping |
| `RegisterInstance<TService>(TService instance)` | Register generic singleton |

### Usage

```csharp
var services = new ServiceCollection();
services.AddSingleton<IMyService, MyService>();

var registrar = new MyRegistrar(services);
var app = new CommandApp(registrar);
```

## Command Interceptors

### ICommandInterceptor Interface

Hook into command execution for cross-cutting concerns.

| Method | Description |
|--------|-------------|
| `Intercept(CommandContext, CommandSettings)` | Called before command execution |
| `InterceptResult(CommandContext, CommandSettings, ref int)` | Called after execution with result |

**Example:**
```csharp
public class TimingInterceptor : ICommandInterceptor
{
    private Stopwatch? _stopwatch;

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void InterceptResult(CommandContext context, CommandSettings settings, ref int result)
    {
        _stopwatch?.Stop();
        Console.WriteLine($"Elapsed: {_stopwatch?.ElapsedMilliseconds}ms");
    }
}
```

### Registration

Register interceptors through your DI container:

```csharp
services.AddSingleton<ICommandInterceptor, TimingInterceptor>();
```

Multiple interceptors can be registered and execute in registration order.

## Custom Help Providers

### IHelpProvider Interface

Implement to customize help output formatting.

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Write(ICommandModel, ICommandInfo?)` | IEnumerable\<IRenderable\> | Generate help content |

**Parameters:**

| Parameter | Description |
|-----------|-------------|
| `model` | Complete command tree model |
| `command` | Current command (null for root) |

### Registration

```csharp
app.Configure(config =>
{
    config.SetHelpProvider<CustomHelpProvider>();
    // Or with instance:
    config.SetHelpProvider(new CustomHelpProvider());
});
```

## Custom Validation

### Settings-Level Validation

Override `Validate()` in your settings class:

```csharp
public class MySettings : CommandSettings
{
    [CommandOption("--min")]
    public int Min { get; set; }

    [CommandOption("--max")]
    public int Max { get; set; }

    public override ValidationResult Validate()
    {
        if (Min > Max)
            return ValidationResult.Error("Min cannot exceed max");
        return ValidationResult.Success();
    }
}
```

### Command-Level Validation

Override `Validate()` in your command class:

```csharp
public class MyCommand : Command<MySettings>
{
    public override ValidationResult Validate(CommandContext context, MySettings settings)
    {
        // Additional validation with context access
        return ValidationResult.Success();
    }

    public override int Execute(CommandContext context, MySettings settings)
    {
        // Command logic
        return 0;
    }
}
```

### Property-Level Validation

Create reusable validation attributes:

```csharp
public sealed class ValidPathAttribute : ParameterValidationAttribute
{
    public override ValidationResult Validate(CommandParameterContext context)
    {
        if (context.Value is string path && !Path.Exists(path))
            return ValidationResult.Error($"Path not found: {path}");
        return ValidationResult.Success();
    }
}
```

**Usage:**
```csharp
[ValidPath]
[CommandArgument(0, "<path>")]
public string Path { get; set; }
```

## Exception Handling

### Custom Exception Handler

Handle exceptions globally:

```csharp
app.Configure(config =>
{
    config.Settings.ExceptionHandler = (ex, resolver) =>
    {
        Console.Error.WriteLine($"Error: {ex.Message}");
        return -1; // Exit code
    };
});
```

### Exception Propagation

Let exceptions bubble up for external handling:

```csharp
config.Settings.PropagateExceptions = true;

try
{
    return app.Run(args);
}
catch (Exception ex)
{
    // Handle exception
    return -1;
}
```

## See Also

- [Configuration API Reference](/cli/reference/configuration-api-reference) - App configuration
- [CommandContext Reference](/cli/reference/command-context) - Accessing interceptor data
- [Dependency Injection Tutorial](/cli/tutorials/dependency-injection-in-cli-apps) - DI setup guide
