using System.Reflection;
using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

internal class CanvasImageSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Spectre.Docs.Examples.spectre-logo.png")!;
        using var ms = new MemoryStream();
        stream.CopyTo(ms);

        var image = new CanvasImage(ms.ToArray());
        image.MaxWidth(22);

        var panel = new Panel(image).Padding(8,1,8,1)
            .Header("[yellow]Canvas Image[/]")
            .BorderColor(Color.Blue);

        console.Write(panel);
    }
}
