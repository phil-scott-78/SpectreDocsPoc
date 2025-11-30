Output "Spectre.Docs/Content/assets/rule.svg"
Output "Spectre.Docs/Content/assets/rule.gif"
Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 8
Set EndBuffer 5s

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase rule"
