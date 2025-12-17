Output "anim.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 10

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase creating-custom-renderables-tutorial"
Wait
Screenshot "Spectre.Docs/Content/assets/creating-custom-renderables-tutorial.svg"