using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Docs.Examples.Showcase;

namespace Spectre.Docs.Examples;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class ShowcaseCommand(IAnsiConsole console) : Command<ShowcaseCommand.Settings>
{
    public class Settings(string sample) : CommandSettings
    {
        [CommandArgument(0, "[sample]")] public string Sample { get; } = sample;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var samples = typeof(BaseSample).Assembly
            .GetTypes()
            .Where(i => i is { IsClass: true, IsAbstract: false } && i.IsSubclassOf(typeof(BaseSample)))
            .Select(Activator.CreateInstance)
            .Cast<BaseSample>()
            .ToArray();

        var selectedSample = settings.Sample;
        if (string.IsNullOrWhiteSpace(selectedSample))
        {
            selectedSample = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an example to run")
                    .PageSize(25)
                    .AddChoices(samples.Select(x => x.Name())));
        }

        if (!string.IsNullOrWhiteSpace(selectedSample))
        {
            var desiredSample =
                samples.FirstOrDefault(i => i.Name().Equals(selectedSample, StringComparison.OrdinalIgnoreCase));
            if (desiredSample == null)
            {
                console.MarkupLine($"[red]Error:[/] could not find sample [blue]{selectedSample}[/]");
                return -1;
            }

            samples = [desiredSample];
        }

        foreach (var sample in samples)
        {
            try
            {
                sample.Run(console);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
        }

        return 0;
    }
}
