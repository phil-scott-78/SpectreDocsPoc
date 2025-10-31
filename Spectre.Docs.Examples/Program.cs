using System.Text;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Docs.Examples;

Console.OutputEncoding = Encoding.UTF8;

// Without the explicit clear, we'll sometimes get an extra line to start the recording.
// I can't sort out if this is something the shell is doing, or an extra line dotnet run might be
// throwing in there. Either way, this seems to allow consistent output...for now.
AnsiConsole.Clear();

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<ShowcaseCommand>("showcase");
});
app.SetDefaultCommand<ExampleCommand>();
await app.RunAsync(args);
