---
title: "Prompt for User Input"
description: "Collect input from users with text prompts, confirmations, and selection menus"
uid: "console-howto-prompting-for-user-input"
order: 2050
---

When your application needs user input, use the interactive prompts.

> [!CAUTION]
> Prompts are not thread safe. Using them together with other interactive components such as progress displays, status displays, or other prompts is not supported.

## Ask for Text

To get text input, use `AnsiConsole.Ask<T>()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AskForText
```

## Ask for Confirmation

To get a yes/no answer, use `AnsiConsole.Confirm()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AskForConfirmation
```

## Present Choices

To let users pick from options, use `SelectionPrompt`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.PresentChoices
```

## Allow Multiple Selections

To let users select multiple items, use `MultiSelectionPrompt`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AllowMultipleSelections
```

## See Also

- <xref:console-prompt-text> - Full text prompt API
- <xref:console-prompt-selection> - Selection prompt options
- <xref:console-prompt-multi-selection> - Multi-selection options
