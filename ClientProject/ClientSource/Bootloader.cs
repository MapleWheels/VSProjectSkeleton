using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework.Input;
using ModdingToolkit;
using ModdingToolkit.Patches;
using ModdingToolkit.Config;

[assembly: IgnoresAccessChecksTo("Barotrauma")]

namespace HotkeyReload;

sealed class Bootloader : IAssemblyPlugin
{
    internal static IConfigControl?
        KeybindReload,
        KeybindQuickLootAll,
        KeybindQuickStackToPlayer,
        KeybindQuickStackToStorage;

    static Bootloader()
    {
        Util.RegisterCompatibilityRule("plasmacutter",
            (heldItem, storableItem) => storableItem.HasTag("oxygensource"));
        
        Util.RegisterCompatibilityRule("weldingtool",
            (_, storableItem) => storableItem.HasTag("weldingtoolfuel"));
        
        Util.RegisterCompatibilityRule("flamer",
            (heldItem, storableItem) => storableItem.HasTag("weldingtoolfuel"));
    }

    private void RegisterConfig()
    {
        KeybindReload = ConfigManager.AddConfigKeyOrMouseBind(
            "Reload",
            "HotkeyReload",
            new KeyOrMouse(Keys.R)
        );
        
        KeybindQuickLootAll = ConfigManager.AddConfigKeyOrMouseBind(
            "QuickLootAll",
            "HotkeyReload",
            new KeyOrMouse(Keys.H)
        );
        
        KeybindQuickStackToPlayer = ConfigManager.AddConfigKeyOrMouseBind(
            "QuickStackPlayer",
            "HotkeyReload",
            new KeyOrMouse(Keys.K)
        );
        
        KeybindQuickStackToStorage = ConfigManager.AddConfigKeyOrMouseBind(
            "QuickStackStorage",
            "HotkeyReload",
            new KeyOrMouse(Keys.L)
        );
    }

    private void RegisterPatches()
    {
        PatchManager.RegisterPatches(new List<PatchManager.PatchData>()
        {
            new PatchManager.PatchData(
                AccessTools.Method(typeof(Barotrauma.LuaCsSetup), "Update"),
                null,
                new HarmonyMethod(AccessTools.Method(typeof(P_LuaCsSetup_Update), "Postfix"))
                )
        });
    }

    public void Initialize()
    {
        
    }

    public void OnLoadCompleted()
    {
        RegisterConfig();
        RegisterPatches();
    }

    public void PreInitPatching()
    {
        
    }

    public void Dispose()
    {
    }
}