#define AppName "DesktopTool"
#define AppVersion "1.0"
#define AppPublisher ""
#define VCRedistFile "VC_redist.x86.exe"
#define InstallerFileName "DesktopTool_setup_x86"

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
WizardStyle=modern
DefaultDirName={localappdata}\{#AppName}
DefaultGroupName={#AppName}
UninstallDisplayIcon={app}\Uninstall.exe
Compression=lzma2
SolidCompression=yes
OutputDir=.\output
OutputBaseFilename={#InstallerFileName}
PrivilegesRequired=admin

[Files]
Source: {#DesktopAppPath}; DestDir: {#DesktopAppDestPath}; Flags: ignoreversion recursesubdirs
; VC++ redistributable runtime. Extracted by VCRedistNeedsInstall() if needed.
Source: ".\redist\{#VCRedistFile}"; DestDir: {tmp}; Flags: dontcopy

[Run]
Filename: "{sys}\regsvr32.exe"; Parameters: "/s ""{#VirtualCamDestPathX86}\{#VirtualCamBinFile}""";
Filename: "{tmp}\{#VCRedistFile}"; Parameters: "/quiet"; Check: VCRedistNeedsInstall; Flags: waituntilterminated

[UninstallRun]
Filename: "{sys}\regsvr32.exe"; Parameters: "/u /s ""{#VirtualCamDestPathX86}\{#VirtualCamBinFile}"""; RunOnceId: "UnregisterVCamX86"


[Icons]
Name: "{group}\DesktopTool"; Filename: "{#ServerDestPath}\{#ServerBinFile}";
Name: "{group}\DesktopTool (development)"; Filename: "{#ServerDestPath}\{#ServerBinFile}"; Parameters: "--key MDEyMzQ1Njc4OTAxMjM0NTY3ODkwMTIzNDU2Nzg5MDE= --nonce MDEyMzQ1Njc4OTAxMjM0NQ==";

[Code]
function VCRedistNeedsInstall: Boolean;
var 
  Version: String;
begin
  if RegQueryStringValue(HKEY_LOCAL_MACHINE,
       'SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x86', 'Version', Version) then
  begin
    Result := (CompareStr(Version, 'v14.28.29914.00')<0);
  end
  else 
  begin
    Result := True;
  end;
  if (Result) then
  begin
    ExtractTemporaryFile(ExpandConstant('{#VCRedistFile}'));
  end;
end;