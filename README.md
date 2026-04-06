This is a project template for making plugin-powered ContentPackages for LuaCsForBarotrauma. For more details,
visit the Wiki.

Please see the [Luatrauma Docs](https://luatrauma.github.io/Luatrauma.Docs/cs/setup-assembly-csharp/) for detailed instructions.

Quick Start Steps:

1. Download the Libraries file [luacsforbarotrauma_refs.zip](https://github.com/evilfactory/LuaCsForBarotrauma/releases) from latest release, unzip it, and place the contents in /Refs as shown below.

- The libraries for this project must be downloaded separately. They can be found in the LuaCsForBarotrauma Steam Workshop
folder under the subfolder "Publicized". Just copy the ones listed below into "/Refs".

You need to edit the "Build.props" file: 

> <ModDeployDir>..\LUATRAMA_DEBUG_LOCALMODS_MYMODDIR\</ModDeployDir>

Replace "..\LUATRAMA_DEBUG_LOCALMODS_MYMODDIR\" with the absolute path of your mod in "Barotrauma\LocalMods\", ie. "Steam\..\Barotrauma\LocalMods\MyModName\"

> <AssemblyName>MyModName</AssemblyName>
> <RootNamespace>MyModName</RootNamespace>

Replace "MyModName" with a valid assembly name, this should be similar to your mod name but does not need to match. This name should:
- Not include spaces.
- Not include special characters, periods are allowed.
- Use english characters.


2. Set the executable directory for the Launch Configurations (Client, Server).

3. Set your details (modname, files, etc) in Assets/Content/filelist.xml
- Note: All files should be placed under "/Content" and will be copied automatically to "LocalMods/<YourMod>/...".
