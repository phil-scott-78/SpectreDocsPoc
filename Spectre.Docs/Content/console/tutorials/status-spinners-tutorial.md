---
title: "Showing Status and Spinners"
description: "Display animated spinners while operations are running"
uid: "console-status-spinners"
order: 1050
---

## Overview

In this tutorial, we'll learn to show the user that work is happening. We'll display an animated spinner with a status message while a task runs.

## What We're Building

A simple program that:
- Shows a spinner while "processing" happens
- Updates the status message during the work
- Shows a success message when done

## Topics to be Covered

A complete version of this tutorial would include:

<Steps>
<Step stepNumber="1">
**Your First Status Spinner**

- Using `AnsiConsole.Status()` to show work is happening
- Running a task inside the status context
- Seeing the spinner animate automatically

</Step>
<Step stepNumber="2">
**Updating Status Text**

- Changing the status message as work progresses
- Using `ctx.Status()` to update the text
- Showing different stages of work

</Step>
<Step stepNumber="3">
**Trying Different Spinners**

- Changing the spinner style (dots, lines, etc.)
- Picking a spinner that matches your app's style
- See the [Spinner Reference](/console/reference/spinner-reference) for all available styles

</Step>
</Steps>

## Prerequisites

- .NET 6.0 or later
- Basic C# knowledge
- Completion of the [Getting Started tutorial](/console/tutorials/getting-started-building-rich-console-app)

## Next Steps

After completing this tutorial:
- [Multiple Progress Bars](/console/how--to/showing-progress-bars-and-spinners) - Track several operations at once
