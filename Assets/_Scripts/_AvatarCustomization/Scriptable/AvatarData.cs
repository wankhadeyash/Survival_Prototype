using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;

[CreateAssetMenu(menuName = "ScriptableObject/AvatarCustomization/NewAvatar")]
public class AvatarData : ScriptableObject
{
    public GameObject networkPrefab;
    public GameObject avatarPrefab;
    public string classType;

    [Header("Stats")]
    public PlayerStatsSO stats;

    [Header("Default Items")]
    public List<InventoryItemData> m_DefaultItems;

}

