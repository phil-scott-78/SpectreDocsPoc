using Microsoft.CodeAnalysis;

namespace Spectre.Docs.Playground.Services;

public class CompilationService
{
    private readonly WorkspaceService _workspaceService;

    public CompilationService(WorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<CompilationResult> CompileAsync(string code)
    {
        await _workspaceService.EnsureInitializedAsync();

        var compilation = _workspaceService.CreateCompilation(code);

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
}

public class CompilationResult
{
    public bool Success { get; set; }
    public byte[]? Assembly { get; set; }
    public List<Diagnostic> Diagnostics { get; set; } = [];
}
