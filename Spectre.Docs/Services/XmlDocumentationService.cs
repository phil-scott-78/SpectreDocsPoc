using System.Reflection;
using System.Xml.Linq;

namespace Spectre.Docs.Services;

public class XmlDocumentationService
{
    private readonly Dictionary<string, XmlDocumentation> _documentation = new();
    private bool _isLoaded;

    public XmlDocumentationService()
    {
        LoadDocumentation();
    }

    public XmlDocumentation? GetDocumentation(string memberName)
    {
        return _documentation.TryGetValue(memberName, out var doc) ? doc : null;
    }

    public XmlDocumentation? GetDocumentation(Type type)
    {
        var memberName = $"T:{type.FullName}";
        return GetDocumentation(memberName);
    }

    public XmlDocumentation? GetDocumentation(PropertyInfo property)
    {
        var memberName = $"P:{property.DeclaringType?.FullName}.{property.Name}";
        return GetDocumentation(memberName);
    }

    public XmlDocumentation? GetDocumentation(MethodInfo method)
    {
        var memberName = BuildMethodMemberName(method);
        return GetDocumentation(memberName);
    }

    public XmlDocumentation? GetDocumentation(ConstructorInfo constructor)
    {
        var memberName = BuildConstructorMemberName(constructor);
        return GetDocumentation(memberName);
    }

    private void LoadDocumentation()
    {
        if (_isLoaded) return;

        try
        {
            var xmlPath = FindXmlDocumentationPath();
            if (xmlPath == null || !File.Exists(xmlPath))
            {
                // XML documentation file not found - service will return null for all lookups
                return;
            }

            ParseXmlDocumentation(xmlPath);
            _isLoaded = true;
        }
        catch (Exception)
        {
            // Error loading XML documentation - service will return null for all lookups
        }
    }

    private string? FindXmlDocumentationPath()
    {
        // Strategy 1: Check assembly location (works when XML is copied to output)
        var assembly = typeof(Spectre.Console.AnsiConsole).Assembly;
        var assemblyPath = assembly.Location;

        if (!string.IsNullOrEmpty(assemblyPath))
        {
            var xmlPath = Path.ChangeExtension(assemblyPath, ".xml");
            if (File.Exists(xmlPath))
            {
                return xmlPath;
            }
        }

        // Strategy 2: Search NuGet global packages folder
        var nugetPath = FindInNuGetCache();
        if (nugetPath != null && File.Exists(nugetPath))
        {
            return nugetPath;
        }

        return null;
    }

    private string? FindInNuGetCache()
    {
        // Get NuGet global packages folder (cross-platform)
        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var nugetFolder = Path.Combine(userProfile, ".nuget", "packages", "spectre.console");

        if (!Directory.Exists(nugetFolder))
        {
            return null;
        }

        // Find all version directories and sort by version (descending)
        var versionDirs = Directory.GetDirectories(nugetFolder)
            .Select(d => new { Path = d, Name = Path.GetFileName(d) })
            .OrderByDescending(d => d.Name)
            .ToList();

        // Search each version for the XML file
        foreach (var versionDir in versionDirs)
        {
            var libFolder = Path.Combine(versionDir.Path, "lib");
            if (!Directory.Exists(libFolder))
            {
                continue;
            }

            // Search all target framework folders (net9.0, net8.0, etc.)
            var tfmDirs = Directory.GetDirectories(libFolder)
                .OrderByDescending(d => Path.GetFileName(d)) // Prefer newer frameworks
                .ToList();

            foreach (var tfmDir in tfmDirs)
            {
                var xmlPath = Path.Combine(tfmDir, "Spectre.Console.xml");
                if (File.Exists(xmlPath))
                {
                    return xmlPath;
                }
            }
        }

        return null;
    }

    private void ParseXmlDocumentation(string xmlPath)
    {
        var xdoc = XDocument.Load(xmlPath);
        var members = xdoc.Descendants("member");

        foreach (var member in members)
        {
            var name = member.Attribute("name")?.Value;
            if (string.IsNullOrEmpty(name))
            {
                continue;
            }

            var summary = member.Element("summary")?.Value.Trim();
            var remarks = member.Element("remarks")?.Value.Trim();
            var returns = member.Element("returns")?.Value.Trim();
            var example = member.Element("example")?.Value.Trim();

            var parameters = member.Elements("param")
                .Select(p => new XmlParameter(
                    p.Attribute("name")?.Value ?? "",
                    p.Value.Trim()))
                .ToList();

            _documentation[name] = new XmlDocumentation(
                summary,
                remarks,
                returns,
                example,
                parameters);
        }
    }

    private string BuildMethodMemberName(MethodInfo method)
    {
        var declaringType = method.DeclaringType?.FullName;
        var parameters = method.GetParameters();

        if (parameters.Length == 0)
        {
            return $"M:{declaringType}.{method.Name}";
        }

        var paramTypes = string.Join(",", parameters.Select(p => GetParameterTypeName(p.ParameterType)));
        return $"M:{declaringType}.{method.Name}({paramTypes})";
    }

    private string BuildConstructorMemberName(ConstructorInfo constructor)
    {
        var declaringType = constructor.DeclaringType?.FullName;
        var parameters = constructor.GetParameters();

        if (parameters.Length == 0)
        {
            return $"M:{declaringType}.#ctor";
        }

        var paramTypes = string.Join(",", parameters.Select(p => GetParameterTypeName(p.ParameterType)));
        return $"M:{declaringType}.#ctor({paramTypes})";
    }

    private string GetParameterTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            var genericType = type.GetGenericTypeDefinition();
            var genericArgs = type.GetGenericArguments();
            var genericTypeName = genericType.FullName?.Split('`')[0];
            var genericArgNames = string.Join(",", genericArgs.Select(GetParameterTypeName));
            return $"{genericTypeName}{{{genericArgNames}}}";
        }

        return type.FullName ?? type.Name;
    }
}

public record XmlDocumentation(
    string? Summary,
    string? Remarks,
    string? Returns,
    string? Example,
    List<XmlParameter> Parameters);

public record XmlParameter(string Name, string Description);
