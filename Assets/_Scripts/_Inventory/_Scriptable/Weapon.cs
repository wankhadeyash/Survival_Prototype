using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Meele, Ranged, Explosive
}
[CreateAssetMenu(fileName = "Weapon Item", menuName = "ScriptableObject/Inventory/Weapon")]
public class Weapon : InventoryItemData
{
    [Header("Weapon")]
    public WeaponType weaponType;
    [DrawIf("weaponType", WeaponType.Ranged)]
    public float range;
    public int damage;
    public float durability;
}
