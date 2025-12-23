Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 20
Set EndBuffer 2s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase canvas-image"
Wait
Screenshot "Spectre.Docs/Content/assets/canvas-image.svg"
