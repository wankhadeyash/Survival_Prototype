using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventoryManager
{
    private int m_NumberOfSlots;
    [SerializeField] protected List<InventorySlot> m_InventorySlots;
    public List<InventorySlot> InventorySlots => m_InventorySlots;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventoryManager(int numberOfSlots)
    {
        m_NumberOfSlots = numberOfSlots;
        m_InventorySlots = new List<InventorySlot>(numberOfSlots);
        for (int i = 0; i < m_NumberOfSlots; i++)
        {
            m_InventorySlots.Add(new InventorySlot());
        }
    }

    // Add an item to the inventory
    public bool AddItem(InventoryItemData itemData)
    {
        //Check if item is already present in Inventory
        if (CheckForItemInInventory(itemData, out List<InventorySlot> slots))
        {
            //Check if slot has remaning stack 
            foreach (InventorySlot slot in slots)
            {
                if (slot.m_StackSize < slot.m_ItemData.maxQuantity)
                {
                    slot.AddToStack(1);
                    OnInventorySlotChanged?.Invoke(slot);
                    Debug.Log($"Found slot which has {itemData.name} and adding it to stack");
                    return true;
                }
                Debug.Log($"Found slot which has {itemData.name} but already slot is full");

            }
        }

        //If item is not present in inventory add item to next free slot
        if (GetAvailableSlot(out InventorySlot freeSlot)) 
        {
            freeSlot.UpdateSlot(itemData, 1);
            OnInventorySlotChanged?.Invoke(freeSlot);
            Debug.Log($"Assigning new slot to {itemData.name}");
            return true;
        }
        Debug.Log($"All slots are full can add {itemData.name}");

        return false;
    }

    bool CheckForItemInInventory(InventoryItemData itemData, out List<InventorySlot> slotsContainingThisItem) 
    {
         slotsContainingThisItem = m_InventorySlots.Where(s => s.m_ItemData == itemData).ToList();

        return slotsContainingThisItem == null ? false : true;
    }

    bool GetAvailableSlot(out InventorySlot freeSlot) 
    {
        freeSlot = m_InventorySlots.Find(s => s.m_ItemData == null);
        return freeSlot == null ? false : true;
    }
}