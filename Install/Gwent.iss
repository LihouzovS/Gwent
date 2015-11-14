#define Name "Gwent"
#define Version "0.0.0.16"
#define URL "http://www.google.com"
#define ExeName "Gwent.exe"
#define ApplicationVersion GetFileVersion(ExeName)
[Setup]
AppId={{361E65A9-ABA7-44CA-A747-99B28252BA56}}
AppName={#Name}
AppSupportURL={#URL}
AppUpdatesURL={#URL}
DefaultDirName={pf}\{#Name}
DefaultGroupName={#Name}
OutputDir="..\Gwent\Install\Installer"
OutputBaseFileName=test-setup
SetupIconFile=Gwent.ico
Compression=lzma
SolidCompression=yes
AppVersion={#ApplicationVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "License_ENG.txt"
[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
[Files]


Source: "..\Gwent\bin\Debug\Gwent.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Gwent\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
[Icons]
Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"
Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon
[Code]
#include "install.pas"
