using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventoryManager m_InventoryManager;
    public InventoryManager InventoryManager => m_InventoryManager;


    public List<InventorySlot_UI> m_UISlots;


    protected Dictionary<InventorySlot_UI, InventorySlot> m_SlotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => m_SlotDictionary;
    protected virtual void Start()
    {
        
    }

    public abstract void AssignSlots(InventoryManager invToDisplay);

    protected virtual void UpdateSlot(InventorySlot udpatedSlot) 
    {
        foreach (var slot in m_SlotDictionary) 
        {
            if (slot.Value == udpatedSlot)
            {
                slot.Key.UpdateUISlot(udpatedSlot);
                break;
            }
        }
    }


}
