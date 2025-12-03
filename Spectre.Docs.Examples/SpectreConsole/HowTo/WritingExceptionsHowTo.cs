using Spectre.Console;

namespace Spectre.Docs.Examples.SpectreConsole.HowTo;

internal static class WritingExceptionsHowTo
{
    /// <summary>
    /// Write an exception to the console with default formatting.
    /// </summary>
    public static void WriteBasicException()
    {
        try
        {
            ProcessUserData(null!);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }

    /// <summary>
    /// Shorten file paths for cleaner stack traces.
    /// </summary>
    public static void ShortenFilePaths()
    {
        try
        {
            ProcessUserData(null!);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths);
        }
    }

    /// <summary>
    /// Shorten everything and add clickable links to source files.
    /// </summary>
    public static void ShortenEverythingWithLinks()
    {
        try
        {
            ProcessUserData(null!);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex,
                ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks);
        }
    }

    /// <summary>
    /// Customize exception colors using ExceptionSettings.
    /// </summary>
    public static void CustomizeColors()
    {
        try
        {
            ProcessUserData(null!);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, new ExceptionSettings
            {
                Format = ExceptionFormats.ShortenEverything,
                Style = new ExceptionStyle
                {
                    Exception = new Style(Color.Grey),
                    Message = new Style(Color.White),
                    Method = new Style(Color.Red),
                    Path = new Style(Color.Yellow),
                    LineNumber = new Style(Color.Blue),
                }
            });
        }
    }

    // Helper to generate a realistic nested exception
    private static void ProcessUserData(string data)
    {
        try
        {
            ValidateInput(data);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to process user data", ex);
        }
    }

    private static void ValidateInput(string data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Input cannot be null");
        }
    }
}
