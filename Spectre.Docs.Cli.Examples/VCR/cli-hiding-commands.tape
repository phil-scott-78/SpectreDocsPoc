Output "Spectre.Docs/Content/assets/cli-hiding-commands.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 20
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.HidingCommandsAndOptions.Demo.RunAsync(System.String[])"

Type "./example --help"
Sleep 400ms
Enter
Sleep 2500ms

Type "./example diagnostics"
Sleep 400ms
Enter
Sleep 2s

Type "./example deploy production --skip-hooks"
Sleep 400ms
Enter
Sleep 3s
