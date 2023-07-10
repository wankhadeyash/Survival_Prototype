using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;

[CreateAssetMenu(menuName = "ScriptableObject/AvatarCustomization/NewAvatar")]
public class AvatarData : ScriptableObject
{
    public GameObject networkPrefab;
    public GameObject avatarPrefab;
    public Avatar animatorAvatar;
    public string classType;

    [Header("Stats")]
    public List<StatsData> statsData;

    [Header("Default Items")]
    public List<InventoryItemData> m_DefaultItems;

}

