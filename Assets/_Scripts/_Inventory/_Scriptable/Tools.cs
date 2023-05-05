using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum UsedFor 
{
    None,
    ChoppingWoods,
    Iron,
    Digging
}

[CreateAssetMenu(fileName = "Tool Item", menuName = "ScriptableObject/Inventory/Tools")]
public class Tools : InventoryItemData
{
    public UsedFor usedFor;
    public float durability;
}
