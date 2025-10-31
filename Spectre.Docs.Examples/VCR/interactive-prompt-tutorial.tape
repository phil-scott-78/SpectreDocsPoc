Output "Spectre.Docs/Content/assets/interactive-prompt-tutorial.svg"

Set DisableCursor true
Set FontSize 22
Set Theme "one dark"
Set TransparentBackground "true"
Set Cols 82
Set Rows 16
Set EndBuffer 5s
Set TypingSpeed 190ms

Exec "dotnet run --project .\Spectre.Docs.Examples\ --no-build showcase interactive-prompts-tutorial"
Wait /What's your/
Sleep 1250ms
Type "Teddy"
Sleep 550ms
Enter
Wait /size pizza/
Sleep 1250ms
Down
Down
Down
Down
Sleep 750ms
Up
Down
Sleep 1250ms
Enter
Wait /like/
Sleep 750ms
Space
Down
Down
Space
Sleep 500ms
Down
Down
Down
Sleep 500ms
Up
Space
Sleep 1250ms
Enter
Wait /Place this order/
Sleep 1250ms
Type "Y"
Enter

