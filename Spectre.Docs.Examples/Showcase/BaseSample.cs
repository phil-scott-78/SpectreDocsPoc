using System.Text;
using JetBrains.Annotations;
using Spectre.Console;

namespace Spectre.Docs.Examples.Showcase;

/// <summary>Base class for sample demonstrations.</summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.WithMembers)]
public abstract class BaseSample
{
    /// <summary>Runs the sample on the specified console.</summary>
    /// <param name="console">The console to render to.</param>
    public abstract void Run(IAnsiConsole console);

    /// <summary>Gets the kebab-case name of the sample.</summary>
    /// <returns>The sample name.</returns>
    public virtual string Name() => PascalToKebab(GetType().Name.Replace("Sample",""));

    private string PascalToKebab(ReadOnlySpan<char> input)
    {
        var sb = new StringBuilder();
        var previousUpper = true;
        foreach (var chr in input)
        {
            if (char.IsUpper(chr) && previousUpper == false)
            {
                sb.Append('-');
                previousUpper = true;
            }
            else
            {
                previousUpper = false;
            }

            sb.Append(char.ToLower(chr));
        }

        return sb.ToString();

    }

    /// <summary>Cleans up a caller expression string for display.</summary>
    /// <param name="caller">The raw caller expression.</param>
    /// <returns>The cleaned expression.</returns>
    protected static string CleanupCallerExpression(string caller)
    {
        if (string.IsNullOrWhiteSpace(caller))
            return caller;

        var lines = caller.Split('\n').Select(l => l.TrimEnd('\r')).ToArray();

        // Remove lambda syntax from first line
        if (lines.Length > 0)
        {
            lines[0] = lines[0].TrimStart();
            if (lines[0].StartsWith("() =>"))
            {
                lines[0] = lines[0].Substring(5).TrimStart();
            }
        }

        // Check if we have explicit braces (multi-line lambda with braces)
        var hasExplicitBraces = false;
        if (lines.Length > 0 && lines[0].Trim() == "{")
        {
            hasExplicitBraces = true;
            // Remove the opening brace line
            lines = lines.Skip(1).ToArray();
        }
        else if (lines.Length > 0 && lines[0].EndsWith("{"))
        {
            hasExplicitBraces = true;
            // Remove the brace from the end of the first line
            lines[0] = lines[0].Substring(0, lines[0].Length - 1).TrimEnd();
            if (string.IsNullOrWhiteSpace(lines[0]))
                lines = lines.Skip(1).ToArray();
        }

        // Remove closing brace if it exists
        if (hasExplicitBraces && lines.Length > 0)
        {
            var lastLine = lines[^1].Trim();
            if (lastLine == "}" || lastLine == "});")
            {
                lines = lines.Take(lines.Length - 1).ToArray();
            }
        }

        // Remove return statement from last non-empty line
        for (int i = lines.Length - 1; i >= 0; i--)
        {
            var trimmed = lines[i].Trim();
            if (!string.IsNullOrWhiteSpace(trimmed))
            {
                if (trimmed.StartsWith("return "))
                {
                    var returnValue = trimmed.Substring(7).TrimEnd(';').Trim();
                    lines[i] = lines[i].Replace(trimmed, returnValue);
                }
                break;
            }
        }

        // Calculate minimum indentation from line 2 onwards (skip first line which was already processed)
        var minIndent = lines.Length > 1
            ? lines.Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Length - l.TrimStart().Length)
                .DefaultIfEmpty(0)
                .Min()
            : 0;

        // Remove minimum indentation from all lines
        lines = lines.Select(l =>
        {
            if (string.IsNullOrWhiteSpace(l))
                return string.Empty;
            return l.Length >= minIndent ? l.Substring(minIndent) : l;
        }).ToArray();

        // Trim empty lines from start and end
        var result = string.Join('\n', lines).Trim();

        return result;
    }
}