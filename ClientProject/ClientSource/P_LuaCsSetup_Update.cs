using System;
using Microsoft.Xna.Framework;
using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework.Input;

namespace HotkeyReload;

public class P_LuaCsSetup_Update
{
    static void Postfix()
    {
        if (Bootloader.KeybindReload?.IsHit() ?? false)
            Reloader.ReloadHeldItems();
        
        if (Bootloader.KeybindQuickLootAll?.IsHit() ?? false)
            QuickActions.QuickLootAllToPlayerInventory();
        
        if (Bootloader.KeybindQuickStackToPlayer?.IsHit() ?? false)
            QuickActions.QuickStackToPlayerInventory();
        
        if (Bootloader.KeybindQuickStackToStorage?.IsHit() ?? false)
            QuickActions.QuickStackToStorageInventory();
    }
}