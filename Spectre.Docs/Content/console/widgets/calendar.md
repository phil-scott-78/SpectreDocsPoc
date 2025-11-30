---
title: "Calendar Widget"
description: "Display monthly calendars with highlighted dates and events"
uid: "console-widget-calendar"
order: 3700
---

The Calendar widget renders monthly calendars with customizable date highlighting and event tracking.

<Screenshot src="/assets/calendar.svg" />

## When to Use

Use Calendar when you need to **visualize dates and events in a month view**. Common scenarios:

- **Scheduling and planning**: Display project timelines, deadlines, or milestone dates
- **Event tracking**: Highlight important dates like releases, meetings, or reminders
- **Date selection interfaces**: Show available or unavailable dates in CLI tools
- **Multi-month comparisons**: Display several months side by side to show patterns

For **displaying dates inline within text**, use [Markup](/console/markup) with formatted date strings. For **progress over time**, use [BarChart](/console/widgets/bar-chart) to show metrics across time periods.

## Basic Usage

Create a calendar by specifying the year and month. Events are marked with an asterisk and highlighted in blue by default.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.BasicCalendarExample
```

You can also create a calendar from a `DateTime` object to show the current month.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarFromDateExample
```

## Highlighting Dates

### Adding Calendar Events

Use `AddCalendarEvent()` to mark specific dates as important. Each event appears with an asterisk indicator.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarEventsExample
```

You can pass `DateTime` objects instead of individual year, month, and day values.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarEventDateTimeExample
```

### Customizing Event Styles

Use `HighlightStyle()` to change the default appearance for all event dates.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarHighlightStyleExample
```

For more control, assign custom styles to individual events by passing a `Style` to `AddCalendarEvent()`. This is useful when different events have different priorities or categories.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarCustomEventStylesExample
```

## Customizing Appearance

### Header Styling

Use `HeaderStyle()` to customize the month and year display at the top of the calendar.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarHeaderStyleExample
```

Use `HideHeader()` when you need a more compact display or when the month is obvious from context.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarHideHeaderExample
```

### Border Styles

Customize the calendar border to match your application's visual style. Since Calendar uses a table internally, all table borders are supported.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarBorderExample
```

> [!NOTE]
> See the [Table Border Reference](/console/reference/table-border-reference) for all available border styles.

## Localization

### Culture Support

Set the `Culture` property to control the week start day, day names, and month formatting. This ensures calendars match your users' regional expectations.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.CalendarCultureExample
```

## Advanced Usage

### Multiple Calendars

Display several months together using the [Columns](/console/widgets/columns) widget. This is useful for showing quarterly views or comparing date ranges.

```csharp:xmldocid
M:Spectre.Docs.Examples.SpectreConsole.Reference.Widgets.CalendarExamples.MultipleCalendarsExample
```

## See Also

- [Table Border Reference](/console/reference/table-border-reference) - Border styles for calendar display
- [Color Reference](/console/reference/color-reference) - Colors for event highlighting
- [Columns Widget](/console/widgets/columns) - Display multiple calendars side by side
- [Getting Started Tutorial](/console/tutorials/getting-started-building-rich-console-app) - Learn Spectre.Console basics

## API Reference

<WidgetApiReference TypeName="Spectre.Console.Calendar" />
