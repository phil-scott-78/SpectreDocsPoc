using Spectre.Docs.Cli.Examples;

var demos = DemoRunner.DiscoverDemos();

// No arguments or list flag: show usage and available demos
if (args.Length == 0 || args[0] is "--list" or "-l")
{
    Console.WriteLine("Spectre.Console.Cli Demo Runner");
    Console.WriteLine("================================");
    Console.WriteLine();
    Console.WriteLine("Usage: dotnet run -- <demo-name> [demo-args...]");
    Console.WriteLine();
    DemoRunner.ListDemos(demos);
    return 0;
}

// Help flag
if (args[0] is "--help" or "-h")
{
    Console.WriteLine("Spectre.Console.Cli Demo Runner");
    Console.WriteLine();
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run -- <demo-name> [args...]   Run a specific demo");
    Console.WriteLine("  dotnet run -- --list, -l              List all available demos");
    Console.WriteLine("  dotnet run -- --help, -h              Show this help message");
    Console.WriteLine();
    Console.WriteLine("Examples:");
    Console.WriteLine("  dotnet run -- hello-world");
    Console.WriteLine("  dotnet run -- hello-world John");
    return 0;
}

// Run the specified demo
var demoName = args[0];
var demoArgs = args.Skip(1).ToArray();

if (!demos.TryGetValue(demoName, out var demoType))
{
    Console.WriteLine($"Error: Demo '{demoName}' not found.");
    Console.WriteLine();
    DemoRunner.ListDemos(demos);
    return 1;
}

try
{
    var demo = DemoRunner.CreateInstance(demoType);
    return await demo.RunAsync(demoArgs);
}
catch (Exception ex)
{
    Console.WriteLine($"Error running demo '{demoName}': {ex.Message}");
    return 1;
}
