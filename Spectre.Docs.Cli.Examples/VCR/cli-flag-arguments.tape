Output "Spectre.Docs/Content/assets/cli-flag-arguments.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 20
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.FlagArguments.Demo.RunAsync(System.String[])"

Type "./example"
Sleep 400ms
Enter
Sleep 2s

Type "./example --port"
Sleep 400ms
Enter
Sleep 2s

Type "./example --port 8080"
Sleep 400ms
Enter
Sleep 2s

Type "./example --port 8080 --timeout 30 --host 0.0.0.0"
Sleep 400ms
Enter
Sleep 3s
