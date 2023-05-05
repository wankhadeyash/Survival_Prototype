using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public class CraftingMaterials : Item
{
    public MaterialType materialType;
    public Recipes recipes;
}
