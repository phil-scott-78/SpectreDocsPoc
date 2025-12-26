using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Spectre.Docs.Playground.Services;

public class CompilationService
{
    private readonly HttpClient _httpClient;
    private static readonly List<MetadataReference> _cachedReferences = new();
    private static bool _referencesLoaded;
    private static readonly SemaphoreSlim _loadLock = new(1, 1);

    // Core assemblies needed for compilation
    private static readonly string[] RequiredAssemblies = new[]
    {
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
        "Spectre.Console",
    };

    public CompilationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CompilationResult> CompileAsync(string code)
    {
        await EnsureReferencesLoadedAsync();

        // Wrap the code in a class if it's top-level statements style
        var wrappedCode = WrapCode(code);

        var syntaxTree = CSharpSyntaxTree.ParseText(wrappedCode, new CSharpParseOptions(LanguageVersion.Latest));

        var compilation = CSharpCompilation.Create(
            $"PlaygroundAssembly_{Guid.NewGuid():N}",
            new[] { syntaxTree },
            _cachedReferences,
            new CSharpCompilationOptions(OutputKind.ConsoleApplication)
                .WithOptimizationLevel(OptimizationLevel.Release)
                .WithConcurrentBuild(true));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            return new CompilationResult
            {
                Success = false,
                Diagnostics = result.Diagnostics.ToList()
            };
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = ms.ToArray();

        return new CompilationResult
        {
            Success = true,
            Assembly = assembly,
            Diagnostics = result.Diagnostics.ToList()
        };
    }

    private string WrapCode(string code)
    {
        // Check if code already has a Program class or Main method
        if (code.Contains("class Program") || code.Contains("static void Main") || code.Contains("static async Task Main"))
        {
            return code;
        }

        // Wrap top-level statements in a Main method
        return $$"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using System.Threading;
            using System.Threading.Tasks;
            using Spectre.Console;

            public class Program
            {
                public static void Main(string[] args)
                {
                    {{RemoveUsings(code)}}
                }
            }
            """;
    }

    private string RemoveUsings(string code)
    {
        // Remove using statements from the code as they're already included in the wrapper
        var lines = code.Split('\n');
        var nonUsingLines = lines.Where(l => !l.TrimStart().StartsWith("using ") || l.Contains("("));
        return string.Join('\n', nonUsingLines);
    }

    private async Task EnsureReferencesLoadedAsync()
    {
        if (_referencesLoaded)
            return;

        await _loadLock.WaitAsync();
        try
        {
            if (_referencesLoaded)
                return;

            // In WASM, we need to fetch assembly bytes via HTTP
            // Get the list of assemblies from blazor.boot.json or use known names
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assemblyName in RequiredAssemblies)
            {
                try
                {
                    // Try to find the assembly in already loaded assemblies
                    var loadedAssembly = loadedAssemblies.FirstOrDefault(a =>
                        a.GetName().Name?.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) == true);

                    if (loadedAssembly != null)
                    {
                        // In WASM, we can use the assembly image directly if available
                        // Try to get the raw bytes from the HTTP endpoint
                        var bytes = await TryLoadAssemblyBytesAsync(assemblyName);
                        if (bytes != null)
                        {
                            _cachedReferences.Add(MetadataReference.CreateFromImage(bytes));
                        }
                    }
                    else
                    {
                        // Try to load via HTTP
                        var bytes = await TryLoadAssemblyBytesAsync(assemblyName);
                        if (bytes != null)
                        {
                            _cachedReferences.Add(MetadataReference.CreateFromImage(bytes));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Failed to load assembly {assemblyName}: {ex.Message}");
                }
            }

            _referencesLoaded = true;
        }
        finally
        {
            _loadLock.Release();
        }
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
}

public class CompilationResult
{
    public bool Success { get; set; }
    public byte[]? Assembly { get; set; }
    public List<Diagnostic> Diagnostics { get; set; } = new();
}
