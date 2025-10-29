---
title: "Using Canvas for Pixel Art and Images"
description: "How to draw graphics in the console using the Canvas and CanvasImage widgets"
uid: "console-canvas-pixel-art"
order: 2550
---

When you need to add simple graphics—like logos, diagrams, or visual indicators—directly in the terminal, use the `Canvas` widget to plot colored "pixels" and draw basic shapes. Create a canvas with specific dimensions and set individual pixels to build simple images like icons or low-resolution logos.

For converting actual images to console output, use `CanvasImage` to render pictures as ASCII representations with appropriate color approximation. Configure color fidelity based on terminal capabilities (from 3-bit to 24-bit colors) and adjust scaling or dithering to improve how images appear in the console. This lets you include visual branding or graphical elements alongside text, enhancing your tool's visual identity.