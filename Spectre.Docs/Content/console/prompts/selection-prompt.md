---
title: "SelectionPrompt"
description: "Let users select a single option from a list with keyboard navigation"
uid: "console-prompt-selection"
order: 5050
---

The SelectionPrompt creates interactive menus where users navigate with arrow keys to select one option from a list.

## When to Use

Use SelectionPrompt when you need to **present a clear set of mutually exclusive options**. Common scenarios:

- **Menu navigation**: Main menus, configuration choices, action selection
- **Categorical selection**: Countries, languages, categories with defined options
- **Mode switching**: Environment selection (Dev/Stage/Prod), output formats, themes

For **multiple selections**, use [MultiSelectionPrompt](/console/prompts/multi-selection-prompt) instead. For **free-form text input**, use [TextPrompt](/console/prompts/text-prompt) instead.

> [!CAUTION]
> Selection prompts are not thread safe. Using them together with other interactive components such as progress displays, status displays, or other prompts is not supported.

## Basic Usage

The simplest selection prompt needs a title and choices.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.BasicSelectionExample
```

## Adding a Title

Use markup to style the title and draw attention to key information.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.SelectionWithTitleExample
```

## Populating Choices

### Multiple Ways to Add Choices

You can add choices using params arrays, IEnumerable collections, or individual AddChoice calls.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.AddChoicesVariationsExample
```

### Hierarchical Choices

Use `AddChoiceGroup()` to organize choices into categories with parent-child relationships.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.HierarchicalChoicesExample
```

## Navigation

### Pagination

Use `PageSize()` to control how many items display at once, and customize the hint text shown when more choices exist.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.PageSizeExample
```

### Wrap-Around

Enable `WrapAround()` for circular navigation - pressing up at the top jumps to the bottom, and vice versa.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.WrapAroundExample
```

## Search

### Enabling Search

Use `EnableSearch()` to let users type and filter the list instantly - essential for long lists.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.SearchEnabledExample
```

### Search Highlighting

Customize how matched characters are highlighted during search with `SearchHighlightStyle()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.SearchHighlightStyleExample
```

## Styling

### Highlight Style

Use `HighlightStyle()` to customize the appearance of the currently selected item.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.HighlightStyleExample
```

### Disabled Item Style

Use `DisabledStyle()` to style non-selectable items like group headers.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.DisabledStyleExample
```

## Selection Modes

### Leaf Mode

Use `SelectionMode.Leaf` to only allow selecting leaf nodes - parent group headers become non-selectable.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.SelectionModeLeafExample
```

### Independent Mode

Use `SelectionMode.Independent` to allow selecting both parent groups and their children.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.SelectionModeIndependentExample
```

## Working with Complex Objects

Use `UseConverter()` to display custom formatted text for complex objects while returning the actual object.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.CustomConverterExample
```

## Complete Example

This comprehensive example combines search, pagination, wrap-around, custom styling, and complex objects for a realistic project selection menu.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Reference.Prompts.SelectionPromptExamples.CompleteExampleWithAllFeatures
```

## See Also

- [How to Prompt for User Input](/console/how--to/prompting-for-user-input) - Step-by-step guide
- [Asking User Questions Tutorial](/console/tutorials/interactive-prompts-tutorial) - Learn prompts basics
- [MultiSelectionPrompt](/console/prompts/multi-selection-prompt) - Select multiple items
- [TextPrompt](/console/prompts/text-prompt) - Free-form text input

## API Reference

<WidgetApiReference TypeName="Spectre.Console.SelectionPrompt`1" />
