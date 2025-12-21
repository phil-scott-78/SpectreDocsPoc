using System.ComponentModel;
using System.Globalization;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.CustomTypeConverters;

/// <summary>
/// Demonstrates how to use custom type converters for complex types.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<DrawCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// Represents a 2D point with X and Y coordinates.
/// </summary>
public readonly record struct Point(int X, int Y)
{
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>
/// Custom TypeConverter that converts strings like "10,20" to Point.
/// </summary>
public sealed class PointConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str)
        {
            var parts = str.Split(',');
            if (parts.Length == 2 &&
                int.TryParse(parts[0].Trim(), out var x) &&
                int.TryParse(parts[1].Trim(), out var y))
            {
                return new Point(x, y);
            }

            throw new FormatException($"Invalid point format: '{str}'. Expected format: X,Y (e.g., 10,20)");
        }

        return base.ConvertFrom(context, culture, value);
    }
}

/// <summary>
/// A drawing command demonstrating custom type converter usage.
/// </summary>
internal class DrawCommand : Command<DrawCommand.Settings>
{
    /// <summary>
    /// Settings demonstrating custom type converters.
    /// </summary>
    public class Settings : CommandSettings
    {
        // Custom type with TypeConverter attribute
        [CommandOption("--point <POINT>")]
        [Description("A point in X,Y format (e.g., 10,20)")]
        [TypeConverter(typeof(PointConverter))]
        public required Point Location { get; init; }

        // Optional point with nullable
        [CommandOption("--offset [OFFSET]")]
        [Description("Optional offset point")]
        [TypeConverter(typeof(PointConverter))]
        public required FlagValue<Point> Offset { get; init; }

        [CommandOption("-c|--color")]
        [Description("The drawing color")]
        [DefaultValue("black")]
        public string Color { get; init; } = "black";
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        System.Console.WriteLine($"Drawing at location: {settings.Location}");
        System.Console.WriteLine($"Color: {settings.Color}");

        if (settings.Offset.IsSet)
        {
            System.Console.WriteLine($"With offset: {settings.Offset.Value}");
            var adjusted = new Point(
                settings.Location.X + settings.Offset.Value.X,
                settings.Location.Y + settings.Offset.Value.Y);
            System.Console.WriteLine($"Adjusted location: {adjusted}");
        }

        return 0;
    }
}
