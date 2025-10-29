---
title: "Showing Progress Bars and Spinners"
description: "How to track and display progress of long-running tasks in the console"
uid: "console-progress-bars"
order: 2100
---

When you have long-running operations—like processing files, downloading data, or running multiple tasks—and need to keep users informed of progress, use `AnsiConsole.Progress` to display progress bars with percentage completion and status messages. Track multiple concurrent tasks with individual progress bars, updating them as work completes.

For single operations where you can't determine percentage completion, use the `Status` widget with an animated spinner and descriptive message. Configure spinner styles, refresh rates, and completion behavior through the fluent API. Avoid running multiple live-updating elements simultaneously to prevent display conflicts. This gives users clear, real-time visibility into what your tool is doing and how long they might wait.

## See Also

- [Spinner Styles Reference](/console/reference/spinner-reference) - Visual guide to all available spinner animations