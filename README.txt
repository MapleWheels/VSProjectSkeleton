This is a project template for making plugin-powered ContentPackages for LuaCsForBarotrauma. For more details,
visit the Wiki.


Quick Start Steps:

1. Download the Libraries and place them in /Refs as shown below.

- The libraries for this project must be downloaded separately. They can be found in the LuaCsForBarotrauma Steam Workshop
folder under the subfolder "Publicized". Just copy the ones listed below into "/Refs". 
- The file-folder structure should match the following:

./0Harmony.dll
./Barotrauma.dll
./DedicatedServer.dll
./Farseer.NetStandard.dll
./Lidgren.NetStandard.dll
./Mono.Cecil.dll
./MonoGame.Framework.Linux.NetStandard.dll
./MonoGame.Framework.Windows.NetStandard.dll
./MonoMod.Common.dll
./MoonSharp.Interpreter.dll
./XNATypes.dll


For all 5 projects (WindowsClient, WindowsServer, LinuxClient, LinuxServer, Assets), you need to edit the .csproj 
Property Group: 

<ModDeployDir>..\LUATRAMA_DEBUG_LOCALMODS_MYMODDIR\</ModDeployDir>

Replace "..\LUATRAMA_DEBUG_LOCALMODS_MYMODDIR\" with the directory of your mod in "Barotrauma/LocalMods/" 


2. Set the executable directory for the Launch Configurations (Client, Server).


3. Set your details (modname, files, etc) in Assets/filelist.xml
- Note: All files should be placed under "/Content" and will be copied automatically to "LocalMods/<YourMod>/Content/...".
your "filelist.xml" should list your mods' content files in the format of "%ModDir%/Content/..."