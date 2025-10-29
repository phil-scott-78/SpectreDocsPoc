---
title: "Testing Command-Line Applications"
description: "How to test CLI apps built with Spectre.Console.Cli to ensure they parse and execute correctly"
uid: "cli-testing"
order: 2090
---

When you need to verify that your CLI correctly parses arguments and produces expected output, use `CommandAppTester` from Spectre.Console.Testing to run commands in-memory. Set up your test by configuring the tester with your commands and DI registrations, then call `app.Run(args)` to capture the result.

Assert on both the exit code and output string to verify behavior—for example, that a greeting command with `--name Bob` returns exit code 0 and outputs "Hello Bob". For interactive commands with prompts, use `TestConsole` to queue input responses. Make your commands testable by injecting `IAnsiConsole` instead of using the static `AnsiConsole` directly, allowing you to capture all output in tests and catch regressions early.