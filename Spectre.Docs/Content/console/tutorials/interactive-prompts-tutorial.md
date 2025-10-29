---
title: "Asking User Questions"
description: "Learn to ask the user simple questions and use their answers"
uid: "console-interactive-prompts"
order: 1040
---

## Overview

In this tutorial, we'll learn to ask the user questions and collect their answers. We'll ask for text input, numbers, yes/no confirmations, and let them choose from a list.

## What We're Building

A simple program that:
- Asks the user for their name
- Asks how old they are (with number validation)
- Confirms if they want to continue
- Lets them pick their favorite color from a menu

## Topics to be Covered

A complete version of this tutorial would include:

<Steps>
<Step stepNumber="1">
**Asking for Text**

- Using `AnsiConsole.Ask()` to get a name
- Displaying the answer back to the user

</Step>
<Step stepNumber="2">
**Asking for Numbers**

- Using `AnsiConsole.Ask<int>()` for age
- Watching automatic validation work when user types letters

</Step>
<Step stepNumber="3">
**Getting Yes/No Confirmation**

- Using `AnsiConsole.Confirm()` to ask if they want to continue
- Making decisions based on their answer

</Step>
<Step stepNumber="4">
**Choosing from a List**

- Creating a simple selection menu with `SelectionPrompt<string>`
- Letting users navigate with arrow keys
- Using the selected value

</Step>
</Steps>

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- Completion of the [Getting Started tutorial](/console/tutorials/getting-started-building-rich-console-app)

## Next Steps

After completing this tutorial:
- [Showing Status and Spinners](/console/tutorials/status-spinners-tutorial) - Show progress while operations run
