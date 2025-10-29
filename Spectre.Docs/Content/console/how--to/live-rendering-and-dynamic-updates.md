---
title: "Live Rendering and Dynamic Updates"
description: "How to use Spectre.Console's live rendering features to continuously update console output"
uid: "console-live-rendering"
order: 2300
---

When you need to show changing data without scrolling the screen—like updating a dashboard, refreshing task statuses, or displaying real-time metrics—use `AnsiConsole.Live` to render widgets in-place. Wrap any `IRenderable` (like a Table or Chart) in a live context, then update its content within a loop to show changes without creating new output lines.

Build dynamic displays like CPU usage monitors, live task lists, or ticking counters that refresh automatically. Keep updates on a single thread and avoid running multiple live renderers simultaneously to prevent conflicts. This creates professional, dashboard-like interfaces directly in the terminal without cluttering output with repeated redraws.