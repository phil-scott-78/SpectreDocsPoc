---
title: "Testing Console Output with Spectre.Console.Testing"
description: "How to write unit tests for console applications built with Spectre.Console"
uid: "console-testing-output"
order: 2650
---

When you need to verify that your console application produces correct output and handles interactions properly, use `TestConsole` from Spectre.Console.Testing to capture output and simulate user input in unit tests. Structure your code to accept `IAnsiConsole` as a dependency instead of using the static `AnsiConsole` directly, enabling you to inject a test instance.

Write tests that verify expected output strings—checking that tables, messages, or styles appear correctly. For interactive prompts, queue input responses with `TestConsole.Input` so tests can simulate user choices. If using Spectre.Console.Cli, combine with `CommandAppTester` to validate both command parsing and output. This lets you catch regressions, verify edge cases, and ensure your console UI works correctly across code changes.