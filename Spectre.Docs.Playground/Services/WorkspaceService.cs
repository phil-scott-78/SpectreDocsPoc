using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;

namespace Spectre.Docs.Playground.Services;

/// <summary>
/// Shared service that manages the Roslyn workspace and assembly references.
/// Used by both CompilationService and CompletionService.
/// </summary>
public class WorkspaceService
{
    private readonly HttpClient _httpClient;
    private readonly List<MetadataReference> _references = [];
    private bool _referencesLoaded;
    private readonly SemaphoreSlim _loadLock = new(1, 1);
    private AdhocWorkspace? _workspace;

    // Core assemblies needed for compilation and completion
    private static readonly string[] RequiredAssemblies =
    [
        "System.Private.CoreLib",
        "System.Runtime",
        "System.Console",
        "System.Collections",
        "System.Linq",
        "System.Threading",
        "System.Text.RegularExpressions",
        "System.ComponentModel.Primitives",
        "System.ComponentModel",
        "System.ObjectModel",
        "System.Runtime.InteropServices",
        "netstandard",
        "Spectre.Console"
    ];

    // Spectre.Console packages that have XML documentation available
    // This mirrors the _packages list in XmlDocumentationService
    private static readonly Dictionary<string, string> SpectrePackageXmlFiles = new()
    {
        ["Spectre.Console"] = "Spectre.Console.xml",
        ["Spectre.Console.Json"] = "Spectre.Console.Json.xml",
        ["Spectre.Console.ImageSharp"] = "Spectre.Console.ImageSharp.xml",
        ["Spectre.Console.Cli"] = "Spectre.Console.Cli.xml"
    };

    // Global usings included as a separate document in the compilation
    public const string GlobalUsings =
        """
        global using System;
        global using System.Collections.Generic;
        global using System.Linq;
        global using System.Threading;
        global using System.Threading.Tasks;
        global using Spectre.Console;
        """;

    public WorkspaceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the loaded metadata references.
    /// </summary>
    public IReadOnlyList<MetadataReference> References => _references;

    /// <summary>
    /// Ensures the workspace and references are loaded.
    /// </summary>
    public async Task EnsureInitializedAsync()
    {
        if (_referencesLoaded)
            return;

        await _loadLock.WaitAsync();
        try
        {
            if (_referencesLoaded)
                return;

            // Load assembly references
            foreach (var assemblyName in RequiredAssemblies)
            {
                try
                {
                    var bytes = await TryLoadAssemblyBytesAsync(assemblyName);
                    if (bytes != null)
                    {
                        // Try to load XML documentation for this assembly
                        var xmlDocProvider = await TryLoadXmlDocumentationAsync(assemblyName);

                        // Create reference with documentation provider if available
                        var reference = MetadataReference.CreateFromImage(
                            bytes,
                            documentation: xmlDocProvider);
                        _references.Add(reference);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Failed to load assembly {assemblyName}: {ex.Message}");
                }
            }

            // Initialize workspace
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            _workspace = new AdhocWorkspace(host);

            _referencesLoaded = true;
        }
        finally
        {
            _loadLock.Release();
        }
    }

    /// <summary>
    /// Creates a document in the workspace for the given code.
    /// Global usings are included as a separate document in the project.
    /// </summary>
    public Document? CreateDocument(string code)
    {
        if (_workspace == null)
            return null;

        var projectId = ProjectId.CreateNewId();
        var projectInfo = ProjectInfo.Create(
            projectId,
            VersionStamp.Create(),
            "PlaygroundProject",
            "PlaygroundProject",
            LanguageNames.CSharp,
            compilationOptions: new CSharpCompilationOptions(OutputKind.ConsoleApplication),
            parseOptions: new CSharpParseOptions(LanguageVersion.Latest),
            metadataReferences: _references);

        var project = _workspace.AddProject(projectInfo);

        // Add global usings as a separate document
        _workspace.AddDocument(project.Id, "GlobalUsings.cs", SourceText.From(GlobalUsings));

        // Add the user's code as the main document
        var sourceText = SourceText.From(code);
        var document = _workspace.AddDocument(project.Id, "Program.cs", sourceText);

        // Clean up the project after getting the document
        _workspace.ClearSolution();

        return document;
    }

    /// <summary>
    /// Creates a CSharp compilation for the given code.
    /// </summary>
    public CSharpCompilation CreateCompilation(string code)
    {
        var parseOptions = new CSharpParseOptions(LanguageVersion.Latest);

        var globalUsingsSyntaxTree = CSharpSyntaxTree.ParseText(
            GlobalUsings,
            parseOptions,
            path: "GlobalUsings.cs");

        var codeSyntaxTree = CSharpSyntaxTree.ParseText(
            code,
            parseOptions,
            path: "Program.cs");

        return CSharpCompilation.Create(
            $"PlaygroundAssembly_{Guid.NewGuid():N}",
            [globalUsingsSyntaxTree, codeSyntaxTree],
            _references,
            new CSharpCompilationOptions(OutputKind.ConsoleApplication)
                .WithOptimizationLevel(OptimizationLevel.Release)
                .WithConcurrentBuild(true));
    }

    /// <summary>
    /// Converts a Monaco editor position (1-based line/column) to an absolute position.
    /// </summary>
    public static int GetPosition(SourceText sourceText, int lineNumber, int column)
    {
        var lines = sourceText.Lines;
        var adjustedLine = lineNumber - 1; // Convert to 0-based

        if (adjustedLine < 0 || adjustedLine >= lines.Count)
        {
            return -1;
        }

        var line = lines[adjustedLine];
        var position = line.Start + Math.Min(column - 1, line.End - line.Start);

        return position;
    }

    private async Task<byte[]?> TryLoadAssemblyBytesAsync(string assemblyName)
    {
        // Try to load assembly by name - server will resolve fingerprinted filename
        // Try .dll first (WebCIL disabled), then .wasm as fallback
        var patterns = new[]
        {
            $"_framework/{assemblyName}.dll",
            $"_framework/{assemblyName}.wasm",
        };

        foreach (var pattern in patterns)
        {
            try
            {
                var response = await _httpClient.GetAsync(pattern);
                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    // Verify it's a valid PE file (starts with MZ)
                    if (bytes.Length > 2 && bytes[0] == 0x4D && bytes[1] == 0x5A)
                    {
                        return bytes;
                    }
                }
            }
            catch
            {
                // Try next pattern
            }
        }

        return null;
    }

    private async Task<DocumentationProvider?> TryLoadXmlDocumentationAsync(string assemblyName)
    {
        // Only load XML documentation for Spectre.Console packages
        // This mirrors the approach in XmlDocumentationService
        if (!SpectrePackageXmlFiles.TryGetValue(assemblyName, out var xmlFileName))
        {
            return null;
        }

        try
        {
            // Try to load XML documentation file from xml folder
            var response = await _httpClient.GetAsync($"xml/{xmlFileName}");
            if (response.IsSuccessStatusCode)
            {
                var xmlBytes = await response.Content.ReadAsByteArrayAsync();

                // Verify it's valid XML by checking for XML declaration or root element
                if (xmlBytes.Length > 5)
                {
                    return XmlDocumentationProvider.CreateFromBytes(xmlBytes);
                }
            }
        }
        catch
        {
            // XML documentation is optional - silently continue without it
        }

        return null;
    }

    /// <summary>
    /// Custom XML documentation provider that can be created from bytes.
    /// </summary>
    private sealed class XmlDocumentationProvider : DocumentationProvider
    {
        private readonly Dictionary<string, string> _documentation = new();

        private XmlDocumentationProvider(byte[] xmlBytes)
        {
            ParseXmlDocumentation(xmlBytes);
        }

        public static XmlDocumentationProvider CreateFromBytes(byte[] xmlBytes)
        {
            return new XmlDocumentationProvider(xmlBytes);
        }

        private void ParseXmlDocumentation(byte[] xmlBytes)
        {
            try
            {
                using var stream = new MemoryStream(xmlBytes);
                using var reader = XmlReader.Create(stream, new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Prohibit,
                    IgnoreWhitespace = false // Preserve whitespace for formatting
                });

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "member")
                    {
                        var name = reader.GetAttribute("name");
                        if (!string.IsNullOrEmpty(name))
                        {
                            // ReadInnerXml gets the content without the <member> wrapper
                            var xml = reader.ReadInnerXml();
                            _documentation[name] = xml;
                        }
                    }
                }
            }
            catch
            {
                // If parsing fails, we just won't have documentation
            }
        }

        protected override string? GetDocumentationForSymbol(
            string documentationMemberID,
            System.Globalization.CultureInfo preferredCulture,
            CancellationToken cancellationToken = default)
        {
            return _documentation.TryGetValue(documentationMemberID, out var xml) ? xml : null;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }
    }
}
