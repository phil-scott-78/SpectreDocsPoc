---
title: "Rule Widget"
description: "Create horizontal dividers and section separators with optional titles"
uid: "console-widget-rule"
order: 3200
---

The Rule widget renders horizontal lines across the console width to visually separate content.

<Screenshot src="/assets/rule.svg" />

## When to Use

Use Rule when you need to **visually separate sections of output**. Common scenarios:

- **Section headers**: Mark the beginning of logical sections in reports or logs
- **Content dividers**: Separate distinct blocks of information for better readability
- **Visual hierarchy**: Create clear boundaries between different output types

For **emphasized section headers with decorative borders**, use [Panel](/console/widgets/panel) instead. For **large decorative text banners**, use [FigletText](/console/widgets/figlet).

## Basic Usage

Create a simple horizontal divider that spans the full console width.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.BasicRuleExample
```

## Adding Titles

Use titles to identify what comes after the rule, turning it into a section header.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleTitleExample
```

### Title Alignment

Position titles on the left, center, or right to match your layout needs.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleTitleAlignmentExample
```

## Border Styles

Choose a line style to match your application's visual tone or to indicate different section types.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleBorderStylesExample
```

> [!NOTE]
> See the [Box Border Reference](/console/reference/box-border-reference) for all available border styles.

## Styling

### Rule Color

Apply colors to emphasize importance or categorize sections by type.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleColorExample
```

### Subtle Separators

Use dim or muted colors for subtle dividers that don't distract from content.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleSubtleSeparatorExample
```

## Common Patterns

### Section Dividers in Reports

Use rules to organize multi-section output like system reports or status dashboards.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleSectionDividersExample
```

### Fluent Configuration

Combine extension methods for concise rule creation.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.RuleExamples.RuleFluentExample
```

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Rule" />
