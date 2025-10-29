---
title: "Configuring CommandApp and Commands"
description: "How to register commands with the CommandApp and configure global settings"
uid: "cli-app-configuration"
order: 2030
---

When you're building a CLI application with multiple commands, you need to register each command with the `CommandApp` and configure how they appear to users. Use `CommandApp.Configure(...)` to add commands via `config.AddCommand<T>("name")`, then customize each command with:

* **Aliases** using `.WithAlias("alias")` to provide alternate names (e.g., both "remove" and "rm")
* **Descriptions** via `.WithDescription("text")` to clarify what each command does in help output
* **Examples** with `.WithExample(new[] {...})` to show users concrete usage patterns

You can also configure global settings through `config.Settings`, such as enabling exception propagation in development or validating examples. This allows you to build a professional CLI with clear, discoverable commands that match your users' expectations.