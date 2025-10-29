using System.Text;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Docs.Examples.AsciiCast;
using Spectre.Docs.Examples.AsciiCast.Samples;
using static Spectre.Docs.Examples.AsciiCast.DelayHelper;

namespace Spectre.Docs.Examples;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class SampleCommand(IAnsiConsole console) : Command<SampleCommand.Settings>
{
    public class Settings(string outputPath, string sample, bool list) : CommandSettings
    {
        [CommandArgument(0, "[sample]")] public string Sample { get; } = sample;

        [CommandOption("-o|--output")] public string OutputPath { get; } = outputPath ?? Environment.CurrentDirectory;

        [CommandOption("-l|--list")] public bool List { get; } = list;

        [CommandOption("-s|--speed")] public int SpeedUp { get; } = 10;
    }

    private readonly AsciiCastConsole _console = new(console);

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var samples = typeof(BaseSample).Assembly
            .GetTypes()
            .Where(i => i is { IsClass: true, IsAbstract: false } && i.IsSubclassOf(typeof(BaseSample)))
            .Select(Activator.CreateInstance)
            .Cast<BaseSample>()
            .ToArray();

        var selectedSample = settings.Sample;
        if (settings.List)
        {
            selectedSample = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an example to record")
                    .PageSize(25)
                    .AddChoices(samples.Select(x => x.Name())));
        }

        if (!string.IsNullOrWhiteSpace(selectedSample))
        {
            var desiredSample =
                samples.FirstOrDefault(i => i.Name().Equals(selectedSample, StringComparison.OrdinalIgnoreCase));
            if (desiredSample == null)
            {
                _console.MarkupLine($"[red]Error:[/] could not find sample [blue]{selectedSample}[/]");
                return -1;
            }

            samples = [desiredSample];
        }

        // from here on out everything we write will be recorded.

        var recorder = _console.WrapWithAsciiCastRecorder();

        foreach (var sample in samples)
        {
            try
            {
                var sampleName = sample.Name();

                var originalWidth = _console.Profile.Width;
                var originalHeight = _console.Profile.Height;

                _console.Profile.Encoding = Encoding.UTF8;
                _console.Profile.Width = sample.ConsoleSize.Cols;
                _console.Profile.Height = sample.ConsoleSize.Rows;

                foreach (var (capabilityName, action) in sample.GetCapabilities())
                {
                    action(_console.Profile.Capabilities);
                    sample.Run(_console);
                    var title = string.IsNullOrWhiteSpace(capabilityName)
                        ? sampleName
                        : $"{sampleName} ({capabilityName})";

                    var json = recorder.GetCastJson(title, sample.ConsoleSize.Cols + 2,
                        sample.ConsoleSize.Rows);
                    File.WriteAllText(Path.Combine(settings.OutputPath, $"{title}.cast"), json);
                }

                _console.Profile.Width = originalWidth;
                _console.Profile.Height = originalHeight;
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
        }

        return 0;
    }
}