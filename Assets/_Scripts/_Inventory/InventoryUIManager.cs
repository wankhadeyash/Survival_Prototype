using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour, IInventoryObserver
{
    List<InventorySlot> m_Slots = new List<InventorySlot>();

    [Tooltip("Under which all the slots will be stored")]
    [SerializeField] GameObject m_SlotsParent;
    [SerializeField] GameObject m_SlotPrefab;

    private void Awake()
    {
        Inventory.RegisterAsObserver(this);
        Invoke(nameof(InitializeSlots),1);
    }

    //Get all the inventory slots in childern
    void InitializeSlots() 
    {
        for (int i = 0; i< Inventory.m_MaxSlots; i++) 
        {
            string slotName = "Slot - " + (i + 1);
            GameObject slot = Instantiate(m_SlotPrefab.gameObject, m_SlotsParent.transform);
            slot.name = slotName;
            m_Slots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public  void OnItemAdded() 
    {
        //Check if 
    }

    public void OnItemRemoved() { }

    public void OnInventoryUpdated()
    {
         
    }
}
