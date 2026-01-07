---
title: "Table Border Reference"
description: "A complete reference of all table border styles available in Spectre.Console"
uid: "console-table-border-reference"
order: 7100
---

Spectre.Console provides various table border styles to customize the appearance of tables.
Each border style defines characters for different parts of the table structure.

## Usage Example

```csharp
var table = new Table();
table.Border(TableBorder.Rounded);
table.AddColumn("Name");
table.AddColumn("Age");
table.AddRow("Alice", "25");
AnsiConsole.Write(table);
```

## Important Notes

- **Unicode Support:** Some border styles use Unicode box-drawing characters. Ensure your terminal supports Unicode.
- **Safe Borders:** Each border has a `SafeBorder` property that provides an ASCII fallback for limited terminals.
- **Custom Borders:** You can create custom borders by inheriting from `TableBorder` and implementing `GetPart()`.

## Available Table Borders

<TableBorderList />

## See Also

- <xref:console-widget-table> - Display tabular data with borders
- <xref:console-howto-displaying-tabular-data> - Table styling guide
- <xref:console-box-border-reference> - Borders for panels and rules
