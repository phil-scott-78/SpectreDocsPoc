Output "Spectre.Docs/Content/assets/table.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 14
Set EndBuffer 5s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase table"
