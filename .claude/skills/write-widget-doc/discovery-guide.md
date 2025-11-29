# Widget Discovery Guide

This guide teaches you how to systematically discover all capabilities of a Spectre.Console widget.

## Key Locations

| What | Where |
|------|-------|
| Widget source | `B:/spectre/spectre.console/src/Spectre.Console/Widgets/` |
| Extension methods | `B:/spectre/spectre.console/src/Spectre.Console/Extensions/` |
| Interface definitions | `B:/spectre/spectre.console/src/Spectre.Console/I*.cs` |

## Step 1: Find the Widget Source

Widgets are located at:
- `B:/spectre/spectre.console/src/Spectre.Console/Widgets/{Widget}.cs`
- Or in a subfolder: `B:/spectre/spectre.console/src/Spectre.Console/Widgets/{Widget}/{Widget}.cs`

## Step 2: Identify Implemented Interfaces

Look at the class declaration for implemented interfaces. Each interface indicates capabilities:

```csharp
// Example from Panel.cs
public sealed class Panel : Renderable, IHasBoxBorder, IHasBorder, IExpandable, IPaddable
```

## Step 3: Find Extension Methods for Each Interface

For each interface `I{Capability}`, search for extension methods:

| Interface | Extension File Pattern | Common Methods |
|-----------|----------------------|----------------|
| `IExpandable` | `ExpandableExtensions.cs` | `Expand()`, `Collapse()` |
| `IPaddable` | `PaddableExtensions.cs` | `Padding()`, `PadLeft()`, `PadRight()`, `PadTop()`, `PadBottom()` |
| `IHasBoxBorder` | `HasBoxBorderExtensions.cs` | `Border()`, `NoBorder()`, `SquareBorder()`, `RoundedBorder()`, `HeavyBorder()`, `DoubleBorder()`, `AsciiBorder()` |
| `IHasBorder` | `HasBorderExtensions.cs` | `BorderColor()`, `BorderStyle()`, `SafeBorder()`, `NoSafeBorder()` |
| `IHasTableBorder` | `HasTableBorderExtensions.cs` | All box borders plus `MinimalBorder()`, `MarkdownBorder()`, etc. |
| `IHasJustification` | `HasJustificationExtensions.cs` | `LeftJustified()`, `Centered()`, `RightJustified()` |
| `IOverflowable` | `OverflowableExtensions.cs` | `Overflow()` |
| `IAlignable` | `AlignableExtensions.cs` | Alignment property |
| `IHasTreeNodes` | `HasTreeNodeExtensions.cs` | `AddNode()` |
| `IHasCulture` | `HasCultureExtensions.cs` | `Culture()` |

## Step 4: Find Widget-Specific Extensions

Check for `{Widget}Extensions.cs` in the Extensions folder:

```
B:/spectre/spectre.console/src/Spectre.Console/Extensions/{Widget}Extensions.cs
```

These contain widget-specific fluent methods like:
- `Table`: `Title()`, `Caption()`, `ShowHeaders()`, `HideHeaders()`, `ShowRowSeparators()`
- `Panel`: `Header()`, `HeaderAlignment()`

## Step 5: Examine Public Properties

In the widget source, identify public properties that can be configured:

```csharp
// Look for patterns like:
public bool Expand { get; set; }
public Padding? Padding { get; set; }
public BoxBorder Border { get; set; }
```

## Step 6: Check Constructors

Note constructor parameters to understand instantiation patterns:

```csharp
// What's required vs optional?
public Panel(IRenderable content)  // content is required
public Table()                      // no required parameters
```

## Step 7: Look for Related Types

Search for supporting types in the same folder:
- `{Widget}Column.cs` (e.g., TableColumn)
- `{Widget}Row.cs` (e.g., TableRow)
- `{Widget}Title.cs` (e.g., TableTitle)

These often have their own extension methods.

## Step 8: Identify Related Widgets for Cross-Referencing

Determine which widgets serve similar or complementary purposes:

| Widget | Related Widgets | When to Cross-Reference |
|--------|-----------------|------------------------|
| BarChart | BreakdownChart | Part-to-whole vs comparison |
| BreakdownChart | BarChart | Comparison vs part-to-whole |
| Table | Grid | Simple layouts vs structured data |
| Panel | Table | Single content vs tabular data |
| Tree | Table | Hierarchical vs flat data |
| Columns | Grid, Table | Side-by-side layout alternatives |

In documentation, guide users to alternatives:
- "For **X scenario**, use [Widget](/console/widgets/widget) instead."

## Discovery Checklist

- [ ] Found widget source file
- [ ] Listed all implemented interfaces
- [ ] Found extension methods for each interface
- [ ] Checked for widget-specific extensions
- [ ] Listed public properties
- [ ] Noted constructor requirements
- [ ] Identified related/supporting types
- [ ] Identified related widgets for cross-references
