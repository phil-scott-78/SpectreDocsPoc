Output "Spectre.Docs/Content/assets/cli-error-handling.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 90
Set Rows 24
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.HandlingErrorsAndExitCodes.Demo.RunAsync(System.String[])"

Type "./example nonexistent-file.txt"
Sleep 400ms
Enter
Sleep 3s

Type "echo Exit code: $LASTEXITCODE"
Sleep 400ms
Enter
Sleep 3s
