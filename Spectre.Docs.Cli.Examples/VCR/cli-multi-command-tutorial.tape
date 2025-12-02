Output "Spectre.Docs/Content/assets/cli-multi-command-tutorial.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 70
Set Rows 16
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.MultiCommand.Finished.Demo.RunAsync(System.String[])"

Type "./example add package Newtonsoft.Json --version 13.0.1"
Sleep 400ms
Enter
Sleep 1s

Type "./example add reference ../MyLib/MyLib.csproj"
Sleep 400ms
Enter
Sleep 1s

Type "./example list --verbose"
Sleep 400ms
Enter
Sleep 1s

Type "./example --help"
Sleep 400ms
Enter
Sleep 2s


Type "./example list --help"
Sleep 400ms
Enter
Sleep 5s