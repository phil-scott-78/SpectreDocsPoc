---
title: "TextPrompt"
description: "Prompt users for text input with validation and default values"
uid: "console-prompt-text"
order: 5000
---

The TextPrompt prompts users to enter text input with validation, default values, and secret input masking.

## When to Use

Use TextPrompt when you need to **collect user input** from the console. Common scenarios:

- **Free-form text input**: Collecting names, email addresses, or any text data
- **Numeric input**: Getting ages, quantities, or measurements with automatic type conversion
- **Secure input**: Requesting passwords or API keys with masked display
- **Validated input**: Ensuring input meets specific requirements before accepting it

For **selecting from a predefined list of options**, use [SelectionPrompt](/console/prompts/selection) instead, which provides a better user experience with arrow key navigation.

> [!CAUTION]
> Text prompts are not thread safe. Using them together with other interactive components such as progress displays, status displays, or other prompts is not supported.

## Basic Usage

Use `AnsiConsole.Ask<T>()` for the simplest way to prompt for user input.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.BasicAskExample
```

## Default Values

### Using Ask with Default

You can provide a default value that users can accept by pressing Enter.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.AskWithDefaultExample
```

### Configuring Default Display

Use `DefaultValue()` to set a default and control whether it's shown to the user.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.DefaultValueExample
```

## Type Conversion

TextPrompt automatically converts input to your target type using built-in type converters.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.TypeConversionExample
```

This works with any type that has a `TypeConverter`, including `int`, `decimal`, `DateTime`, `Guid`, and custom types.

## Direct TextPrompt Usage

For more control over prompt behavior, create a `TextPrompt<T>` instance directly instead of using `Ask()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.TextPromptBasicExample
```

## Secret Input

### Password Masking

Use `Secret()` to mask sensitive input with asterisks by default.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.SecretInputExample
```

### Custom Mask Characters

Specify a custom mask character or use `null` to completely hide input.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.CustomMaskExample
```

## Validation

### Simple Validation

Use `Validate()` with a boolean function to check if input is acceptable.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.SimpleValidationExample
```

### Rich Validation

For more complex validation with custom error messages, use `ValidationResult`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.RichValidationExample
```

## Restricting to Choices

### Showing Choices

Use `AddChoices()` to restrict input to specific values, displayed as options to the user.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.ChoicesExample
```

This is useful for limited options where users can type their choice. For better UX with many options, use [SelectionPrompt](/console/prompts/selection) instead.

### Hidden Choices

Use `HideChoices()` when you want to validate against specific values without revealing them.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.HiddenChoicesExample
```

## Optional Input

Use `AllowEmpty()` to make input optional, allowing users to press Enter without typing anything.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.AllowEmptyExample
```

## Styling

Customize the appearance of your prompts with different colors for the prompt text, default values, and choices.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.StylingExample
```

## Custom Converters

Use `WithConverter()` to control how choices are displayed to users while keeping their underlying values.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.TextPromptExamples.CustomConverterExample
```

## See Also

- [How to Prompt for User Input](/console/how--to/prompting-for-user-input) - Step-by-step guide
- [Asking User Questions Tutorial](/console/tutorials/interactive-prompts-tutorial) - Learn prompts basics
- [SelectionPrompt](/console/prompts/selection-prompt) - Choose from predefined options
- [Terminal Compatibility](/console/reference/compatibility-matrix) - Interactive mode support
- [Testing Console Output](/console/how--to/testing-console-output) - Test prompts in CI

## API Reference

<WidgetApiReference TypeName="Spectre.Console.TextPrompt`1" />
