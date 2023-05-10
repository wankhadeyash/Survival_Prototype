using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        protected MouseSlot m_MouseSlot; // Reference to the mouse slot
        protected InventoryManager m_InventoryManager; // Reference to the inventory manager
        public InventoryManager InventoryManager => m_InventoryManager; // Property to get the inventory manager reference


        protected Dictionary<InventorySlot_UI, InventorySlot> m_SlotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>(); // Dictionary to map the UI slots to the actual inventory slots
        public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => m_SlotDictionary; // Property to get the dictionary reference

        protected virtual void Awake()
        {
            m_MouseSlot = FindObjectOfType<MouseSlot>(); // Find the mouse slot in the scene
        }

        protected virtual void Start()
        {

        }

        // Abstract method to assign the slots for the inventory to be displayed
        public abstract void AssignSlots(InventoryManager invToDisplay);

        // Method to update the UI slot when the inventory slot is updated
        protected virtual void UpdateSlot(InventorySlot udpatedSlot)
        {
            foreach (var slot in m_SlotDictionary) // Loop through the slot dictionary
            {
                if (slot.Value == udpatedSlot) // If the inventory slot matches the updated slot
                {
                    slot.Key.UpdateUISlot(udpatedSlot); // Update the UI slot
                    break; // Exit the loop
                }
            }
        }

        // Abstract method to handle the slot button click event
        public abstract void OnSlotButtonClicked(InventorySlot_UI slot);
    }
}