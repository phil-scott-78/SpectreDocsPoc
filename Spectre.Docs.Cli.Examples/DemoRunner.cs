using System.Reflection;
using System.Text;

namespace Spectre.Docs.Cli.Examples;

/// <summary>
/// Discovers and runs demo applications using reflection.
/// </summary>
public static class DemoRunner
{
    /// <summary>
    /// Discovers all IDemoApp implementations in the DemoApps namespace.
    /// </summary>
    public static Dictionary<string, Type> DiscoverDemos()
    {
        return typeof(DemoRunner).Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Where(t => typeof(IDemoApp).IsAssignableFrom(t))
            .Where(t => t.Namespace?.Contains("DemoApps") == true)
            .ToDictionary(
                GetDemoKey,
                t => t,
                StringComparer.OrdinalIgnoreCase
            );
    }

    /// <summary>
    /// Extracts the demo key from the type's namespace.
    /// DemoApps/BasicCommand/Main.cs -> "basic-command"
    /// </summary>
    private static string GetDemoKey(Type type)
    {
        var ns = type.Namespace ?? string.Empty;
        var parts = ns.Split('.');
        var folder = parts.LastOrDefault() ?? type.Name;
        return ToKebabCase(folder);
    }

    /// <summary>
    /// Converts PascalCase to kebab-case.
    /// </summary>
    private static string ToKebabCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var result = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c) && i > 0)
            {
                result.Append('-');
            }
            result.Append(char.ToLowerInvariant(c));
        }
        return result.ToString();
    }

    /// <summary>
    /// Creates an instance of the demo.
    /// </summary>
    public static IDemoApp CreateInstance(Type demoType)
        => (IDemoApp)Activator.CreateInstance(demoType)!;

    /// <summary>
    /// Lists all available demos with descriptions.
    /// </summary>
    public static void ListDemos(Dictionary<string, Type> demos)
    {
        System.Console.WriteLine("Available demos:");
        System.Console.WriteLine();

        foreach (var (key, type) in demos.OrderBy(d => d.Key))
        {
            var demo = CreateInstance(type);
            System.Console.WriteLine($"  {key,-30} {demo.Description}");
        }
    }
}
