# Remove-TagsFromMarkdown.ps1
# This script removes the 'tags' field from YAML front matter in all markdown files
# in the Spectre.Docs\Content directory and its subdirectories.

# Set the content directory path
$contentPath = Join-Path $PSScriptRoot "Spectre.Docs\Content"

# Verify the directory exists
if (-not (Test-Path $contentPath)) {
    Write-Error "Content directory not found: $contentPath"
    exit 1
}

Write-Host "Scanning markdown files in: $contentPath" -ForegroundColor Cyan
Write-Host ""

# Find all markdown files
$markdownFiles = Get-ChildItem -Path $contentPath -Filter "*.md" -Recurse

$totalFiles = $markdownFiles.Count
$modifiedFiles = 0
$skippedFiles = 0

Write-Host "Found $totalFiles markdown files" -ForegroundColor Cyan
Write-Host ""

# Process each file
foreach ($file in $markdownFiles) {
    $relativePath = $file.FullName.Substring($PSScriptRoot.Length + 1)

    # Read the file content
    $content = Get-Content -Path $file.FullName -Raw

    # Check if the file has YAML front matter with tags
    if ($content -match '(?s)^---\r?\n(.*?)\r?\n---') {
        $frontMatter = $matches[1]

        # Check if tags field exists (using multiline mode)
        if ($frontMatter -match '(?m)^\s*tags:\s*(\[.*?\])?\s*$') {
            # Remove the tags line from the content
            # This regex matches the tags line including the newline
            $modifiedContent = $content -replace '(?m)^\s*tags:\s*(\[.*?\])?\s*\r?\n', ''

            # Write the modified content back to the file
            Set-Content -Path $file.FullName -Value $modifiedContent -NoNewline

            Write-Host "[MODIFIED] $relativePath" -ForegroundColor Green
            $modifiedFiles++
        }
        else {
            Write-Host "[SKIPPED]  $relativePath (no tags field)" -ForegroundColor Gray
            $skippedFiles++
        }
    }
    else {
        Write-Host "[SKIPPED]  $relativePath (no front matter)" -ForegroundColor Gray
        $skippedFiles++
    }
}

# Summary
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Summary:" -ForegroundColor Cyan
Write-Host "  Total files processed: $totalFiles" -ForegroundColor White
Write-Host "  Files modified:        $modifiedFiles" -ForegroundColor Green
Write-Host "  Files skipped:         $skippedFiles" -ForegroundColor Gray
Write-Host "============================================" -ForegroundColor Cyan
