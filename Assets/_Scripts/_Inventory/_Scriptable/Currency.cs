using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType 
{
    None,
    Gold,
    Silver,
    Bronze
}
[CreateAssetMenu(fileName = "Currency Item", menuName = "ScriptableObject/Inventory/Currency")]
public class Currency : InventoryItemData
{
    public CurrencyType currencyType;
}
