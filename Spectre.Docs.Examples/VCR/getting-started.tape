Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 5

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase getting-started-tutorial"
Wait
Screenshot "Spectre.Docs/Content/assets/getting-started-tutorial.svg"
