using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Barotrauma;
using Barotrauma.Extensions;
using Barotrauma.Items.Components;
using HarmonyLib;
using ModdingToolkit;

namespace HotkeyReload;

public static class Reloader
{
    /// <summary>
    /// Removes the inventory items of the item currently being held by the player (hands) if the condition is below 0
    /// and will then try to replace the item with another equivalent from the player's inventory, prioritizes trying
    /// to put the same item type. 
    /// </summary>
    public static void ReloadHeldItems()
    {
        //?? check if in-game && single player or host in multiplayer and we're not switching maps
        if (!Util.CheckIfValidToInteract() || !Util.CheckIfCharacterReady(Character.Controlled))
            return;
        
        var charInv = Character.Controlled.Inventory;

        foreach (Item heldItem in Character.Controlled.HeldItems
                     .Where(i => i.OwnInventory is
                     {
                         Capacity: > 0,
                         Locked: false
                     }))
        {
            for (int slotIndex = 0; slotIndex < heldItem.OwnInventory.Capacity; slotIndex++)
            {
                ItemPrefab? prefItemPrefab = null;
                //item exists in inventory?
                if (heldItem.OwnInventory.GetItemAt(slotIndex) is Item { Condition: <= 0 } item)
                {
                    //yes--item condition 0?
                    //yes--remove item, mark replacement type preference
                    prefItemPrefab = item.Prefab;
                    if (!charInv.TryPutItem(item, Character.Controlled, new[] { InvSlotType.Any }))
                        continue;
                }

                //if empty find a suitable replacement
                if (heldItem.OwnInventory.GetItemAt(slotIndex) is null)
                {
                    if (Character.Controlled.Inventory.FindCompatWithPreference(
                            heldItem, prefItemPrefab, slotIndex, item1 =>
                                item1.Condition > 0 
                                && !item1.IsLimbSlotItem(Character.Controlled)
                                && ((item1.ParentInventory is { Owner: Item ownerItem} && !Character.Controlled.HeldItems.Contains(ownerItem)) 
                                    || item1.ParentInventory?.Owner is not Item)
                                && Util.CompatibilityRulesCheck(heldItem, item1)
                        ) is { } it )
                    {
                        if (!heldItem.OwnInventory.TryPutItem(it, slotIndex, true, false, Character.Controlled))
                            continue;
                    }
                    else
                    {
                        continue;
                    }
                }

                //is it not full stack?
                int diff;
                if (heldItem.OwnInventory.GetItemAt(slotIndex) is { } it1
                    && (diff = heldItem.GetSlotMaxStackSize(it1, slotIndex) -
                               heldItem.OwnInventory.GetItemsAt(slotIndex).Count()) > 0)
                {
                    List<Item> refillItems = Character.Controlled.Inventory.FindAllCompatWithPreference(heldItem,
                        it1.Prefab, slotIndex,
                        item1 => item1.Condition > 0
                                 && item1.Prefab.Identifier.Equals(it1.Prefab.Identifier)
                                 && !item1.IsLimbSlotItem(Character.Controlled)
                                 && ((item1.ParentInventory is { Owner: Item ownerItem} && !Character.Controlled.HeldItems.Contains(ownerItem)) 
                                     || item1.ParentInventory?.Owner is not Item)
                                 && Util.CompatibilityRulesCheck(heldItem, item1)
                                 );
                    foreach (Item refillItem in refillItems)
                    {
                        if (diff < 1)
                            break;
                        if (heldItem.OwnInventory.TryPutItem(refillItem, slotIndex, false, true,
                                Character.Controlled))
                            diff -= 1;
                    }
                }
            }

        }
    }
}