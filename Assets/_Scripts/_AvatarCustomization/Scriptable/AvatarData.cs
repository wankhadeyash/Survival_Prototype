using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;

[CreateAssetMenu(menuName = "ScriptableObject/AvatarCustomization/NewAvatar")]
public class AvatarData : ScriptableObject
{
    public GameObject prefab;
    public string classType;

    [Header("Stats")]
    public float health;
    public float armor;
    public float stamina;

    [Header("Default Items")]
    public List<InventoryItemData> m_DefaultItems;

}
