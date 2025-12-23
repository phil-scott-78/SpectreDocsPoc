Output "Spectre.Docs/Content/assets/cli-custom-type-converters.svg"

Set Shell "pwsh"
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 80
Set Rows 18
Set EndBuffer 5s
Set WorkingDirectory "Spectre.Docs.Cli.Examples/bin/Debug/net10.0"
Env SPECTRE_APP "M:Spectre.Docs.Cli.Examples.DemoApps.CustomTypeConverters.Demo.RunAsync(System.String[])"

Type "./example --point 10,20"
Sleep 400ms
Enter
Sleep 2s

Type "./example --point 100,200 --offset 5,5 -c red"
Sleep 400ms
Enter
Sleep 3s
