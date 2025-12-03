using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.HandlingErrorsAndExitCodes;

/// <summary>
/// Demonstrates error handling with SetExceptionHandler.
/// </summary>
public class Demo
{
    public static int RunAsync(string[] args)
    {
        var app = new CommandApp<ProcessCommand>();

        app.Configure(config =>
        {
            config.SetExceptionHandler((ex, resolver) =>
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths);

                // Return specific exit codes based on exception type
                return ex switch
                {
                    InvalidOperationException => 2,
                    FileNotFoundException => 3,
                    _ => 1
                };
            });
        });

        return app.Run(args);
    }
}

/// <summary>
/// Demonstrates error handling with PropagateExceptions.
/// </summary>
public class PropagateExceptionsDemo
{
    public static int Run(string[] args)
    {
        var app = new CommandApp<ProcessCommand>();
        app.Configure(config => config.PropagateExceptions());

        try
        {
            return app.Run(args);
        }
        catch (FileNotFoundException ex)
        {
            AnsiConsole.MarkupLine($"[red]File not found:[/] {ex.FileName}");
            return 3;
        }
        catch (InvalidOperationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Invalid operation:[/] {ex.Message}");
            return 2;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return 1;
        }
    }
}

/// <summary>
/// A command that processes files and may throw exceptions.
/// </summary>
internal class ProcessCommand : Command<ProcessCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        [Description("The file path to process")]
        public string Path { get; init; } = string.Empty;
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        if (!File.Exists(settings.Path))
        {
            throw new FileNotFoundException("The specified file does not exist.", settings.Path);
        }

        // Process the file...
        System.Console.WriteLine($"Processing {settings.Path}");
        return 0;
    }
}
