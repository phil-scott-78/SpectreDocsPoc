using System.Reflection;

namespace Spectre.Docs.Cli.Examples;

/// <summary>
/// Runs demo methods by XmlDocId.
/// </summary>
public static class DemoRunner
{
    /// <summary>
    /// Runs a demo method identified by its XmlDocId.
    /// </summary>
    /// <param name="xmlDocId">The XmlDocId in format M:Namespace.Type.Method(System.String[])</param>
    /// <param name="args">Arguments to pass to the method</param>
    /// <returns>The exit code from the demo method</returns>
    public static async Task<int> RunAsync(string xmlDocId, string[] args)
    {
        // Parse XmlDocId: M:Namespace.Type.Method(params)
        if (!xmlDocId.StartsWith("M:"))
        {
            throw new ArgumentException($"XmlDocId must start with 'M:'. Got: {xmlDocId}");
        }

        var withoutPrefix = xmlDocId[2..]; // Remove "M:"
        var parenIndex = withoutPrefix.IndexOf('(');
        if (parenIndex < 0)
        {
            throw new ArgumentException($"XmlDocId must contain parameters. Got: {xmlDocId}");
        }

        var qualifiedName = withoutPrefix[..parenIndex];
        var lastDot = qualifiedName.LastIndexOf('.');
        if (lastDot < 0)
        {
            throw new ArgumentException($"XmlDocId must contain a type and method name. Got: {xmlDocId}");
        }

        var typeName = qualifiedName[..lastDot];
        var methodName = qualifiedName[(lastDot + 1)..];

        // Find the type in the assembly
        var type = typeof(DemoRunner).Assembly.GetType(typeName)
            ?? throw new InvalidOperationException($"Type not found: {typeName}");

        // Find the static method
        var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(string[])])
            ?? throw new InvalidOperationException($"Static method not found: {methodName}(string[]) on {typeName}");

        // Invoke and return result
        var result = method.Invoke(null, [args]);

        return result switch
        {
            Task<int> taskInt => await taskInt,
            Task task => throw new InvalidOperationException($"Method {methodName} returns Task but must return Task<int>"),
            int exitCode => exitCode,
            _ => throw new InvalidOperationException($"Method {methodName} must return Task<int> or int")
        };
    }
}
