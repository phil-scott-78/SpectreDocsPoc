Output "Spectre.Docs/Content/assets/text-path.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 6
Set EndBuffer 5s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase text-path"
