using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    [CreateAssetMenu(fileName = "Armor Item", menuName = "ScriptableObject/Inventory/Amror")]
    public class Armor : InventoryItemData
    {
        public float durability;
        [Range(0, 1)]
        public float damageAbsorbPercent;
    }
}