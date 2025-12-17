---
title: "Understanding Spectre.Console's Rendering Model"
description: "An in-depth explanation of how Spectre.Console renders text and widgets to the terminal"
uid: "console-rendering-model"
order: 6050
---

When your application starts, Spectre.Console automatically figures out what your terminal can do. This determines whether you get vibrant colors, smooth Unicode box-drawing, or simpler fallbacks. Most of the time this just works, but understanding how it works helps when things don't look right or when you need specific behavior.

<Screenshot src="/assets/capabilities.svg" alt="Demonstration of capabilities being detected by env variables" />

## How Detection Works

Spectre.Console examines your environment to determine terminal capabilities. It checks environment variables like `TERM`, `COLORTERM`, and `NO_COLOR`, inspects the console encoding, and detects whether you're running in a CI environment.

The detected capabilities are stored in `AnsiConsole.Profile.Capabilities` and influence every rendering decision:

- **ColorSystem** determines whether colors get downgraded or displayed in full fidelity
- **Unicode** controls whether box-drawing uses `─` and `│` or falls back to `-` and `|`
- **Interactive** determines whether prompts wait for input or fail gracefully
- **Ansi** controls whether escape sequences are emitted at all

CI environments get special handling. When Spectre.Console detects it's running in a build pipeline, it automatically disables interactive mode so prompts don't hang waiting for input that will never come.

## The IRenderable Interface

Every widget in Spectre.Console implements `IRenderable`, the core abstraction that makes the rendering system work. This interface defines two methods:

```csharp
public interface IRenderable
{
    Measurement Measure(RenderOptions options, int maxWidth);
    IEnumerable<Segment> Render(RenderOptions options, int maxWidth);
}
```

The design separates **measuring** from **rendering**:

- **Measure** returns minimum and maximum width constraints without producing any output. This lets containers like `Panel` or `Table` calculate how much space their children need before deciding on final dimensions.

- **Render** produces the actual output as a sequence of `Segment` objects. It receives the final width constraint and must respect it.

This two-phase approach enables intelligent auto-sizing. A `Table` can measure all its columns, determine optimal widths, then render each cell knowing exactly how much space it has.

## Segments: The Atomic Rendering Unit

When a widget renders, it doesn't write directly to the console. Instead, it produces `Segment` objects—atomic units of styled text:

```csharp
public class Segment
{
    public string Text { get; }           // The actual text content
    public Style Style { get; }           // Colors and decorations
    public bool IsLineBreak { get; }      // Explicit line break
    public bool IsControlCode { get; }    // ANSI escape sequence
}
```

Segments are the universal currency of the rendering system. A `Panel` produces segments for its border. A `Table` produces segments for its grid lines and cell content. Even simple text becomes segments with associated styles.

The `Segment` class includes a `CellCount()` method that returns the actual console width the text occupies. This handles wide characters correctly—a single emoji might occupy two console cells even though it's one character.

## Console Width and Auto-Sizing

Spectre.Console automatically adapts to your terminal's width. When you resize your terminal window and re-run your app, tables and panels adjust accordingly.

The width resolution follows this chain:

1. Check `Profile.Width` for an explicit override
2. Fall back to the output stream's width
3. Query `Console.BufferWidth` from the system
4. Default to 80 columns if nothing else works

Every `Render` call receives a `maxWidth` parameter. Widgets must produce output that fits within this constraint. When a `Panel` renders:

1. It measures its child content to find preferred dimensions
2. It adds the border width (typically 2 characters for left and right)
3. It renders the child with the remaining inner width
4. It wraps everything in border segments

This cascading measurement ensures that nested widgets all respect their available space.

## The Rendering Pipeline

Here's what happens when you call `AnsiConsole.Write()`:

```
AnsiConsole.Write(renderable)
       ↓
Acquire render lock (thread safety)
       ↓
Create RenderOptions from profile
       ↓
Call renderable.Render(options, width)
       ↓
Collect Segment[] output
       ↓
Convert segments to ANSI escape codes
       ↓
Write to TextWriter and flush
```

The render lock ensures thread safety. If multiple threads try to write simultaneously, they queue up rather than interleaving their output into garbage.

`RenderOptions` carries the rendering context—capabilities, console size, and style preferences. Widgets use this to make capability-aware decisions, like choosing ASCII borders when Unicode isn't available.

The final step converts segments into actual ANSI escape codes. A segment with red foreground and bold style becomes `\x1b[31;1mtext\x1b[0m`. On legacy consoles without ANSI support, the escape codes are omitted and you get plain text.

## Live Rendering

Progress bars, spinners, and status displays need to update in place without scrolling the terminal. Spectre.Console achieves this through **render hooks**—a mechanism that intercepts the normal rendering pipeline.

When you start a live display:

1. The cursor is hidden (so you don't see it jumping around)
2. A render hook attaches to the pipeline
3. A background thread triggers refreshes (default: every 100ms)
4. Each refresh re-renders the content at the same screen position

The render hook intercepts all console output during the live session. It injects cursor-positioning commands before the live content, ensuring updates overwrite the previous frame rather than appending below it.

```csharp
await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        var task = ctx.AddTask("Processing");
        while (!task.IsFinished)
        {
            task.Increment(1);
            await Task.Delay(50);
        }
    });
```

Behind the scenes, the progress display re-renders itself 10 times per second, each time repositioning the cursor to overwrite the previous state.

## Flicker Prevention

Naive terminal updates cause visible flicker—clear the screen, redraw everything, repeat. Spectre.Console uses several techniques to avoid this:

**Cursor repositioning instead of clearing**: Rather than erasing the screen and redrawing from scratch, Spectre.Console moves the cursor back to where the live content started and overwrites in place. This is much faster and produces no visible flash.

```
\r                    ← Return to line start
\x1b[3A               ← Move cursor up 3 lines
[render new content]  ← Overwrites old content
```

**Shape tracking**: The system remembers the dimensions of the previous render. If the new content is smaller, it pads with spaces to "erase" leftover characters from the old frame. If content grows, it handles the overflow gracefully.

**Atomic writes**: The render lock ensures that a complete frame is written before another thread can interfere. You never see partial updates.

**Synchronous flush**: After writing, the output is immediately flushed to the terminal. This ensures the entire frame appears at once rather than trickling out character by character.

There's no double-buffering or off-screen composition. Cursor movement through ANSI escape codes is fast enough that the simpler approach works well.

## Unicode and Encoding

Spectre.Console detects Unicode support by checking your output encoding. When Unicode is available, you get nice box-drawing characters for tables and panels. Without it, you get ASCII fallbacks like `+`, `-`, and `|`.

If you're seeing garbled characters instead of clean borders, your terminal's encoding might not match. On Windows, you can fix this a few ways:

**For the current session (Command Prompt):**
```bash
chcp 65001
```

**Permanently in PowerShell** (add to your `$PROFILE`):
```powershell
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()
```

**Programmatically in your app:**
```csharp
// If the console is still using the Windows default encoding, switch to UTF-8
if (Console.OutputEncoding.CodePage == 437) // DOS/OEM encoding
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
}
```

## Manual Configuration

Sometimes you need to override what Spectre.Console detected. You can do this when creating a console:

```csharp
var console = AnsiConsole.Create(new AnsiConsoleSettings
{
    Ansi = AnsiSupport.Yes,                      // Force ANSI on
    ColorSystem = ColorSystemSupport.TrueColor,  // Force 24-bit color
    Interactive = InteractionSupport.No,         // Disable prompts
});
```

Or tweak the default console directly:

```csharp
// Disable Unicode if your terminal mangles box characters
AnsiConsole.Profile.Capabilities.Unicode = false;

// Check what was detected
if (AnsiConsole.Profile.Capabilities.ColorSystem == ColorSystem.TrueColor)
{
    // We have full color support
}
```

Common reasons to override:
- Detection fails for an unusual or custom terminal
- You want consistent output across different machines
- Testing how your app looks at different capability levels

## See Also

- <xref:console-capabilities-reference> - Complete list of capabilities, environment variables, and CI detection
- <xref:console-howto-creating-custom-renderables> - Build custom widgets
- <xref:console-live-live-display> - Real-time rendering in practice
