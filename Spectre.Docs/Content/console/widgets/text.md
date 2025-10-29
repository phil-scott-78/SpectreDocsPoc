---
title: "Text Widget"
description: "Render styled text with precise control over formatting and overflow"
uid: "console-widget-text"
order: 3000
---

The Text widget provides a renderable wrapper for text content with explicit control over styling, justification, and overflow behavior. While markup strings are often sufficient, the Text widget offers programmatic styling control and advanced text handling features.

**Key Topics Covered:**

* **Creating Text widgets** - Using `new Text(content)` vs. markup strings
* **Styling** - Applying colors, bold, italic, underline, and other styles programmatically with `Style` objects

> [!NOTE]
> See the [Color Reference](/console/reference/color-reference) for all available colors and the [Text Style Reference](/console/reference/text-style-reference) for decoration options.
* **Justification** - Left, center, right, or full justification of text content
* **Overflow behavior** - Controlling how text behaves when it exceeds available width (truncate, wrap, ellipsis)
* **Line breaks** - Handling multi-line text and explicit line breaks
* **When to use Text** - Choosing between Text widgets, markup strings, and simple string output
* **Composition** - Using Text as content for other widgets like panels and tables
