---
title: "TextPath Widget"
description: "Display file paths with intelligent truncation and styling"
uid: "console-widget-text-path"
order: 3800
---

The TextPath widget displays file paths with smart truncation that preserves important parts (typically the beginning and end) while fitting within available console width. It's designed specifically for showing file system paths in a readable way even when space is limited.

**Key Topics Covered:**

* **Path display** - Creating TextPath widgets from file path strings
* **Intelligent truncation** - How TextPath decides which parts of the path to show/hide when space is limited
* **Stem and leaf** - Understanding how root and filename are preserved during truncation
* **Styling** - Applying colors to different path components (root, separator, filename, etc.)
* **Root display** - Options for showing or abbreviating root directories
* **Separator handling** - Cross-platform path separator rendering
* **Use cases** - File operation output, build logs, search results, directory listings

