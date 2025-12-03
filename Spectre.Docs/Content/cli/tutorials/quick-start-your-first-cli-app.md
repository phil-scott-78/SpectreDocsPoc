---
title: "Quick Start: Your First CLI App"
description: "Build your first command-line application with Spectre.Console.Cli"
uid: "cli-quick-start"
order: 1010
---

In this tutorial, we'll build a greeting command-line application together. By the end, we'll have a working CLI that accepts a name, repeats greetings on demand, and shows helpful usage information automatically.

## What We're Building

Here's how our CLI will work when we're done:

<Screenshot Src="/assets/cli-quickstart.svg" Alt="Quickstart Tutorial" />

## Prerequisites

- .NET 8.0 or later
- Basic C# knowledge
- A terminal or command prompt


<Steps>
<Step stepNumber="1">
**Create the Project**

Let's start by creating a new console application and adding the Spectre.Console.Cli package:

```bash
dotnet new console -n GreetingApp
cd GreetingApp
dotnet add package Spectre.Console.Cli
```

The output confirms the package was installed. Now we're ready to write some code.

</Step>
<Step stepNumber="2">
**Create Your First Command**

Open `Program.cs` and replace its contents with our greeting command:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand.GreetCommand
```

Run the application:

```bash
dotnet run -- Alice
```

You should see `Hello, Alice!` printed to the console. Notice the `<name>` in angle brackets? That means it's required. Try running without a name to see the built-in error handling:

```bash
dotnet run
```

The CLI automatically tells you that the name argument is missing. No extra code needed.

</Step>
<Step stepNumber="3">
**Add an Option**

Arguments are great for required values, but sometimes you want optional behavior. Let's add a `--count` option to repeat the greeting:

```csharp:xmldocid
T:Spectre.Docs.Cli.Examples.DemoApps.QuickStart.Complete.GreetCommand
```

Run the application with the new option:

```bash
dotnet run -- Alice --count 3
```

The greeting appears three times. The `-c` short form works too:

```bash
dotnet run -- Alice -c 2
```

Options default to the value you specify (here, `1`), so the original command still works:

```bash
dotnet run -- Alice
```

</Step>
<Step stepNumber="4">
**Explore Built-in Help**

Every Spectre.Console.Cli application gets automatic help. Run with `--help`:

```bash
dotnet run -- --help
```

The output shows your command's usage, the required `<name>` argument, and the optional `--count` flag with its description. All of this was generated from the attributes we added. The `[Description]` attributes appear right in the help text.

You didn't write any help-rendering code. It just works.

</Step>
<Step stepNumber="5">
**See Validation in Action**

Spectre.Console.Cli validates input automatically. Try passing an invalid count:

```bash
dotnet run -- Alice --count abc
```

The CLI shows an error explaining that `abc` isn't a valid integer. Try omitting the required name:

```bash
dotnet run
```

Again, a clear error message appears. This built-in validation saves you from writing repetitive error-handling code.

</Step>
</Steps>

## Congratulations!

You've built a complete command-line application from scratch. Your CLI accepts arguments, supports options with defaults, displays auto-generated help, and validates user input - all with minimal code.

These same patterns scale to larger applications with multiple commands, complex arguments, and rich validation rules.

## Next Steps

- <xref:cli-multi-command-tutorial> - Add subcommands like `add`, `list`, and `remove`
- <xref:cli-attributes-parameters> - All available attributes and options
- <xref:cli-type-converters> - Parse custom types from command-line arguments

## Related Console Tutorials

Looking to enhance your CLI app with rich console features? Check out these Spectre.Console tutorials:

- <xref:console-getting-started> - Add colorful text, tables, and progress bars to your CLI commands
- <xref:console-interactive-prompts> - Ask for names, numbers, and let users choose from lists
- <xref:console-status-spinners> - Display animated spinners while work is happening
