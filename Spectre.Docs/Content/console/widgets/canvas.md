---
title: "Canvas Widget"
description: "Draw pixel-level graphics and patterns using Braille characters"
uid: "console-widget-canvas"
order: 3850
---

The Canvas widget enables pixel-level drawing in the console using Unicode Braille characters, where each character represents an 8-pixel (2×4) grid. This allows for surprisingly detailed graphics, patterns, and simple visualizations in text mode.

**Key Topics Covered:**

* **Canvas basics** - Understanding the Braille character grid and how pixels map to console characters
* **Drawing pixels** - Setting individual pixels with `SetPixel(x, y, color)` to create patterns and shapes
* **Canvas dimensions** - Working with canvas width and height, understanding resolution limitations
* **Colors** - Applying colors to pixels for creating colorful graphics
* **Drawing primitives** - Creating lines, rectangles, circles, and other shapes (if available)
* **Performance considerations** - Handling large canvases and many pixel operations efficiently
* **Use cases** - Creating sparklines, simple graphs, QR codes, ASCII art, and decorative patterns
