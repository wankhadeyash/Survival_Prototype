using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] InventoryHolder m_InventoryHolder;

    protected override void Start()
    {
        base.Start();
        if (m_InventoryHolder != null)
        {
            m_InventoryManager = m_InventoryHolder.InventoryManager;
            m_InventoryManager.OnInventorySlotChanged += UpdateSlot;
        }
        else 
        {
            Debug.LogError($"No Inventory holder attached to {gameObject.name}");
            return;
        }

        AssignSlots(m_InventoryManager);
    }
    public override void AssignSlots(InventoryManager invToDisplay)
    {
        if (m_UISlots.Count != invToDisplay.InventorySlots.Count)
        {
            Debug.LogError($"Please make sure UI slots count and inventory slot count matches");
            return;
        }
        for (int i = 0; i < m_UISlots.Count; i++) 
        {
            m_SlotDictionary.Add(m_UISlots[i], invToDisplay.InventorySlots[i]);
            UpdateSlot(invToDisplay.InventorySlots[i]);
        }
    }


}
