using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlankBrains.Inventory
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] InventoryHolder m_InventoryHolder;
        
        public List<InventorySlot_UI> m_UISlots;

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

        public void SetInventoryHolder(InventoryHolder inventoryHolder) 
        {
            m_InventoryHolder = inventoryHolder;
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

        public virtual void Update()
        {
            
        }

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

        //Method to assign the slots for the inventory to be displayed
        public virtual void AssignSlots(InventoryManager invToDisplay) 
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

        // Abstract method to handle the slot button click event
        public abstract void OnSlotButtonClicked(InventorySlot_UI slot);
    }
}
