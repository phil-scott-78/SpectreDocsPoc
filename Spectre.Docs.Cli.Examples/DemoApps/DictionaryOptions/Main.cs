using System.ComponentModel;
using Spectre.Console.Cli;

namespace Spectre.Docs.Cli.Examples.DemoApps.DictionaryOptions;

/// <summary>
/// Demonstrates how to use dictionary and lookup options for key-value pairs.
/// </summary>
public class Demo
{
    public static async Task<int> RunAsync(string[] args)
    {
        var app = new CommandApp<ConfigCommand>();
        return await app.RunAsync(args);
    }
}

/// <summary>
/// A configuration command demonstrating dictionary option patterns.
/// </summary>
internal class ConfigCommand : Command<ConfigCommand.Settings>
{
    /// <summary>
    /// Settings demonstrating IDictionary, ILookup, and IReadOnlyDictionary options.
    /// </summary>
    public class Settings : CommandSettings
    {
        // IDictionary<string, int> - key=value pairs with typed values
        // Usage: --value port=8080 --value timeout=30
        [CommandOption("--value <VALUE>")]
        [Description("Configuration values in key=value format (e.g., port=8080)")]
        public IDictionary<string, int>? Values { get; set; }

        // ILookup<string, string> - allows multiple values per key
        // Usage: --lookup env=dev --lookup env=test --lookup region=us
        [CommandOption("--lookup <VALUE>")]
        [Description("Lookup values allowing multiple entries per key")]
        public ILookup<string, string>? Lookups { get; set; }

        // IReadOnlyDictionary<string, string> - immutable key-value pairs
        // Usage: --readonly name=myapp --readonly version=1.0
        [CommandOption("--readonly <VALUE>")]
        [Description("Read-only configuration values")]
        public IReadOnlyDictionary<string, string>? ReadOnlyValues { get; set; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken cancellation)
    {
        // Display IDictionary values
        if (settings.Values?.Count > 0)
        {
            System.Console.WriteLine("Values (IDictionary<string, int>):");
            foreach (var kvp in settings.Values)
            {
                System.Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            }
        }

        // Display ILookup values (note: can have multiple values per key)
        if (settings.Lookups != null)
        {
            System.Console.WriteLine("Lookups (ILookup<string, string>):");
            foreach (var group in settings.Lookups)
            {
                var values = string.Join(", ", group);
                System.Console.WriteLine($"  {group.Key} = [{values}]");
            }
        }

        // Display IReadOnlyDictionary values
        if (settings.ReadOnlyValues?.Count > 0)
        {
            System.Console.WriteLine("ReadOnly Values (IReadOnlyDictionary<string, string>):");
            foreach (var kvp in settings.ReadOnlyValues)
            {
                System.Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            }
        }

        return 0;
    }
}
