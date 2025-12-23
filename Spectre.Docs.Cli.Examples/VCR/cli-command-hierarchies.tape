Output "Spectre.Docs/Content/assets/cli-command-hierarchies.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 20
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.CommandHierarchies.Demo.RunAsync(System.String[])"

Type "./example remote add origin https://github.com/user/repo.git"
Sleep 400ms
Enter
Sleep 2s

Type "./example remote ls --verbose"
Sleep 400ms
Enter
Sleep 2s

Type "./example remote rm origin"
Sleep 400ms
Enter
Sleep 3s
