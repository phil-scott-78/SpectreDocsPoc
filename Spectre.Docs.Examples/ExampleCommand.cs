using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Docs.Examples;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class ExampleCommand(IAnsiConsole console) : AsyncCommand<ExampleCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[xmldocid]")]
        [Description("XmlDocId of the method to execute (e.g., M:Namespace.Class.Method)")]
        public string? XmlDocId { get; init; }
    }

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var xmlDocs = LoadXmlDocumentation();
        var methods = DiscoverExampleMethods();

        if (methods.Count == 0)
        {
            console.MarkupLine("[red]No examples found.[/]");
            return -1;
        }

        MethodInfo selectedMethod;

        if (!string.IsNullOrWhiteSpace(settings.XmlDocId))
        {
            selectedMethod = methods.FirstOrDefault(m => GetXmlDocId(m) == settings.XmlDocId)
                ?? throw new InvalidOperationException($"Could not find method with XmlDocId: {settings.XmlDocId}");
        }
        else
        {
            selectedMethod = ShowSelectionPrompt(methods, xmlDocs);
        }

        return await ExecuteMethodAsync(selectedMethod);
    }

    private List<MethodInfo> DiscoverExampleMethods()
    {
        var assembly = typeof(ExampleCommand).Assembly;

        return assembly.GetTypes()
            .Where(t => t.Namespace?.Contains("SpectreConsole") == true)
            .Where(t => t is { IsClass: true, IsAbstract: true, IsSealed: true }) // static class check
            .Where(t => !IsCommandType(t))
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
            .Where(m => m.DeclaringType == m.ReflectedType)
            .Where(m => !m.IsSpecialName)
            .Where(m => m.GetParameters().Length == 0)
            .ToList();
    }

    private static bool IsCommandType(Type type)
    {
        var current = type;
        while (current != null)
        {
            if (current.Name.StartsWith("Command") || current.Name == "CommandSettings")
                return true;
            if (current.BaseType?.FullName?.Contains("Spectre.Console.Cli") == true)
                return true;
            current = current.BaseType;
        }
        return false;
    }

    private MethodInfo ShowSelectionPrompt(List<MethodInfo> methods, Dictionary<string, string?> xmlDocs)
    {
        var grouped = methods
            .GroupBy(m => GetCleanClassName(m.DeclaringType!))
            .OrderBy(g => g.Key)
            .ToList();

        var prompt = new SelectionPrompt<MethodInfo>()
            .Title("Select an [green]example[/] to run")
            .PageSize(20)
            .EnableSearch()
            .UseConverter(m => FormatMethodChoice(m, xmlDocs));

        foreach (var group in grouped)
        {
            prompt.AddChoiceGroup(
                group.First(), // Use first method as group header marker
                group.OrderBy(m => m.Name).ToArray());
        }

        return console.Prompt(prompt);
    }

    private static string FormatMethodChoice(MethodInfo method, Dictionary<string, string?> xmlDocs)
    {
        var methodName = method.Name;
        var xmlDocId = GetXmlDocId(method);
        var summary = xmlDocs.GetValueOrDefault(xmlDocId);
        var shortSummary = GetFirstChunk(summary);

        return string.IsNullOrEmpty(shortSummary)
            ? methodName
            : $"{methodName} [dim]{Markup.Escape(shortSummary)}[/]";
    }

    private static string GetCleanClassName(Type type)
    {
        var name = type.Name;
        if (name.EndsWith("Examples"))
            return name[..^8];
        if (name.EndsWith("Example"))
            return name[..^7];
        return name;
    }

    private static string GetXmlDocId(MethodInfo method)
    {
        return $"M:{method.DeclaringType?.FullName}.{method.Name}";
    }

    private static string? GetFirstChunk(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;

        var cleaned = text.Trim().Replace("\n", " ").Replace("\r", "");
        // Collapse multiple spaces
        while (cleaned.Contains("  "))
        {
            cleaned = cleaned.Replace("  ", " ");
        }

        var dotIndex = cleaned.IndexOf('.');
        if (dotIndex is > 0 and < 80)
        {
            return cleaned[..(dotIndex + 1)];
        }
        if (cleaned.Length > 60)
        {
            return cleaned[..57] + "...";
        }
        return cleaned;
    }

    private static Dictionary<string, string?> LoadXmlDocumentation()
    {
        var result = new Dictionary<string, string?>();

        var assembly = typeof(ExampleCommand).Assembly;
        var xmlPath = Path.ChangeExtension(assembly.Location, ".xml");

        if (!File.Exists(xmlPath)) return result;

        try
        {
            var xdoc = XDocument.Load(xmlPath);
            foreach (var member in xdoc.Descendants("member"))
            {
                var name = member.Attribute("name")?.Value;
                var summary = member.Element("summary")?.Value?.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    result[name] = summary;
                }
            }
        }
        catch
        {
            // Ignore XML parsing errors
        }

        return result;
    }

    private async Task<int> ExecuteMethodAsync(MethodInfo method)
    {
        console.MarkupLine($"\n[green]Running:[/] {GetCleanClassName(method.DeclaringType!)}.{method.Name}");
        console.WriteLine();

        try
        {
            var result = method.Invoke(null, null);

            // Handle async methods
            if (result is Task task)
            {
                await task;
            }

            return 0;
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            console.WriteException(ex.InnerException);
            return -1;
        }
        catch (Exception ex)
        {
            console.WriteException(ex);
            return -1;
        }
    }
}