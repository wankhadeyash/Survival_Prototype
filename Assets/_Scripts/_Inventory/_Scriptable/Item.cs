using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enumeration of different item types.
public enum ItemType
{
    None,
    Weapon,
    Armor,
    Clothing,
    Currency,
    Treasure, //coins, gems, artifacts, rare items
    Notes, //journals, maps, quests
    Shield,
    Consumable, // health packs, stamina boosters, food, water, potions
    CraftingMaterial, //wood, metal, cloth, leather, herbs, minerals
    Tools //pickaxe, axe, shovel, fishing rod, lockpick
}

// An enumeration of different item pickup types.
public enum ItemPickupType
{
    None,
    Automatic, // Automatically picked up when player is nearby.
    Manual // Must be manually picked up by the player.
}

// An enumeration of different item use button types.
public enum ItemUseButtonType
{
    None,
    ButtonPress, // Use button must be pressed to use item.
    ButtonHold, // Use button must be held down to use item.
    ButtonRelease // Use button must be released to use item.
}

// An enumeration of different item pickup types.
public enum ItemStackableType 
{
    None,
    Stackable,
    NonStackable
}

// A ScriptableObject representing an item in the game.
public class Item : ScriptableObject
{
    [Header("Item")]
    public ItemType itemType; // The type of the item.
    public ItemPickupType itemPickupType; // How the item is picked up by the player.

    // A conditional field that is only visible in the inspector if itemPickupType is set to Manual.
    [DrawIf("itemPickupType", ItemPickupType.Manual)]
    public KeyCode pickUpButton; // The button to press to manually pick up the item.

    public ItemStackableType itemStackableType;
    [DrawIf("itemStackableType", ItemStackableType.Stackable)]
    public int maxQuantity;

    public KeyCode useButton; // The button to press to use the item.
    public ItemUseButtonType useButtonType; // The type of button press required to use the item.

    public string itemName; // The name of the item.
    public Sprite icon; // The icon to represent the item in the game.
    public string description; // A description of the item.
    public int weight; // The weight of the item in the player's inventory.
}
