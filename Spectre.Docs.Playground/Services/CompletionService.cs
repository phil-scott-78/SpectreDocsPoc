using System.Collections.Immutable;
using Microsoft.CodeAnalysis.QuickInfo;
using Microsoft.CodeAnalysis.Text;

namespace Spectre.Docs.Playground.Services;

public class CompletionService
{
    private readonly WorkspaceService _workspaceService;

    public CompletionService(WorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<List<CompletionItemData>> GetCompletionsAsync(string code, int lineNumber, int column)
    {
        try
        {
            await _workspaceService.EnsureInitializedAsync();

            var document = _workspaceService.CreateDocument(code);
            if (document == null)
            {
                return [];
            }

            // Convert line/column to absolute position
            var sourceText = SourceText.From(code);
            var position = WorkspaceService.GetPosition(sourceText, lineNumber, column);

            if (position < 0 || position > code.Length)
            {
                return [];
            }

            var completionService = Microsoft.CodeAnalysis.Completion.CompletionService.GetService(document);
            if (completionService == null)
            {
                return [];
            }

            var completions = await completionService.GetCompletionsAsync(document, position);

            List<CompletionItemData> result = [];
            foreach (var item in completions.ItemsList.Take(100)) // Limit to 100 items for performance
            {
                result.Add(new CompletionItemData
                {
                    Label = item.DisplayText,
                    Kind = GetCompletionKind(item.Tags),
                    InsertText = item.DisplayText,
                    Detail = item.InlineDescription,
                    SortText = item.SortText
                });
            }

            return result;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Completion error: {ex.Message}");
            return [];
        }
    }

    public async Task<HoverData?> GetHoverAsync(string code, int lineNumber, int column)
    {
        try
        {
            await _workspaceService.EnsureInitializedAsync();

            var document = _workspaceService.CreateDocument(code);
            if (document == null)
            {
                return null;
            }

            // Convert line/column to absolute position
            var sourceText = SourceText.From(code);
            var position = WorkspaceService.GetPosition(sourceText, lineNumber, column);

            if (position < 0 || position > code.Length)
            {
                return null;
            }

            var quickInfoService = QuickInfoService.GetService(document);
            if (quickInfoService == null)
            {
                return null;
            }

            var quickInfo = await quickInfoService.GetQuickInfoAsync(document, position);
            if (quickInfo == null)
            {
                return null;
            }

            // Extract text content from QuickInfo sections
            var contents = new List<string>();

            foreach (var section in quickInfo.Sections)
            {
                var text = GetTextFromSection(section);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    // Format based on section kind
                    switch (section.Kind)
                    {
                        case QuickInfoSectionKinds.Description:
                            contents.Add($"```csharp\n{text}\n```");
                            break;
                        case QuickInfoSectionKinds.DocumentationComments:
                            contents.Add(text);
                            break;
                        case QuickInfoSectionKinds.TypeParameters:
                        case QuickInfoSectionKinds.AnonymousTypes:
                        case QuickInfoSectionKinds.Exception:
                            contents.Add($"*{text}*");
                            break;
                        default:
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                contents.Add(text);
                            }
                            break;
                    }
                }
            }

            if (contents.Count == 0)
            {
                return null;
            }

            // Calculate the range in the code
            var span = quickInfo.Span;
            var startLine = sourceText.Lines.GetLineFromPosition(span.Start);
            var endLine = sourceText.Lines.GetLineFromPosition(span.End);

            return new HoverData
            {
                Contents = string.Join("\n\n", contents),
                StartLine = startLine.LineNumber + 1, // Convert to 1-based
                StartColumn = span.Start - startLine.Start + 1,
                EndLine = endLine.LineNumber + 1,
                EndColumn = span.End - endLine.Start + 1
            };
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Hover error: {ex.Message}");
            return null;
        }
    }

    private string GetTextFromSection(QuickInfoSection section)
    {
        var parts = new List<string>();

        foreach (var part in section.TaggedParts)
        {
            parts.Add(part.Text);
        }

        return string.Join("", parts);
    }

    private int GetCompletionKind(ImmutableArray<string> tags)
    {
        // Map Roslyn tags to Monaco CompletionItemKind
        // Monaco kinds: https://microsoft.github.io/monaco-editor/api/enums/monaco.languages.CompletionItemKind.html
        if (tags.Contains("Method")) return 0; // Method
        if (tags.Contains("Function")) return 1; // Function
        if (tags.Contains("Constructor")) return 2; // Constructor
        if (tags.Contains("Field")) return 3; // Field
        if (tags.Contains("Variable")) return 4; // Variable
        if (tags.Contains("Class")) return 5; // Class
        if (tags.Contains("Struct")) return 22; // Struct
        if (tags.Contains("Interface")) return 7; // Interface
        if (tags.Contains("Module")) return 8; // Module
        if (tags.Contains("Property")) return 9; // Property
        if (tags.Contains("Event")) return 10; // Event
        if (tags.Contains("Operator")) return 11; // Operator
        if (tags.Contains("Unit")) return 12; // Unit
        if (tags.Contains("Constant")) return 14; // Constant
        if (tags.Contains("Enum")) return 15; // Enum
        if (tags.Contains("EnumMember")) return 16; // EnumMember
        if (tags.Contains("Keyword")) return 17; // Keyword
        if (tags.Contains("Snippet")) return 27; // Snippet
        if (tags.Contains("TypeParameter")) return 24; // TypeParameter
        if (tags.Contains("Namespace")) return 8; // Module (used for namespace)
        return 18; // Text (default)
    }
}

public class CompletionItemData
{
    public string Label { get; set; } = "";
    public int Kind { get; set; }
    public string? InsertText { get; set; }
    public string? Detail { get; set; }
    public string? SortText { get; set; }
}

public class HoverData
{
    public string Contents { get; set; } = "";
    public int StartLine { get; set; }
    public int StartColumn { get; set; }
    public int EndLine { get; set; }
    public int EndColumn { get; set; }
}
