---
title: "Grid Widget"
description: "Arrange content in rows and columns without visible borders for flexible layouts"
uid: "console-widget-grid"
order: 3250
---

The Grid widget provides a flexible layout system for arranging content in rows and columns without the visual weight of table borders. It's ideal for creating custom layouts, forms, and structured presentations where borders would be distracting.

**Key Topics Covered:**

* **Grid structure** - Understanding rows and columns in Grid vs. Table (no borders, different performance characteristics)
* **Adding columns** - Using `AddColumn()` with width specifications (fixed, proportional, auto)
* **Adding rows** - Populating grid rows with `AddRow()`, passing renderables or markup strings for each cell
* **Column sizing** - Controlling column widths with fixed sizes, ratios, or content-based auto-sizing
* **Alignment** - Setting per-column alignment and per-cell content positioning
* **Expansion behavior** - Configuring whether the grid expands to fill available width
* **Use cases** - When to choose Grid over Table, Columns, or custom layout compositions
