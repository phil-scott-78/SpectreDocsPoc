---
title: "Async Patterns"
description: "Best practices for using live rendering with asynchronous operations"
uid: "console-explanation-async-patterns"
order: 6200
---

This guide covers patterns and best practices for combining Spectre.Console's live rendering features (Progress, Status, Live Display) with asynchronous programming in .NET, enabling responsive console applications that handle multiple concurrent operations effectively.

> [!CAUTION]
> Live rendering features (Progress, Status, Live Display) and prompts are not thread safe. Using them together with other interactive components is not supported.

**Key Topics Covered:**

* **Async contexts** - Using `StartAsync()` methods for progress, status, and live displays with async operations
* **Progress with async** - Tracking progress of async tasks like HTTP downloads, database operations, etc.
* **Status with async** - Showing spinners during async API calls, file operations, and network requests
* **Parallel operations** - Tracking multiple concurrent async tasks with individual progress indicators
* **Task coordination** - Using `Task.WhenAll()`, `Task.WhenAny()` with progress tracking
* **Cancellation tokens** - Integrating CancellationToken support with live displays
* **Error handling** - Managing exceptions in async operations while maintaining display integrity
* **Long-running background tasks** - Patterns for monitoring background work with live updates

Examples show downloading multiple files concurrently with progress bars, calling multiple APIs in parallel with status displays, processing items asynchronously with progress tracking, building async data pipelines with visual feedback, creating responsive CLI tools that handle user interrupts, and monitoring background workers. The guide provides proven patterns for common async scenarios in console applications and discusses pitfalls to avoid when combining async code with live rendering.

## See Also

- <xref:console-live-progress> - Track async task progress
- <xref:console-live-status> - Show spinners during async work
- <xref:console-live-live-display> - Real-time content updates
- <xref:console-howto-showing-progress-bars> - Practical examples
- [Best Practices](/console/explanation/best-practices-for-console-applications) - Production guidelines
