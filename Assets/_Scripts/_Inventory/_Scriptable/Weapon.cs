using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Meele, Ranged
}
[CreateAssetMenu(fileName = "Weapon Item", menuName = "ScriptableObject/Inventory/Weapon")]
public class Weapon : Item
{
    [Header("Weapon")]
    public WeaponType weaponType;
    public int damage;
}
