[Setup]
AppName=Radio Player Basic
AppVersion=1.0
DefaultDirName={pf}\RadioPlayerBasic
DefaultGroupName=Radio Player Basic
OutputDir=Output
OutputBaseFilename=RadioPlayerBasicInstall
Compression=lzma
SolidCompression=yes

[Files]
Source: "D:\Users\amr2\source\repos\RadioPlayer_Basic\RadioPlayer_Basic\bin\Release\RadioPlayer_Basic.exe"; DestDir: "{app}"; Flags: ignoreversion

Source: "D:\Users\amr2\source\repos\RadioPlayer_Basic\RadioPlayer_Basic\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Users\amr2\source\repos\RadioPlayer_Basic\RadioPlayer_Basic\bin\Release\Microsoft.Toolkit.Uwp.Notifications.dll"; DestDir: "{app}"; Flags: ignoreversion

; הוסף ספריות אחרות, אם יש

[Icons]
Name: "{group}\Radio Player Basic"; Filename: "{app}\RadioPlayer_Basic.exe"
Name: "{group}\Uninstall Radio Player Basic"; Filename: "{uninstallexe}"

[Run]
; הפעל את האפליקציה בסיום ההתקנה
Filename: "{app}\RadioPlayer_Basic.exe"; Description: "Launch Radio Player Basic"; Flags: nowait postinstall skipifsilent

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; 
