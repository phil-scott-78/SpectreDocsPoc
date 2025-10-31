#!/usr/bin/env pwsh

#Requires -Version 7.0

# Build the project
Write-Host "Building project..." -ForegroundColor Cyan
dotnet build Spectre.Docs.Examples

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed with exit code $LASTEXITCODE"
    exit $LASTEXITCODE
}

# Generate all screenshots in parallel
Write-Host "`nGenerating screenshots in parallel..." -ForegroundColor Cyan

$tapeFiles = Get-ChildItem "Spectre.Docs.Examples\VCR\*.tape"
$totalFiles = $tapeFiles.Count
Write-Host "Found $totalFiles tape files to process`n" -ForegroundColor Green

dotnet build B:\VcrSharp\

$results = $tapeFiles | ForEach-Object -Parallel {
    $file = $_
    $fileName = $file.Name
    $gifName = $file.BaseName + ".gif"

    try {
        Write-Host "[$(Get-Date -Format 'HH:mm:ss')] Processing: $fileName" -ForegroundColor Yellow

        
        # Execute VCR command
        dotnet run --no-build --project b:/vcrsharp/src/VcrSharp.Cli -- -o $gifName $file.FullName   | Out-Null

        if ($LASTEXITCODE -eq 0) {
            Write-Host "[$(Get-Date -Format 'HH:mm:ss')] ✓ Completed: $fileName" -ForegroundColor Green
            return @{ Success = $true; File = $fileName }
        } else {
            Write-Warning "[$(Get-Date -Format 'HH:mm:ss')] ✗ Failed: $fileName (Exit code: $LASTEXITCODE)"
            return @{ Success = $false; File = $fileName; ExitCode = $LASTEXITCODE }
        }
    }
    catch {
        Write-Error "[$(Get-Date -Format 'HH:mm:ss')] ✗ Error processing $fileName : $_"
        return @{ Success = $false; File = $fileName; Error = $_.Exception.Message }
    }
} -ThrottleLimit 1   
# THEORETICALLY, this could be increased for more parallelism, but VCRSharp might have issues with concurrent runs.
# Either we do or ttyd does. It seems an extra line feed gets added to the output (svg and gif) when run in parallel
# and I don't even know how to begin debugging that.


# Report results
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Screenshot Generation Summary" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

$successful = ($results | Where-Object { $_.Success }).Count
$failed = ($results | Where-Object { -not $_.Success }).Count

Write-Host "Total: $totalFiles | Success: $successful | Failed: $failed" -ForegroundColor $(if ($failed -eq 0) { 'Green' } else { 'Yellow' })

if ($failed -gt 0) {
    Write-Host "`nFailed files:" -ForegroundColor Red
    $results | Where-Object { -not $_.Success } | ForEach-Object {
        Write-Host "  - $($_.File)" -ForegroundColor Red
        if ($_.Error) {
            Write-Host "    Error: $($_.Error)" -ForegroundColor Gray
        }
        if ($_.ExitCode) {
            Write-Host "    Exit code: $($_.ExitCode)" -ForegroundColor Gray
        }
    }
    exit 1
}

Write-Host "`n✓ All screenshots generated successfully!" -ForegroundColor Green
