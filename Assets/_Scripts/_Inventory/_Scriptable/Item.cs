using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Weapon,Armor,Shield,Food,Decoration,Resource
}
public enum ItemPickupType 
{
    Automatic, Manual
}
public class Item : ScriptableObject
{
    [Header("Item")]
    public ItemType itemType;
    public ItemPickupType itemPickupType;
    [DrawIf("itemPickupType", ItemPickupType.Manual)]
    public KeyCode pickUpButton = KeyCode.E;
    public string itemName;
    public Sprite icon;
    public string description;
    public int weight;
}