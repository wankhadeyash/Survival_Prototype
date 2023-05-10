using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace BlankBrains.Inventory
{
    public class HudInventoryDisplay : InventoryDisplay
    {
        [SerializeField] Image m_BackgroundImg; // Field used when inventory is opened
        bool m_IsInventoryOpen;

        protected override void Start()
        {
            base.Start();
            
        }
        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Tab))
                ToggleInventoryDisplay();
        }

        void ToggleInventoryDisplay() 
        {
            m_IsInventoryOpen = m_IsInventoryOpen == false ? true : false;
            m_BackgroundImg.enabled = m_IsInventoryOpen;
        }

        // Called when a UISlot button is clicked
        public override void OnSlotButtonClicked(InventorySlot_UI slot)
        {
            //Check if inventory is opened
            if (!m_IsInventoryOpen)
                return;
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
}
