Output "Spectre.Docs/Content/assets/capabilities.svg"

Set Shell "pwsh"
Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 12
Set EndBuffer 5s
Set WorkingDirectory "./Spectre.Docs.Examples/"

Type "dotnet run -- showcase capabilities"
Enter
Wait
Sleep 1000ms

# set NO_COLOR
Type "$env:NO_COLOR='true'"
Enter
Sleep 500ms

Type "dotnet run -- showcase capabilities"
Enter
Wait
Sleep 1000ms

# set a CI env variable
Type "$env:CI_SERVER='yes'"
Enter
Sleep 500MS

Type "dotnet run -- showcase capabilities"
Enter
Wait
Sleep 3s