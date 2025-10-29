---
title: "Prompting for User Input"
description: "How to interactively prompt the user for input using Spectre.Console"
uid: "console-user-input"
order: 2150
---

When your tool needs user input—like collecting configuration values, confirming destructive actions, or selecting from options—use the interactive prompts. Call `AnsiConsole.Ask<T>` to collect typed input (strings, numbers, etc.) with validation and optional defaults. Use `Confirm` for yes/no questions that require explicit user acknowledgment.

For choices, present a `SelectionPrompt` with arrow-key navigation when users should pick one option, or `MultiSelectionPrompt` with checkboxes when they can select multiple items. Configure `TextPrompt` with validation rules or masking for sensitive inputs like passwords. These prompts create polished, intuitive interactions that guide users through decisions and data entry without requiring custom input handling.