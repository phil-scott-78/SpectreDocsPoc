---
title: "How to Prompt for User Input"
description: "Collect input from users with text prompts, confirmations, and selection menus"
uid: "console-howto-prompting-for-user-input"
order: 2150
---

When your application needs user input, use the interactive prompts.

## Ask for Text

To get text input, use `AnsiConsole.Ask<T>()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AskForText
```

## Ask for Confirmation

To get a yes/no answer, use `AnsiConsole.Confirm()`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AskForConfirmation
```

## Present Choices

To let users pick from options, use `SelectionPrompt`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.PresentChoices
```

## Allow Multiple Selections

To let users select multiple items, use `MultiSelectionPrompt`.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.HowTo.PromptingForUserInputHowTo.AllowMultipleSelections
```

## See Also

- [Text Prompt](/console/prompts/text-prompt) - Full text prompt API
- [Selection Prompt](/console/prompts/selection-prompt) - Selection prompt options
- [Multi-Selection Prompt](/console/prompts/multi-selection-prompt) - Multi-selection options
