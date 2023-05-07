using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] InventoryHolder m_InventoryHolder;

    public List<InventorySlot_UI> m_UISlots;

    protected override void Start()
    {
        base.Start();
        // Check if an InventoryHolder is attached to this GameObject
        if (m_InventoryHolder != null)
        {
            // Get the InventoryManager from the attached InventoryHolder
            m_InventoryManager = m_InventoryHolder.InventoryManager;
            // Subscribe to the OnInventorySlotChanged event of the InventoryManager
            m_InventoryManager.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            // Log an error if no InventoryHolder is attached and return from the function
            Debug.LogError($"No Inventory holder attached to {gameObject.name}");
            return;
        }

        // Assign the InventorySlots to the UISlots
        AssignSlots(m_InventoryManager);
    }

    // Assigns the InventorySlots to the UISlots and initializes each UISlot
    public override void AssignSlots(InventoryManager invToDisplay)
    {
        // Check if the count of UISlots matches the count of InventorySlots
        if (m_UISlots.Count != invToDisplay.InventorySlots.Count)
        {
            // Log an error if the counts do not match and return from the function
            Debug.LogError($"Please make sure UI slots count and inventory slot count matches");
            return;
        }
        // Loop through each UISlot and assign its corresponding InventorySlot and initialize it
        for (int i = 0; i < m_UISlots.Count; i++)
        {
            m_SlotDictionary.Add(m_UISlots[i], invToDisplay.InventorySlots[i]);
            m_UISlots[i].Init(invToDisplay.InventorySlots[i]);
        }
    }

    // Called when a UISlot button is clicked
    public override void OnSlotButtonClicked(InventorySlot_UI slot)
    {
        // Check if the UISlot has an item in it
        if (slot.AssignedInventorySlot.ItemData == null)
        {
            // Log a message if there is no item in the UISlot and return from the function
            Debug.Log($"No Item is slot");
            return;
        }
        // Update the MouseSlot to the clicked UISlot
        m_MouseSlot.UpdateUISlot(slot);
    }
}
