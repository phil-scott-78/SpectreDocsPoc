---
title: "Design Philosophy: Convention over Configuration"
description: "An explanation of the guiding philosophy behind Spectre.Console.Cli"
uid: "cli-design-philosophy"
order: 3010
---

Spectre.Console.Cli is built on a **convention over configuration** principle. In practice, this means the library is
highly opinionated about how command-line apps should be structured, yet it aligns with common CLI standards so you
_don't_ have to configure every little detail. By following established conventions, Spectre.Console.Cli provides a lot
of functionality out of the box, letting you focus on your app's logic instead of boilerplate. In fact, it deliberately
draws inspiration from the CLI tools you use every day and adheres to industry norms. The result is a framework that
feels familiar (if you've used tools like Git or the .NET CLI) and spares you from writing repetitive parsing or help
text code.

Following Established CLI Conventions
-------------------------------------

One way Spectre.Console.Cli lives up to its philosophy is by automatically following common command-line conventions.
You don’t need to manually specify how options or help should work – the library has sensible defaults:

* **Short and long option names:** Options follow the typical Unix-style naming. Single-character options use a single
  dash (for example `-v`), while multi-character (long) options use a double dash (for example `--version`).
  Spectre.Console.Cli enforces this naming scheme when you define options via attributes, so your application's switches
  behave like those in most CLI tools.

* **Automatic help generation:** Spectre.Console.Cli will generate a help screen for you automatically. If a user passes
  `-h` or `--help`, the library detects it and prints a formatted usage guide without any extra work on your
  part. This help text includes a summary of commands, options, arguments, and even context-aware details. For example,
  running `--help` at the top level shows all available commands, while `myapp command --help` shows help for that
  specific command and its options. You get a professional-looking `--help` output by default, following the style
  users expect.

* **Standard parsing rules:** The parser understands common patterns (like grouping short flags, `--option value` vs
  `--option=value`, etc.) according to conventions. Boolean flags can be specified without a value (just `--flag` to set
  true) as a special case of options. These standard behaviors mean less surprise for users and less configuration for
  you.

All these conventions are baked in. You generally only need to configure things if you want to override the defaults. By
relying on conventions, Spectre.Console.Cli reduces the number of decisions and configurations you have to make for the
basic CLI behavior – it works as expected.

Commands and Settings: A Structured Approach
--------------------------------------------

Beyond option syntax, Spectre.Console.Cli is opinionated about how you structure your application's commands. Rather
than parsing raw `string[] args` manually or handling everything in one place, you organize your CLI into **commands**
and **settings**. This separation of concerns is central to the design.

*   **Commands vs Settings:** In Spectre.Console.Cli, every command is a class that contains the logic to execute, and it is
    paired with a _settings_ class that defines the inputs (options, flags, arguments) that the command needs. You define a
    settings class by inheriting from `CommandSettings` and adding properties with attributes indicating how to parse
    them. Then you create a command class by inheriting `Command<T>` (or `AsyncCommand<T>` for asynchronous commands) using
    that settings class as the generic type parameter. This means each command explicitly declares what kind of settings it
    expects at compile time.

    This design enforces a clear separation: the data the command operates on is separate from the code that runs. For
    example, you might have a `BuildSettings` class that defines options like `--configuration` and a
    `BuildCommand : Command<BuildSettings>` that executes a build using those options. The command doesn't need to know
    about parsing command-line text at all – it just receives a populated `BuildSettings` object.

*   **Composition of commands:** Spectre.Console.Cli encourages you to compose complex command-line interfaces from simple
    pieces. The library lets you define a hierarchy or tree of commands (subcommands and nested subcommands) in a very
    natural way. The .NET type system declares the commands, while composition ties them together. In practice, you
    register your commands with a central `CommandApp` and can nest them under branch commands. Settings classes can
    inherit from one another, so common options are defined once in a base class and shared across related commands. The
    library enforces that only commands with compatible settings can be added under a given branch.

Leveraging the .NET Type System for Safety
------------------------------------------

A key rationale behind this design is to catch errors early (ideally at compile time) and provide more safety than
ad-hoc string parsing. By using generics and attributes (the .NET type system), Spectre.Console.Cli shifts a lot of
potential mistakes out of the runtime parsing logic and into the compile-time structure of your program.

* **Compile-time guidance:** Because commands are generic classes tied to specific settings types, many mismatches are
  simply impossible to compile. For instance, if you try to add a command to a branch where it doesn't belong, the types
  won't line up. The library's configuration API is designed so that you _cannot_ configure commands in incompatible
  ways. The compiler and the Spectre API will complain if, say, you attempted to attach a `Command<SettingsA>` under a
  branch expecting a different base settings type. This means your program's structure follows logical rules enforced by
  the type system, guiding you to do the right thing.

* **Automatic validation:** Even beyond compile-time, the framework helps catch mistakes as soon as possible. When you
  run your app, Spectre.Console.Cli automatically handles basic validation like type conversion. If an option is
  supposed to be an integer and the user provides a non-integer string, the parser will detect that and report an error
  without your code needing to do anything. You can also easily implement richer validation rules. Each
  `CommandSettings` class can override a `Validate()` method to perform custom checks on the parsed input and return an
  error if something is amiss. For example, you might ensure that an option's value is within a certain range, or that
  not more than one of two flags is used at the same time. This validation step runs before the command's `Execute` is
  called. In other words, by the time your command logic runs, you are guaranteed to have a well-formed, strongly-typed
  settings object. This greatly reduces the chances of runtime errors caused by missing or invalid arguments – problems
  are caught either at compile-time or at startup, not halfway through your execution.

* **Declarative schema:** Using attributes to declare arguments and options means you're writing a declarative CLI
  schema rather than imperative parsing code. Compared to manually splitting strings in `Main`, this approach is less
  error-prone and easier to maintain.

Composition and Reusability
---------------------------

Because commands and settings are separate and can be composed flexibly, reusability comes naturally. Common options
like `--verbose` can live in a base settings class that all subcommands inherit, avoiding duplication.

The design also makes it trivial to extend or rearrange your command structure. Adding a new subcommand means creating
a settings/command pair and registering it in one line. Since the structure is declared in `app.Configure`, you can
reorganize the hierarchy with minimal code changes.

Benefits for Testing and Maintenance
------------------------------------

These patterns yield practical benefits. Your codebase ends up organized by commands, making it easy to navigate – a
new contributor can look at the `Commands/` folder and quickly understand what the CLI does. Unit testing becomes
straightforward: invoke your command classes directly with constructed settings objects, testing pure business logic
rather than parsing code. And since the library handles help generation, parsing edge cases, and error reporting, you
write less custom code where bugs can hide.

Spectre.Console.Cli's convention-over-configuration approach means the library handles parsing rules, help text, and
command wiring in a standard way. You get a robust application with clean structure and compile-time safety, without
writing boilerplate.