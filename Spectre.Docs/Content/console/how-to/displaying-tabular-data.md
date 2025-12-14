---
title: "Display Tabular Data"
description: "Display structured data in tables with borders, alignment, and styling"
uid: "console-howto-displaying-tabular-data"
order: 2060
---

When you need to display data in rows and columns, use `Table`.

## Create a Table

To create a table, add columns then rows.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.DisplayingTabularDataHowTo.CreateBasicTable
```

## Style the Borders

If you want rounded borders, call `.RoundedBorder()`.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.DisplayingTabularDataHowTo.ApplyBorderStyle
```

## Align Columns

To right-align a column, use `.RightAligned()` in the column configuration.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.DisplayingTabularDataHowTo.AlignColumns
```

## Add a Title and Footer

If you need a title, use `.Title()`. For totals, set column footers.

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.HowTo.DisplayingTabularDataHowTo.AddTitleAndFooter
```

## See Also

- <xref:console-widget-table> - Full table API reference
- <xref:console-table-border-reference> - All border styles
