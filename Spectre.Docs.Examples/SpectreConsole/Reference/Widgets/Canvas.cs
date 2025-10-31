using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class CanvasExamples
{
    /// <summary>
    /// Demonstrates creating a basic canvas with individual pixels.
    /// </summary>
    public static void BasicCanvasExample()
    {
        var canvas = new Canvas(16, 8);

        // Draw some colored pixels
        for (var x = 0; x < 16; x++)
        {
            canvas.SetPixel(x, 0, Color.Blue);
            canvas.SetPixel(x, 7, Color.Blue);
        }

        for (var y = 1; y < 7; y++)
        {
            canvas.SetPixel(0, y, Color.Blue);
            canvas.SetPixel(15, y, Color.Blue);
        }

        // Fill the center
        for (var x = 1; x < 15; x++)
        {
            for (var y = 1; y < 7; y++)
            {
                canvas.SetPixel(x, y, Color.Green);
            }
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates drawing a simple pattern with multiple colors.
    /// </summary>
    public static void CanvasPatternExample()
    {
        var canvas = new Canvas(20, 10);

        // Create a checkerboard pattern
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 20; x++)
            {
                var color = (x + y) % 2 == 0 ? Color.Red : Color.Yellow;
                canvas.SetPixel(x, y, color);
            }
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates creating a gradient effect across the canvas.
    /// </summary>
    public static void CanvasGradientExample()
    {
        var canvas = new Canvas(30, 10);

        // Create a horizontal color gradient
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 30; x++)
            {
                var intensity = (byte)(x * 255 / 29);
                var color = new Color(intensity, 0, (byte)(255 - intensity));
                canvas.SetPixel(x, y, color);
            }
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates controlling canvas width and scaling behavior.
    /// </summary>
    public static void CanvasScalingExample()
    {
        var canvas = new Canvas(40, 10);

        // Fill with a simple pattern
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 40; x++)
            {
                canvas.SetPixel(x, y, x < 20 ? Color.Blue : Color.Green);
            }
        }

        AnsiConsole.MarkupLine("[yellow]Original size:[/]");
        AnsiConsole.Write(canvas);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]With MaxWidth = 20:[/]");
        canvas.MaxWidth = 20;
        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates adjusting pixel width for different visual effects.
    /// </summary>
    public static void CanvasPixelWidthExample()
    {
        var canvas = new Canvas(10, 5);

        // Fill with alternating colors
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                canvas.SetPixel(x, y, y % 2 == 0 ? Color.Purple : Color.Orange1);
            }
        }

        AnsiConsole.MarkupLine("[yellow]PixelWidth = 1:[/]");
        canvas.PixelWidth = 1;
        AnsiConsole.Write(canvas);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]PixelWidth = 2 (default):[/]");
        canvas.PixelWidth = 2;
        AnsiConsole.Write(canvas);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]PixelWidth = 4:[/]");
        canvas.PixelWidth = 4;
        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates disabling automatic scaling for pixel-perfect rendering.
    /// </summary>
    public static void CanvasNoScalingExample()
    {
        var canvas = new Canvas(30, 8);

        // Create a pattern
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 30; x++)
            {
                if (x % 3 == 0)
                {
                    canvas.SetPixel(x, y, Color.Cyan1);
                }
            }
        }

        AnsiConsole.MarkupLine("[yellow]With scaling (default):[/]");
        canvas.Scale = true;
        canvas.MaxWidth = 15;
        AnsiConsole.Write(canvas);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Without scaling:[/]");
        canvas.Scale = false;
        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates creating a simple bar chart visualization using canvas pixels.
    /// </summary>
    public static void CanvasBarVisualizationExample()
    {
        var canvas = new Canvas(25, 15);

        // Draw bars with different heights
        int[] heights = { 5, 10, 7, 13, 9 };
        var colors = new[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Purple };

        for (var i = 0; i < heights.Length; i++)
        {
            var barX = i * 5;
            var barHeight = heights[i];

            // Draw the bar from bottom up
            for (var y = 15 - barHeight; y < 15; y++)
            {
                for (var x = barX; x < barX + 4 && x < 25; x++)
                {
                    canvas.SetPixel(x, y, colors[i]);
                }
            }
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates drawing a diagonal line pattern across the canvas.
    /// </summary>
    public static void CanvasDiagonalLineExample()
    {
        var canvas = new Canvas(20, 20);

        // Draw diagonal lines
        for (var i = 0; i < 20; i++)
        {
            // Main diagonal
            canvas.SetPixel(i, i, Color.Red);

            // Anti-diagonal
            canvas.SetPixel(i, 19 - i, Color.Blue);
        }

        // Draw a horizontal line through the middle
        for (var x = 0; x < 20; x++)
        {
            canvas.SetPixel(x, 10, Color.Green);
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Demonstrates creating a complex pattern with multiple drawing operations.
    /// </summary>
    public static void CanvasComplexPatternExample()
    {
        var canvas = new Canvas(40, 20);

        // Fill background
        for (var y = 0; y < 20; y++)
        {
            for (var x = 0; x < 40; x++)
            {
                canvas.SetPixel(x, y, Color.Grey11);
            }
        }

        // Draw concentric rectangles
        DrawRectangle(canvas, 5, 5, 30, 10, Color.Red);
        DrawRectangle(canvas, 10, 7, 20, 6, Color.Yellow);
        DrawRectangle(canvas, 15, 9, 10, 2, Color.Green);

        AnsiConsole.Write(canvas);
    }

    private static void DrawRectangle(Canvas canvas, int x, int y, int width, int height, Color color)
    {
        // Top and bottom edges
        for (var i = 0; i < width && x + i < canvas.Width; i++)
        {
            if (y >= 0 && y < canvas.Height)
                canvas.SetPixel(x + i, y, color);
            if (y + height - 1 >= 0 && y + height - 1 < canvas.Height)
                canvas.SetPixel(x + i, y + height - 1, color);
        }

        // Left and right edges
        for (var i = 0; i < height && y + i < canvas.Height; i++)
        {
            if (x >= 0 && x < canvas.Width)
                canvas.SetPixel(x, y + i, color);
            if (x + width - 1 >= 0 && x + width - 1 < canvas.Width)
                canvas.SetPixel(x + width - 1, y + i, color);
        }
    }
}
