using Spectre.Console;
using Spectre.Console.Rendering;

namespace Spectre.Docs.Examples.Showcase;

internal class CanvasSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        IRenderable[] outputs =
        [
            new Rows(
                CreateSinglePixelCanvas(),
                new Text("Single pixel").LeftJustified()),
            new Rows(
                CreateDiagonalLineCanvas(),
                new Text("Diagonal line").LeftJustified()),
            new Rows(
                CreateCrossPatternCanvas(),
                new Text("Cross pattern").LeftJustified()),
            new Rows(
                CreateBorderCanvas(),
                new Text("Border").LeftJustified()),
            new Rows(
                CreateColoredBordersCanvas(),
                new Text("Colored borders").LeftJustified()),
            new Rows(
                CreateGridPatternCanvas(),
                new Text("Grid pattern").LeftJustified()),
            new Rows(
                CreateCheckerboardCanvas(),
                new Text("Checkerboard").LeftJustified())
        ];

        // Animate
        console.Live(new Text("")).Start(context =>
        {
            foreach (var output in outputs)
            {
                context.UpdateTarget(output);
                context.Refresh();
                Thread.Sleep(2000);
            }
        });
    }

    private static Canvas CreateSinglePixelCanvas()
    {
        var canvas = new Canvas(10, 10);
        canvas.SetPixel(5, 5, Color.Red);
        return canvas;
    }

    private static Canvas CreateDiagonalLineCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var i = 0; i < canvas.Width; i++)
            canvas.SetPixel(i, i, Color.Blue);
        return canvas;
    }

    private static Canvas CreateCrossPatternCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var i = 0; i < canvas.Width; i++)
        {
            canvas.SetPixel(i, i, Color.Yellow);
            canvas.SetPixel(canvas.Width - i - 1, i, Color.Yellow);
        }
        return canvas;
    }

    private static Canvas CreateBorderCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var i = 0; i < canvas.Width; i++)
        {
            canvas.SetPixel(i, 0, Color.Red);
            canvas.SetPixel(i, canvas.Height - 1, Color.Red);
        }
        for (var i = 0; i < canvas.Height; i++)
        {
            canvas.SetPixel(0, i, Color.Red);
            canvas.SetPixel(canvas.Width - 1, i, Color.Red);
        }
        return canvas;
    }

    private static Canvas CreateColoredBordersCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var i = 0; i < 10; i++)
        {
            canvas.SetPixel(i, 0, Color.Red);
            canvas.SetPixel(0, i, Color.Green);
            canvas.SetPixel(i, 9, Color.Blue);
            canvas.SetPixel(9, i, Color.Yellow);
        }
        return canvas;
    }

    private static Canvas CreateGridPatternCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var x = 0; x < 10; x += 2)
            for (var y = 0; y < 10; y++)
                canvas.SetPixel(x, y, Color.Green);
        for (var y = 0; y < 10; y += 2)
            for (var x = 0; x < 10; x++)
                canvas.SetPixel(x, y, Color.Blue);
        return canvas;
    }

    private static Canvas CreateCheckerboardCanvas()
    {
        var canvas = new Canvas(10, 10);
        for (var x = 0; x < 10; x++)
            for (var y = 0; y < 10; y++)
                if ((x + y) % 2 == 0)
                    canvas.SetPixel(x, y, Color.Grey);
        return canvas;
    }
}
