using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class UsingCanvasHowTo
{
    /// <summary>
    /// Create a canvas and set pixels.
    /// </summary>
    public static void DrawPixels()
    {
        var canvas = new Canvas(16, 8);

        for (int x = 0; x < 16; x++)
        {
            canvas.SetPixel(x, 0, Color.Green);
            canvas.SetPixel(x, 7, Color.Green);
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Draw a simple pattern.
    /// </summary>
    public static void DrawPattern()
    {
        var canvas = new Canvas(8, 8);

        for (int i = 0; i < 8; i++)
        {
            canvas.SetPixel(i, i, Color.Red);
            canvas.SetPixel(7 - i, i, Color.Blue);
        }

        AnsiConsole.Write(canvas);
    }

    /// <summary>
    /// Display an image file.
    /// </summary>
    public static void ShowImage()
    {
        var image = new CanvasImage("logo.png");
        image.MaxWidth(32);

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Control image scaling quality.
    /// </summary>
    public static void ControlImageQuality()
    {
        var image = new CanvasImage("photo.png")
            .MaxWidth(48)
            .BilinearResampler();

        AnsiConsole.Write(image);
    }
}
