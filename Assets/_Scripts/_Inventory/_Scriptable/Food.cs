using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Meat, Fruit, Water
}
[CreateAssetMenu(fileName = "Food Item", menuName = "ScriptableObject/Inventory/Food")]
public class Food : Item
{
    [Header("Food")]
    public float healthRestore;
    public float staminaRestore;
}
