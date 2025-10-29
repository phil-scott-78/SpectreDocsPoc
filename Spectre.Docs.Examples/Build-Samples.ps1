#Requires -Version 7.0

<#
.SYNOPSIS
    Builds AsciiCast samples and converts them to WebP format.

.DESCRIPTION
    This script generates AsciiCast (.cast) files from the Spectre.Docs.Examples project,
    then converts them through the following pipeline:
    1. .cast -> .gif (using agg)
    2. .gif -> .webp (using ffmpeg)
    Finally, it cleans up intermediate .cast and .gif files.

.EXAMPLE
    .\Build-Samples.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

# Function to check if a command exists
function Test-CommandExists {
    param([string]$Command)

    $null = Get-Command $Command -ErrorAction SilentlyContinue
    return $?
}

# Function to write colored output
function Write-Step {
    param([string]$Message)
    Write-Host "==> " -ForegroundColor Cyan -NoNewline
    Write-Host $Message
}

function Write-Success {
    param([string]$Message)
    Write-Host "✓ " -ForegroundColor Green -NoNewline
    Write-Host $Message
}

function Write-Error {
    param([string]$Message)
    Write-Host "✗ " -ForegroundColor Red -NoNewline
    Write-Host $Message -ForegroundColor Red
}

# Check prerequisites
Write-Step "Checking prerequisites..."

if (-not (Test-CommandExists "agg")) {
    Write-Error "agg is not installed or not in PATH"
    Write-Host "Install agg from: https://github.com/asciinema/agg" -ForegroundColor Yellow
    exit 1
}
Write-Success "agg found"

if (-not (Test-CommandExists "ffmpeg")) {
    Write-Error "ffmpeg is not installed or not in PATH"
    Write-Host "Install ffmpeg from: https://ffmpeg.org/download.html" -ForegroundColor Yellow
    exit 1
}
Write-Success "ffmpeg found"

# Set paths
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$outputPath = Join-Path (Split-Path -Parent $scriptPath) "Spectre.Docs\Content\assets"

Write-Step "Output path: $outputPath"

# Ensure output directory exists
if (-not (Test-Path $outputPath)) {
    New-Item -ItemType Directory -Path $outputPath -Force | Out-Null
    Write-Success "Created output directory"
}

# Run dotnet to generate .cast files
Write-Step "Generating AsciiCast files..."
Push-Location $scriptPath
try {
    dotnet run -- -o $outputPath
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet run failed with exit code $LASTEXITCODE"
    }
    Write-Success "AsciiCast files generated"
}
finally {
    Pop-Location
}

# Find all .cast files
$castFiles = Get-ChildItem -Path $outputPath -Filter "*.cast"

if ($castFiles.Count -eq 0) {
    Write-Error "No .cast files found in $outputPath"
    exit 1
}

Write-Step "Found $($castFiles.Count) .cast file(s) to process"

# Process each .cast file
$successCount = 0
$failCount = 0

foreach ($castFile in $castFiles) {
    $baseName = $castFile.BaseName
    $castPath = $castFile.FullName
    $gifPath = Join-Path $outputPath "$baseName.gif"
    $webpPath = Join-Path $outputPath "$baseName.webp"

    Write-Host ""
    Write-Step "Processing: $baseName"

    try {
        # Convert .cast to .gif using agg
        Write-Host "  Converting to GIF..." -ForegroundColor Gray
        & agg --theme dracula --font-size 18 --speed 0.75 --font-family "Cascadia Code" $castPath $gifPath
        if ($LASTEXITCODE -ne 0) {
            throw "agg conversion failed with exit code $LASTEXITCODE"
        }

        # Convert .gif to .webp using ffmpeg
        Write-Host "  Converting to WebP..." -ForegroundColor Gray
        & ffmpeg -i $gifPath -lossless 0 -q:v 75 -y $webpPath 2>&1 | Out-Null
        if ($LASTEXITCODE -ne 0) {
            throw "ffmpeg conversion failed with exit code $LASTEXITCODE"
        }

        # Clean up intermediate files
        Write-Host "  Cleaning up intermediate files..." -ForegroundColor Gray
        Remove-Item $castPath -Force
        Remove-Item $gifPath -Force

        Write-Success "$baseName.webp created"
        $successCount++
    }
    catch {
        Write-Error "Failed to process $baseName : $_"
        $failCount++

        # Clean up any partial files
        if (Test-Path $gifPath) { Remove-Item $gifPath -Force -ErrorAction SilentlyContinue }
        if (Test-Path $castPath) { Remove-Item $castPath -Force -ErrorAction SilentlyContinue }
    }
}

# Summary
Write-Host ""
Write-Host "===========================================" -ForegroundColor Cyan
if ($failCount -eq 0) {
    Write-Success "All $successCount file(s) processed successfully"
}
else {
    Write-Host "Processed: $successCount succeeded, $failCount failed" -ForegroundColor Yellow
}
Write-Host "===========================================" -ForegroundColor Cyan
