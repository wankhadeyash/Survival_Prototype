using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor Item", menuName = "ScriptableObject/Inventory/Amror")]
public class Armor : Item
{
    public float durability;
    [Range(0,1)]
    public float damageAbsorbPercent;
}
