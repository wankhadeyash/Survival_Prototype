using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventoryData : SaveLoadDataBase
{
    public List<InventorySlot> m_InventorySlotData;
    public InventoryData(string folderName, string fileName)
    {
        base.m_DirPath = folderName;
        base.m_FileName = fileName;
        m_InventorySlotData = new List<InventorySlot>();
    }
}
// This class manages data on inventory slots, adding and removing of items from slots
// It also notifies the Inventory display when data is changed on inventory via UnityAction
[System.Serializable]
public class InventoryManager
{
    private int m_NumberOfSlots; // number of slots in the inventory
    [SerializeField] protected List<InventorySlot> m_InventorySlots; // list of inventory slots
    public List<InventorySlot> InventorySlots => m_InventorySlots; // property to get the inventory slots
    public UnityAction<InventorySlot> OnInventorySlotChanged; // event that is triggered when the inventory slot data changes
    
    protected InventoryData m_InventoryData;
    public InventoryManager(int numberOfSlots)
    {
        m_NumberOfSlots = numberOfSlots;
        m_InventorySlots = new List<InventorySlot>(numberOfSlots);
        m_InventoryData = new InventoryData("Player", "HUDInventory");
        // creating inventory slots
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
            //Check if slot has remaining stack 
            foreach (InventorySlot slot in slots)
            {
                if (slot.StackSize < slot.ItemData.maxQuantity)
                {
                    slot.AddToStack(1);
                    OnInventorySlotChanged?.Invoke(slot); // triggering the event OnInventorySlotChanged
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

            OnInventorySlotChanged?.Invoke(freeSlot); // triggering the event OnInventorySlotChanged
            Debug.Log($"Assigning new slot to {itemData.name}");
            return true;
        }
        Debug.Log($"All slots are full can add {itemData.name}");

        return false;
    }

    // Check if item is present in inventory and get the slots containing this item
    bool CheckForItemInInventory(InventoryItemData itemData, out List<InventorySlot> slotsContainingThisItem)
    {
        slotsContainingThisItem = m_InventorySlots.Where(s => s.ItemData == itemData).ToList();

        return slotsContainingThisItem == null ? false : true;
    }

    // Get the first available slot in the inventory
    bool GetAvailableSlot(out InventorySlot freeSlot)
    {
        freeSlot = m_InventorySlots.Find(s => s.ItemData == null);
        return freeSlot == null ? false : true;
    }

    public void SaveInventoryData() 
    {
        //Get data from InventoryItemData and set it to InventoryItemData in data class
        foreach (InventorySlot slot in m_InventorySlots) 
        {
            m_InventoryData.m_InventorySlotData.Add(slot);
        }

        Serializer.SaveJsonData(m_InventoryData);

    }

    public void LoadInventoryData()
    {
        // Load the user's saved avatar customization choices
        // AvatarData data = Serializer.LoadBinaryData(m_AvatarData);
        InventoryData data = Serializer.LoadJsonData(m_InventoryData);
        if (data != null)
        {
            if (data.m_InventorySlotData.Count != m_InventorySlots.Count) {
                Debug.LogError($"Data missmatch");
                return;
            }
            for (int i = 0; i < m_InventorySlots.Count; i++) 
            {
                if (data.m_InventorySlotData[i].ItemData != null){
                    m_InventorySlots[i].UpdateSlot(data.m_InventorySlotData[i].ItemData, data.m_InventorySlotData[i].StackSize);
                    OnInventorySlotChanged?.Invoke(m_InventorySlots[i]);
                }
            }
        }
    }
}
