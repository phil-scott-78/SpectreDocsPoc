---
title: "Customizing Help Text and Usage"
description: "How to tailor the automatically generated help output of Spectre.Console.Cli"
uid: "cli-help-customization"
order: 2040
---

When your CLI's default help output doesn't match your needs—whether for branding, accessibility, or specific formatting requirements—you can customize it. Start by setting your application name and top-level examples so `--help` shows meaningful context. Then adjust the styling through `config.Settings.HelpProviderStyles` to change colors or remove styling entirely for plain text output.

If you need to hide internal or advanced commands from users, use `.IsHidden()` on commands or `IsHidden=true` on options. For complete control over help formatting, implement a custom `IHelpProvider` and register it with `config.SetHelpProvider(...)`. This gives you full control over how help information is structured and displayed to your users.