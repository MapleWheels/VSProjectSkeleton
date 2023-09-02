This is a project template for making plugin-powered ContentPackages for LuaCsForBarotrauma. For more details,
visit the Wiki.


Quick Start Steps:
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