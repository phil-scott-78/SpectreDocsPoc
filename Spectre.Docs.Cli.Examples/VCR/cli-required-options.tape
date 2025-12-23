Output "Spectre.Docs/Content/assets/cli-required-options.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 18
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.MakingOptionsRequired.Demo.RunAsync(System.String[])"

Type "./example"
Sleep 400ms
Enter
Sleep 2s

Type "./example --environment production"
Sleep 400ms
Enter
Sleep 2s

Type "./example --environment production --version 1.2.3"
Sleep 400ms
Enter
Sleep 2s

Type "./example -e staging -v 2.0.0 --dry-run"
Sleep 400ms
Enter
Sleep 3s
