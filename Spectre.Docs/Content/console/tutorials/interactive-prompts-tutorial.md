---
title: "Asking User Questions"
description: "Learn to ask the user simple questions and use their answers"
uid: "console-interactive-prompts"
order: 1040
---

In this tutorial, we'll build a pizza ordering system that collects user input. By the end, you'll know how to ask for text, let users choose from lists, and confirm their selections.

## What We're Building

Here's what our pizza order flow will look like:

<Screenshot Src="/assets/interactive-prompt-tutorial.svg" Alt="Interactive Prompts Tutorial" />

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- Completion of the [Getting Started](/console/tutorials/getting-started-building-rich-console-app) tutorial

<Steps>
<Step stepNumber="1">
**Ask for the Customer's Name**

Let's start by asking for the customer's name. The `Ask<string>()` method displays a prompt and waits for input:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.InteractivePromptsTutorial.AskCustomerName
```

Run the code:

```bash
dotnet run
```

You should see "What's your name?" with a cursor waiting for input. Type your name and press Enter. The program then greets you by name.

Notice how we used `[green]` markup in the prompt? You can style your prompts just like any other Spectre.Console output.

You've captured your first user input.

</Step>
<Step stepNumber="2">
**Choose a Pizza Size**

Now let's let the user pick from a list of options. A `SelectionPrompt` shows an interactive menu where users navigate with arrow keys:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.InteractivePromptsTutorial.ChoosePizzaSize
```

Run it:

```bash
dotnet run
```

You should see a list of pizza sizes with one highlighted. Use the up/down arrow keys to move between options, then press Enter to select.

Notice how the prompt handles all the keyboard interaction for you? No need to parse input or validate choices - Spectre.Console takes care of it.

That's an interactive menu with just a few lines of code.

</Step>
<Step stepNumber="3">
**Select Your Toppings**

What if the user wants to pick multiple items? That's where `MultiSelectionPrompt` comes in. Users can toggle items with the spacebar and confirm with Enter:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.InteractivePromptsTutorial.SelectToppings
```

Run it:

```bash
dotnet run
```

You should see a list of toppings with checkboxes. Press Space to select or deselect items, use arrow keys to navigate, and press Enter when you're done.

Notice the `NotRequired()` call? That allows the user to select zero items (for a plain cheese pizza). Without it, at least one selection would be required.

Your users can now make multiple selections.

</Step>
<Step stepNumber="4">
**Confirm the Order**

Before placing the order, let's ask for confirmation. The `Confirm()` method presents a simple yes/no question:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.InteractivePromptsTutorial.ConfirmOrder
```

Run it:

```bash
dotnet run
```

You should see "Place this order? [y/n]" with options to type `y` or `n`. The method returns `true` for yes and `false` for no.

Notice how the `[y/n]` hint is automatically added? Spectre.Console handles the common UX patterns for you.

Now you can get confirmation before important actions.

</Step>
<Step stepNumber="5">
**Complete Pizza Order**

Let's put it all together into a complete ordering flow with a styled order summary:

```csharp:xmldocid,bodyonly
M:Spectre.Docs.Examples.SpectreConsole.Tutorials.InteractivePromptsTutorial.Run(Spectre.Console.IAnsiConsole)
```

Run the complete application:

```bash
dotnet run
```

You should see the full ordering experience: enter your name, pick a size, select toppings, review the summary in a styled panel, and confirm your order.

Notice how we used a `Panel` to display the order summary? Combining prompts with other Spectre.Console widgets creates polished, professional interfaces.

That's a complete interactive ordering flow.

</Step>
</Steps>

## Congratulations!

We've built a pizza ordering system that demonstrates all the core prompting features. Our application asks for text input, presents single-choice and multiple-choice menus, displays a styled summary, and confirms the order before processing.

These same techniques work for any interactive console application: configuration wizards, CLI tools, installation scripts, and more.

## Next Steps

- [Showing Status and Spinners](/console/tutorials/status-spinners-tutorial) - Display animated feedback while operations run
- [Text Prompts Reference](/console/prompts/text-prompt) - Explore validation, secrets, and default values
- [Selection Prompts Reference](/console/prompts/selection-prompt) - Learn about grouping, search, and styling options
