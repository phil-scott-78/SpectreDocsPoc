---
title: "Tree Guide Reference"
description: "A complete reference of all tree guide styles available in Spectre.Console"
uid: "console-tree-guide-reference"
order: 7150
---

Spectre.Console provides various tree guide styles for rendering hierarchical data structures.
Each guide style defines the visual characters used for branches, continuations, and spacing.

## Usage Example

```csharp
var tree = new Tree("Root");
tree.Guide(TreeGuide.Line);

var node1 = tree.AddNode("Child 1");
node1.AddNode("Grandchild 1.1");
node1.AddNode("Grandchild 1.2");

tree.AddNode("Child 2");

AnsiConsole.Write(tree);
```

## Important Notes

- **Unicode Support:** Some tree guides use Unicode box-drawing characters. Ensure your terminal supports Unicode.
- **Four Parts:** Each guide defines Space, Continue, Fork, and End characters for building tree structures.
- **Custom Guides:** You can create custom tree guides by inheriting from `TreeGuide` and implementing `GetPart()`.

## Available Tree Guides

<TreeGuideList />

## See Also

- <xref:console-widget-tree> - Display hierarchical data
- <xref:console-howto-displaying-hierarchical-data> - Tree usage guide
