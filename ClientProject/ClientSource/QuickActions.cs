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

namespace HotkeyReload;

public static class QuickActions
{
    private static readonly InvSlotType[] InvPriorityOrder =
    {
        InvSlotType.LeftHand, InvSlotType.RightHand,
        InvSlotType.Bag, InvSlotType.InnerClothes,
        InvSlotType.OuterClothes, InvSlotType.Head,
        InvSlotType.Any, InvSlotType.None
    };
    
    /// <summary>
    /// Tries to put all items of an inventory a player is interacting with into the player's inventory.
    /// Prioritizes any storages the player is holding in their hands, then most of their limb slots, then their hot bar.
    /// </summary>
    public static void QuickLootAllToPlayerInventory()
    {
        Inventory source;
        CharacterInventory target;

        if (IsCharReadyToExchangeWithItem(Character.Controlled))
        {
            target = Character.Controlled.Inventory;
            source = Character.Controlled.SelectedItem.OwnInventory;
            
            if (Character.Controlled.SelectedItem == target.GetItemInLimbSlot(InvSlotType.LeftHand)
                || Character.Controlled.SelectedItem == target.GetItemInLimbSlot(InvSlotType.RightHand))
                return;
        }
        else if (IsCharReadyToExchangeWithChar(Character.Controlled))
        {
            target = Character.Controlled.Inventory;
            source = Character.Controlled.SelectedCharacter.Inventory;
        }
        else
        {
            return;
        }
        
        foreach (InvSlotType slotType in InvPriorityOrder)
        {
            if (slotType is InvSlotType.LeftHand or InvSlotType.RightHand or InvSlotType.Bag)
            {
                if (target.GetItemInLimbSlot(slotType) is { OwnInventory: { Capacity: > 0, Locked: false } } item)
                {
                    TransferItems(source, item.OwnInventory, Character.Controlled, null);
                    continue;
                }
            }
            
            if (slotType is InvSlotType.LeftHand or InvSlotType.RightHand)
                continue;
            
            TransferItems(source, target,  Character.Controlled, new List<InvSlotType>{ slotType });
        }
    }

    private static void TransferItems(Inventory source, Inventory target, Character character, List<InvSlotType>? allowedSlots = null)
    {
        foreach (Item item in source.AllItemsMod)
        {
            target.TryPutItem(item, character, allowedSlots);
        }
    }
    
    /// <summary>
    /// Tries to refill the stacks of all items of the storage inventory a player is interacting with using
    /// items in the player's inventory.
    /// </summary>
    public static void QuickStackToStorageInventory()
    {
        if (!IsCharReadyToExchangeWithItem(Character.Controlled))
            return;
        Character.Controlled.SelectedItem.RefillItemStacksUsingContainer(Character.Controlled.Inventory);
    }

    /// <summary>
    /// Tries to refill the stacks of all items in the player's inventory using the items in the storage
    /// inventory the player is interacting with.
    /// </summary>
    public static void QuickStackToPlayerInventory()
    {
        if (!IsCharReadyToExchangeWithItem(Character.Controlled))
            return;
        Character.Controlled.RefillItemStacksUsingContainer(Character.Controlled.SelectedItem.OwnInventory);
    }

    private static bool IsCharReadyToExchangeWithItem(Character character)
    {
        return Util.CheckIfValidToInteract()
               && Util.CheckIfCharacterReady(character)
               && (character.SelectedItem is 
                   { OwnInventory: { Capacity: > 0, Locked: false }});
    }
    
    private static bool IsCharReadyToExchangeWithChar(Character character)
    {
        return Util.CheckIfValidToInteract()
               && Util.CheckIfCharacterReady(character)
               && character.SelectedCharacter is 
                   { CanInventoryBeAccessed: true, Inventory: { Locked: false, Capacity: > 0 }};
    }
    
}