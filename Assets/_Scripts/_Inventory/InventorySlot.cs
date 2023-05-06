using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    public InventoryItemData m_ItemData;
    public int m_StackSize;


    public InventorySlot(InventoryItemData itemToAdd, int amount) 
    {
        m_ItemData = itemToAdd;
        m_StackSize = amount;
    }

    public InventorySlot() 
    {
        ClearSlot();
    }
    public void ClearSlot() 
    {
        m_ItemData = null;
        m_StackSize = -1;
    }

    public void UpdateSlot(InventoryItemData item, int amount) 
    {
        m_ItemData = item;
        m_StackSize = amount;
    }

    bool RoomLeftInStack(int amountToAdd) 
    {
        if (m_StackSize + amountToAdd <= m_ItemData.maxQuantity)
            return true;
        return false;
    }

    public void AddToStack(int amountToadd) 
    {
        if(RoomLeftInStack(amountToadd))
        {
            m_StackSize += amountToadd;  

        }
    }

    public void RemoveFromStack(int amountToRemove) 
    {
        m_StackSize -= amountToRemove;
    }

}
