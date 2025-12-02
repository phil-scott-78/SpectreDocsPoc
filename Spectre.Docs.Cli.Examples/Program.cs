using Spectre.Docs.Cli.Examples;

var xmlDocId = Environment.GetEnvironmentVariable("SPECTRE_APP");

if (string.IsNullOrEmpty(xmlDocId))
{
    if (args.Length > 0 && args[0].StartsWith("M:"))
    {
        xmlDocId = args[0];
        args = args.Skip(1).ToArray();
    }
    else
    {
        Console.WriteLine("Usage: Set SPECTRE_APP environment variable to a method XmlDocId");
        Console.WriteLine("Example: M:Spectre.Docs.Cli.Examples.DemoApps.QuickStart.FirstCommand.Demo.RunAsync(System.String[])");
        return 1;
    }
}

return await DemoRunner.RunAsync(xmlDocId, args);
