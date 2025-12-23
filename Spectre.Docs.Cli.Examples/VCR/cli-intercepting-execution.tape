Output "Spectre.Docs/Content/assets/cli-intercepting-execution.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 14
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.InterceptingCommandExecution.Demo.RunAsync(System.String[])"

Type "./example process myfile.txt"
Sleep 400ms
Enter
Sleep 3s

Type "./example process another-file.txt"
Sleep 400ms
Enter
Sleep 3s
