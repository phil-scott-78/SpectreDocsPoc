Output "Spectre.Docs/Content/assets/hero.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 16
Set EndBuffer 2s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build hero"
