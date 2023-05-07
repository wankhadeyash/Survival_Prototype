using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    [SerializeField]InventoryItemData m_ItemData; // The data of the item that is stored in this slot
    public InventoryItemData ItemData => m_ItemData;
    
    [SerializeField]int m_StackSize; // The amount of items stored in this slot
    public int StackSize => m_StackSize;                        
    // Constructor that initializes the slot with an item and a stack size
    public InventorySlot(InventoryItemData itemToAdd, int amount)
    {
        m_ItemData = itemToAdd;
        m_StackSize = amount;
    }

    // Default constructor that initializes the slot with no item and a stack size of -1
    public InventorySlot()
    {
        ClearSlot();
    }

    // Clears the slot by setting the item data to null and the stack size to -1
    public void ClearSlot()
    {
        m_ItemData = null;
        m_StackSize = -1;
    }

    // Updates the slot with new item data and stack size
    public void UpdateSlot(InventoryItemData item, int amount)
    {
        m_ItemData = item;
        m_StackSize = amount;
    }

    // Checks if there is enough room in the stack to add more items
    bool RoomLeftInStack(int amountToAdd)
    {
        if (m_StackSize + amountToAdd <= m_ItemData.maxQuantity)
            return true;
        return false;
    }

    // Adds the specified amount of items to the stack if there is enough room
    public void AddToStack(int amountToadd)
    {
        if (RoomLeftInStack(amountToadd))
        {
            m_StackSize += amountToadd;
        }
    }

    // Removes the specified amount of items from the stack
    public void RemoveFromStack(int amountToRemove)
    {
        m_StackSize -= amountToRemove;
    }
}

