namespace Spectre.Docs.Examples.AsciiCast;

/// <summary>
/// This was well meaning to speed up the generation, but the internal delays and sleeps of the Live renderables
/// can't handle it. Just leave it at 1 for now.
/// </summary>
public static class DelayHelper
{
    private const int SpeedUp = 1;

    public static void Sleep(int milliseconds)
    {
        var actualDelay = milliseconds / SpeedUp;
        if (actualDelay > 0)
        {
            Thread.Sleep(actualDelay);
        }
    }

    public static async Task Delay(int milliseconds)
    {
        var actualDelay = milliseconds / SpeedUp;
        if (actualDelay > 0)
        {
            await Task.Delay(actualDelay);
        }
    }
}