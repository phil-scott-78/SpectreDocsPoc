---
title: "Hiding Commands and Options"
description: "How to hide commands and options from help output while keeping them functional"
uid: "cli-hidden-commands"
order: 2045
---

Sometimes you need commands or options that work but shouldn't appear in help output—internal debugging tools, deprecated features you're phasing out, or advanced options that would overwhelm typical users. Spectre.Console.Cli supports this through the `IsHidden` property.

For commands, use `.IsHidden()` when configuring your command in the `CommandApp`. For options, set `IsHidden = true` on the `[CommandOption]` attribute. Hidden commands and options remain fully functional—users who know about them can still use them—but they won't appear in `--help` output or command listings. This is useful for maintaining backward compatibility while discouraging use, or for providing power-user features without cluttering the interface.
