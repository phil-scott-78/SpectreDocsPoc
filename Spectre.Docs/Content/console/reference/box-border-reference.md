---
title: "Box Border Reference"
description: "A complete reference of all box border styles available in Spectre.Console"
uid: "console-box-border-reference"
order: 7050
---

Spectre.Console provides various box border styles for panels, rules, and other boxed content.
Each border style defines characters for the eight parts of a box structure.

## Usage Example

```csharp
var panel = new Panel("Hello, World!");
panel.Border(BoxBorder.Rounded);
panel.Header("Welcome");
AnsiConsole.Write(panel);
```

## Important Notes

- **Unicode Support:** Some border styles use Unicode box-drawing characters. Ensure your terminal supports Unicode.
- **Use Cases:** Box borders are used in panels, rules, and other container components.
- **Custom Borders:** You can create custom box borders by inheriting from `BoxBorder` and implementing `GetPart()`.

## Available Box Borders

<BoxBorderList />

## See Also

- [Panel Widget](/console/widgets/panel) - Bordered containers using box borders
- [Rule Widget](/console/widgets/rule) - Horizontal dividers using box borders
- [Terminal Compatibility](/console/reference/compatibility-matrix) - Unicode support by terminal
- [Table Border Reference](/console/reference/table-border-reference) - Borders for tables
