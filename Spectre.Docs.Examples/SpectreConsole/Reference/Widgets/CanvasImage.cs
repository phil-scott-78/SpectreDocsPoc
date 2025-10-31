using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.Reference.Widgets;

internal static class CanvasImageExamples
{
    /// <summary>
    /// Demonstrates loading and displaying a basic image from a file.
    /// </summary>
    public static void BasicCanvasImageExample()
    {
        var image = new CanvasImage("path/to/image.png");
        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates loading an image from a byte array.
    /// </summary>
    public static void CanvasImageFromBytesExample()
    {
        byte[] imageData = File.ReadAllBytes("path/to/image.png");
        var image = new CanvasImage(imageData);
        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates loading an image from a stream.
    /// </summary>
    public static void CanvasImageFromStreamExample()
    {
        using var stream = File.OpenRead("path/to/image.png");
        var image = new CanvasImage(stream);
        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates setting a maximum width to constrain image size.
    /// </summary>
    public static void CanvasImageMaxWidthExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(80);

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates removing width constraints with NoMaxWidth.
    /// </summary>
    public static void CanvasImageNoMaxWidthExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(80)
            .NoMaxWidth(); // Remove constraint

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates adjusting pixel width to control character-to-pixel ratio.
    /// </summary>
    public static void CanvasImagePixelWidthExample()
    {
        AnsiConsole.MarkupLine("[yellow]Pixel width 1 (narrow):[/]");
        var narrow = new CanvasImage("path/to/image.png")
            .PixelWidth(1);
        AnsiConsole.Write(narrow);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Pixel width 2 (default):[/]");
        var normal = new CanvasImage("path/to/image.png")
            .PixelWidth(2);
        AnsiConsole.Write(normal);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Pixel width 4 (wide):[/]");
        var wide = new CanvasImage("path/to/image.png")
            .PixelWidth(4);
        AnsiConsole.Write(wide);
    }

    /// <summary>
    /// Demonstrates using bicubic resampling for high-quality image scaling.
    /// </summary>
    public static void CanvasImageBicubicResamplerExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(60)
            .BicubicResampler();

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates using bilinear resampling for balanced quality and performance.
    /// </summary>
    public static void CanvasImageBilinearResamplerExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(60)
            .BilinearResampler();

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates using nearest neighbor resampling for fast, pixelated scaling.
    /// </summary>
    public static void CanvasImageNearestNeighborResamplerExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(60)
            .NearestNeighborResampler();

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates comparing different resampling methods side by side.
    /// </summary>
    public static void CanvasImageResamplerComparisonExample()
    {
        AnsiConsole.MarkupLine("[yellow]Bicubic (highest quality):[/]");
        var bicubic = new CanvasImage("path/to/image.png")
            .MaxWidth(40)
            .BicubicResampler();
        AnsiConsole.Write(bicubic);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Bilinear (balanced):[/]");
        var bilinear = new CanvasImage("path/to/image.png")
            .MaxWidth(40)
            .BilinearResampler();
        AnsiConsole.Write(bilinear);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Nearest neighbor (fastest):[/]");
        var nearestNeighbor = new CanvasImage("path/to/image.png")
            .MaxWidth(40)
            .NearestNeighborResampler();
        AnsiConsole.Write(nearestNeighbor);
    }

    /// <summary>
    /// Demonstrates applying ImageSharp mutations to modify the image before rendering.
    /// </summary>
    public static void CanvasImageMutateExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(60)
            .Mutate(ctx => ctx.Rotate(90));

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates combining multiple ImageSharp mutations for advanced image processing.
    /// </summary>
    public static void CanvasImageAdvancedMutateExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(80)
            .Mutate(ctx => ctx
                .Rotate(45)
                .Flip(FlipMode.Horizontal));

        AnsiConsole.Write(image);
    }

    /// <summary>
    /// Demonstrates combining sizing, resampling, and mutation for complete image control.
    /// </summary>
    public static void CanvasImageCompleteExample()
    {
        var image = new CanvasImage("path/to/image.png")
            .MaxWidth(80)
            .PixelWidth(2)
            .BicubicResampler()
            .Mutate(ctx => ctx.Crop(new Rectangle(10, 10, 200, 200)));

        AnsiConsole.Write(image);
    }
}
