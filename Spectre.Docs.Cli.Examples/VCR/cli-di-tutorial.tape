Output "Spectre.Docs/Content/assets/cli-di-tutorial.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 70
Set Rows 12
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.DependencyInjection.DIComplete.Demo.RunAsync(System.String[])"

Type "./example --help"
Sleep 400ms
Enter
Sleep 2500ms

Type "./example Alice"
Sleep 400ms
Enter
Sleep 1s

Type "./example Alice --formal"
Sleep 400ms
Enter
Sleep 1s

Type "./example Alice --enthusiastic"
Sleep 400ms
Enter
Sleep 3s