using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryManager
{
    private int m_NumberOfSlots;
    [SerializeField] protected List<InventorySlot> m_InventorySlots;
    public List<InventorySlot> InventorySlots => m_InventorySlots;


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
        // Check if there is space in the inventory
        if (m_InventorySlots.Count >= m_NumberOfSlots)
        {
            Debug.Log("Inventory is full!");
            return false;
        }
        //Add Item to slot
        m_InventorySlots.Add(new InventorySlot(itemData,1));

        return true;
    }

    // Remove an item from the inventory
    public void RemoveItem(InventoryItemData itemData)
    {
        


    }
}