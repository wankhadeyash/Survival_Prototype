using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    //Materials type
    public enum MaterialType
    {
        None,
        Wood,
        Feather,
    }
    //Which items can be crafted from this material
    [System.Flags]
    public enum Recipes
    {
        None,
        Workbench,
        Aex
    }
    [CreateAssetMenu(fileName = "CraftingMaterial Item", menuName = "ScriptableObject/Inventory/CraftingMaterials")]

    public class CraftingMaterials : InventoryItemData
    {
        public MaterialType materialType;
        public Recipes recipes;
    }
}