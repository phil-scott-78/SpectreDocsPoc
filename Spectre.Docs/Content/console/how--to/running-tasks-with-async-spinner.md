---
title: "Running Tasks with an Async Spinner"
description: "How to run asynchronous tasks with a spinner animation using Spectre.Console's async extensions"
uid: "console-async-spinner"
order: 2500
---

When you have a single async operation—like fetching data, running a computation, or waiting for a service—and need to show users that work is happening, use the `.Spinner()` extension on `Task` or `Task<T>`. This displays an animated spinner automatically while the task runs, requiring minimal code to provide visual feedback.

Customize the spinner's appearance by choosing from built-in animation styles or defining your own sequence, and apply colors to match your tool's theme. Keep in mind this is designed for standalone tasks—avoid using it alongside other interactive elements like prompts or live displays. This provides clean, automatic progress indication for simple async operations without the overhead of full progress tracking.

## See Also

- [Spinner Styles Reference](/console/reference/spinner-reference) - Visual guide to all available spinner animations