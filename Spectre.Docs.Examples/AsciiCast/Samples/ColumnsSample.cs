using Spectre.Console;

namespace Spectre.Docs.Examples.AsciiCast.Samples;

public class ColumnsSample : BaseSample
{
    public override void Run(IAnsiConsole console)
    {
        var cards = Fruit
            .LoadFruits()
            .Select(GetContent)
            .ToList();

        // Animate
        console.Live(new Text(""))
            .AutoClear(true)
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Top)
            .Start(ctx =>
            {
                for (var i = 1; i < cards.Count; i++)
                {
                    var toShow = cards.Take(i);
                    ctx.UpdateTarget(new Columns(toShow));
                    //ctx.Refresh();
                    DelayHelper.Sleep(200);
                }
            });

        // Render all cards in columns
        AnsiConsole.Write(new Columns(cards));
    }

    private static string GetContent(Fruit fruit)
    {
        return $"[b][yellow]{fruit.Name}[/][/]";
    }

    private sealed class Fruit
    {
        public required string Name { get; init; }

        public static List<Fruit> LoadFruits()
        {
            return new []
            {
                "Apple",
                "Apricot",
                "Avocado",
                "Banana",
                "Blackberry",
                "Blueberry",
                "Boysenberry",
                "Breadfruit",
                "Cacao",
                "Cherry",
                "Cloudberry",
                "Coconut",
                "Dragonfruit",
                "Elderberry",
                "Grape",
                "Grapefruit",
                "Jackfruit",
                "Kiwifruit",
                "Lemon",
                "Lime",
                "Mango",
                "Melon",
                "Orange",
                "Blood orange",
                "Clementine",
                "Mandarine",
                "Tangerine",
                "Papaya",
                "Passionfruit",
                "Plum",
                "Pineapple",
                "Pomelo",
                "Raspberry",
                "Salmonberry",
                "Strawberry",
                "Ximenia",
                "Yuzu",
            }
                .Select(x => new Fruit{Name = x})
                .ToList();
        }
    }
}