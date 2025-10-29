// See https://aka.ms/new-console-template for more information

using System.Text;
using Spectre.Console.Cli;
using Spectre.Docs.Examples;

Console.OutputEncoding = Encoding.UTF8;

var app = new CommandApp();
app.SetDefaultCommand<SampleCommand>();
await app.RunAsync(args);
