using Spectre.Console;

namespace Spectre.Docs.Examples.AsciiCast;

public static class AsciiCastExtensions
{
    public static AsciiCastOut WrapWithAsciiCastRecorder(this IAnsiConsole ansiConsole)
    {
        AsciiCastOut castRecorder = new(ansiConsole.Profile.Out);
        ansiConsole.Profile.Out = castRecorder;

        return castRecorder;
    }
}