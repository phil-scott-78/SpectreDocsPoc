---
title: "Table Widget"
description: "Display tabular data with customizable columns, rows, borders, and styling"
uid: "console-widget-table"
order: 3100
---

The Table widget is one of the most versatile and commonly used components in Spectre.Console for displaying structured tabular data. This page provides comprehensive documentation on creating, configuring, and styling tables.

**Key Topics Covered:**

* **Basic table creation** - Adding columns with `AddColumn()` and rows with `AddRow()`, including how column order is determined
* **Column configuration** - Setting alignment (left, right, center), padding, width constraints, and whether columns can wrap
* **Border styles** - Choosing from numerous built-in border styles (ASCII, rounded, heavy, double, etc.) and creating custom borders

> [!NOTE]
> See the [Table Border Reference](/console/reference/table-border-reference) for a complete visual guide to all available border styles.
* **Header styling** - Customizing header appearance with colors, styles, and alignment separate from data rows
* **Cell content** - Using markup in cells, embedding other renderables (panels, text, etc.) within table cells
* **Table title and caption** - Adding decorative titles above and captions below the table
* **Expand/collapse behavior** - Controlling whether the table expands to fill console width
* **Performance considerations** - Handling large datasets efficiently
