Output "Spectre.Docs/Content/assets/cli-defining-arguments.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 20
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.DefiningCommandsAndArguments.Demo.RunAsync(System.String[])"

Type "./example myfile.txt"
Sleep 400ms
Enter
Sleep 2s

Type "./example myfile.txt /backup --force -b 128"
Sleep 400ms
Enter
Sleep 2s

Type "./example input.txt output.txt -v --preserve-timestamps"
Sleep 400ms
Enter
Sleep 3s
