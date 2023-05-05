using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    List<InventorySlot> m_Slots = new List<InventorySlot>();

    [Tooltip("Under which all the slots will be stored")]
    [SerializeField] GameObject m_SlotsParent;
    [SerializeField] GameObject m_SlotPrefab;

    private void Awake()
    {

    }

    //Get all the inventory slots in childern
    void InitializeSlots() 
    {
        
    }

    public void OnInventoryUpdated()
    {
        //for (int i = 0; i < Inventory.m_MaxSlots; i++) 
        //{
        //    //Check if item is stackable and 
        //    //If Slot is empty assign empty slot
        //    if (m_Slots[i].m_ItemData == null)
        //    {
        //        m_Slots[i].m_ItemData = Inventory.m_Items[i];
        //    }

           
        //}
    }
}
