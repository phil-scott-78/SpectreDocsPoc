Output "Spectre.Docs/Content/assets/grid.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 12
Set EndBuffer 5s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase grid"
