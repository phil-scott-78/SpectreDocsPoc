---
title: "MultiSelectionPrompt"
description: "Allow users to select multiple options from a list"
uid: "console-prompt-multi-selection"
order: 5100
---

The MultiSelectionPrompt creates interactive checkbox-style lists where users can select multiple options using the spacebar to toggle selections and enter to confirm.

## When to Use

Use MultiSelectionPrompt when you need to **allow users to select multiple items from a list**. Common scenarios:

- **Feature selection**: Enable multiple application features, plugins, or modules
- **Batch operations**: Select multiple files, records, or items for processing
- **Permission configuration**: Grant multiple permissions or roles to a user
- **Multi-target deployment**: Choose multiple environments or servers for deployment

For **single-item selection**, use [SelectionPrompt](/console/prompts/selection-prompt) instead.

> [!CAUTION]
> Multi-selection prompts are not thread safe. Using them together with other interactive components such as progress displays, status displays, or other prompts is not supported.

## Basic Usage

Create a multi-selection prompt by specifying the type and adding choices. Users navigate with arrow keys, toggle selections with spacebar, and confirm with enter. The result is a `List<T>` of selected items.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.BasicMultiSelectionPromptExample
```

## Adding Choices

### Bulk Adding with AddChoices

Use `AddChoices()` to add multiple options at once from a collection.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.AddChoicesExample
```

### Grouped Choices

Use `AddChoiceGroup()` to organize related choices into labeled sections, making it easier for users to understand the structure of available options.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.GroupedChoicesExample
```

## Pre-Selecting Items

Use `Select()` to mark specific items as checked by default. This is useful when you want to suggest recommended options while still allowing users to customize their selection.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.PreSelectItemsExample
```

## Selection Requirements

### Required Selections

Use `Required()` to enforce that users must select at least one item before they can confirm. Use `NotRequired()` to allow an empty selection (no items checked).

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.RequiredSelectionExample
```

## Hierarchical Choices

### Creating Hierarchies

Use `AddChild()` to create nested tree structures with parent and child items. This is ideal for organizing permissions, file systems, or any hierarchical data.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.HierarchicalChoicesExample
```

### Selection Modes

The `Mode()` method controls which items in a hierarchy can be selected:

**Leaf Mode (Default)**: Only leaf nodes (items without children) can be selected. Use this when you want users to select specific items, not categories.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.SelectionModeLeafExample
```

**Independent Mode**: Any node can be selected, whether it has children or not. Use this when selecting a parent folder should be different from selecting all its contents.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.SelectionModeIndependentExample
```

## Pagination and Navigation

### Page Size

Use `PageSize()` to control how many items are visible at once. This improves readability for long lists by showing a scrollable window.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.PageSizeExample
```

### Wrap Around Navigation

Use `WrapAround()` to enable circular navigation where pressing down on the last item jumps to the first item, and pressing up on the first item jumps to the last.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.WrapAroundExample
```

## Styling

### Highlight Style

Use `HighlightStyle()` to customize the appearance of the currently focused item with colors and text decorations.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.HighlightStyleExample
```

### Custom Instructions

Use `InstructionsText()` and `MoreChoicesText()` to provide clearer guidance to users about how to interact with the prompt.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.CustomInstructionsExample
```

## Using Custom Types

### Custom Display with UseConverter

Use `UseConverter()` to control how complex objects are displayed in the prompt while still working with the actual object type in your code.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.UseConverterExample
```

### Complete Custom Object Example

This example demonstrates working with custom record types, including display conversion, pre-selection, and processing the selected results.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.MultiSelectionPromptExamples.ComplexObjectExample
```

## See Also

- [How to Prompt for User Input](/console/how--to/prompting-for-user-input) - Step-by-step guide
- [Asking User Questions Tutorial](/console/tutorials/interactive-prompts-tutorial) - Learn prompts basics
- [SelectionPrompt](/console/prompts/selection-prompt) - Single-item selection
- [TextPrompt](/console/prompts/text-prompt) - Free-form text input
- [Terminal Compatibility](/console/reference/compatibility-matrix) - Interactive mode support

## API Reference

<WidgetApiReference TypeName="Spectre.Console.MultiSelectionPrompt`1" />
