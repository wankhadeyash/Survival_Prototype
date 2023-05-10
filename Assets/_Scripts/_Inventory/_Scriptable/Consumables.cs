using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    [System.Flags]
    public enum StatsRestoreType
    {
        Health = 1 << 0,
        Stamina = 1 << 1,
        Hunger = 1 << 2,
        Thirst = 1 << 3
    }
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "ScriptableObject/Inventory/Consumable")]
    public class Consumables : InventoryItemData
    {
        [Header("Consumable")]
        public StatsRestoreType statsRestore;

        [DrawIf("statsRestore", StatsRestoreType.Health)]
        public float healthRestore;

        [DrawIf("statsRestore", StatsRestoreType.Stamina)]
        public float staminaRestore;

        [DrawIf("statsRestore", StatsRestoreType.Hunger)]
        public float hungerRestore;

        [DrawIf("statsRestore", StatsRestoreType.Thirst)]
        public float thirstRestore;

    }
}